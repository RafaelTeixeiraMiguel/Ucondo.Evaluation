# Acesse https://aka.ms/customizeContainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ucondo.Evaluation/Ucondo.Evaluation.API.csproj", "Ucondo.Evaluation/"]
COPY ["Ucondo.Evaluation.Application/Ucondo.Evaluation.Application.csproj", "Ucondo.Evaluation.Application/"]
COPY ["Ucondo.Evaluation.Common/Ucondo.Evaluation.Common.csproj", "Ucondo.Evaluation.Common/"]
COPY ["Ucondo.Evaluation.Domain/Ucondo.Evaluation.Domain.csproj", "Ucondo.Evaluation.Domain/"]
COPY ["Ucondo.Evaluation.IoC/Ucondo.Evaluation.IoC.csproj", "Ucondo.Evaluation.IoC/"]
COPY ["Ucondo.Evaluation.ORM/Ucondo.Evaluation.ORM.csproj", "Ucondo.Evaluation.ORM/"]
RUN dotnet restore "Ucondo.Evaluation/Ucondo.Evaluation.API.csproj"
COPY . .
WORKDIR "/src/Ucondo.Evaluation"
RUN dotnet build "./Ucondo.Evaluation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ucondo.Evaluation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ucondo.Evaluation.API.dll"]