using LibraryProject.Business.Exceptions.Common;
using System.ComponentModel;
using System.Net;
using LibraryProject.Business.Extensions;

namespace LibraryProject.Business.Exceptions
{

    public enum GenreExceptionTypes
    {
        [Description("Genre non trouvé")]
        GENRE_NOT_FOUND
    }
    public class GenreException: BusinessException
    {
        public GenreExceptionTypes GenreType;

        public override HttpStatusCode HttpStatusCode => SelectHttpCode();

        public override int BusinessErrorCode => (int)GenreType;

        public GenreException(GenreExceptionTypes type, string message): base(BusinessExceptionTypes.GENRE, $"{type.ToDescriptionString()} : {message}")
        {
            GenreType = type;
        }

         private HttpStatusCode SelectHttpCode()
        {
            return GenreType switch
            {
                GenreExceptionTypes.GENRE_NOT_FOUND => HttpStatusCode.NotFound,
                _ => HttpStatusCode.BadRequest
            };
        }

    }
}
