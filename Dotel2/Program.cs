using Dotel2.Models;
using Dotel2.Repository.MemberShip;
using Dotel2.Repository.Message;
using Dotel2.Repository.Rental;
using Dotel2.Repository.Reviews;
using Dotel2.Repository.User;
using EXE_Dotel.Repository.Rental;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DotelDBContext>();
builder.Services.AddSession();
builder.Services.AddScoped<IRentalRepository, RentalRepostiory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRespository>();
builder.Services.AddScoped<IMemberShipRepository, MemberShipRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
