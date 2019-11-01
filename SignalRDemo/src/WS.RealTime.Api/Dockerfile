#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["WS.RealTime.Api/WS.RealTime.Api.csproj", "WS.RealTime.Api/"]
RUN dotnet restore "WS.RealTime.Api/WS.RealTime.Api.csproj"
COPY . .
WORKDIR "/src/WS.RealTime.Api"
RUN dotnet build "WS.RealTime.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WS.RealTime.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WS.RealTime.Api.dll"]
