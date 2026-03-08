using AutoMail.Application; 
using AutoMail.Infrastructure; 
using AutoMail.Web.Endpoints; 
using AutoMail.Web.Components; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddTransient<TransportTransactionEndpoints>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAntiforgery(); 

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AutoMail.Web.Client._Imports).Assembly); 

using (var scope = app.Services.CreateScope())
{
    var transportEndpoints = scope.ServiceProvider.GetRequiredService<TransportTransactionEndpoints>();
    transportEndpoints.MapEndpoint(app);
}

app.Run();