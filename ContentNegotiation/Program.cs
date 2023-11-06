using ContentNegotiation.ContentFormater;
using ContentNegotiation.Data;
using ContentNegotiation.Models;
using ContentNegotiation.Models.DTO;
using ContentNegotiation.Repository;
using ExtensionMethod;
using JWTAuthentication.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers(options=>options.ReturnHttpNotAcceptable=true)
//    .AddXmlDataContractSerializerFormatters();


builder.Services.AddControllersWithViews(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = false;
}).AddXmlDataContractSerializerFormatters().CustomCSVFormatter().CustomHtmlFormatter()
.CustomTextFormatter();

/*.AddApplicationPart(typeof(StudentDTO.Presentation.AssemblyReference).Assembly)*/;

builder.Services.AddMvc().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddScoped<IStudentRepository, SQLStudentRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddHttpContextAccessor();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Content Negotiate", Version = "v1" });
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
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Content Nogotiate");
});
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
