FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ToDoList/*.csproj", "ToDoList/"]
RUN dotnet restore "ToDoList/ToDoList.csproj"

COPY . .

<<<<<<< HEAD
RUN dotnet build "ToDoList/ToDoList.csproj" -c Release -o /app/build
RUN dotnet build "ToDoList.Tests/ToDoList.Tests.csproj" -c Release -o /app/build-tests

RUN dotnet test "ToDoList.Tests/ToDoList.Tests.csproj" --no-restore --no-build --logger:"trx;LogFileName=TestResults.trx"

=======
>>>>>>> 1da1463c5f953b75878aa04e17a4d69fcba3d838
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

FROM build AS publish
WORKDIR /src
RUN dotnet publish "ToDoList/ToDoList.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoList.dll"]
