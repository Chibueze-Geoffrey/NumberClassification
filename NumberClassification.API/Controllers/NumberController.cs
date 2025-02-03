using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumberClassification.Application.UseCases;

namespace NumberClassification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        private readonly ClassifyNumber _classifyNumberUseCase;

        public NumberController(ClassifyNumber classifyNumberUseCase)
        {
            _classifyNumberUseCase = classifyNumberUseCase;
        }

        [HttpGet("classify-number")]
        public IActionResult ClassifyNumber([FromQuery] string number)
        {
            if (!int.TryParse(number, out int parsedNumber))
            {
                return BadRequest(new { number, error = true });
            }

            var result = _classifyNumberUseCase.Execute(parsedNumber);
            return Ok(result);
        }
    }
}
