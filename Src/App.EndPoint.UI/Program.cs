using App.Domain.AppServices.AppFile;
using App.Domain.AppServices.Order;
using App.Domain.AppServices.Service;
using App.Domain.AppServices.User;
using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.Category.Contracts.Repositories;
using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Contracts.Repositories;
using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Service.Contracts.AppServices;
using App.Domain.Core.Service.Contracts.Repositories;
using App.Domain.Core.Service.Contracts.Services;
using App.Domain.Core.Service.Dtos;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Entities;
using App.Domain.Services.AppFile;
using App.Domain.Services.Order;
using App.Domain.Services.Service;
using App.Domain.Services.User;
using App.EndPoint.UI.Areas.Admin.Models.AppFile;
using App.EndPoint.UI.Models.Extentions;
using App.Infrastructure.DataBase.SQL.DbContext;
using App.Infrastructure.DataBase.SQL.SeedDate;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/LogIn";
    options.LogoutPath = "/Account/Logout";

});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnectionString")));
builder.Services.AddIdentity<AppUser, IdentityRole<int>>(
       options =>
       {
           options.SignIn.RequireConfirmedAccount = false;
           options.SignIn.RequireConfirmedPhoneNumber = false;
           options.SignIn.RequireConfirmedEmail = false;

           options.Password.RequireDigit = false;
           options.Password.RequireLowercase = false;
           options.Password.RequireUppercase = false;
           options.Password.RequireNonAlphanumeric = true;
           options.Password.RequiredLength = 6;
           options.Password.RequiredUniqueChars = 2;

       }).AddEntityFrameworkStores<AppDbContext>();

#region AppFile
builder.Services.AddScoped<IAppFileCommandRepository, AppFileCommandRepository>();
builder.Services.AddScoped<IAppFileQueryRepository, AppFileQueryRepository>();
builder.Services.AddScoped<IFileTypeCommandRepository, FileTypeCommandRepository>();
builder.Services.AddScoped<IFileTypeQueryRepository, FileTypeQueryRepository>();
builder.Services.AddScoped<IFileTypeAppService, FileTyepAppService>();
builder.Services.AddScoped<IFileTypeService, FileTypeService>();
builder.Services.AddScoped<IAppFileAppService, AppFileAppService>();
builder.Services.AddScoped<IAppFileService, AppFileService>();
builder.Services.AddScoped<IAppFileStorageService, AppFileStorageService>();
#endregion AppFile

#region Category
builder.Services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
builder.Services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
#endregion Category

#region Order
builder.Services.AddScoped<IBidCommandRepository, BidCommandRepository>();
builder.Services.AddScoped<IBidQueryRepository, BidQueryRepository>();
builder.Services.AddScoped<ICommentCommandRepository, CommentCommandRepository>();
builder.Services.AddScoped<ICommentQueryRepository, CommentQueryRepository>();
builder.Services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
builder.Services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
builder.Services.AddScoped<IOrderFileCommandRepository, OrderFileCommandRepository>();
builder.Services.AddScoped<IOrderFileQueryRepository, OrderFileQueryRepository>();
builder.Services.AddScoped<IOrderProposalCommandRepository, OrderProposalCommandRepository>();
builder.Services.AddScoped<IOrderProposalQueryRepository, OrderProposalQueryRepository>();
builder.Services.AddScoped<IBidAppService, BidAppService>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<ICommentAppService, CommentAppService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IOrderAppService, OrderAppService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderProposalAppService, OrderProposalAppService>();
builder.Services.AddScoped<IOrderProposalService, OrderProposalService>();
#endregion Order

#region Service
builder.Services.AddScoped<IExpertFavoritServiceCommandRepository, ExpertFavoritServiceCommandRepository>();
builder.Services.AddScoped<IExpertFavoritServiceQueryRepository, ExpertFavoritServiceQueryRepository>();
builder.Services.AddScoped<IServiceCommandRepository, ServiceCommandRepository>();
builder.Services.AddScoped<IServiceQueryRepository, ServiceQueryRepository>();
builder.Services.AddScoped<IServiceFileCommandRepository, ServiceFileCommandRepository>();
builder.Services.AddScoped<IServiceFileQueryRepository, ServiceFileQueryRepository>();
builder.Services.AddScoped<IServiceAppService, ServiceAppService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
#endregion Service

#region User
builder.Services.AddScoped<IAdminCommandRepository, AdminCommandRepository>();
builder.Services.AddScoped<IAdminQueryRepository, AdminQueryRepository>();
builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IExpertCommandRepository, ExpertCommandRepository>();
builder.Services.AddScoped<IExpertQueryRepository, ExpertQueryRepository>();
builder.Services.AddScoped<IExpertAppService, ExpertAppService>();
builder.Services.AddScoped<IExpertService, ExpertService>();
builder.Services.AddScoped<IAdminAppService, AdminAppService>();
builder.Services.AddScoped<IAdminService, AdminService>();

#endregion User

builder.Services.AddScoped<IdentitySeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//await app.SeedAsync();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}"
   );
});


app.Run();
