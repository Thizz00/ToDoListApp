# ToDoList - Aplikacja do zarządzania zadaniami
## 📋 Opis
**ToDoList** to nowoczesna aplikacja webowa do zarządzania zadaniami, umożliwiająca efektywne śledzenie, organizowanie i priorytetyzowanie zadań. Aplikacja została zbudowana z wykorzystaniem **ASP.NET Core MVC** i bazy danych **MySQL**, oferując intuicyjny interfejs użytkownika z dynamicznymi elementami.

## 🛠️ Technologie
### Backend
**ASP.NET Core 9.0** - framework do budowania aplikacji webowych

**Entity Framework Core 5.0.17** - ORM do komunikacji z bazą danych

**Pomelo.EntityFrameworkCore.MySql** - provider do obsługi MySQL

**xUnit** - framework do testów jednostkowych

**Moq** - biblioteka do mockowania obiektów w testach

## Frontend
**ASP.NET Razor** - silnik widoków do generowania HTML

**Bootstrap 5** - framework CSS dla responsywnego interfejsu

**Font Awesome** - biblioteka ikon

**Animate.css** - biblioteka do animacji elementów

## Baza danych
**MySQL 8.0** - system zarządzania bazami danych

## Narzędzia
**Docker** - konteneryzacja aplikacji

**Docker Compose** - orkiestracja kontenerów

**Entity Framework Migrations** - zarządzanie schematem bazy danych

## ✨ Funkcjonalności
- Zarządzanie zadaniami - dodawanie, przeglądanie, edycja i usuwanie zadań

- Statusy zadań - oznaczanie zadań jako ukończone/nieukończone

- Priorytety - przypisywanie niskiego, średniego lub wysokiego priorytetu

- Terminy - ustawianie i śledzenie terminów wykonania zadań

- Powiadomienia wizualne - oznaczenia kolorystyczne dla przeterminowanych i zbliżających się terminów

- Filtrowanie zadań - podział na zadania aktywne i ukończone

- Responsywny interfejs - dostosowanie do różnych urządzeń

## 🚀 Uruchomienie projektu

Sklonuj repozytorium:

```bash
git clone https://github.com/Thizz00/ToDoList.git
cd ToDoList
```

Uruchom aplikację za pomocą **Docker Compose**:

```bash
docker-compose up --build
```

Otwórz przeglądarkę i przejdź do adresu:

```bash
http://localhost:8080
```

## 🧪 Testy
Aplikacja zawiera testy jednostkowe napisane przy użyciu **xUnit**:

```bash
dotnet test ToDoList.Tests
```