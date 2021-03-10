using System;
using Xunit;
using NumerosPorExtenso.Controllers;
using NumerosPorExtenso.CustomExceptions;

namespace XUnitTestNumerosPorExtenso
{
    public class ExtensoControllerUnitTest
    {
        ExtensoController controller = new ExtensoController();

        [Fact]
        public void TrioToWordsWorksForSpecifiedNumbers()
        {
            Assert.Equal("zero", controller.TrioToWords("000"));
            Assert.Equal("cem", controller.TrioToWords("100"));
            Assert.Equal("onze", controller.TrioToWords("011"));
            Assert.Equal("vinte e um", controller.TrioToWords("021"));
            Assert.Equal("novecentos e noventa e nove", controller.TrioToWords("999"));
        }

        [Fact]
        public void TrioToWordsThrowsExceptionForInvalidFormats()
        {
            Assert.ThrowsAny<Exception>(() => controller.TrioToWords("99"));
            Assert.ThrowsAny<Exception>(() => controller.TrioToWords("9"));
        }

        [Fact]
        public void NumberToWordsWorksForSpecifiedNumbers()
        {
            Assert.Equal("zero", controller.NumberToWords("0"));
            Assert.Equal("zero", controller.NumberToWords("-00"));
            Assert.Equal("um", controller.NumberToWords("0000000001"));
            Assert.Equal("mil", controller.NumberToWords("1000"));
            Assert.Equal("doze mil", controller.NumberToWords("12000"));
            Assert.Equal("noventa e nove mil e novecentos e noventa e nove", controller.NumberToWords("99999"));
            Assert.Equal("menos noventa e nove mil e novecentos e noventa e nove", controller.NumberToWords("-99999"));
            Assert.Equal("vinte e sete mil e duzentos e cinquenta e oito", controller.NumberToWords("27258"));
        }

        [Fact]
        public void NumberToWordsTrimsWhiteSpaces()
        {
            Assert.Equal("novecentos", controller.NumberToWords(" 900"));
            Assert.Equal("cinquenta e oito mil e quatro", controller.NumberToWords("  58004   "));
            Assert.Equal("dez", controller.NumberToWords("10   "));
        }


        [Fact]
        public void ValidateInputThrowsExceptionForInvalidFormats()
        {
            Assert.Throws<HttpStatusException>(() => controller.ValidateInput("9-9"));
            Assert.Throws<HttpStatusException>(() => controller.ValidateInput("9 9"));
            Assert.Throws<HttpStatusException>(() => controller.ValidateInput("45a"));
        }

        [Fact]
        public void ValidateInputThrowsExceptionForNumberOutOfRange()
        {
            Assert.Throws<HttpStatusException>(() => controller.ValidateInput("-100000"));
            Assert.Throws<HttpStatusException>(() => controller.ValidateInput("100000"));
        }
    }
}
