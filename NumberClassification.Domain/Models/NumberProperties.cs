using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberClassification.Domain.Models
{
    public class NumberProperties
    {
        public int Number { get; set; }
        public bool IsPrime { get; set; }
        public bool IsPerfect { get; set; }
        public List<string>? Properties { get; set; } = new List<string>();
        public int DigitSum { get; set; }
        public string? FunFact { get; set; } = string.Empty;
    }
}
