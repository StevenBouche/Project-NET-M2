using LibraryProject.Business.Dto.Books;
using Microsoft.AspNetCore.SignalR;

namespace LibraryProject.API.Hubs
{
    public class LibraryProxyHub
    {

        private readonly IHubContext<LibraryHub> _hub;

        public LibraryProxyHub(IHubContext<LibraryHub> hub)
        {
            _hub = hub;
        }

        public Task OnCreatedBook(BookDetailsDto book)
        {
            return _hub.Clients.All.SendAsync("OnCreatedBook", book);
        }

        public Task OnDeletedBook(int id)
        {
            return _hub.Clients.All.SendAsync("OnDeletedBook", id);
        }
    }
}
