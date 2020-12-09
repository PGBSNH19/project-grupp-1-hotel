# Planering Hotel projekt

## Appen

Börjar designas som mobile first, meny överst, och en ingång till bokning tidigt på sidan. 
Längre ner på sidan bilder, betyg, texter, rekkomendationer etc, här kan man vara kreativ.

### Länkar i menyn
* Bokning
* Avbokning
* Boka konferensrum [disabled]
* Resturang -> Resturangsida med meny
* Vårat spa

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
(ImageUrl)
```

### HotelReview
```
Created = DateTime.Now
Description
Grade (1,5) [HttpGet] > 4
(ImageUrl)
```