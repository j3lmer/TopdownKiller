# TopdownKiller Documentatie

## Projectomschrijving

De TopdownKiller game is een computerspel waarbij de camera gericht staat op de grond.Hierbij kan de gebruiker een speler op het scherm besturen doormiddel van de WASD knoppen of de pijltjestoetsen op het toetsenbord, ook kan de gebruiker deze speler laten draaien en richten doormiddel van de muis. Er ontstaan tegenspelers welke de speler moet verslaan doormiddel van schieten. Ook moet er willekeurig een powerup komen in de scene welke alle vijanden verwijderd. Er moet een puntensysteem aanwezig zijn in de TopdownKiller game.

--- 

## Functioneel ontwerp

### Screen flow

Menu -> gamescene -> final screen -> menu

Menu -> uitleg -> menu

Menu -> highscores -> menu

---

### Wireframe

Inbegrepen

---

### Interactieve objecten en hun werking

#### Menu:

- Start knop: Laad de game scene

- Uitleg knop: Geeft uitleg weer

- Highscores knop: Geeft highscores weer

- Quit knop: Sluit de applicatie

- Terug knoppen: zetten de highscores & de uitleg uit, en het hoofdmenu aan

#### Game scene

- Speler: WASD knoppen of de pijltjes toetsen om voort te bewegen, neemt damage wanneer geraakt door een bullet object, schiet zelf witte bullets af door middel van de linker muisknop. Aan de speler zitten 3 afvuur punten op, eerst word alleen de middelste gebruikt, bij de eerste RGBA(152, 42, 152, 255) powerup, de 2 buitensten, en bij de tweede alle 3, en worden de kogels in een driehoek afgevuurt.

- Enemy: navigeert zichzelf naar de speler toe, en blijft op een een afstand schieten. wanneer de speler te dichtbij komt gaat de enemy naar achter. schiet continu bullets af.

- Powerup: Prefab object welke verschillende werkingen/kleuren kan hebben. lijst aan soorten en kleuren is als volgt: 

- - Magenta: alle enemy objecten in het veld verwijderen.
  
  - Blauw: geeft de speler een speedboost, wanneer de maximale snelheid is bereikt spawnt deze niet meer en in plaats daarvan een punten powerup.
  
  - Groen: geeft de speler een health boost van 50 punten.
  
  - Punten: geeft de speler 50 extra punten.
  
  - Cyaan: stopt alle enemies in het scherm van bewegen, ze schieten nog wel.
  
  - Wit: geeft de speler een extra leven.
  
  - RGBA(94, 136, 220, 255): Laat de speler sneller kogels afvuren, wanneer de max is bereikt, word deze vervangen met een punten powerup.
  
  - RGBA(152, 42, 152, 255): geeft de speler een extra punt om van uit te schieten, wanneer de max is bereikt, word deze vervangen met een punten powerup.

- Bullet: interact niet met andere kogels, interact niet met enemies als een enemy hem heeft afgevuurt, interact niet met de speler als de speler hem heeft afgevuurt, interact niet met powerups. doet normaliter 10 damage, als een default boss hem heeft afgevuurt 50. despawned na 2 seconden.

- Default boss: is twee keer zo groot als een normale enemy, heeft standaard 50 levens, maar dit word meer naarmate je ze vaker tegenkomt.

#### Final screen:

- Volgende knop: elke keer dat deze geklikt word neemt deze het volgende karakter in een lijst en zet deze neer bij de geselecteerde letter.

- Vorige knop: zie vorige knop, enkel in de andere richting.

- Volgende letter knop: selecteert de volgende letter.

- Confirmatie knop: Zet alle letters aan elkaar, en gebruikt dit als speler naam, hierna update hij/genereert hij een nieuwe highscore lijst en update/genereert hij een nieuwe speler, zet de naam en score en slaat deze op. dan word het hoofdmenu weer ingeladen.

---

### Speciafieke onderdelen

##### EnemyController:

Elke functie hier spawnt 1 type vijand.

##### PowerupController:

Kiest een willekeurige powerup uit, controleert of deze nog gespawned kan worden, instantieert deze powerup, zet het type en de kleur, wacht 5 tot 20 seconden en herhaalt dit proces totdat de game is afgelopen.

#### GameController:

Zet de framerate 

Update de grote en kleine wave naar 0, zodat dit word weergegeven als Wave: 1-1 in het scherm

creert de speler

Start de wavemaster, welke een grote wave spawnt, wacht tot deze klaar is, en dan nog 5 seconden wacht en zich herhaalt, totdat de speler dood is

Een grote wave is een taak die een kleine wave ronde met 5 waves start, wacht tot die allemaal klaar zijn, hierna de kleine waves op 0 (1 op het scherm) zet en de grote waves op wat het nu is + 1.

Een kleine wave ronde neemt een hoeveelheid waves en de index van de loop van de wavemaster, hierna loopt hij over de hoeveelheid waves dat hij moest genereren, en start hier 1 kleine wave mee, hij geeft ook de index van deze loop en de loop van de gamemaster mee. wanneer alle enemies dood zijn, wacht hij nog 5 seconden, zet de kleine wave op wat het nu is + 1, en herhaalt dit totdat de hoeveelheid waves die hij meekreeg -1 zijn geweest, hierna stopt hij de powerup spawner, start hij een bosswave met de grote wave, wacht hij tot de boss(es) dood zijn, start hij de powerupspawner weer, en wacht hij 8 seconden.

Een kleine wave called een enemyspawner met als hoeveelheid enemies (WaveMasterLoopIndex +1) + (SmallWaveRoundIndex +1), en de default enemy spawner functie in de gamecontroller als argumenten.

een enemy spawner loopt enkel voor het nummer aan enemies wat het meekreeg, en executeert de meegegeven functie.

#### HighscoreManager:

Heeft 2 publieke functies: AddOrUpdateHighscore en GetHighscores

AddOrUpdate neemt een spelernaam en een speler score, zet de lokale score en naam naar deze argumenten, en schrijft dan de uitkomst van de UpdateList functie weg. hierna zet hij de lokale score en naam naar 0 en "" respectief.

De updatelist functie kijkt eerst naar of alle noodzakelijke variabelen aanwezig en gevuld zijn, mocht dit niet het geval zijn dan creert hij een nieuwe speler

Anders word de lijst aan highscores gesorteerd, en word er gezocht naar een speler met de lokale speler naam in de lijst, mocht deze niet bestaan word er een nieuwe aangemaakt. Als er wel een speler bestaat word er gekeken naar of de lokale score hoger is dan de gevonden speler score, als dit het geval is word de score geupdate naar de nieuwe score, elders blijft de huidige highscore.

De GetHighscores functie leest het highscore json bestand uit, en converteert deze naar een lijst van HighscorePlayerData, en geeft deze terug aan de roepende functie.

---

## Technisch ontwerp

#### Algemene omschrijving

TopdownKiller is een game waarbij een speler vijanden kan verslaan doormiddel van schieten

Er is een puntensysteem

Er is een hoofdmenu

#### Benodigdheden

- Een computer

- Een game engine

- Een ide

- Internet

### Projectregels

NVT

### Technische keuzes

Ik ga mijn eigen computer gebruiken met een actieve internetverbinding, Als game engine heb ik gekozen voor Unity, omdat dit vrij recht door zee is om mee te werken en ik er enige eerdere ervaring mee heb. Als IDE Heb ik gekozen voor Jetbrains Rider aangezien ik aan de Jetbrains werkomgevingen ben gewend.

---

## Plan van aanpak

### Middelen:

- Persoonlijke laptop

- Unity licentie en Editor

- Jetbrains rider Licentie en installatie

- Persoonlijk internetverbinding

### Grenzen/Limieten:

- Korte tijdsduur (2 weken)

- Tijdsbesteding aan documentatie schrijven

#### Mijlpalen:

- Werkend bewegingssysteem

- Werkend schietsysteem

- Werkend menu

- Werkend final screen

- Post processing

- Highscores inladen en updaten

- Afgeronde Game controller

### Kosten:

NVT

## Testrapporten

### Testpersoon 1:

- [x] "Speedboost is te veel"

- [x] "triple shot is wat breed"

- [x] tegenaangelopen dat speedboost en extra afvuurpunten powerups blijven spawnen ookal doen ze niks meer (sneller afvuren ook)

- [x] "wat te makkelijk"

- [x] ok knop in de finalscreen doet het niet in build

- [x] "mis geluid"

- [x] punt powerups zijn groen

### Testpersoon 2:

- [ ] "Voeg muziek toe"

- [x] "voeg bosses toe"

- [x] "voeg geluidjes voor powerups toe"

- [ ] "voeg een muntensysteem toe"

### Testpersoon 3:

- [ ] "Bosses zijn wat te zwak"

- [ ] "mist muziek"

- [ ] "Als je diagonaal gaat ga je sneller"

## Versiebeheer:

[Commits · j3lmer/TopdownKiller · GitHub](https://github.com/j3lmer/TopdownKiller/commits/main)
