using Dapper;
using System.Linq.Expressions;

namespace StoreManager.Repository
{
    internal static class ExpressionHelper
    {
        public static string Translate(Expression expression, DynamicParameters parameters)
        {
            if (expression is BinaryExpression binaryExpression)
            {
                var left = Translate(binaryExpression.Left, parameters);
                var right = Translate(binaryExpression.Right, parameters);
                var operation = GetSqlOperator(binaryExpression.NodeType);

                return $"({left} {operation} {right})";
            }
            else if (expression is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            else if (expression is ConstantExpression constantExpression)
            {
                string paramName = $"@param_{parameters.ParameterNames.Count()}";
                parameters.Add(paramName, constantExpression.Value);
                return paramName;
            }
            else
            {
                throw new NotSupportedException($"Unsupported expression type: {expression.GetType()}");
            }
        }

        private static string GetSqlOperator(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.AndAlso:
                    return "And";
                case ExpressionType.OrElse:
                    return "Or";
                default:
                    throw new NotSupportedException($"Unsupported operator: {nodeType}");
            }
        }
    }
}

