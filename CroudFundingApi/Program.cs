using Core.Identity;
using Core.Interfaces;
using CroudFundingApi.Services;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IServices, Services>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "next drivent api", Version = "v1" });
    options.ResolveConflictingActions(x => x.First());
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT authorization header dldldld",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Name="Bearer",
                In=ParameterLocation.Header,
            },
            new String[]{}
        }
    });
});
builder.Services.AddIdentityCore<AppUser>(op =>
{

}).AddEntityFrameworkStores<AppIdentityDbContext>().AddSignInManager<SignInManager<AppUser>>();
var jwt = builder.Configuration.GetSection("token");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(optionns =>
{
    optionns.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["key"])),
        ValidIssuer = jwt["Issuer"],
        ValidateIssuer=true,
        ValidateAudience=true,
        
    };
});
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(op =>
{
    op.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();
using var scope = app.Services.CreateScope();
var Services= scope.ServiceProvider;
var Context = Services.GetRequiredService<StoreContext>();
var IdentityContext = Services.GetRequiredService<AppIdentityDbContext>();
var userManager = Services.GetRequiredService<UserManager<AppUser>>();
var Logger = Services.GetRequiredService<ILogger<Program>>();

try
{
    //await Context.Database.MigrateAsync();
    //await StoreContextSeed.SeedAsync(Context);
    //await IdentityContext.Database.MigrateAsync();
    //await IdentityDbContextSeed.SeedUserAsync(userManager);

}
catch(Exception e)
{
    Logger.LogError(e, "Error While Process Migrateion");
}

app.Run();
