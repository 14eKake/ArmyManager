# Army Manager

Ce dépôt contient une solution **.NET 8** destinée à illustrer la gestion d'une petite base de données de types d'unités avec interface graphique WPF.

## Projets

- **Core** : bibliothèque de classes qui définit les entités du jeu ainsi que le `GameDbContext` (EF Core / SQLite). Trois `UnitType` sont insérés lors de la création de la base.
- **UI** : application WPF suivant le modèle MVVM. Elle référence le projet `Core` et utilise l'injection de dépendances pour créer la fenêtre principale et son `MainViewModel`.
- **ArmyManager** : projet WPF minimal généré à l'origine, actuellement non utilisé.
- **Core.Tests** : projet de tests (xUnit) prêt à accueillir des tests unitaires.

## Prérequis

- .NET SDK 8.0 ou supérieur.

## Construction

```bash
# Compilation de la solution
 dotnet build ArmyManager.sln
```

## Exécution de l'interface

```bash
# Lance l'application WPF principale
 dotnet run --project UI/UI.csproj
```

## Tests

```bash
 dotnet test Core.Tests/Core.Tests.csproj
```

Cette base de code peut servir de point de départ pour un outil de gestion d'armées plus complet.
