using NumberClassification.Application.Interface;
using NumberClassification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberClassification.Application.UseCases
{
    public class ClassifyNumber
    {
        private readonly INumberService _numberService;

        public ClassifyNumber(INumberService numberService)
        {
            _numberService = numberService;
        }

        public NumberProperties Execute(int number)
        {
            return _numberService.ClassifyNumber(number);
        }
    }
}
