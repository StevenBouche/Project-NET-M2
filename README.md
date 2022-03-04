# Project-NET-M2

Notre projet est décomposer en 3 composants de la stack .NET :

- ASP.Net Rest API (.NET 6) avec SQLServer
- Web UI Blazor (.NET 6) 
- Client UI WPF (.NET 6) 

Il est nécessaire de start l'API en premier avant le Client et la WebUI.

## API Rest

### Prerequisites

- Docker et Docker-compose (Docker-Desktop pour windows)

### Start app

- Visual Studio : définir docker-compose en tant que projet de démarrage et Run 
- CLI :
    - A la racine du répertoire "Project-NET-M2"
    - docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

- Swagger : http://localhost:8080/swagger ou https://localhost:8081/swagger
- Api accessible via : http://localhost:8080/api ou https://localhost:8081/api

### Fonctionnalités (Documentation : https://localhost:8081/swagger)

Book API :

- GET Book via Id avec son contenu
- GET Recherche book avec pagination, filtre par titre/genre/autheur et sorting sur les champs
- POST Création d'un book
- PUT Mise à jour d'un book 
- DELETE Suppression d'un book et de ses liens avec les genres

Genre API :

- GET Genre via Id
- GET Tous les genres
- GET Tous les livres d'un genre via Id
- POST Création d'un genre
- Delete Suppression d'un genre et de ses liens avec les books

Notification par Websocket :

- Possibilité de se connecter en websocket pour les clients (Client UI et Web UI) afin de recevoir des notifications quand un nouveau livre est disponible ou mise à jour.

## Web UI 

### Start app

- Visual Studio : définir le projet "LibraryProject.WebUI" en tant que projet de démarrage et Run
 
- CLI :
    - Installer le Runtime : https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.2-windows-x64-installer
    - cd LibraryProject.WebUI
    - dotnet run (dotnet watch run pour le hot reload)

Accessible via : https://localhost/library

### Fonctionnalités

- Ajouter des livres dans la bibliothèque
- Mettre à jour des livres de la bibliothèque
- Supprimer des livres de la bibliothèque
- Consulter la liste de tous les livres, avec de la pagination, filtre et tri
- Consulter le contenu d'un livre
- Consulter la liste de tous les genres
- Ajouter de nouveaux genres

## Client UI

- Visual Studio : définir le projet "LibraryProject.ClientUI" en tant que projet de démarrage et Run 
- CLI :
    - cd LibraryProject.ClientUI
    - dotnet build --configuration Release
    - bin/Release/net6.0-windows/LibraryProject.ClientUI.exe

### Fonctionnalités

- Lister les livres (scrolling)
- Afficher les détails d’un livre
- Lire un livre
- Lister tous les genres
- Afficher les livres d’un genre (scrolling)
- Notification quand un nouveau livre est disponible dans la bibliothèque
