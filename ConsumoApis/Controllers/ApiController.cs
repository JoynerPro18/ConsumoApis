using ConsumoApis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConsumoApis.Controllers
{
    public class ApiController : Controller
    {
        public async Task<ActionResult> PrimerMetodo()
        {
            var client = new HttpClient();


            List<API1> result = null;

            try
            {

                // Consumir la primera API
                var json1 = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts/1/comments");
                result = JsonConvert.DeserializeObject<List<API1>>(json1);

                ViewBag.Message = "ESTE ES EL PRIMER MÉTODO";

            }
            catch (Exception ex)
            {
                ViewBag.Error1 = "Error al consumir la primera API: " + ex.Message;
            }


            // Puedes elegir qué resultado mostrar en la vista o combinarlos de alguna manera.
            return View(result);


        }

        public async Task<ActionResult> SegundoMetodo()
        {
            IEnumerable<API1> apivalor = Enumerable.Empty<API1>();

            using (var client = new HttpClient())
            {
                try
                {

                    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts/1/comments");



                    var response = await client.GetAsync("Comments");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        apivalor = JsonConvert.DeserializeObject<IEnumerable<API1>>(json);
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

            return View(apivalor);
        }



    }

}