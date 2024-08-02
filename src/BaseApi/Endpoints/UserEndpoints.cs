using Ardalis.Result.AspNetCore;
using BaseApi.Models;
using BaseApi.Services;

namespace BaseApi.Endpoints;

public static class UserEndpoints
{
	private const string BasePath = "/users";
	private const string Tag = "Users";

	public static void UseUserEndpoints(this IEndpointRouteBuilder app)
	{
		app.MapGet(BasePath, async (IUserService userService) => {
			return await userService.GetAllAsync();
		}).WithTags(Tag);

		app.MapGet($"{BasePath}/{{id:guid}}", async (IUserService userService, Guid id) => {
			var result = await userService.GetByIdAsync(id);
			return result.ToMinimalApiResult();
		}).WithTags(Tag);

		app.MapPost(BasePath, async (IUserService userService, User user) => {
			var result = await userService.CreateAsync(user);
			return Results.Created($"{BasePath}/{result.Value.UserId}", result.Value);
		}).WithTags(Tag);

		app.MapPut($"{BasePath}/{{id:guid}}", async (IUserService userService, Guid id, User user) => {
			var result = await userService.UpdateAsync(id, user);
			return result.ToMinimalApiResult();
		}).WithTags(Tag);

		app.MapDelete($"{BasePath}/{{id:guid}}", async (IUserService userService, Guid id) => {
			var result = await userService.DeleteAsync(id);
			return result.ToMinimalApiResult();
		}).WithTags(Tag);
	}
}
