using ONP.Exceptions;
using ONP.Operators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ONP
{
    public class ONPCalculator
    {
        private readonly Stack<double> _stack = new Stack<double>();
        private readonly IArtmeticOperator[] _operators;

        public ONPCalculator(params IArtmeticOperator[] operators)
        {
            _operators = operators;
        }

        public double Perform(string input)
        {
            try
            {
                return Calculate(input);
            }
            catch (NullReferenceException ex)
            {
                throw new ParseException("Operator wasn't found.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ParseException("Invalid expression.", ex);
            }
        }

        protected double Calculate(string input)
        {
            string[] numbers = input.Split(' ').Where(t => !string.IsNullOrEmpty(t)).ToArray();
            double number;
            foreach (string item in numbers)
            {
                if (double.TryParse(item, out number))
                {
                    _stack.Push(number);
                }
                else
                {
                    double number2 = _stack.Pop();
                    double number1 = _stack.Pop();
                    IArtmeticOperator @operator = Array.Find(_operators, t => t.IsOperator(item));
                    _stack.Push(@operator.Calculate(number1, number2));
                }
            }
            return _stack.Pop();
        }
    }
}