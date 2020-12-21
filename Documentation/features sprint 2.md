* [G2-01] User should be able to create RestaurantReviews
  * 

* [G2-02] User should be able to create HotelReviews
  * 

## Features vi vill ha med

Emailbekräftelse (Backend)

* Utveckla BookingService CreateAsync för att skicka ett e-mail som del av transaktionen. 

Komponent i frontend för att posta nya reviews

* Innehållsfält
* Betyg 1-5
* Checkbox för anonymt namn
* BokningsNummer (required)
* Använder endpoint: ```api/v1.0/review/```

Resturangsida

* Menu
* Bilder

Snitt och urval av Hotelreviews på startsidan 

* Plocka ut 3 reviews till startsidan där betyget är godtyckligt högt. En review visas i taget i en karussell. 
* Visa snittbetyg på startsidan
* Använder endpoint: ```api/v1.0/review/average```
* Använder endpoint: ```api/v1.0/review/top```

Möjlighet att avboka genom att ange BokningsNummer och sedan bekräfta med Emailaddress

* Navmenyn håller ett textfält för att avboka med BokningsNummer 
* Navigerar till booking/cancel/{bookingNumber}

Bygg upp en sida för att avbeställa bokning. Tar en parameter ```BookingNumber```

* Hämta bokning via endpoint: ```/api/v1.0/Booking/{bookingNumber}```
* Visa hämtad bokning eller visa inga resultat
* Vid hämtad bokning ge alternativ att avboka med registrerad Epost address, använder endpoint: ```/api/v1.0/Booking/{bookingNumber}/cancel/{email}```

"Toast" Notifikationer på webbsidan om: 

* Bygga upp ToastService

* Vi inte hittar lediga rum i sökningen skall en notifikation ges
* Vi inte lyckas fullborda en rumsbokning
* Vi inte hittar en bokning med bokningsnummer vid avbokning
* Vi inte lycksa fullborda en avbokning

