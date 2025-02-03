using NumberClassification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberClassification.Application.Interface
{
    public interface INumberService
    {
        NumberProperties ClassifyNumber(int number);
    }
}
