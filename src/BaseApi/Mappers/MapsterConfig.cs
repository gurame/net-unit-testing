using BaseApi.Contracts;
using BaseApi.Models;
using Mapster;

namespace BaseApi.Mappers;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
		TypeAdapterConfig.GlobalSettings.Default
            .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

        TypeAdapterConfig<User, UserResponse>.NewConfig();
    }
}
