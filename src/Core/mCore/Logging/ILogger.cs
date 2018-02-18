namespace mCore.Logging
{
    using System;
    
    //
    // Summary:
    //     /// Manages logging. ///
    //
    // Remarks:
    //     /// This is a facade for the different logging subsystems. /// It offers a simplified
    //     interface that follows IOC patterns /// and a simplified priority/level/severity
    //     abstraction. ///
    public interface ILogger
    {
        //
        // Summary:
        //     /// Determines if messages of priority "warn" will be logged. ///
        bool IsWarnEnabled { get; }
        //
        // Summary:
        //     /// Determines if messages of priority "fatal" will be logged. ///
        bool IsFatalEnabled { get; }
        //
        // Summary:
        //     /// Determines if messages of priority "error" will be logged. ///
        bool IsErrorEnabled { get; }
        //
        // Summary:
        //     /// Determines if messages of priority "debug" will be logged. ///
        bool IsDebugEnabled { get; }
        //
        // Summary:
        //     /// Determines if messages of priority "info" will be logged. ///
        bool IsInfoEnabled { get; }

        //
        // Summary:
        //     /// Create a new child logger. /// The name of the child logger is [current-loggers-name].[passed-in-name]
        //     ///
        //
        // Parameters:
        //   loggerName:
        //     The Subname of this logger.
        //
        // Returns:
        //     The New ILogger instance.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If the name has an empty element name.
        ILogger CreateChildLogger(string loggerName);
        //
        // Summary:
        //     /// Logs a debug message with lazily constructed message. The message will be
        //     constructed only if the Castle.Core.Logging.ILogger.IsDebugEnabled is true. ///
        //
        // Parameters:
        //   messageFactory:
        void Debug(Func<string> messageFactory);
        //
        // Summary:
        //     /// Logs a debug message. ///
        //
        // Parameters:
        //   message:
        //     The message to log
        void Debug(string message);
        //
        // Summary:
        //     /// Logs a debug message. ///
        //
        // Parameters:
        //   exception:
        //     The exception to log
        //
        //   message:
        //     The message to log
        void Debug(string message, Exception exception);
        //
        // Summary:
        //     /// Logs an error message. ///
        //
        // Parameters:
        //   exception:
        //     The exception to log
        //
        //   message:
        //     The message to log
        void Error(string message, Exception exception);
        //
        // Summary:
        //     /// Logs an error message with lazily constructed message. The message will be
        //     constructed only if the Castle.Core.Logging.ILogger.IsErrorEnabled is true. ///
        //
        // Parameters:
        //   messageFactory:
        void Error(Func<string> messageFactory);
        //
        // Summary:
        //     /// Logs an error message. ///
        //
        // Parameters:
        //   message:
        //     The message to log
        void Error(string message);
        //
        // Summary:
        //     /// Logs a fatal message. ///
        //
        // Parameters:
        //   message:
        //     The message to log
        void Fatal(string message);
        //
        // Summary:
        //     /// Logs a fatal message with lazily constructed message. The message will be
        //     constructed only if the Castle.Core.Logging.ILogger.IsFatalEnabled is true. ///
        //
        // Parameters:
        //   messageFactory:
        void Fatal(Func<string> messageFactory);
        //
        // Summary:
        //     /// Logs a fatal message. ///
        //
        // Parameters:
        //   exception:
        //     The exception to log
        //
        //   message:
        //     The message to log
        void Fatal(string message, Exception exception);
        //
        // Summary:
        //     /// Logs an info message. ///
        //
        // Parameters:
        //   exception:
        //     The exception to log
        //
        //   message:
        //     The message to log
        void Info(string message, Exception exception);
        //
        // Summary:
        //     /// Logs an info message. ///
        //
        // Parameters:
        //   message:
        //     The message to log
        void Info(string message);
        //
        // Summary:
        //     /// Logs a info message with lazily constructed message. The message will be
        //     constructed only if the Castle.Core.Logging.ILogger.IsInfoEnabled is true. ///
        //
        // Parameters:
        //   messageFactory:
        void Info(Func<string> messageFactory);
        //
        // Summary:
        //     /// Logs a warn message with lazily constructed message. The message will be
        //     constructed only if the Castle.Core.Logging.ILogger.IsWarnEnabled is true. ///
        //
        // Parameters:
        //   messageFactory:
        void Warn(Func<string> messageFactory);
        //
        // Summary:
        //     /// Logs a warn message. ///
        //
        // Parameters:
        //   exception:
        //     The exception to log
        //
        //   message:
        //     The message to log
        void Warn(string message, Exception exception);
        //
        // Summary:
        //     /// Logs a warn message. ///
        //
        // Parameters:
        //   message:
        //     The message to log
        void Warn(string message);
    }
}