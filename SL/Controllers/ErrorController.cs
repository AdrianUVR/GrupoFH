using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class ErrorController : Controller
    {

        [HttpGet]
        [Route("api/Error/GetAll")]
        public ActionResult GetAll()
        {
            ML.Error error = new ML.Error();
            ML.Result result = BL.Error.GetAll();

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
        [Route("api/Error/GetByIdError/{IdError}")]
        public ActionResult GetById(int IdError)
        {

            ML.Result result = BL.Error.GetByIdError(IdError);

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
        [Route("api/Error/Add")]
        public ActionResult Add([FromBody] ML.Error error)
        {
            ML.Result result = BL.Error.Add(error);

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
        [Route("api/error/Update")]
        public ActionResult Update([FromBody] ML.Error error)
        {
            ML.Result result = BL.Error.Update(error);

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
        [Route("api/error/Delete/{IdError}")]
        public ActionResult Delete(int IdError)
        {
            ML.Result result = BL.Error.Delete(IdError);
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
