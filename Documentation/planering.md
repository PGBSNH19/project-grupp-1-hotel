# Planering Hotel projekt

## Appen

Börjar designas som mobile first, den ska vara mobil och PC vänlig. 

Meny överst, och en ingång till bokning tidigt på sidan. Längre ner på sidan bilder, betyg, texter, rekkomendationer etc, här kan man vara kreativ.

### Utgångspunkt (Mall)
https://www.elite.se/

### Device Breakpoints

Systemet byggs anpassat för en minsta storlek (mobile):

Min-device-width: 360px

### Länkar i menyn
* Bokning
* Avbokning
* Boka konferensrum [disabled]
* Resturang -> Resturangsida med meny
* Vårat spa

## Tekniska krav

- .NET Version 
- C# 5.0 
- Blazor
- Backend Framework API MVC
- REST
- T-SQL
- GitHub Action CI/CD
- Deployment till Azure
- Docker
- Automatiska tester
- Unit tests

## Projektkrav

- Ett SRS dokument på engelska (md) Påbörjat :ballot_box_with_check:
- Krav skrivs som features med BDD på engelska (Påbörjat) :ballot_box_with_check:
- Al kod i ett publikt GitHub repo :ballot_box_with_check:
- GitHub pull requests :ballot_box_with_check:
- Confluenece :ballot_box_with_check:
- Jira :ballot_box_with_check:

## Data Models

### Booking
```
Id [ PK ]
IsCanceled [bool]
BookingNumber [string]
Created = Datetime.Now
StartDate [DateTime]
EndDate [DateTime]
FirstName [string]
LastName [string]
Email [string]
PhoneNumber [string]
Address [string]
Guests [int] = 1
Breakfast [bool]
Dinner [bool]
SpaAccess [bool]
TransportFrom [bool]
TransportTo [bool]

Rooms [FK, ICollection]
```

### Invoice
```
Id [PK]
IsCanceled [Bool]
Total [int]
BookingId [FK]
```

### Room
```
Id [PK]
Beds [int] 
DoubleBeds [int]
IsCondo [bool]
IsSuite [bool]
Smoking [bool]
Pets [bool]
```

### BreakfastReview

```
Created = DateTime.Now
Description
Grade (1,5) [HttpGet] > 4
Author (optional)
(ImageUrl)
```

### HotelReview
```
Created = DateTime.Now
Description
Grade (1,5) [HttpGet] > 4
Author (optional)
(ImageUrl)
```