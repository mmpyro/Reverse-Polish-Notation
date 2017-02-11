using NUnit.Framework;
using ONP.Exceptions;
using ONP.Operators;

namespace ONP
{
    [TestFixture]
    public class Onp_Specification
    {
        [TestCase(5,4,"+", 9)]
        [TestCase(5.5, 4.5, "+", 10)]
        [TestCase(5, 4, "-", 1)]
        public void ShouldAddTwoNumbers(double number1, double number2, string @operator, double expectedResult)
        {
            //Given
            ONPCalculator calculator = new ONPCalculator(ArtmeticOperator.Add(), ArtmeticOperator.Sub());
            //When
            double result = calculator.Perform($"{number1} {number2} {@operator}");
            //Then
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ShouldParseStringWithManySpaces()
        {
            //Given
            ONPCalculator calculator = new ONPCalculator(ArtmeticOperator.Add(), ArtmeticOperator.Sub());
            //When
            double result = calculator.Perform("4    5    +");
            //Then
            Assert.That(result, Is.EqualTo(9));
        }

        [Test]
        public void ShouldThrowParseExceptionForWrongSyntax()
        {
            //Given
            ONPCalculator calculator = new ONPCalculator(ArtmeticOperator.Add(), ArtmeticOperator.Sub());
            //When - Then
            var ex = Assert.Throws<ParseException>(() => calculator.Perform("5 +"));
            Assert.That(ex.Message, Is.EqualTo("Invalid expression."));
        }

        [Test]
        public void ShouldThrowParseExceptionForWrongOperator()
        {
            //Given
            ONPCalculator calculator = new ONPCalculator(ArtmeticOperator.Add(), ArtmeticOperator.Sub());
            //When - Then
            var ex = Assert.Throws<ParseException>(() => calculator.Perform("5 4 *"));
            Assert.That(ex.Message, Is.EqualTo("Operator wasn't found."));
        }

        [Test]
        public void ShouldCalculateMoreThanOnce()
        {
            //Given
            ONPCalculator calculator = new ONPCalculator(ArtmeticOperator.Add(), ArtmeticOperator.Sub());
            //When
            double firstCalculation = calculator.Perform("7 3 +");
            double secondCalculation = calculator.Perform("7 3 -");
            //Then
            Assert.That(firstCalculation, Is.EqualTo(10));
            Assert.That(secondCalculation, Is.EqualTo(4));
        }

        [Test]
        public void ShouldCalculateAfterException()
        {
            //Given
            ONPCalculator calculator = new ONPCalculator(ArtmeticOperator.Add(), ArtmeticOperator.Sub());
            //When
            Assert.Throws<ParseException>( () => calculator.Perform("4 *"));
            double result = calculator.Perform("4 2 +");
            //Then
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void ShouldCalculateComplexExpression()
        {
            //Given
            ONPCalculator calculator = new ONPCalculator(ArtmeticOperator.Add(), ArtmeticOperator.Sub());
            //When
            double result = calculator.Perform("4 5 + 2 - 1 +");
            //Then
            Assert.That(result, Is.EqualTo(8));
        }
    }
}
