<h1>Dokumentation</h1>

Dokumentationen fanns samlad på en plats i GitHub men den var stundtals förvirrande. I **Planering.md** skrivs det att  man ska använda T-SQL och i **ProjectRequierments.md** står det att man ska använda MongoDB. Men det var en SQL databas i slutändan. Och vi trodde länge att det inte fanns någon branching strategi, men den var inbakat i ett annat dokument och inte under någon tydlig rubrik.

Swagger var också väldokumenterad.

 I övrigt fanns all dokumentationen.



<h1>Beskrivning av hur det gick att få lösningen att köra lokalt</h1>

Det var svårt att få igång inledningsvis. I README.md fanns en rubrik "Användning av projektet" men den var inte ifylld. Men när vi väl förstod så gick det fort.

Vi skapade en lokal SQL-databas med innehåll och alla endpoints funkade i swagger. Men innan det fanns någon databas crasha och krångla applikationen istället för att ge ett felmeddelande ( = lite svårare att ta över koden för nästa utvecklare när det inte fanns några migrationer).



<h1>Infrastruktur</h1>

Miljöer? Blazor frontend - Restfull api Azure, Docker, Jira, confluence, 

CI/CD deployas enbart på development branchen.

<h1>Branching och merging strategi</h1>

Development branch instead of using main as merge branch

Main branch is updated during sprint endings and on proposal

During pull requests Frontend and Backend team handle their own requests

Always add all members to pull requests