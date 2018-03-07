namespace mCore.Exception
{
    /// <summary>
    /// 应用异常
    /// </summary>
    [System.Serializable]
    public class ApplicationException : System.Exception
    {
        public ApplicationException() { }

        public ApplicationException(string message) : base(message) { }

        public ApplicationException(string message, System.Exception inner) : base(message, inner) { }

        protected ApplicationException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}