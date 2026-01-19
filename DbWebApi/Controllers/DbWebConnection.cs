using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace DbWebApi.Controllers
{
    public class DbWebConnectionController : ApiController
    {        
        private static string _сonnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;        
        private static int _currentConnNum = 0;
        private static ConcurrentDictionary<string, SqlConnection> _sessions = new ConcurrentDictionary<string, SqlConnection>();


        [HttpGet]
        [Route("api/database/connect")]
        public IHttpActionResult Connect()
        {
            string session = Request.Headers.GetValues("X-Session-ID").FirstOrDefault();
            string message = "Подключение...";
            SqlConnection conn = null;

            try
            {
                if (!_sessions.ContainsKey(session))
                {
                    conn = new SqlConnection(_сonnectionString);
                    
                    if (_sessions.TryAdd(session, conn))
                    {
                        conn.Open();
                        message = "Подключение успешно.";
                    }
                    else
                    {
                        throw new Exception("Не удалось добавить подключение в список подключений");
                    }                    
                }
                else 
                {
                    conn = _sessions[session];

                    if (conn == null
                        || conn.State == System.Data.ConnectionState.Closed
                        || conn.State == System.Data.ConnectionState.Broken)
                    {
                        conn = new SqlConnection(_сonnectionString);
                        conn.Open();
                        message = "Подключение успешно.";
                    }
                    else
                    {
                        message = "Подключение уже существует.";
                    }
                }                

                return Ok(new
                {
                    Route = "api/database/connect",
                    Success = true,
                    Message = message,
                    ActiveSessions = _sessions.Count 
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
            string session = Request.Headers.GetValues("X-Session-ID").FirstOrDefault();

            try
            {
                if (!_sessions.TryGetValue(session, out SqlConnection conn)
                    || (conn == null || conn.State != System.Data.ConnectionState.Open))
                {
                    //return BadRequest("Подключение к БД не установлено. Сначала вызовите /api/database/connect");                    
                    var errorDetails = new
                    {
                        Route = "api/database/version",
                        Success = false,
                        Message = "Подключение к БД не установлено. Сначала вызовите /api/database/connect",
                        ActiveSessions = _sessions.Count
                    };
                    return Content(HttpStatusCode.BadRequest, errorDetails);
                }

                using (var cmd = new SqlCommand("SELECT @@VERSION", conn))
                {
                    var version = cmd.ExecuteScalar()?.ToString() ?? "Неизвестно";
                    
                    return Ok(new 
                    {
                        Route = "api/database/version",
                        Success = true,
                        Version = version 
                    });
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
            string session = Request.Headers.GetValues("X-Session-ID").FirstOrDefault();
            string message = "Отключение...";

            if (!_sessions.TryGetValue(session, out SqlConnection conn))
            {
                var errorDetails = new
                {
                    Route = "api/database/disconnect",
                    Success = false,
                    Message = "Подключение к БД не установлено. Сначала вызовите /api/database/connect",
                    ActiveSessions = _sessions.Count
                };
                return Content(HttpStatusCode.BadRequest, errorDetails);
            }

            try
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    //System.Data.ConnectionState.
                    conn.Close();
                    conn.Dispose();                    
                    message = "Отключение выполнено.";
                    
                    _sessions.TryRemove(session, out conn);
                    conn = null;                    
                }
                else
                {
                    message = "Уже отключено.";
                }

                

                return Ok(new 
                {
                    Route = "api/database/disconnect",
                    Success = true, 
                    Message = message, 
                    ActiveSessions = _sessions.Count 
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

                _currentConnNum = sessions.Count;
                return Ok(new 
                {
                    Route = "api/database/current_sessions",
                    Success = true,
                    MyActiveSessions = _sessions.Count,
                    MyActiveSessionsList = _sessions.Select(x => new 
                    { 
                        WebSessionId = x.Key, 
                        Sql_ClienConnectionId = x.Value.ClientConnectionId
                    }).ToArray(),
                    AllOpenedSQLConnections = _currentConnNum,                    
                    SQLConnectionsDetails = sessions, 
                    
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Ошибка подключения: " + ex.Message));
            }
        
        }

        [HttpGet]
        [Route("index.html")]
        public IHttpActionResult Index()
        {
            string html = @"
                <!DOCTYPE html>
                <html lang='ru'>
                <head>
                    <meta charset='utf-8' />
                    <title>О нас</title>
                    <style>
                        body { font-family: Georgia, serif; background: #eef; padding: 20px; }
                        h1 { color: #0066cc; }
                    </style>
                </head>
                <body>
                    <h1>О нас</h1>
                    <p>Мы — команда разработчиков, которая любит чистый код.</p>
                    <a href='/'>← Вернуться на главную</a>
                </body>
                </html>";

            return Content(HttpStatusCode.OK, html);
        }
    }
    
}

    

