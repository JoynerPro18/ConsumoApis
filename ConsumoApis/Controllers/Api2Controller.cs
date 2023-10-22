using ConsumoApis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConsumoApis.Controllers
{
    public class Api2Controller : Controller
    {
        public async Task<ActionResult> IndexPartial(string number_api)
        {
            var client = new HttpClient();


            List<API2> result = null;

            try
            {
                switch(number_api)
            {
                    case "API2":
                        // Consumir la segunda API
                        var json1 = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
                        result = JsonConvert.DeserializeObject<List<API2>>(json1);
                        break;
                    case "API3":
                        //Consumir la tercera API
                         var json2 = await client.GetStringAsync("https://jsonplaceholder.typicode.com/comments?postId=1");
                        result = JsonConvert.DeserializeObject<List<API2>>(json2);
                        break;
                    default:
                        break;
                        //throw new ArgumentOutOfRangeException(nameof(parameter), parameter, null);
                    }


                }
            catch (Exception ex)
            {
                ViewBag.Error1 = "Error al consumir la primera API: " + ex.Message;
            }


            
            return View(result);


        }

    }

}