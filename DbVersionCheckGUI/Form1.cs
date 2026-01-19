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
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DbVersionCheckGUI
{
    public partial class Form1 : Form
    {

        private readonly HttpClient _httpClient;
        private readonly string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];        

        private readonly string _sessionId;

        public Form1()
        {
            InitializeComponent();

            _sessionId = Guid.NewGuid().ToString();
            
            _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };            
            
            _httpClient.DefaultRequestHeaders.Add("X-Session-ID", _sessionId);
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            btn.Enabled = false;
            await CallApi("/api/database/connect");
            btn.Enabled = true;
        }
        private async void btnGetVersion_Click(object sender, EventArgs e)
        {
            await CallApi("/api/database/version");
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            await CallApi("/api/database/disconnect");
        }       
        

        private async void btnSessions_Click(object sender, EventArgs e)
        {
            await CallApi("/api/database/current_sessions");
        }

        private async void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            await CallApi("/api/database/disconnect");
        }

        private async void Form1_FormClosed(object sender, FormClosingEventArgs e)
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
                    var parsed = JToken.Parse(rawJson);
                    txtResult.Text = parsed.ToString(Formatting.Indented).Replace("\\n\\t", " ");

                    var versionObj = JsonConvert.DeserializeObject<dynamic>(rawJson);

                    string versionText = versionObj.Version?.ToString() ?? "Неизвестно";
                    versionText = "Версия SQL Server: \n\n" + versionText;
                    MessageBox.Show(versionText);
                }
                else
                {
                    var parsed = JToken.Parse(rawJson);
                    
                    txtResult.Text = parsed.ToString(Formatting.Indented).Replace("\\n\\t", " ");
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

        private async Task CheckedCalculate()
        {
            if (rbConnect.Checked)
                await CallApi("/api/database/connect");
            else
                await CallApi("/api/database/disconnect");
        }

        private async void rbConnect_CheckedChanged(object sender, EventArgs e)
        {
            await CheckedCalculate();
        }

        
    }
}
