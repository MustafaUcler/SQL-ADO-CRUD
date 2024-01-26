# SQL-ADO-CRUD

Instruktioner

Northwind CRUD
Er kopia av databasen skall heta Northwind2023_Förnamn_Efternamn (Ditt förnamn och efternamn)
Uppgiften är en enskild uppgift, men ni får naturligtvis lov att prata med varandra så länge som ni inte skickar in copypaste kod. Samarbete är lärorikt.
 Ni skriver egen kod och får inte kopiera varandras kod. Däremot kommer era sql-frågor i stor utsträckning se likadana ut, medan övrig kod som era menyer mm kommer att se olika ut.
Om ni arbetar med med stored procedures, views eller triggers i uppgiften sparar ni dessa som sql-filer och bifogar i inlämningen.
Uppgiften att skapa ett program med ett antal metoder som använder sig av ado.net för att köra CRUD mot Northwind-databasen.
Pröva gärna att köra både parametriserat och som genererad SQLfråga där eventuella värden läggs in via konkatenering av en sträng.
I allt ni gör så är det stränghantering i C# som kommer vara grunden. Glöm inte att testa era frågor i SSMS innan ni försöker få dem att fungera i C#
Programmet skall ha följande metoder som kan ta emot lämpliga parametrar och returvärden. De skall kunna anropas utifrån och behöver därför ta emot sin data via parametrar. Databasuppkopplingen kan skickas som en parameter eller göras inom metoden.
•	AddCustomer - Lägg till en ny kund
•	DeleteCustomer - Ta bort en kund baserat på namn eller id. Tänk overloading OBS Denna metod är klurigare än den ser ut att vara
•	UpdateEmployee - Uppdatera existerande anställd med ny adressdata
•	ShowCountrySales - Visa ordervärdet för ett valfritt land grupperat på säljare - utskrift på skärm
•	En sammansatt metod som kan lägga en ny order för en ny kund, ordern måste innehålla minst en vara.

Detta är VG-del.
Lite svårare uppgift, enkel rapportgenerator – visa upp kunders ordrar (och orderrader) baserat på vilkor.
Ni kan lugnt mata in strängar utan felhantering, det är uppgiften inte användarvänligheten som är viktig här.
Ge er även på att låta användarna välja vilka kolumner som skall visas ur tabellerna.
Därefter så låter ni dem välja sorteringsordning efter en eller flera kolumner, väljer man ingen då sker ingen sortering
Testa även att lägga till ett eller flera villkor,
samt efter vilka kolumner
Få det att funka, därefter få det att funka snyggt.
Lämna in zippad solutionmapp CRUDFörnamnEfternamn.zip samt en scriptad version av din databas – med data och alla objekt.


 
