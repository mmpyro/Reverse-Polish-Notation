namespace ONP.Operators
{
    public class AddOperator : IArtmeticOperator
    {
        public string Operator => "+";

        public double Calculate(double a, double b)
        {
            return a + b;
        }

        public bool IsOperator(string @operator)
        {
            return @operator.Equals(Operator);
        }

    }
}
