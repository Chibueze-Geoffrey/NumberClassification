using NumberClassification.Application.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumberClassification.Application.UseCase
{
    public class ClassifyNumber : IClassifyNumber
    {
        private readonly INumberService _numberService;

        public ClassifyNumber(INumberService numberService)
        {
            _numberService = numberService;
        }

        public async Task<Dictionary<string, object>> ExecuteAsync(int number)
        {
            return await _numberService.ClassifyNumberAsync(number);
        }
    }
}
