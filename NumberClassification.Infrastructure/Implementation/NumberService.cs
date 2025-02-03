using NumberClassification.Application.Interface;
using NumberClassification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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

        public async Task<Dictionary<string, object>> ClassifyNumberAsync(int number)
        {
            var numberProperties = new NumberProperties
            {
                Number = number,
                IsPrime = IsPrime(number),
                IsPerfect = IsPerfect(number),
                Properties = GetProperties(number),
                DigitSum = GetDigitSum(number),
                FunFact = await GetFunFact(number)
            };

            return ToSnakeCaseDictionary(numberProperties);
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

        private Dictionary<string, object> ToSnakeCaseDictionary(NumberProperties properties)
        {
            var dict = new Dictionary<string, object>
            {
                { "number", properties.Number },
                { "is_prime", properties.IsPrime },
                { "is_perfect", properties.IsPerfect },
                { "properties", properties.Properties.Select(ToSnakeCase).ToList() },
                { "digit_sum", properties.DigitSum },
                { "fun_fact", properties.FunFact }
            };

            return dict;
        }

        private string ToSnakeCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}
