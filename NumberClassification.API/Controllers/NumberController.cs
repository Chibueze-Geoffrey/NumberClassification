using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumberClassification.Application.Interface;
using NumberClassification.Application.UseCase;

namespace NumberClassification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        private readonly IClassifyNumber _classifyNumberUseCase;

        public NumberController(IClassifyNumber classifyNumberUseCase)
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
