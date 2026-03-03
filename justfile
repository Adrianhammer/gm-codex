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

install:
    dotnet tool uninstall --global gm-codex >/dev/null 2>&1 || true
    dotnet pack -c Release
    dotnet tool install --global \
      --add-source ./gm_codex.Presentation/nupkg \
      gm-codex \
      --version 1.0.0