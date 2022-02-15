using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using LibraryProject.WebUI;
using LibraryProject.WebUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalization();

builder.Services.AddBlazorise(options => { options.ChangeTextOnKeyPress = true; })
      .AddBootstrapProviders()
      .AddFontAwesomeIcons();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<BookService>();

var app = builder.Build();

//app.UseRequestLocalization("en-US");

await app.RunAsync();