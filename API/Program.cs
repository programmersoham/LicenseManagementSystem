using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using API.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//for entity framework
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>();
// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        builder => builder.WithOrigins("https://localhost:7081") 
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorApp");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
CreateUserRoles(app.Services).Wait();
app.Run();

async Task CreateUserRoles(IServiceProvider serviceProvider)
{
    using (var scope = app.Services.CreateScope())
    {

        var RoleManager = scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;

        //Adding Admin Role  
        var roleCheckSuperAdmin = await RoleManager.RoleExistsAsync("SuperAdmin");
        if (!roleCheckSuperAdmin)
        {
            //create the roles and seed them to the database  
            await RoleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        }
        var UserManager = scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>)) as UserManager<IdentityUser>;

        var resultWeb = await UserManager.FindByEmailAsync("soham@gmail.com");

        if (resultWeb == null)
        {
            var user = new IdentityUser { UserName = "soham@gmail.com", Email = "soham@gmail.com", EmailConfirmed = true };
            var result = await UserManager.CreateAsync(user, "Soham@123");
            if (result.Succeeded)
            {
                // code for adding user to role
                await UserManager.AddToRoleAsync(user, "SuperAdmin");
            }
        }


    }
}


