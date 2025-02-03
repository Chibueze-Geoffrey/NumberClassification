﻿using NumberClassification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberClassification.Application.Interface
{
    public interface IClassifyNumber
    {
        Task<Dictionary<string, object>> ExecuteAsync(int number);
    }
}
