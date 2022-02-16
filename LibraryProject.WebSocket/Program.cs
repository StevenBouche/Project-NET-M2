// See https://aka.ms/new-console-template for more information
using LibraryProject.Business.Dto.Books;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

HubConnection connection;

connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:8080/LibraryHub")
                .Build();

connection.Closed += async (error) =>
{
    await Task.Delay(new Random().Next(0, 5) * 1000);
    await connection.StartAsync();
};

connection.On<BookDetailsDto>("OnCreatedBook", book =>
{
    string output = JsonConvert.SerializeObject(book);
    Console.WriteLine($"Nouveau {output} cree");
});

connection.On<int>("OnDeletedBook", id =>
{
    Console.WriteLine(id);
});

await connection.StartAsync();

Console.ReadLine();