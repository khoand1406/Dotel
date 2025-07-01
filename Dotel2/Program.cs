using Dotel2.Models;
using Dotel2.Repository.Conversation;
using Dotel2.Repository.MemberShip;
using Dotel2.Repository.Message;
using Dotel2.Repository.Rental;
using Dotel2.Repository.Reviews;
using Dotel2.Repository.User;
using Dotel2.Repository.User.ChatAI;
using Dotel2.Service.Admin.Auth;
using Dotel2.Service.Admin.Rental;
using Dotel2.Service.AIChatService;
using Dotel2.Service.Chat;
using Dotel2.Service.Chat.Conversations;
using Dotel2.Service.Chat.Messages;
using Dotel2.Service.Mail;
using Dotel2.Service.Rental;
using Dotel2.Service.User.EmailVerfification;
using Dotel2.Service.User.Login;
using Dotel2.Service.User.Profile;
using Dotel2.Service.User.Register;
using Dotel2.Service.User.ResetPassword;
using Dotel2.SignalR;
using EXE_Dotel.Repository.Rental;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DotelDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddSession();

builder.Services.AddScoped<IRentalRepository, RentalRepostiory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRespository>();
builder.Services.AddScoped<IMemberShipRepository, MemberShipRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();

builder.Services.AddScoped<ILoginService,  LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IEmailVerificationService,  EmailVerificationService>();
builder.Services.AddScoped<IResetPasswordService, ResetPasswordService>();
builder.Services.AddScoped<IRentalService,  RentalService>();   
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IConversationService, ConversationServices>();
builder.Services.AddScoped<iMessageService, MessageService>();
builder.Services.AddScoped<ISendMailService, SendMailService>();
builder.Services.AddScoped<IOpenAiChatService, OpenAIChatService>();

builder.Services.AddScoped<IAdminAuthService,  AdminAuthService>();
builder.Services.AddScoped<IAdmiinRentalService, AdminRentalService>();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();

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

app.MapHub<MessageHub>("/messageHub");

app.Run();
