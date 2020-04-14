using System;
using System.Collections.Generic;
using System.Linq;
using Calc4.Extenstions;

namespace Calc4
{
    public static class Calculator
    {
        private static readonly char _delimeter = ' ';
        private static readonly char _openBracket = '(';
        private static readonly char _closeBracket = ')';
        private static readonly string _separators= ".,";

        public static double Execute(string input)
        {
            if (!IsValid(input))
                throw new Exception("Invalid input. Try again.");
            
            var formalizedInput = Formalize(input.Replace(_separators[0], _separators[1]));
            
            return Calculate(formalizedInput);
        }

        private static bool IsValid(string input)
        {
            return !string.IsNullOrEmpty(input)
                && input.All(x => x.IsDigit() || x.IsOperator() || _separators.Contains(x))
                && CheckBrackets(input);
        }

        private static bool CheckBrackets(string input)
        {
            var brackets = new Stack<char>();
            
            for (int i = 0; i < input.Length; i++)
            {
                var ch = input[i];

                if (ch == _openBracket)
                    brackets.Push(ch);
                else if (ch == _closeBracket)
                    if (!brackets.TryPop(out char exist))
                        return false;
            }

            return brackets.Count == 0;
        }

        private static string Formalize(string input)
        {
            var result = string.Empty;
            var operators = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                var ch = input[i];
                if (ch.IsDigit())
                {
                    while (!ch.IsOperator())
                    {
                        result += ch;
                        i++;

                        if (i == input.Length)
                            break;

                        ch = input[i];
                    }

                    result += _delimeter;
                    i--;
                }
                else if (ch.IsOperator())
                {
                    if (ch == _openBracket)
                        operators.Push(ch);
                    else if (ch == _closeBracket)
                    {
                        var s = operators.Pop();

                        while (s != _openBracket)
                        {
                            result += $"{s}{_delimeter}";
                            s = operators.Pop();
                        }
                    }
                    else
                    {
                        if (operators.Count > 0)
                            if (ch.OperatorPriority() <= operators.Peek().OperatorPriority())
                                result += $"{operators.Pop()}{_delimeter}";

                        operators.Push(ch);
                    }
                }
            }

            while (operators.Count > 0)
                result += $"{operators.Pop()}{_delimeter}";

            return result;
        }

        private static double Calculate(string input)
        {
            var results = new Stack<double>();

            for (int i = 0; i < input.Length; i++)
            {
                var ch = input[i];
                if (ch.IsDigit())
                {
                    var number = string.Empty;

                    while (!ch.IsOperator() && ch != _delimeter)
                    {
                        number += ch;
                        i++;
                        
                        if (i == input.Length)
                            break;

                        ch = input[i];
                    }

                    results.Push(double.Parse(number));
                    i--;
                }
                else if (ch.IsOperator())
                {
                    var first = results.Pop();
                    var second = results.Pop();

                    results.Push(Operation(ch, first, second));
                }
            }

            return results.Peek();
        }

        private static double Operation(char ch, double first, double second)
        {
            switch (ch)
            {
                case '+':
                    return first + second;
                case '-':
                    return second - first;
                case '*':
                    return second * first;
                case '/':
                    return second / first;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
