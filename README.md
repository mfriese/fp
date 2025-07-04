
# Todo App (MAUI + Minimal API Backend)

Diese Anwendung besteht aus einem **.NET MAUI Frontend** und einem **Minimal API Backend** mit
Entity Framework Core (InMemory), Refit, Serilog und dynamischer Filterung via DynamicData. Sie
demonstriert moderne .NET-Praktiken wie Dependency Injection, IObservable-Reaktivität,
Integrationstests mit `WebApplicationFactory`, strukturierte Logs und eine klare Trennung von
API, Service und Datenzugriff.

## Projektstruktur

```text
Fp.Api/              => Backend (Minimal API)
Fp.App/              => MAUI-App (Frontend)
Fp.Api.Test/         => Integration- & Unittests
```

## Features

* Todos auflisten, erstellen, aktualisieren, löschen
* Filterung (Completed, NotCompleted, All)
* Dynamische Listen mit `SourceList` + `AutoRefreshOnObservable`
* API Kommunikation über [Refit](https://github.com/reactiveui/refit)
* Backend in .NET 8 mit Minimal APIs und Entity Framework Core (InMemory)
* Logging mit [Serilog](https://serilog.net/)
* Integrationstests mit Seed-DB über `WebApplicationFactory`
* Unterstützung für Scalar UI (API-Dokumentation über `/openapi/v1.json` und `/scalar`)

## Architektur

### Backend (Minimal API)

* Endpunkte: `/todo`, `/todo/{id}`
* Getrennte, eigene DTOs pro Handler
* UnitOfWork + Generic Repository Pattern
* Seed-Datenbank für Tests
* Serilog Logging

### Frontend (MAUI)

* `CommunityToolkit.MVVM`: Observable Properties, RelayCommands
* `CommunityToolkit.Maui.Alerts`: Toasts bei Fehlern
* `DynamicData`: reaktive Listendarstellung + Filterung
* Navigation über `Shell`
* Integration mit Refit über Dependency Injection

## Teststrategie

* **Unit Tests**:

  * Mock von `IRepository` und `IUnitOfWork`
  * Fokus auf Service-Logik

* **Integrationstests**:

  * `WebApplicationFactory<Program>` mit InMemory DB
  * Jeder Test hat eigene isolierte DB
  * Validierung von API-Routen, HTTP-Codes und Rückgabeobjekten

## Setup & Start

### Voraussetzungen

* .NET 8 SDK
* Visual Studio 2022 (mit MAUI-Workload)
* Android Emulator (API 33+ empfohlen)

### Start der Anwendung

Das Backend ist als Minimal API im Projekt `Fp.Api` integriert. **Hinweis**: Im
Android-Emulator kann das Backend nur über `http://10.0.2.2:5000` erreicht werden.

**Ausführen über Visual Studio:**

Option 1) Debug start aus Visual Studio heraus auf Emulator:

* Solution Fp.Api.sln in Visual Studio öffnen
* Konfiguration: **Debug**.
* Geräteauswahl: Einen Android Emulator auswählen.
* Projekte `Fp.Api` und `Fp.App` werden in der Config `Debug` *beide* gestartet
* Backend läuft auf: `https://localhost:5001` oder `http://localhost:5000`
* App versucht auf `http://10.0.2.2:5000` zuzugriefen, hier läuft das Backend.

Option 2) Release start aus Visual Studio heraus auf echtem Gerät:

* Solution Fp.Api.sln in Visual Studio öffnen
* Konfiguration: **Release**.
* [TODO]

### API-Dokumentation

* OpenAPI JSON: [`http://localhost:5000/openapi/v1.json`](http://localhost:5000/openapi/v1.json)
* Scalar UI (wenn aktiviert): [`http://localhost:5000/scalar`](http://localhost:5000/scalar)

## 📁 Verzeichnisstruktur

```text
Fp.Api/
├── Endpoints/           // Minimal API Handler
├── Models/              // Domain Models (z.B. TodoModel)
├── Persistence/         // DbContext, Repository, UnitOfWork
├── Services/            // Application Services (z.B. TodoService)
├── Seed/                // Seed-Initialisierung der DB
├── Program.cs           // Einstiegspunkt, DI + Logging + Routing
```

```text
Fp.App/
├── ViewModels/          // MVVM ViewModels mit State-Management
├── Views/               // XAML-Seiten (Main, Edit)
├── Models/              // DTOs, die mit dem Backend verwendet werden
├── Services/            // Refit-Schnittstellen, lokale Todo-Services
```
