namespace Calc4.Extenstions
{
    public static class CharExtensions
    {
        private static readonly string _operators = "+-/*()";

        public static bool IsDigit(this char ch)
        {
            return char.IsDigit(ch);
        }

        public static bool IsOperator(this char ch)
        {
            return _operators.Contains(ch);
        }

        public static int OperatorPriority(this char ch)
        {
            switch (ch)
            {
                case '(':
                    return 0;
                case ')': 
                    return 1;
                case '+': 
                    return 2;
                case '-': 
                    return 3;
                case '*': 
                    return 4;
                case '/': 
                    return 4;
                default:
                    return 5;
            }
        }
    }
}
