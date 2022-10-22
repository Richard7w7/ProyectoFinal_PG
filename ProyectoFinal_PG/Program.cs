using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_PG;
using ProyectoFinal_PG.Models;
using ProyectoFinal_PG.Servicios;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense
    ("Mgo+DSMBMAY9C3t2VVhjQlFac19JXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRd0VhWH1cdHZQT2VVV0c=");
builder.Services.AddControllersWithViews(opc =>
{
    opc.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
    
});
//builder.Services.AddControllers()
//.AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler 
//= System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<BD_ControlVacacionalContext>(opc => 
opc.UseNpgsql(builder.Configuration.GetConnectionString("devConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//Inyeccion de nuestras Interfaces

builder.Services.AddTransient<IServiciosRegistroLogueo, ServiciosRegistroLogueo>();
builder.Services.AddTransient<IServicioEmpleados, ServicioEmpleados>();
builder.Services.AddTransient<IServiciosSolicitudes, ServiciosSolicitudes>();

builder.Services.AddTransient<IUserStore<TbEmpleados>, EmpleadoStore>();

builder.Services.AddTransient<SignInManager<TbEmpleados>>();

builder.Services.AddIdentityCore<TbEmpleados>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddIdentityCore<TbEmpleados>(opc =>
{
    opc.Password.RequireDigit = false;
    opc.Password.RequireLowercase = false;
    opc.Password.RequireUppercase = false;
    opc.Password.RequireNonAlphanumeric = false;
}).AddErrorDescriber<MensajesDeErrorIdentity>();
builder.Services.AddAuthentication(opc =>
{
    opc.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    opc.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    opc.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, opc =>
{
    opc.LoginPath = "/usuarios/logueo";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=logueo}/{id?}");

app.Run();
