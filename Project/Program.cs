using JWT.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using JWT.Context;
using Microsoft.EntityFrameworkCore;
using Project.Services;

// var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// builder.Services.AddDbContext<DatabaseContext>(opt =>
//     opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//
// builder.Services.AddAuthentication().AddJwtBearer(opt =>
// {
//     opt.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = builder.Configuration["JWT:Issuer"],
//         ValidAudience = builder.Configuration["JWT:Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
//     };
// });
// // ===
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// // === Uruchom autoryzacje dla wyznaczonych koncowek
// app.UseAuthorization();
//
//
// app.MapControllers();
//
// // app.Use(async (context, next) =>
// // {
// //     Console.WriteLine("Hello world");
// //     await next(context);
// //     Console.WriteLine("Hello world 2");
// // });
//
// app.ConfigureExceptionHandler();
//
//
// app.Run();

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
       
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers().AddJsonOptions(x => 
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        builder.Services.AddDbContext<DatabaseContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        builder.Services.AddScoped<IIndividualClientService, IndividualClientService>();
        builder.Services.AddScoped<ICompanyClientService, CompanyClientService>();
        builder.Services.AddScoped<IContractService, ContractService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IRevenueService, RevenueService>();
        
        var app = builder.Build();

        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}