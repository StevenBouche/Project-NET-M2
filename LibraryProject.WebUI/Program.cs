using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Blazorise.RichTextEdit;
using LibraryProject.WebUI;
using LibraryProject.WebUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RestSharp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalization();

builder.Services.AddBlazorise(options =>
{
    options.ChangeTextOnKeyPress = true;
    options.DelayTextOnKeyPress = true;
    options.DelayTextOnKeyPressInterval = 300;
})
      .AddBootstrap5Providers()
      .AddFontAwesomeIcons();

builder.Services
    .AddBlazoriseRichTextEdit(options => { });

var clientRest = new RestClient(new HttpClient())
    .AddDefaultHeader(KnownHeaders.ContentType, "application/json");

builder.Services.AddSingleton(clientRest);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<GenreService>();

var app = builder.Build();

//app.UseRequestLocalization("en-US");

await app.RunAsync();