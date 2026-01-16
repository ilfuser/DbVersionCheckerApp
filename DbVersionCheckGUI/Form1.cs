using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbVersionCheckGUI
{
    public partial class Form1 : Form
    {

        private readonly HttpClient _httpClient;
        private readonly string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        //"http://localhost:63855";        

        public Form1()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            await CallApi("/api/database/connect");
        }
        private async void btnGetVersion_Click(object sender, EventArgs e)
        {
            await CallApi("/api/database/version");
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            await CallApi("/api/database/disconnect");
        }


        private async Task CallApi(string endpoint)
        {
            string rawJson = "";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
                rawJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {                    
                    var parsed = JToken.Parse(rawJson);
                    txtResult.Text = parsed.ToString(Formatting.Indented);                    
                    return;
                }

                
                if (endpoint.Contains("version"))
                {
                    rawJson = rawJson.Replace("\\\\","\\");
                    
                    var versionObj = JsonConvert.DeserializeObject<dynamic>(rawJson);
                    
                    string versionText = versionObj.Version?.ToString() ?? "Неизвестно";
                    txtResult.Text = "Версия SQL Server: \n\n" + versionText;
                }
                else
                {                
                    var parsed = JToken.Parse(rawJson);
                    txtResult.Text = parsed.ToString(Formatting.Indented);
                }
            }
            catch (JsonReaderException)
            {                
                txtResult.Text = rawJson;
            }
            catch (Exception ex)
            {
                txtResult.Text = "Ошибка:\n" + ex.Message;
            }
        }
        

        private async void btnSessions_Click(object sender, EventArgs e)
        {
            await CallApi("/api/database/current_sessions");
        }
    }
}
