using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
      

            //Inyeccion de dependencias-- patron de diseño
            private readonly IConfiguration _configuration;
            private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

            public EmpleadoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
            {
                _configuration = configuration;
                _hostingEnvironment = hostingEnvironment;
            }
            [HttpGet]
            public ActionResult GetAll()
            {
                ML.Result result = new ML.Result();
                ML.Empleado empleado = new ML.Empleado();

                result.Objects = new List<object>();

                try
                {
                    using (var Client = new HttpClient())
                    {
                        string urlApi = _configuration["urlApi"];
                        Client.BaseAddress = new Uri(urlApi);

                        var responseTask = Client.GetAsync("Empleado/GetAll");
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            foreach (var resultItem in readTask.Result.Objects)
                            {
                                ML.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(resultItem.ToString());
                                result.Objects.Add(resultItemList);
                            }
                        }
                        empleado.Empleados = result.Objects;
                    }

                }
                catch (Exception ex)
                {

                }
                return View(empleado);
            }


        [HttpGet]
        public ActionResult Form(int? IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
      


            if (IdEmpleado != null)
            {
                empleado.IdEmpleado = IdEmpleado.Value;
                ML.Result result = new ML.Result();

                try
                {
                    using (var Client = new HttpClient())
                    {
                        string urlApi = _configuration["urlApi"];
                        Client.BaseAddress = new Uri(urlApi);

                        var responseTask = Client.GetAsync("Empleado/GetAllByIdEmpleado/" + IdEmpleado);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            string empleadoo = readTask.Result.Object.ToString();

                            ML.Empleado resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(empleadoo);
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
                    empleado = (ML.Empleado)result.Object;
                    return View(empleado);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del Empleado";
                    return View("Modal");
                }
            }
            else
            {
                return View(empleado);
            }
        }

        [HttpPost]

        public ActionResult Form(ML.Empleado empleado)
        {


            using (var client = new HttpClient())
            {
                if (empleado.IdEmpleado == 0)
                {

                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Empleado>("Empleado/Add", empleado);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se registro el empleado";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado el empleado";
                        return PartialView("Modal");
                    }

                }
                else
                {
                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Empleado>("Empleado/Update/", empleado);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha actualizado el empleado";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado el empleado";
                        return PartialView("Modal");
                    }

                }

            }
        }

        [HttpGet]
        public ActionResult Delete(int IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.IdEmpleado = IdEmpleado;
            ML.Result resultDelete = BL.Empleado.Delete(IdEmpleado);
            if (resultDelete.Correct == true)
            {
                ViewBag.Message = "Se elimino el registro";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "ocurrio un error al eliminar";
                return PartialView("Modal");
            }
        }


    }

}
