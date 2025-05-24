using AutoMapper;
using UserContacts.Bll.Services;
using UserContacts.Bll.Services.AuthServices;
using UserContacts.Bll.Services.UserServices;

namespace UserContacts.Server.Configurations;

public static class ServiceConfigurations
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        builder.Services.AddScoped<IUserRoleService, UserRoleService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddAutoMapper(typeof(Mapper));
    }
}
