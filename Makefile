build:
	dotnet build UnitTesting.sln

test:
	dotnet test UnitTesting.sln

test-api-coverage:
	dotnet test ./tests/BaseApi.Tests.Unit/BaseApi.Tests.Unit.csproj --collect "XPlat Code Coverage" /p:CollectCoverage=true;Exclude="[*]BaseApi.Repositories,[*]BaseApi.Endpoints"
	# install coverlet to execute the command below
	# dotnet tool install --global coverlet.console
	# coverlet ./tests/BaseApi.Tests.Unit/bin/Debug/net8.0/BaseApi.Tests.Unit.dll --target "dotnet" --targetargs "test --no-build"

run-api:
	dotnet run --project ./src/BaseApi/BaseApi.csproj