using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using WebApiWithJwtSwagger.Models;

namespace WebApiWithJwtSwagger.Models
{
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }


//public static class UserInfoEndpoints
//{
//	public static void MapUserInfoEndpoints (this IEndpointRouteBuilder routes)
//    {
//        var group = routes.MapGroup("/api/UserInfo").WithTags(nameof(UserInfo));

//        group.MapGet("/", async (DatabaseContext db) =>
//        {
//            return await db.Users.ToListAsync();
//        })
//        .WithName("GetAllUserInfos")
//        .WithOpenApi();

//        group.MapGet("/{id}", async Task<Results<Ok<UserInfo>, NotFound>> (int userid, DatabaseContext db) =>
//        {
//            return await db.Users.AsNoTracking()
//                .FirstOrDefaultAsync(model => model.UserId == userid)
//                is UserInfo model
//                    ? TypedResults.Ok(model)
//                    : TypedResults.NotFound();
//        })
//        .WithName("GetUserInfoById")
//        .WithOpenApi();

//        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int userid, UserInfo userInfo, DatabaseContext db) =>
//        {
//            var affected = await db.Users
//                .Where(model => model.UserId == userid)
//                .ExecuteUpdateAsync(setters => setters
//                  .SetProperty(m => m.UserId, userInfo.UserId)
//                  .SetProperty(m => m.UserName, userInfo.UserName)
//                  .SetProperty(m => m.Password, userInfo.Password)
//                  );
//            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
//        })
//        .WithName("UpdateUserInfo")
//        .WithOpenApi();

//        group.MapPost("/", async (UserInfo userInfo, DatabaseContext db) =>
//        {
//            db.Users.Add(userInfo);
//            await db.SaveChangesAsync();
//            return TypedResults.Created($"/api/UserInfo/{userInfo.UserId}",userInfo);
//        })
//        .WithName("CreateUserInfo")
//        .WithOpenApi();

//        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int userid, DatabaseContext db) =>
//        {
//            var affected = await db.Users
//                .Where(model => model.UserId == userid)
//                .ExecuteDeleteAsync();
//            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
//        })
//        .WithName("DeleteUserInfo")
//        .WithOpenApi();
//    }
//}
}
