FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
ENV ConnectionStrings__Default=Server=192.168.0.252,5433;Database=Core.Template.Demo;User Id=sa;Password=Pass@word;
ENV ConnectionStrings__Event=Server=192.168.0.252,5433;Database=Core.Template.Demo.EventStore;User Id=sa;Password=Pass@word;
ENV App__ConsulUrl=http://192.168.0.252:8500
ENV App__EventBus__ServiceName=rabbitmq-5672
ENV App__EventBus__UserName=guest
ENV App__EventBus__Password=guest
ENV App__EnableSwagger=true
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/src/Core.Template"
RUN dotnet restore  -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Core.Template.dll"]
