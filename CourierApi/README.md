# Tworzenie aplikacji webowych z wykorzystaniem .NET

## Wprowadzenie

Witaj! To jest repozytorium z materiałami do projektu w ramach zajęć **Tworzenie aplikacji webowych z wykorzystaniem .NET** na Politechnice Warszawskiej.

Do rozpoczęcia tego kursu potrzebujesz następujących rzeczy:

1. [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).
2. Sklonuj repozytorium  git repository from GitHub.

## Sklonuj repozytorium Git

```
git clone https://github.com/sulmar/pw-mini-winter2023-net
```

## Podstawy

### Komendy CLI

#### Środowisko
- ``` dotnet --version ``` - wyświetlenie aktualnie używanej wersji SDK
- ``` dotnet --list-sdks ``` - wyświetlenie listy zainstalowanych SDK
- ``` dotnet new globaljson ``` - utworzenie pliku _global.json_
- ``` dotnet new globaljson --sdk-version {version} ``` - utworzenie pliku _global.json_ i ustawienie wersji SDK
- ``` dotnet new gitignore ``` - utworzenie pliku _.gitignore_

#### Rozwiązanie
- ``` dotnet new sln ``` - utworzenie nowego rozwiązania
- ``` dotnet new sln --name {name} ``` - utworzenie nowego rozwiązania o określonej nazwie
- ``` dotnet sln add {folder}``` - dodanie projektu z folderu do rozwiązania
- ``` dotnet sln remove {folder}``` - usunięcie projektu z folderu z rozwiązania
- ``` dotnet sln add {project.csproj}``` - dodanie projektu do rozwiązania
- ``` dotnet sln remove {project.csproj}``` - usunięcie projektu z rozwiązania
- 
#### Projekt
- ``` dotnet new --list ``` - wyświetlenie listy dostępnych szablonów
- ``` dotnet new {template} ``` - utworzenie nowego projektu na podstawie wybranego szablonu, np. console, web
- ``` dotnet new {template} -o {output} ``` - utworzenie nowego projektu w podanym katalogu
- ``` dotnet restore ``` - pobranie pakietów nuget powiązanych z projektem
- ``` dotnet build ``` - kompilacja projektu
- ``` dotnet run ``` - uruchomienie projektu
- ``` dotnet watch run ``` - uruchomienie projektu w trybie śledzenia zmian
- ``` dotnet run {app.dll}``` - uruchomienie aplikacji
- ``` dotnet test ``` - uruchomienie testów jednostkowych
- ``` dotnet watch test ``` - uruchomienie testów jednostkowych w trybie śledzenia zmian
- ``` dotnet add {project.csproj} reference {library.csproj} ``` - dodanie odwołania do biblioteki
- ``` dotnet remove {project.csproj} reference {library.csproj} ``` - usunięcie odwołania do biblioteki
- ``` dotnet clean ``` - wyczyszczenie wyniku kompilacji, czyli zawartości folderu pośredniego _obj_ oraz folderu końcowego _bin_

#### Pakiety
- ``` dotnet add package {package name}``` - dodanie pakietu do projektu
- ``` dotnet list package ``` - wyświetlenie listy pakietów
- ``` dotnet remove package {package name}``` - usunięcie pakietu z projektu

#### Entity Framework Core
- ``` dotnet ef ``` - weryfikacja instalacji
- ``` dotnet ef migrations add {migration} ``` - dodanie migracji
- ``` dotnet ef migrations remove ``` - usunięcie ostatniej migracji
- ``` dotnet ef migrations list ``` - wyświetlenie listy wszystkich migracji
- ``` dotnet ef migrations script ``` - wygenerowanie skryptu do aktualizacji bazy danych do najnowszej wersji
- ``` dotnet ef database update ``` - aktualizacja bazy danych do najnowszej wersji
- ``` dotnet ef database update -verbose ``` - aktualizacja bazy danych do najnowszej wersji i wyświetlanie logu
- ``` dotnet ef database update {migration} ``` - aktualizacja bazy danych do podanej migracji
- ``` dotnet ef database drop ``` - usunięcie bazy danych
- ``` dotnet ef dbcontext info ``` - wyświetlenie informacji o DbContext (provider, nazwa bazy danych, źródło)
- ``` dotnet ef dbcontext list ``` - wyświetlenie listy DbContextów
- ``` dotnet ef dbcontext scaffold {connectionstring} Microsoft.EntityFrameworkCore.SqlServer -o Models ``` - wygenerowanie modelu na podstawie bazy danych

## Protokół HTTP 

~~~
curl -v https://ww2.mini.pw.edu.pl
~~~

- Request:
~~~
  GET /customers/index.html HTTP/1.1
  Host: ww2.mini.pw.edu.pl
  Accept: text/html
  {blank-line}
~~~

- Response:
~~~
  200 OK
  Content-Type: text/html
  
  <html>...</html>
~~~

- Request:
~~~ 
 POST / HTTP/1.1
  host: ww2.mini.pw.edu.pl
  content-type: application/xml
  <xml><customer>...</customer></xml>
  {blank-line}
~~~

- Response:
~~~
201 Created
~~~

## REST API
### Akcje
| Akcja  | Opis                  |
|--------|-----------------------|
| GET    | Pobierz zasób         |
| POST   | Utwórz zasób          |
| PUT    | Zmodyfikuj zasób      |
| PATCH  | Zmodyfikuj częściowo  |
| DELETE | Usuń                  |
| HEAD   | Pobierz nagłówek      |

### Kody Statusów
| Kod statusu   | Akcja                  | Opis                                              | 
|---------------|------------------------|---------------------------------------------------| 
| 200           | OK                     | Żądanie się powiodło                              |
| 201           | Created                | Zasób został utworzony                            |
| 202           | Accepted               | Żądanie został przyjęte do przetwarzania          |
| 204           | No Content             | Żądanie się powiodło ale nie ma nic w odpowiedzi  |
| 301           | Moved Permanently      | Zasób został przeniesiony                         |
| 400           | Bad Request            | Błędne żądanie                                    |
| 401           | Unauthorized           | Brak poświadczeń do zasobu                        |
| 403           | Forbidden              | Brak dostępu do zasobu                            |
| 403           | Not Found              | Zasób nie znaleziony                              |
| 415           | Unsupported Media Type | Nieobsługiwany format                             |
| 500           | Internal Server Error  | Wewnętrzny błąd serwera                           |


### Adresy
- ``` /api/customers ``` 
- ``` /api/customers/10 ``` 
- ``` /api/customers/10/orders ``` 
- ``` /api/orders/2023 ``` 
- ``` /api/orders/2023/10 ``` 



