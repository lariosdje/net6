#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Net6.Api/Net6.Api.csproj", "Net6.Api/"]
RUN dotnet restore "Net6.Api/Net6.Api.csproj"
COPY . .
WORKDIR "/src/Net6.Api"
RUN dotnet build "Net6.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Net6.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Net6.Api.dll"]