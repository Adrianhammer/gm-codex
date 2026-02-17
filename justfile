# Root task runner (https://github.com/casey/just)

run *args:
    dotnet run --project gm_codex.Presentation -- {{args}}

build:
    dotnet build

build-release:
    dotnet build -c Release

test *args:
    dotnet test

pack:
    dotnet pack gm_codex.Presentation/gm_codex.Presentation.csproj -c Release

clean:
    dotnet clean
