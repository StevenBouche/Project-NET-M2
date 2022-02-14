using LibraryProject.Business.Extensions;
using System.ComponentModel;
using System.Net;

namespace LibraryProject.Business.Exceptions.Common
{
    public enum BusinessExceptionTypes
    {
        [Description("Book business")]
        BOOK,

        [Description("Genre business")]
        GENRE,
    }

    public abstract class BusinessException : Exception
    {
        public abstract HttpStatusCode HttpStatusCode { get; }
        public BusinessExceptionTypes Type;

        public int BusinessServiceErrorCode { get => (int)Type; }
        public abstract int BusinessErrorCode { get; }

        public BusinessException(BusinessExceptionTypes type, string message)
            : base($"{type.ToDescriptionString()} : {message}")
        {
            Type = type;
        }
    }
}