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
            // Handle negative fun fact
            if (number < 0)
            {
                return $"{number} is an uninteresting number.";
            }

            var response = await _httpClient.GetStringAsync($"http://numbersapi.com/{number}/math");
            return response;
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        private bool IsPerfect(int number)
        {
            if (number < 1) return false;

            int sum = 0;
            for (int i = 1; i <= number / 2; i++)
            {
                if (number % i == 0)
                    sum += i;
            }

            return sum == number;
        }

        private List<string> GetProperties(int number)
        {
            var properties = new List<string>();

            if (IsArmstrong(number))
                properties.Add("armstrong");

            if (number % 2 == 0)
                properties.Add("even");
            else
                properties.Add("odd");

            return properties;
        }

        private bool IsArmstrong(int number)
        {
            number = Math.Abs(number); // Make the number absolute before Armstrong check

            int sum = 0;
            int temp = number;
            int numberOfDigits = number.ToString().Length;

            while (temp != 0)
            {
                int digit = temp % 10;
                sum += (int)Math.Pow(digit, numberOfDigits);
                temp /= 10;
            }

            return sum == number;
        }

        private int GetDigitSum(int number)
        {
            return Math.Abs(number).ToString().Sum(c => c - '0');
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
