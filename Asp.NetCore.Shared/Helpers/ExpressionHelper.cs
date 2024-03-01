namespace Asp.NetCore.Shared.Helpers
{
    /// <summary>
    /// Get object's member info by linq expression
    /// </summary>
    public static class ExpressionUtils
    {

        /// <summary>
        /// Gets the member expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static MemberExpression GetMemberExpression(LambdaExpression expression)
        {
            return RemoveUnary(expression.Body) as MemberExpression;
        }

        /// <summary>
        /// Removes the unary.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        private static Expression RemoveUnary(Expression body)
        {
            var unary = body as UnaryExpression;
            if (unary != null)
            {
                return unary.Operand;
            }
            return body;
        }

        /// <summary>
        /// Gets the type from member expression.
        /// </summary>
        /// <param name="memberExpression">The member expression.</param>
        /// <returns></returns>
        public static Type GetTypeFromMemberExpression(MemberExpression memberExpression)
        {
            if (memberExpression == null) return null;

            var dataType = GetTypeFromMemberInfo(memberExpression.Member, (PropertyInfo p) => p.PropertyType);
            if (dataType == null) dataType = GetTypeFromMemberInfo(memberExpression.Member, (MethodInfo m) => m.ReturnType);
            if (dataType == null) dataType = GetTypeFromMemberInfo(memberExpression.Member, (FieldInfo f) => f.FieldType);

            return dataType;
        }

        /// <summary>
        /// Gets the name of the member inferred.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string GetMemberInferredName(LambdaExpression expression)
        {
            var memberExpression = GetMemberExpression(expression);
            return memberExpression == null ? null : memberExpression.Member.Name;
        }

        /// <summary>
        /// Gets the type from member info.
        /// </summary>
        /// <typeparam name="TMember">The type of the member.</typeparam>
        /// <param name="member">The member.</param>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        private static Type GetTypeFromMemberInfo<TMember>(MemberInfo member, Func<TMember, Type> func) where TMember : MemberInfo
        {
            if (member is TMember)
            {
                return func((TMember)member);
            }
            return null;
        }
    }
}
