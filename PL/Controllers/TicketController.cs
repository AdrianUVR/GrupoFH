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

    }
}
