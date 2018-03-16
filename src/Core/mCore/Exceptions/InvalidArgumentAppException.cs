namespace mCore.Exceptions
{
    using System;

    /// <summary>
    /// 无效的参数异常
    /// </summary>
    public class InvalidArgumentAppException : ArgumentAppException
    {
        public InvalidArgumentAppException(string paramName = null, string message = null, Exception innerException = null) : base(message, paramName, innerException)
        {
        }
    }
}