using Ardalis.Result.AspNetCore;
using BaseApi.Models;
using BaseApi.Services;

namespace BaseApi.Endpoints;

public static class UserEndpoints
{
	public static void UseUserEndpoints(this IEndpointRouteBuilder app)
	{
		app.MapGet("/users", async (IUserService userService) => {
			return await userService.GetAllAsync();
		});

		app.MapGet("/users/{id}", async (IUserService userService, Guid id) => {
			var result = await userService.GetByIdAsync(id);
			return result.ToMinimalApiResult();
		});

		app.MapPost("/users", async (IUserService userService, User user) => {
			var result = await userService.CreateAsync(user);
			return result.ToMinimalApiResult();
		});

		app.MapPut("/users/{id}", async (IUserService userService, Guid id, User user) => {
			var result = await userService.UpdateAsync(id, user);
			return result.ToMinimalApiResult();
		});

		app.MapDelete("/users/{id}", async (IUserService userService, Guid id) => {
			var result = await userService.DeleteAsync(id);
			return result.ToMinimalApiResult();
		});
	}
}
