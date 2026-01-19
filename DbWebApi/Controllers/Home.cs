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
using System.Web.Mvc;

namespace DbWebApi.Controllers
{
    public class HomeController : ApiController
    {
        // Обрабатывает запрос на "/"
        public IHttpActionResult Index()
        {
            string html = @"
                <!DOCTYPE html>
                <html lang='ru'>
                <head>
                    <meta charset='utf-8' />
                    <title>Главная страница</title>
                    <style>
                        body { font-family: Arial, sans-serif; background: #f4f4f4; padding: 20px; }
                        h1 { color: #333; }
                    </style>
                </head>
                <body>
                    <h1>Добро пожаловать!</h1>
                    <p>Эта страница сгенерирована прямо в контроллере — без Razor и .cshtml файлов.</p>
                    <a href='/about'>Перейти на страницу «О нас»</a>
                </body>
                </html>";

            return Content(HttpStatusCode.OK, html);//, "text/html; charset=utf-8");            
        }


    }

}



