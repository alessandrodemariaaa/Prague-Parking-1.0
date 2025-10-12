# Prague Parking 1.0

Textbaserat system för hantering av en parkeringsplats i Prag (valet parking).

## Funktioner
- Parkera bil eller MC
- Flytta fordon mellan platser
- Hämta ut fordon
- Sök efter registreringsnummer
- Textbaserat gränssnitt

## Tekniskt
- C# Console Application
- Parkeringsrutorna hanteras som array[100] av strängar
- Ingen filhantering – all data rensas när programmet avslutas

## Körinstruktioner
Öppna lösningen i Visual Studio. Tryck **Ctrl + F5** för att köra programmet.


LOGGBOK

2025-09-25
Började projektet. Jag läste igenom uppgiften och försökte förstå kraven. Jag var lite förvirrad kring arrayen med 100 platser, men jag tror jag fattar att det är som en lista där varje ruta är en plats.

2025-09-27
Skapade själva programfilen och lade in menyn. Jag hade problem med att förstå switch-case men testade och det funkade till slut. Jag lyckades få fram en meny som loopar.

2025-09-29
Testade att lägga till funktion för att parkera fordon. Först skrev jag fel så att det blev "index out of range". Jag lärde mig att man måste tänka på att datorn räknar från 0 medan människor räknar från 1.

2025-09-30
Gjorde funktionen för att söka fordon. Det var ganska svårt för jag visste inte hur man skulle kolla igenom hela arrayen. Men med en for-loop gick det bra. Jag skrev ut att bilen finns på en plats.

2025-10-01
Jobbade med att ta bort fordon. Det var lite knepigt att förstå hur man gör platsen tom igen, men till slut fattade jag att man bara sätter den till null. Jag lade också till att man ser hur länge fordonet stått parkerat. 
Det kändes kul!

2025-10-02
Provade att flytta ett fordon. Jag fick fel först när platsen redan var upptagen. Jag löste det genom att lägga till en kontroll. Nu funkar det men det känns fortfarande lite svårt.

2025-10-03
Fixade visualisering av parkeringen. Jag gjorde det enkelt med utskrifter av alla platser. Det blev lite tråkigt i konsolen, men det funkar. Jag tycker det är häftigt att man kan se alla platser i listan.

2025-10-04
Jobbade med validering av registreringsnummer. Jag fick många fel när jag skrev mellanslag eller för många tecken. Men nu kastas det fel och jag lärde mig om try/catch.
