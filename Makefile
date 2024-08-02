ifeq ($(OS),Windows_NT)
    OPEN_CMD := start
else
    UNAME_S := $(shell uname -s)
    ifeq ($(UNAME_S),Linux)
        OPEN_CMD := xdg-open
    endif
    ifeq ($(UNAME_S),Darwin)
        OPEN_CMD := open
    endif
endif

build:
	dotnet build UnitTesting.sln

test:
	dotnet test UnitTesting.sln

# install coverlet to execute the command below
# dotnet tool install --global coverlet.console
# coverlet ./tests/BaseApi.Tests.Unit/bin/Debug/net8.0/BaseApi.Tests.Unit.dll --target "dotnet" --targetargs "test --no-build" 
test-coverage-generate:
	dotnet test ./tests/BaseApi.Tests.Unit/BaseApi.Tests.Unit.csproj \
		/p:CollectCoverage=true \
		/p:CoverletOutputFormat=cobertura \
		/p:Exclude="[BaseApi]BaseApi.Data.*%2C[BaseApi]BaseApi.Repositories.*%2C[BaseApi]BaseApi.Endpoints.*%2C[BaseApi]BaseApi.Logging.*"

test-coverage-report:
	reportgenerator "-reports:./tests/BaseApi.Tests.Unit/coverage.cobertura.xml" \
		"-targetdir:./tests/BaseApi.Tests.Unit/coverage-report" -reporttypes:Html
	$(OPEN_CMD) ./tests/BaseApi.Tests.Unit/coverage-report/index.html

run-api:
	dotnet run --project ./src/BaseApi/BaseApi.csproj