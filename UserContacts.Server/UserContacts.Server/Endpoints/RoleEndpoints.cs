using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using UserContacts.Bll.Dtos;
using UserContacts.Bll.Services;

namespace UserContacts.Server.Endpoints;

public static class RoleEndpoints
{
    public static void MapRoleEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/role")
            .RequireAuthorization()
            .WithTags("UserRole Management");

        userGroup.MapGet("/get-all-roles", [Authorize(Roles = "Admin, SuperAdmin")]
        async (IUserRoleService _roleService) =>
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Results.Ok(roles);
        })
        .WithName("GetAllRoles");

        userGroup.MapGet("/post-role", [Authorize(Roles = "SuperAdmin")]
        async (UserRoleCreateDto role,IUserRoleService _roleService) =>
        {
            var roles = await _roleService.AddRoleAsync(role);
            return Results.Ok(roles);
        })
        .WithName("PostRole");

        userGroup.MapGet("/delete - role", [Authorize(Roles = "Admin, SuperAdmin")]
        async (string role, IUserRoleService _roleService) =>
        {
            await _roleService.DeleteRoleAsync(role);
            return Results.Ok();
        })
        .WithName("DeleteRoles");
    }
}
