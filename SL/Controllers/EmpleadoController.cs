using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class EmpleadoController : Controller
    {

        [HttpGet]
        [Route("api/Empleado/GetAll")]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        [Route("api/Empleado/GetAllByIdEmpleado/{IdEmpleado}")]
        public ActionResult GetById(int IdEmpleado)
        {

            ML.Result result = BL.Empleado.GetAllByIdEmpleado(IdEmpleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Empleado/Add")]
        public ActionResult Add([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Add(empleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/empleado/Update")]
        public ActionResult Update([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Update(empleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpDelete]
        [Route("api/empleado/Delete/{IdEmpleado}")]
        public ActionResult Delete(int IdEmpleado)
        {
            ML.Result result = BL.Empleado.Delete(IdEmpleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
