using Microsoft.AspNetCore.SignalR;


namespace LibraryProject.API.Hubs
{
    public class LibraryHub : Hub
    {
        private readonly ILogger<LibraryHub> Logger;

        public LibraryHub(ILogger<LibraryHub> logger)
        {
            Logger = logger;
        }

        public override async Task OnConnectedAsync()
        {

            var id = Context.ConnectionId;

            await base.OnConnectedAsync();

            Logger.LogInformation($"Client connected : {id}.");
        }
    }
}
