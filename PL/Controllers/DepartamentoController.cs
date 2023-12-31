﻿using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DepartamentoController : Controller
    {

        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public DepartamentoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult GetAllDepartamento()
        {
            ML.Result result = new ML.Result();
            ML.Departamento departamento = new ML.Departamento();

            result.Objects = new List<object>();

            try
            {
                using (var Client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    Client.BaseAddress = new Uri(urlApi);

                    var responseTask = Client.GetAsync("Departamento/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Departamento resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Departamento>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    departamento.Departamentos = result.Objects;
                }

            }
            catch (Exception ex)
            {

            }
            return View(departamento);
        }


        [HttpGet]
        public ActionResult Form(int? IdDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();

            ML.Result resultArea = BL.Area.GetAllArea();
            departamento.Area = new ML.Area();


            if (resultArea.Correct)
            {
                departamento.Area.Areas = resultArea.Objects;
            }

            if (IdDepartamento != null)
            {
                departamento.IdDepartamento = IdDepartamento.Value;
                ML.Result result = new ML.Result();

                try
                {
                    using (var Client = new HttpClient())
                    {
                        string urlApi = _configuration["urlApi"];
                        Client.BaseAddress = new Uri(urlApi);

                        var responseTask = Client.GetAsync("Departamento/GetAllByIdDepartamento/" + IdDepartamento);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            string departamentoo = readTask.Result.Object.ToString();

                            ML.Departamento resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Departamento>(departamentoo);
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
                    departamento = (ML.Departamento)result.Object;
                    departamento.Area = new ML.Area();


                    if (resultArea.Correct)
                    {
                        departamento.Area.Areas = resultArea.Objects;
                    }
                    return View(departamento);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del Departamento" + result.ErrorMessage;
                    return View("Modal");
                }
            }
            else
            {
                return View(departamento);
            }
        }

        [HttpPost]

        public ActionResult Form(ML.Departamento departamento)
        {


            using (var client = new HttpClient())
            {
                if (departamento.IdDepartamento == 0)
                {

                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Departamento>("Departamento/Add", departamento);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se registro el Departamento";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado el Departamento";
                        return PartialView("Modal");
                    }

                }
                else
                {
                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Departamento>("Departamento/Update/", departamento);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha actualizado ";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado el Departamento";
                        return PartialView("Modal");
                    }

                }

            }
        }
        [HttpGet]
        public ActionResult Delete(int IdDepartamento)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.IdEmpleado = IdDepartamento;
            ML.Result resultDelete = BL.Departamento.Delete(IdDepartamento);
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
