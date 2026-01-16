using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Services.Description;

namespace WebApp4.Controllers
{
    public class DbWebConnectionController : ApiController
    {
        private static SqlConnection _connection;
        private static string _сonnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static int _connectionTries = 0;
        private static int _currentConnections = 0;
        private static List<object> _activeSessions = new List<object>();

        [HttpGet]
        [Route("api/database/connect")]
        public IHttpActionResult Connect()
        {
            try
            {
                string message = "Подключение...";
                if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
                {
                    //_сonnectionString = 
                    _connection = new SqlConnection(_сonnectionString);
                    _connection.Open();
                    _connectionTries++;
                    //_activeSessions.Add(new 
                    //{ 
                    //    Session = _activeSessions.Count, 
                    //    SessionId = _connection.ClientConnectionId 
                    //});
                    
                    message = "Подключение успешно.";
                }
                else
                {
                    message = "Подключение уже существует.";
                }
                return Ok(new 
                { 
                    Success = true, 
                    Message = message, 
                    //ActiveSessions = _activeSessions.Count 
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Ошибка подключения: " + ex.Message));
            }
        }

        [HttpGet]
        [Route("api/database/version")]
        public IHttpActionResult GetVersion()
        {
            try
            {
                if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
                {
                    return BadRequest("Подключение к БД не установлено. Сначала вызовите /api/database/connect");
                }

                using (var cmd = new SqlCommand("SELECT @@VERSION", _connection))
                {
                    var version = cmd.ExecuteScalar()?.ToString() ?? "Неизвестно";
                    return Ok(new { Version = version });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Ошибка получения версии: " + ex.Message));
            }
        }

        [HttpGet]
        [Route("api/database/disconnect")]
        public IHttpActionResult Disconnect()
        {
            try
            {
                string message = "Отключение...";

                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    _connection.Dispose();                    
                    message = "Отключение выполнено.";
                    //_activeSessions.Remove();
                }
                else
                {
                    message = "Уже отключено.";
                }

                _connection = null;

                return Ok(new 
                { 
                    Success = true, 
                    Message = message, 
                    //ActiveSessions = _activeSessions.Count 
                });

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Ошибка отключения: " + ex.Message));
            }
        }


        [HttpGet]
        [Route("api/database/current_sessions")]        
        public async Task<IHttpActionResult> CurrentSessions()
        {
            try
            {
                string message = "Подключение...";
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                var sessions = new List<Object>();
                const string dbName = "master"; // "TestDB";



                using (var conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    // Запрос: все пользовательские сессии к целевой БД                    

                    const string sql = @"
                        SELECT 
                            session_id,
                            login_name,
                            host_name,
                            program_name,
                            status,
                            last_request_start_time,
                            last_request_end_time,
                            DATEDIFF(MINUTE, last_request_end_time, GETDATE()) AS idle_minutes
                        FROM sys.dm_exec_sessions
                        WHERE 
                            is_user_process = 1
                            AND database_id = DB_ID(@DatabaseName)
                            --AND status = 'sleeping'
                            --AND last_request_end_time < DATEADD(MINUTE, -10, GETDATE()) -- простой > 10 мин
                        ORDER BY last_request_end_time ASC";


                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.Parameters.AddWithValue("@DatabaseName", dbName);

                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                object[] data = new object[reader.FieldCount];
                                var obj = reader.GetValues(data);                                
                                sessions.Add(new
                                {
                                    SessionId = reader["session_id"],
                                    LoginName = reader["login_name"]?.ToString(),
                                    HostName = reader["host_name"]?.ToString(),
                                    ProgramName = reader["program_name"]?.ToString(),
                                    Status = reader["status"]?.ToString(),
                                    LastRequestEndTime = (reader["last_request_end_time"] as DateTime?)?.ToLocalTime(),
                                    IdleMinutes = reader["idle_minutes"] as int? ?? 0
                                });
                            }
                        }

                    }
                }

                _currentConnections = sessions.Count;
                return Ok(new 
                { 
                    Success = true, 
                    Message = sessions, 
                    CurrentConnections = _currentConnections 
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Ошибка подключения: " + ex.Message));
            }
        
        } 
    }
    
}

    

