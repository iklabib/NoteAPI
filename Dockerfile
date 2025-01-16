FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build
COPY . .
RUN dotnet publish -o output NoteAPI.csproj

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /build/output .
ENTRYPOINT ["dotnet", "NoteAPI.dll"]