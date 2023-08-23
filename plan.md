# Oppgave

Du skal lage et skjema som tar imot brukerdata og lagrer dette på en server.

## Frontend - React

- 1 Lag ett skjema som tar imot denne dataen:

  - Fornavn
  - Etternavn
  - Mellom nanvn?
  - Alder
  - Epost
  - Tlf
  - Adresse
  - PostNr

- 2 Validering av data

  - Sjekk at all nødvendig data faktisk er der
  - Valider navn
  - Valider Epost
  - Valider tlf
  - Ønsker
    - Adresse burde importere all nødvendig data automatisk

## Backend - C#/.NET

- 1 Lag en klasse som tar inn all dataen
- 2 Lag en klasse som validerer all dataen på nytt siden det er gøy med brakpoints
- 3 Lag en klasse som lagrer all dataen i en DB
  - Hvis denne er dependensy injecta hadde jo det vært velig tøft så jeg kan bytte på de fritt vilt
