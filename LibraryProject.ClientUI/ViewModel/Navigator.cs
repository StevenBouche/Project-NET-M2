using LibraryProject.Business.Dto.Books;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    /// <summary>
    /// Aucune raison de toucher a cette classe, mais vous pouvez par contre utilisé les propriété GoBack et GoToHome
    /// </summary>
    class Navigator : INotifyPropertyChanged
    {
        HubConnection connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:8080/LibraryHub")
            .Build();

        public event PropertyChangedEventHandler PropertyChanged;
        public string canvasVisibility { get; set; } = "Hidden";
        public string createdBookName { get; set; }

        public Frame Frame => Ioc.Default.GetRequiredService<INavigationService>().Frame;

        public ICommand GoBack { get; init; } = new RelayCommand(x => { Ioc.Default.GetRequiredService<INavigationService>().Frame.GoBack(); });
        public ICommand GoToHome { get; init; } = new RelayCommand(x =>
        {
            var service = Ioc.Default.GetRequiredService<INavigationService>();
            if (service.Frame.CanGoBack)
            {
                service.Frame.RemoveBackEntry();
                var entry = service.Frame.RemoveBackEntry();
                while (entry != null)
                {
                    entry = service.Frame.RemoveBackEntry();
                }
            }
            service.Navigate<ListBook>();
        });

        public ICommand GoToDetails { get; init; } = new RelayCommand(x =>
        {
            var service = Ioc.Default.GetRequiredService<INavigationService>();
            if (service.Frame.CanGoBack)
            {
                service.Frame.RemoveBackEntry();
                var entry = service.Frame.RemoveBackEntry();
                while (entry != null)
                {
                    entry = service.Frame.RemoveBackEntry();
                }
            }
            service.Navigate<DetailsBook>(((BookDto)x).Id);
        });

        public ICommand GoToRead { get; init; } = new RelayCommand(x =>
        {
            var service = Ioc.Default.GetRequiredService<INavigationService>();
            if (service.Frame.CanGoBack)
            {
                service.Frame.RemoveBackEntry();
                var entry = service.Frame.RemoveBackEntry();
                while (entry != null)
                {
                    entry = service.Frame.RemoveBackEntry();
                }
            }
            service.Navigate<ReadBook>((BookDetailsDto)x);
        });

        public Navigator()
        {
            connection.Closed += async (error) =>
            {
                //Prevent spam connection 
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<BookDetailsDto>("OnCreatedBook", book =>
            {
                createdBookName = $"Nouveau livre crée: {book.Name} ";
                canvasVisibility = "Visible";
                Console.Beep(700, 200);

                Task.Run(async () =>
                {
                    await Task.Delay(4000);
                    canvasVisibility = "Hidden";
                });
            });

            connection.StartAsync();
        }

        ~Navigator()
        {
            connection.StopAsync();
        }
    }
}
