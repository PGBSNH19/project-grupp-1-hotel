## Projektkrav

- Krav skrivs som features med BDD på engelska
- Al kod i ett publikt GitHub repo
- GitHub pull requests
- Confluence
- Jira

## Tekniska krav

- **.NET 5** eller .NET Core 3.1
  - **C#**
- En (eller fler) frontend
  - **Blazor Webassembly**
- En backend
  - **REST .NET 5 API**
- En databas
  - **MSSQL Azure Database**
- **GitHub Action CI/CD**
- Deployment till moln AWS eller **Azure**
- **Docker**
- **Automatiska test**
  - **Unit tests**
  - evt. Integration tests

## Projektgrund

Vad tänker ni att bygga? Börja diskutera, detta kan påverka ert tekniska design

Börja att skåpa en grund till programmet. Skåpa alla projekt och se till att få till namngivning och mapp struktur från början.

Förslag 1: Börja med ett simpelt “view” som visar ett “Hello world”, som det hämtar från ett Hello World-endpoint i ert API.

Skriv ett par automatiska test av frontend och backend.

Sätta upp Docker och Docker-compose.

Konfigurara upp GitHub actions. Börja med CI.

Få till deployment av denna Hello World.