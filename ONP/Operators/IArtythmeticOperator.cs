namespace ONP.Operators
{
    public interface IArtmeticOperator
    {
        double Calculate(double a, double b);
        string Operator { get; }
        bool IsOperator(string @operator);
    }
}