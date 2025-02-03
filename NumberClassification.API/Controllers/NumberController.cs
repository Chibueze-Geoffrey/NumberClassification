using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumberClassification.Application.Interface;
using NumberClassification.Application.UseCase;
using System.Threading.Tasks;

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
        public async Task<IActionResult> ClassifyNumber([FromQuery] string number)
        {
            if (!int.TryParse(number, out int parsedNumber))
            {
                return BadRequest(new { number, error = true });
            }

            var result = await _classifyNumberUseCase.ExecuteAsync(parsedNumber);
            return Ok(result);
        }
    }
}
