
# Todo App (MAUI + Minimal API Backend)

Diese Anwendung besteht aus einem **.NET MAUI Frontend** und einem **Minimal API Backend** mit
Entity Framework Core (InMemory), Refit, Serilog und dynamischer Filterung via DynamicData. Sie
demonstriert moderne .NET-Praktiken wie Dependency Injection, IObservable-Reaktivität,
Integrationstests mit `WebApplicationFactory`, strukturierte Logs und eine klare Trennung von
API, Service und Datenzugriff.

<!-- TOC -->
- [Todo App (MAUI + Minimal API Backend)](#todo-app-maui--minimal-api-backend)
  - [Projektstruktur](#projektstruktur)
  - [Features](#features)
  - [Architektur](#architektur)
    - [Backend (Minimal API)](#backend-minimal-api)
    - [Frontend (MAUI)](#frontend-maui)
  - [Teststrategie](#teststrategie)
  - [Setup \& Start](#setup--start)
    - [Voraussetzungen](#voraussetzungen)
    - [Start der Anwendung](#start-der-anwendung)
      - [**Debug** Start mit **Emulator**](#debug-start-mit-emulator)
      - [**Release** Start mit **Smartphone**](#release-start-mit-smartphone)
    - [API-Dokumentation](#api-dokumentation)
  - [📁 Verzeichnisstruktur](#-verzeichnisstruktur)
<!-- /TOC -->

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

**Ausführen (aus Visual Studio):**

Der Einfachheit halber sollte die Anwendung aus Visual Studio heraus gestartet werden.
In der App gibt es zwei separate Config Dateien:

```text
/Fp.App/settings.Debug.json
/Fp.App/settings.Release.json
```

Die **settings.Debug.json** Datei ist für lokalen Start mit Emulator gedacht. Die
**settings.Release.json** ist für dem Start mit einem Smartphone gedacht. Es muss
in der Relase config allerdings die IP Adresse des Computer hinterlegt werden, auf
dem das Backend läuft.

#### **Debug** Start mit **Emulator**

1) Solution Fp.Api.sln in Visual Studio öffnen.
1) Per **Rechtsklick** die **Properties** der **Solution** öffnen.
1) Unter **Common Properties** > **Configure Startup Projects** folgende Einstellungen
  vornehmen:
     * **Project**: Fp.Api -> **Action**: Start without debugging -> **Debug Target**: run in console
     * **Project**: Fp.App -> **Action**: Start -> **Debug Target**: [eingerichteter Android Emulator]
     * Dialog schließen.
1) Die Solution Configuration auf **Debug** stellen.
1) Backend läuft auf `http://localhost:5000` und die App kontaktiert es über die Adresse
  `http://10.0.2.2:5000`. Unter dieser Adresse verbirgt sich der localhost des Host-Rechners
  (nur im Emulator).
1) **Start** klicken

#### **Release** Start mit **Smartphone**

1) Solution Fp.Api.sln in Visual Studio öffnen.
1) Per **Rechtsklick** die **Properties** der **Solution** öffnen.
1) Unter **Common Properties** > **Configure Startup Projects** folgende Einstellungen
  vornehmen:
     * **Project**: Fp.Api -> **Action**: Start without debugging -> **Debug Target**: run in console
     * **Project**: Fp.App -> **Action**: Start without debugging -> **Debug Target**: [eingerichtetes
       Android Gerät]*1
     * Dialog schließen.
1) Die Solution Configuration auf **Release** stellen.
1) Backend läuft auf: `http://localhost:5000`
1) Datei `settings.Release.json` so anpassen, dass die IP des Computers, auf dem das
  Backend gestartet wird, eingetragen ist. Z.B. per `ipconfig` in der Kommandozeile abfragen
  und die IP des lokalen Netzwerkgeräts identifizieren. Die IP aus der json entsprechend
  ersetzen.
1) **Achtung** Damit die App auf den Port zugreifen kann muss er in der Windows Firewall freigegeben
   werden! Windows Defender Firewall starten > Eingehende Regeln wählen > Aktion > Neue Regel > Port
   5000 freigeben.
1) **Start** klicken

*1 Wird das Gerät nicht angezeigt Dialog schließen und stattdessen über die Debug-Leiste das Gerät
aus der Liste wählen (Dropdown am Startbutton -> Android Local Devices).

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
