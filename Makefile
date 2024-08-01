build:
	dotnet build UnitTesting.sln

test:
	dotnet test UnitTesting.sln

run-api:
	dotnet run --project ./src/BaseApi/BaseApi.csproj