using LibraryProject.Business.Exceptions.Common;
using LibraryProject.Business.Extensions;
using System.ComponentModel;
using System.Net;

namespace LibraryProject.Business.Exceptions
{
    public enum BookBusinessExceptionTypes
    {
        [Description("Livre non référencé")]
        BOOK_NOT_FOUND,
        [Description("Genre non référencé")]
        GENRE_NOT_FOUND
    }

    public class BookException : BusinessException
    {
        public BookBusinessExceptionTypes BookType;
        public override HttpStatusCode HttpStatusCode => SelectHttpCode();
        public override int BusinessErrorCode => (int)BookType;

        public BookException(BookBusinessExceptionTypes type, string message)
            : base(BusinessExceptionTypes.BOOK, $"{type.ToDescriptionString()} : {message}")
        {
            BookType = type;
        }

        private HttpStatusCode SelectHttpCode()
        {
            return BookType switch
            {
                BookBusinessExceptionTypes.BOOK_NOT_FOUND => HttpStatusCode.NotFound,
                BookBusinessExceptionTypes.GENRE_NOT_FOUND => HttpStatusCode.NotFound,
                _ => HttpStatusCode.BadRequest
            };
        }
    }
}