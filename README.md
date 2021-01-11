<h1> Hotel Web Template</h1>
<h2>Kurs Producera och leverera mjukvara</h2>

![Development CI/CD](https://github.com/PGBSNH19/project-grupp-1-hotel/workflows/Development%20CI/CD/badge.svg?branch=development)

<h2>Grupp 1</h2>

Medverkande i grupp 1 är Aron Cederlund, Ted Henriksson, Pierre Nygård, Samir Ehsani, Fredrika Eriksson, Mirko Pralica, Adam Martinsson, Dimitris Nasis, Gita Frankus, Johan Käll, Shipra Sharma och Julian Rzodkiewicz

<h2>Tanken bakom projektet</h2>

**Hotel Web Template** är en webbplats där man kan boka och avboka rum på ett hotel. Man ska kunna välja antal gäster, datum för in- och utcheckning,  vilka rum som ska bokas samt tillval till rummet och bokningen överlag såsom frukost, spa tillgång med mera. 

Man ska också kunna läsa och skriva recensioner för hotellet och hotellets restaurang. 

Fokus under projektet ligger på funktionalitet och säkerhet för hotellbokning och avbokninga, samt dataintegritet och åtminstone en grundläggande design för Appen.

<h2>Tech stack</h2>

Systemets Frontend byggs i **Blazor WebAssembly**. Backend Server är ett **.NET 5.0** API och all data lagras i en **relationsdatabas på Azure**.

<h2>Branching strategi</h2>
För utveckling och test staging använder vi en branch Development. Commits till Development triggar en CI/CD pipeline som updaterar en development live server av applikationen. 

Vid slutet av Sprints, eller vid överrenskommelse från gruppen mergas Development in till Main. 

Branches är namngivna antingen efter Jira story eller, om tillgängligt, sub tasks ifrån storyn.  Pull Requests mot Development skall ha samtliga utvecklare som reviewers och givet att det inte finns Change Requests så behöver den vara godkänd av 2 personer innan merge.

<h2>Användning av projektet</h2>

För att starta upp projektet lokalt så behöver man bygga upp en appsettings.Development.json med ConnectionStrings och DefaultConnection riktad mot en Sql Server. Denna fil ska ligga inuti Hotel.Server.

Stega in i Hotel.Server/ och gör en migration. Uppdatera sedan databasen med <a href="https://docs.microsoft.com/en-us/ef/core/cli/dotnet">Ef tools</a>.

Ifrån solution, välj multiple startup projects (Hotel.Server, Hotel.Client) och kör igång!

<h2>Api dokumentation</h2>

Swagger används som dokumentation av vårat server API.

Dokumentationen går att nå genom att bygga och köra API:et i en docker container i development mode. Första steget är att ställa sig i rotmappen för projektet och bygga en image genom detta kommando:

```powershell
docker build -t hotelapi ./Hotel.Server
```

Därefter köra containern med följande kommando.

```powershell
docker run -d -p 8080:80 --name hotelapi --env ASPNETCORE_ENVIRONMENT=Development hotelapi
```

När det är gjort ska det gå att nå dokumentationen på: 
http://localhost:8080/swagger/index.html

<h2>Övriga resurser och länkar</h2>
<h2>Resurser</h2>
* <a href="Documentation/BDD.md">Länk till BDD dokument</a>
* <a href="https://plushogskolan.atlassian.net/jira/software/c/projects/G1/pages">Övrig dokumentation på Confluence</a>
* <a href="https://pgbsnh19.github.io/course-producera-leverera/assignments/project">Länk till projekt</a>
