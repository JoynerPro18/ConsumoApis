using ConsumoApis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsumoApis.Controllers
{
    public class SegundoMetodoController : Controller
    {
        // GET: SegundoMetodo
        public async Task<ActionResult> IndexPartial(string number_api)
        {

            IEnumerable<API2> apivalor = Enumerable.Empty<API2>();

            using (var client = new HttpClient())
            {
                try
                {
                    if (number_api == "API2")
                    {
                        client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts");

                    }
                    else if (number_api == "API3")
                    {
                        client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/comments?postId=1");

                    }

                    var response = await client.GetAsync("Comments");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        apivalor = JsonConvert.DeserializeObject<IEnumerable<API2>>(json);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error en el consumo de la API: " + response.ReasonPhrase);
                    }
                    ViewBag.Message = "ESTE ES EL SEGUNDO MÉTODO";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Excepción al consumir la API: " + ex.Message);
                }
            }

            //return View("~/Views/Api2/IndexPartial.cshtml");
            return View(apivalor);
        }



    }
}


