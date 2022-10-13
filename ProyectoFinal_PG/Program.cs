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
builder.Services.AddControllersWithViews(opc =>
{
    opc.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
});
builder.Services.AddDbContext<BD_ControlVacacionalContext>(opc => 
opc.UseNpgsql(builder.Configuration.GetConnectionString("devConnection")));
builder.Services.AddTransient<IServiciosRegistroLogueo, ServiciosRegistroLogueo>();
builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();
builder.Services.AddTransient<IUserStore<TbEmpleados>, EmpleadoStore>();
builder.Services.AddTransient<SignInManager<TbEmpleados>>();
builder.Services.AddIdentityCore<TbEmpleados>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddIdentityCore<TbEmpleados>(opc =>
{
    opc.Password.RequireDigit = true;
    opc.Password.RequireLowercase = false;
    opc.Password.RequireUppercase = false;
    opc.Password.RequireNonAlphanumeric = true;
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
