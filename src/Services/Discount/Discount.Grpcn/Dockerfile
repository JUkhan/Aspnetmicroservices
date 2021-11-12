#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Discount/Discount.Grpcn/Discount.Grpcn.csproj", "Services/Discount/Discount.Grpcn/"]
RUN dotnet restore "Services/Discount/Discount.Grpcn/Discount.Grpcn.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Discount.Grpcn"
RUN dotnet build "Discount.Grpcn.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.Grpcn.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Grpcn.dll"]
