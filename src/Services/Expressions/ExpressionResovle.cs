using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using mCore.Exceptions;

namespace mCore.Services.Expressions
{
    public class ExpressionResovle
    {
        private readonly string expr;
        private readonly Stack<Expression> stack;
        private readonly Stack<string> operationStack;

        public ExpressionResovle(string expressionString)
        {
            if (string.IsNullOrWhiteSpace(expressionString))
            {
                throw new System.ArgumentNullException(nameof(expressionString));
            }

            expr = expressionString;
            stack = new Stack<Expression>();
            operationStack = new Stack<string>();
        }

        public Expression Build()
        {
            const string splitChars = "+-*/,";

            var tempary = new StringBuilder();

            var currentChar = default(char);
            for (int i = 0; i < expr.Length; i++, currentChar = expr[i])
            {
                // 未遇到分割符时继续拼接
                if (!splitChars.Contains(currentChar.ToString()))
                {
                    tempary.Append(currentChar);
                    continue;
                }

                if (functionReg.IsMatch(tempary.ToString()))
                {
                    operationStack.Push(tempary.ToString());
                }
                else
                {
                    var operands = GetOperands(tempary.ToString());
                    stack.Push(operands);
                }

                tempary.Clear();
            }

            throw new NotImplementedException();
        }

        private static readonly Regex numberReg = new Regex(@"\d\.?\d");
        private static readonly Regex stringReg = new Regex(@"\"".*\""");
        private static readonly Regex booleanReg = new Regex(@"true|false");
        private static readonly Regex functionReg = new Regex(@"\w+\(\)");
        private static readonly Regex parameterReg = new Regex(@"@\w+");

        private Expression GetOperands(string input)
        {
            var result = default(Expression);

            if (numberReg.IsMatch(input))
            {
                result = new ConstantExpression<decimal>(Convert.ToDecimal(input));
            }
            else if (stringReg.IsMatch(input))
            {
                result = new ConstantExpression<string>(input);
            }
            else if (booleanReg.IsMatch(input))
            {
                result = new ConstantExpression<bool>(Convert.ToBoolean(input));
            }
            else if (parameterReg.IsMatch(input))
            {
                result = new ParameterExpression<string>(input.Substring(1));
            }
            else
            {
                throw new ArgumentAppException($"不支持的操作数 {input}.", nameof(input));
            }

            return result;
        }
    }
}