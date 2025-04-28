FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ToDoList/*.csproj", "ToDoList/"]
COPY ["ToDoList.Tests/*.csproj", "ToDoList.Tests/"]
RUN dotnet restore "ToDoList/ToDoList.csproj"
RUN dotnet restore "ToDoList.Tests/ToDoList.Tests.csproj"

COPY . .
RUN dotnet build "ToDoList/ToDoList.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR /src
RUN dotnet test "ToDoList.Tests/ToDoList.Tests.csproj"  --verbosity normal

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

FROM test AS publish
WORKDIR /src
RUN dotnet publish "ToDoList/ToDoList.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoList.dll"]
