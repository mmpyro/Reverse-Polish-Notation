namespace ONP.Operators
{
    public static class ArtmeticOperator
    {
        public static IArtmeticOperator Add()
        {
            return new AddOperator();
        }

        public static IArtmeticOperator Sub()
        {
            return new SubOperator();
        }
    }
}
