# Product Management Backend

Dette er Product Mangement Backend / API.

Dette API tilbyder funktionalitet til Product Management Frontend til blandt andet at give overblik over produkter, g�r det muligt at oprette, �ndre og filtrere i produkter.

APIet bruger basepath `[hostname]/api/`

## Funktioner

- **Hent produkter:** Se alle produkter
- **Hent produkt:** Se produkt ud fra id
- **Pagination:** Se 10 produkter per side
- **Oprette produkt:** Opret produkt
- **Redigere produkt:** Rediger produkt
- **Slet produkt:** Slet produkt
- **Filtrere produkter:** Filtrer produktlisten

## Teknologi

- **Frontend**: Next.js
- **Backend**: C# .NET
- **Database**: MsSQL
- **Deployment**: Docker

## Projekt Ops�tning

### Lokalt

1. **Klon Repositoriet**

    ```
    git clone https://github.com/emilstorgaard/ProductManagementBackend.git
    ```

2. **change directory to the project folder**

    ```
    cd ProductManagementBackend
    ```


3. **Init database**

    1. Hvis du vil k�re projektet lokalt, skal du f�rst udfylde `DefaultConnection` i appsettings med en MsSQL connection string.
    2. Nu skal du updatere din MsSQL med kommandoen`update-database`

### Docker

For at bygge og k�re projektet ved hj�lp af Docker, f�lg disse trin:

1. **Byg Docker Image**

    ```
    docker build -t product_management_backend .
    ```

2. **K�r Containeren**

    ```
    docker run -p 8080:8080 product_management_backend
    ```

    Herefter er applikationen tilg�ngelig p� `localhost:8080`.

## Endpoints
### Products

**GET** `/Products` - Henter alle produkter.

**GET** `/Products/{id}` - Henter produkt ud fra id.

**GET** `/Products/page?page={int}` - Henter max 10 produkter.

**POST** `/Products` - Opretter et nyt produkt.

**PUT** `/Products/{id}` - Opdaterer produkt ud fra id.

**DELETE** `/Products/{id}` - Sletter produkt ud fra id.

� Emil Storgaard Andersen, 2024.