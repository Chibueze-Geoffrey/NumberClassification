using NumberClassification.Application.Interface;
using NumberClassification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NumberClassification.Infrastructure.Implementation
{
    public class NumberService : INumberService
    {
        private readonly HttpClient _httpClient;

        public NumberService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public NumberProperties ClassifyNumber(int number)
        {
            var numberProperties = new NumberProperties
            {
                Number = number,
                IsPrime = IsPrime(number),
                IsPerfect = IsPerfect(number),
                Properties = GetProperties(number),
                DigitSum = GetDigitSum(number),
                FunFact = GetFunFact(number).Result
            };

            return numberProperties;
        }

        private async Task<string> GetFunFact(int number)
        {
            var response = await _httpClient.GetStringAsync($"http://numbersapi.com/{number}/math");
            return response;
        }

        private bool IsPrime(int number)
        {
            // Prime number logic
            return true; // Placeholder
        }

        private bool IsPerfect(int number)
        {
            // Perfect number logic
            return false; // Placeholder
        }

        private List<string> GetProperties(int number)
        {
            // Armstrong and odd/even properties
            return new List<string> { "armstrong", "odd" }; // Placeholder
        }

        private int GetDigitSum(int number)
        {
            // Sum of digits logic
            return number.ToString().Sum(c => c - '0'); // Example logic
        }

        //private string GetFunFact(int number)
        //{
        //    // Call to Numbers API
        //    return $"Fun fact about {number}"; // Placeholder
        //}
    }
}
