namespace mCore.Exceptions
{
    using System;

    /// <summary>
    /// 参数为空异常
    /// </summary>
    public class ArgumentNullAppException : ArgumentAppException
    {
        public ArgumentNullAppException(string paramName = null, string message = null, Exception innerException = null) : base(message, paramName, innerException)
        {
        }
    }
}