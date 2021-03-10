using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using NumerosPorExtenso.Models;
using NumerosPorExtenso.CustomExceptions;
using System.Net;

namespace NumerosPorExtenso.Controllers
{
    [Route("/")]
    [ApiController]
    public class ExtensoController : ControllerBase
    {
        private string[,] wordMatrix = new string[4,10]
        {
            {"", "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove"},
            {"dez", "onze", "doze", "treze", "quatorze", "quinze", "dezesseis", "dezessete", "dezoito", "dezenove"},
            { "", "", "vinte", "trinta", "quarenta", "cinquenta", "sessenta", "setenta", "oitenta", "noventa"},
            {"", "cento", "duzentos", "trezentos", "quatrocentos", "quinhentos", "seiscentos", "setecentos", "oitocentos", "novecentos"}
        };

        // upper and lower range bounds (inclusive). Maximum supported range: [-999999, 999999]
        private int upperBound = 99999;
        private int lowerBound = -99999;

        [HttpGet("{number?}")]
        public NumeroPorExtenso Get(string number)
        {
            return new NumeroPorExtenso()
            {
                Extenso = (number == null) ? "" : NumberToWords(number)
            };
        }

        [NonAction]
        public string NumberToWords(string number)
        {
            int numericValue = ValidateInput(number);

            if (numericValue == 0) return "zero";

            string result = "";
            number = number.Trim();

            if (number[0] == '-')
            {
                result = "menos ";
                number = number.Substring(1);
            }

            if (number.Length > 6)
            {
                number = number.Substring(number.Length - 6);
            }
            else if (number.Length < 6)
            {
                number = number.PadLeft(6, '0');
            }

            string thousands = number.Substring(0, 3);
            string hundreds = number.Substring(3, 3);

            if (thousands != "000")
            {
                result += (thousands != "001" ? TrioToWords(thousands) + " " : "") + "mil";

                if (hundreds != "000") result += " e ";
            }

            if (!(thousands != "000" && hundreds == "000"))
            {
                result += TrioToWords(hundreds);
            }

            return result;
        }

        [NonAction]
        public string TrioToWords(string trio)
        {
            // h, t, o: hundreds, tens and ones
            int h = trio[0] - '0';
            int t = trio[1] - '0';
            int o = trio[2] - '0';

            if (trio == "100") return "cem";
            if (trio == "000") return "zero";

            List<string> wordList = new List<string>();

            if (h != 0) wordList.Add(wordMatrix[3, h]);

            if (t == 1)
            {
                wordList.Add(wordMatrix[1, o]);
            }
            else
            {
                if (t != 0) wordList.Add(wordMatrix[2, t]);
                if (o != 0) wordList.Add(wordMatrix[0, o]);
            }
                
            return String.Join(" e ", wordList);
        }

        [NonAction]
        public int ValidateInput(string number)
        {
            try
            {
                int numericValue = Int32.Parse(number);
                if (numericValue > upperBound || numericValue < lowerBound) throw new HttpStatusException(
                        HttpStatusCode.BadRequest,
                        $"Number out of acceptable range: [{lowerBound}, {upperBound}]"
                );
                return numericValue;
            }
            catch (FormatException)
            {
                throw new HttpStatusException(
                        HttpStatusCode.BadRequest,
                        "Invalid number format"
                );
            }
        }
    }
}
