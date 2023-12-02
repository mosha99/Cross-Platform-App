using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using MyGymWeb;
using MyGymWeb.Storage;
using Pages.Interfaces;
using Pages.Pages;
using Report;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddSingleton<IStorage, Storage>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


var app = builder.Build();


await app.RunAsync();


