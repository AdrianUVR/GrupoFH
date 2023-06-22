using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ErrorController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public ErrorController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult GetAllError()
        {
            ML.Result result = new ML.Result();
            ML.Error error = new ML.Error();

            result.Objects = new List<object>();

            try
            {
                using (var Client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    Client.BaseAddress = new Uri(urlApi);

                    var responseTask = Client.GetAsync("Error/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Error resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Error>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    error.Errores = result.Objects;
                }

            }
            catch (Exception ex)
            {

            }
            return View(error);
        }


        [HttpGet]
        public ActionResult Form(int? IdError)
        {
            ML.Error error = new ML.Error();



            if (IdError != null)
            {
                error.IdError = IdError.Value;
                ML.Result result = new ML.Result();

                try
                {
                    using (var Client = new HttpClient())
                    {
                        string urlApi = _configuration["urlApi"];
                        Client.BaseAddress = new Uri(urlApi);

                        var responseTask = Client.GetAsync("Error/GetByIdError/" + IdError);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            string errorr = readTask.Result.Object.ToString();

                            ML.Error resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Error>(errorr);
                            result.Object = resultItem;
                            result.Correct = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
                if (result.Correct)
                {
                    error = (ML.Error)result.Object;
                    return View(error);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del Paciente" + result.ErrorMessage;
                    return View("Modal");
                }
            }
            else
            {
                return View(error);
            }
        }

        [HttpPost]

        public ActionResult Form(ML.Error error)
        {


            using (var client = new HttpClient())
            {
                if (error.IdError == 0)
                {

                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Error>("Error/Add", error);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se registro el IdError";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado el Paciente";
                        return PartialView("Modal");
                    }

                }
                else
                {
                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Error>("Departamento/Update/", error);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha actualizado el Paciente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado el Paciente";
                        return PartialView("Modal");
                    }

                }

            }
        }
        [HttpGet]
        public ActionResult Delete(int IdError)
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["UrlApi"]);

                var postTask = client.GetAsync("Error/Delete/" + IdError);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se ha eliminado el libro";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se ha eliminado el libro";
                    return PartialView("Modal");
                }
            }
        }
    }
}
