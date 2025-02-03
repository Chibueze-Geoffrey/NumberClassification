﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumberClassification.Application.Interface;
using System.Collections.Generic;
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
            if (string.IsNullOrEmpty(number))
            {
                return BadRequest(new
                {
                    detail = new List<object>
                    {
                        new
                        {
                            type = "missing",
                            loc = new List<string> { "query", "number" },
                            msg = "Field required",
                            input = (string)null
                        }
                    }
                });
            }

            if (!int.TryParse(number, out int parsedNumber))
            {
                return BadRequest(new { number = "alphabet", error = true });
            }

            var result = await _classifyNumberUseCase.ExecuteAsync(parsedNumber);
            return Ok(result);
        }
    }
}
