using System;
using System.Threading.Tasks;

namespace LibraryProject.WebUI.Services
{
    public class CommonService
    {

        public const string BaseURL = "https://localhost:8081/api";

        protected async Task<T?> TryExecuteAsync<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception exception)
            {
                HandleException(exception);
                return default;
            }
        }

        protected virtual void HandleException(Exception exception)
        {
            switch (exception)
            {
                default:
                    break;
            }
        }

    }
}
