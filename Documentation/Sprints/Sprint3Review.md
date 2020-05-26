**Sprint 3: Review**

**Datum:** 2020-05-22

**Sekreterare:** Nor

****

**Blev alla issues klara?**

Ja, alla issues blev klara i sprinten. Några issues (generic controller) var kvar men togs bort eftersom vi valde att inte implementera detta.



**Höll tidsplaneringen för issues?**

Ja, fast vissa issues tog snabbare än vad vi hade trott. När det handlar om att implementera nya saker för första gången (som vi inte har gjort hitills, HATEOAS till exempel), så tar det betydligt längre tid. Men när man har gjort det så tar det betydligt lägre tid att implementera för att vi förstår hur det fungerar och det blir liknande kod. I vissa fall kunde en issue ta en längre tid när man fastnar.



**Har det kommit upp nya saker som skall läggas till på er backlog?**

Vi ska fixa delete i MessageControllern och lägga till CRUD-funktioner till varje controller i API:t. Fundera över vad varje controller metod ska returnera (actionresult, task, objekt). Lägga till fler tester, vissa controllers saknar unit tester.



**Förbättringar till nästa sprint **

\- Researcha mer/ läsa på ytterligare innan vi börjar på ett nytt issue.

\- Bättre tydlighet och relevans för vad vi ska uppnå i sprinten.

\- Tydlighet i syfte för issues, ideér och ansvarsområden kring projektet.



**Framtidssynpunkter från Nor och Aron**

- ~~HATEOAS~~
- Strukturera om API:et
  - Kontroller i API
    - Samtliga ska ha CRUD metoder
    - Användare: Unik mejladress och användarnamn.
      - Unik constraint
    - ~~Kan ej lägga till post, message, comment like om användare ej existerar.~~
- Liknande tester för samtliga controllers.
- Generic Repository behövs tillämpas i Controller.
- Sök/filtrering i GetAll-metoder. 
  - User ?name=