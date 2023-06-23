using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class TicketController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public TicketController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult GetAllTicket()
        {
            ML.Result result = new ML.Result();
            ML.Ticket ticket = new ML.Ticket();

            result.Objects = new List<object>();

            try
            {
                using (var Client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    Client.BaseAddress = new Uri(urlApi);

                    var responseTask = Client.GetAsync("Ticket/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Ticket resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Ticket>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    ticket.Tickets = result.Objects;
                }

            }
            catch (Exception ex)
            {

            }
            return View(ticket);
        }

        [HttpGet]
        public ActionResult FormTicket(int? IdTicket)
        {
            ML.Ticket ticket = new ML.Ticket();
            ML.Result resultArea = BL.Area.GetAllArea();
            ticket.Area =new ML.Area();
            
            
            if (resultArea.Correct)
            {
                ticket.Area.Areas = resultArea.Objects;
            }


            ML.Result resultError =BL.Error.GetAll();

            ticket.Error = new ML.Error();
            if (resultError.Correct)
            {
                ticket.Error.Errores = resultError.Objects;
            }

            ML.Result resultEmpleado = BL.Empleado.GetAll();
            ticket.Empleado= new ML.Empleado();

            if (resultEmpleado.Correct)
            {
                ticket.Empleado.Empleados= resultEmpleado.Objects;
            }



            if (IdTicket != null)
            {
                ticket.IdTicket = IdTicket.Value;
                ML.Result result = new ML.Result();

                try
                {
                    using (var Client = new HttpClient())
                    {
                        string urlApi = _configuration["urlApi"];
                        Client.BaseAddress = new Uri(urlApi);

                        var responseTask = Client.GetAsync("Ticket/GetAllByIdTicket/" + IdTicket);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            string tickett = readTask.Result.Object.ToString();

                            ML.Ticket resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Ticket>(tickett);
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
                    ticket = (ML.Ticket)result.Object;



                    return View(ticket);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del ticket" + result.ErrorMessage;
                    return View("Modal");
                }
            }
            else
            {
                return View(ticket);
            }
        }

        [HttpPost]

        public ActionResult FormTicket(ML.Ticket ticket)
        {
            //ticket.AsignadoA = ticket.Empleado.IdEmpleado;

            using (var client = new HttpClient())
            {
                if (ticket.IdTicket == 0)
                {

                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Ticket>("Ticket/Add", ticket);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se registro el ticket";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado el ticket";
                        return PartialView("Modal");
                    }

                }
                else
                {
                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Ticket>("Ticket/Update/", ticket);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha actualizado el ticket";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado el ticket";
                        return PartialView("Modal");
                    }

                }

            }
        }

        [HttpGet]
        public ActionResult DeleteTicket(int IdTicket)
        {
            ML.Ticket ticket = new ML.Ticket();
            ticket.IdTicket = IdTicket;



            ML.Result resultDelete = BL.Empleado.Delete(IdTicket);
            if (resultDelete.Correct == true)
            {
                ViewBag.Message = "Se borro el registro";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se pudo borrar";
                return PartialView("Modal");
            }
        }


    }
}
