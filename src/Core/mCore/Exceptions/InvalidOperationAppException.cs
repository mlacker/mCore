namespace mCore.Exceptions
{
    using System;
    
    /// <summary>
    /// 无效的操作异常
    /// </summary>
    public class InvalidOperationAppException : ApplicationException
    {
        public InvalidOperationAppException(string message = null, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}