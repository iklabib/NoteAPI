FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build
COPY . .
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages dotnet restore
RUN dotnet publish -o output --self-contained --no-restore -r linux-x64 NoteAPI.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /build/output .
ENTRYPOINT ["dotnet", "NoteAPI.dll"]