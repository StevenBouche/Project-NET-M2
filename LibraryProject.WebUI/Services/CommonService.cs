using System;
using System.Threading.Tasks;
using RestSharp;
using Blazorise.Snackbar;

namespace LibraryProject.WebUI.Services
{
    public class CommonService
    {

        public const string BaseURL = "https://localhost:8081/api";
        protected SnackBarService SnackService { get; set; }

        public CommonService(SnackBarService snackService)
        {
            SnackService = snackService;
        }

        protected T? HandleResult<T>(RestResponse<T> response)
        {
            if (response.IsSuccessful)
            {
                return response.Data;
            }

            SnackService.SnackbarStack?.PushAsync(response.Content, SnackbarColor.Danger, options => { options.IntervalBeforeClose = 2000; });

            return default(T);
        }

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
