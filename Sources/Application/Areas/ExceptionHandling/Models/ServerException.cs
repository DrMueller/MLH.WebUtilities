using System;
using JetBrains.Annotations;
using Mmu.Mlh.LanguageExtensions.Areas.Exceptions;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.WebUtilities.Areas.ExceptionHandling.Models
{
    [PublicAPI]
    public class ServerException
    {
        public string Message { get; }
        public string StackTrace { get; }
        public string TypeName { get; }

        private ServerException(string message, string typeName, string stackTrace)
        {
            Guard.StringNotNullOrEmpty(() => message);
            Guard.StringNotNullOrEmpty(() => typeName);

            Message = message;
            TypeName = typeName;
            StackTrace = stackTrace;
        }

        public static ServerException CreateFromException(Exception exception)
        {
            Guard.ObjectNotNull(() => exception);

            var mostInnerEx = exception.GetMostInnerException();
            return new ServerException(mostInnerEx.Message, mostInnerEx.GetType().Name, mostInnerEx.StackTrace);
        }
    }
}