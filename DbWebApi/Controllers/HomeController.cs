using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApp4.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            //return Content("hello!");
            string html = @"
                <!DOCTYPE html>
                <html lang='ru'>
                <head>
                    <meta charset='utf-8' />
                    <title>О нас</title>
                    <style>
                        body { font-family: Arial, serif; background: #eef; padding: 20px; }
                        h1 { color: #0066cc; }
                    </style>
                </head>
                <body>
                    <h1>Сервис Получения Версии MS SQL!</h1>
                    <p>Подключение к WebAPI осуществляется через HTTP, используя WinForms UI.</p>
                    <p>UI позволяет: подключиться, получить версию, отключиться.</p> 
                    <p>Также можно проверить количество подключенных экземпляров UI и общее количество подключений к БД.</p>                    
                </body>
                </html>";

            return Content(html);
        }

        
    }
}