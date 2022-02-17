using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Blazorise.RichTextEdit;
using LibraryProject.WebUI;
using LibraryProject.WebUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Globalization;
using System.Net.Http;

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

SetDefaultCulture(app);

await app.RunAsync();

void SetDefaultCulture(WebAssemblyHost host)
{
    /*var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
    var result = await jsInterop.InvokeAsync<string>("blazorCulture.get");
    CultureInfo culture;
    if (result != null)
        culture = new CultureInfo(result);
    else*/
    var culture = new CultureInfo("en-US");
    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;
}