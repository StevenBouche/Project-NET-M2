using LibraryProject.Business.Exceptions.Common;
using System.ComponentModel;
using System.Net;

namespace LibraryProject.Business.Exceptions
{
    public enum BookBusinessExceptionTypes
    {
        [Description("Cannot find book")]
        BOOK_NOT_FOUND
    }

    public class BookException : BusinessException
    {
        public BookBusinessExceptionTypes BookType;
        public override HttpStatusCode HttpStatusCode => SelectHttpCode();
        public override int BusinessErrorCode => (int)BookType;

        public BookException(BookBusinessExceptionTypes type, string message)
            : base(BusinessExceptionTypes.BOOK, message)
        {
            BookType = type;
        }

        private HttpStatusCode SelectHttpCode()
        {
            return BookType switch
            {
                BookBusinessExceptionTypes.BOOK_NOT_FOUND => HttpStatusCode.NotFound,
                _ => HttpStatusCode.BadRequest
            };
        }
    }
}