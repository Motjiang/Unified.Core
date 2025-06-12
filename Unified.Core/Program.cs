using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Unified.Application.Interfaces;
using Unified.Application.Services;
using Unified.Application.Services.Auth;
using Unified.Domain.Entities;
using Unified.Domain.Interfaces;
using Unified.Infrastructure.Data;
using Unified.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// be able to inject JWTService class inside our Controllers
builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ApplicationDataSeed>();
// be able to inject IMapper interface
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// be able to inject our repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
builder.Services.AddScoped<ITicketSubcategoryRepository, TicketSubcategoryRepository>();
builder.Services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
builder.Services.AddScoped<IBookRequestRepository, BookRequestRepository>();

// be able to inject our services
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDesignationService, DesignationService>();
builder.Services.AddScoped<IBookCategoryService, BookCategoryService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRequestService, BookRequestService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketCategoryService, TicketCategoryService>();
builder.Services.AddScoped<ITicketSubcategoryService, TicketSubcategoryService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();


// defining our IdentityCore Service
builder.Services.AddIdentityCore<Employee>(options =>
{
    // password configuration
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

    // for email confirmation
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddRoles<IdentityRole>() // be able to add roles
    .AddRoleManager<RoleManager<IdentityRole>>() // be able to make use of RoleManager
    .AddEntityFrameworkStores<ApplicationDbContext>() // providing our context
    .AddSignInManager<SignInManager<Employee>>() // make use of Signin manager
    .AddUserManager<UserManager<Employee>>() // make use of UserManager to create users
    .AddDefaultTokenProviders(); // be able to create tokens for email confirmation

// be able to authenticate users using JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false
        };
    });

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(builder.Configuration["JWT:ClientUrl"]);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#region ContextSeed
using var scope = app.Services.CreateScope();
try
{
    var contextSeedService = scope.ServiceProvider.GetService<ApplicationDataSeed>();
    await contextSeedService.InitializeContextAsync();
}
catch (Exception ex)
{
    var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
    logger.LogError(ex.Message, "Failed to initialize and seed the database");
}
#endregion

app.Run();
