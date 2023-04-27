# Documentation de Projet AlimoBio

+Cachiers des recettes :
 
[Télécharger le cachier des recettes en cliquant sur ce lien ](https://github.com/Ismail-Mouyahada/Project-Individuel-AlimBio/files/11341774/Cachiers-recettes.xlsx)

# Modelisation de projet :


### UML :
+  UML | Cas d'usage : 
[UML-Cas-Usage-Alimbio.pdf](https://github.com/Ismail-Mouyahada/Project-Individuel-AlimBio/files/11341306/UML-Cas-Usage-Alimbio.pdf)

+ UML | Cas d'activité : 
[UML-Cas-Activités-Alimbio.pdf](https://github.com/Ismail-Mouyahada/Project-Individuel-AlimBio/files/11341320/UML-Cas-Activites-Alimbio.pdf)

+ UML | Cas des Classes : 
[UML-Cas-Classes-Alimbio.pdf](https://github.com/Ismail-Mouyahada/Project-Individuel-AlimBio/files/11341322/UML-Cas-Classes-Alimbio.pdf)


### MCD
+ MCD | Model conceptuel des données :
 
![MCD-alimobio](https://user-images.githubusercontent.com/66369128/234806491-7e6fc68f-75b1-489a-9295-1681c9abcb50.png)

## Vue de projet :
![chrome-capture-2023-3-23](https://user-images.githubusercontent.com/66369128/234797510-157b27cc-845b-411d-8fe4-6b8d2679b932.gif)



**Projet d’une application de type annuaire d’entreprise en [A](http://Asp.net)SP.net core 7 avec un API (Bonus)**

## **Introduction**

**AlimoBio Annuaire** est une application web créé intégralement en C# a l’aide de la technologie [asp.net](http://asp.net) core 7. ce projet est réalisé dans le but de aider les utilisateurs ou les visiteurs de consulter les données des salariés et pouvoir les contacter facilement, dans ce contexte il est important de souligner que le but de créer cette application est purement communicatif. mais aussi Marketing de l’entreprise.

## Installation

- Installation des prerequis
    - vous douverez installer les independance suivantes :
        - Miscrosoft [ASP.net](http://ASP.net) core v7.0     : [https://dotnet.microsoft.com/en-us/download/dotnet/7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
        - Miscrosoft [AS](http://ASP.net)P.net core tools  :
        - Pour windows
            
            ```jsx
            dotnet tool install dotnetsay --tool-path c:\dotnet-tools
            ```
            
            Pour Mac ou linux :
            
            ```jsx
            dotnet tool install dotnetsay --tool-path ~/bin
            ```
            
            - Maintenant vous devriez avoir accès à deux dotnet CLI
            
            ```jsx
            **> dotnet** 
            > dotnet watch
            > dotnet run
            > dotnet build
            ```
            
            ```jsx
            > **dotnet tools** 
            > dotnet ef migration add   <Nom de la migration> /*ajouter des migrations*/
            > dotnet ef database update  /* importer les migrations vers la DB */
            > dotnet ef database drop /* supprimer tous les tableaux de la DB */
            ```
            
            Si vos installations se sont bien passé à présent vous êtes prêt pour cloner et lancer le projet. 
            
            # Copier le projet Github d’AlimoBio
            
            Clonez le projet depuis Github : https://github.com/Ismail-Mouyahada/Project-Individuel-AlimBio.git
            
            - ou tapez la commande suivante :
            
            ```jsx
            git clone https://github.com/Ismail-Mouyahada/Project-Individuel-AlimBio.git
            ```
            
            Naviguez vers le chemin de projet dans votre CLI.
            
            ## Configuration de base de donnée Base des données MSQL mariaDB
            
            Installation de de la base de données dans le cas ou celle ci n’est pas présente sur votre systéme d’exploitation ou de virtualisation:
            
            - Pour windows :
            
            ```jsx
            [https://dev.mysql.com/downloads/installer/](https://dev.mysql.com/downloads/installer/)
            ou
            [https://www.wampserver.com/en/#wampserver-64-bits-php-5-6-25-php-7](https://www.wampserver.com/en/#wampserver-64-bits-php-5-6-25-php-7)
            ```
            
             
            
            - Pour linux ou mac :
            
            ```jsx
            [https://www.editions-eni.fr/open/mediabook.aspx?idR=a7ff8432c574a7288c54d6351745dab9](https://www.editions-eni.fr/open/mediabook.aspx?idR=a7ff8432c574a7288c54d6351745dab9)
            ```
            
            - Dans le cas ou vous avez déjà MySQL sur votre machine :
            
            naviguez dans le route de projet et chercher le fichier `appsettings.json` ou si Git-ignore vous a empêcher de `appsettings.developement.json` vous devriez le créer et ajouter les code suivante dedans : 
            
            ```json
            {
              "ConnectionStrings": {
                "MySQLConntectionStr": "Server=localhost;Database=AlimoBioDB;Uid=root;Pwd=root;"
              },
              "SmtpSettings": {
                "Server": "sandbox.smtp.mailtrap.io",
                "Port": "2525",
                "Username": "6af039d0b778fd",
                "Password": "7d7de1147afa65"
              },
              "Jwt": {
                "Issuer": "votre-cle-issuer",
                "Audience": "votre-cle-audience",
                "APIKey": "votre-cle-secrete-api-jwt"
              },
              "Logging": {
                "LogLevel": {
                  "Default": "Information",
                  "Microsoft.AspNetCore": "Warning"
                }
              },
              "AllowedHosts": "*"
            }
            ```
            
            Assurez vous de modifier vos information de connection vers la base des données mariaDB sur ce String de connection : 
            
            ```jsx
            "MySQLConntectionStr": "Server=localhost;Database=AlimoBioDB;Uid=nom_utilisateur;Pwd=Mot_de_passe;"
            ```
            
            Si vous avez tout bien configurer vous devrez pouvoir lancer les commandes suivantes pour lancer le projet : 
            
            Dans le route de projet Alimobio, c’est à dire au même chemin que le fichier `appsettings.json` que vous avez créer peut-être :) . 
            
            veuillez  tapez les commandes suivantes : 
            
            Si vous avez le dossier migrations, vous pouvez lancer directement:
            
            ```jsx
            dotnet ef database update 
            ```
            
            Pour importer les tableaux vers la base des données et ensuite : 
            
            Pour lancer le projet
            
            ```jsx
            dotnet run 
            ```
            
                        Pour le mettre en production 
            
            ```jsx
            
            dotnet build
            ```
            
            Pour lancer le projet dans le mode de rechargement à chaud  (Débuguer)
            
            ```jsx
            dotnet watch
            ```
            
 
            le projet est lancer en local sur le port `:5216` suivant
            
            
 ![image](https://user-images.githubusercontent.com/66369128/234809236-abba864b-bd11-477f-aa7a-3f8e3bed056d.png)


Merci pour toutes les personnes qui ont répondu à mes questions et de m'avoir inspiré avec leurs idées et leur savoir faire.

## Pour faire des remarques ou poser des questions veuillez utiliser cette formulaire : 

    
    https://public.zenkit.com/f/hw9-w2Yuo/observation-conseils-des-membre-de-la-jury-cesi?v=oxV0VZiUW  
    
