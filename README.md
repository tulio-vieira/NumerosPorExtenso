# Numeros por Extenso App

This app receives numbers provided in a url (http://localhost:3000/{number}) and returns them in the extensive portuguese form. It is written in ASP.NET Core. Opening the app in Visual Studio makes it easy to run in different profiles (docker or project).

Below, there are instructions to run the app using the command line.

## Run on Docker

### Run app with docker compose

To run the app with docker compose, change to the root directory and execute the following command:

```console
docker-compose up
```

### Run app with docker CLI

To run the app with docker CLI, change to the root directory and execute the following command:

```console
docker build -t numeros-por-extenso -f ./NumerosPorExtenso/Dockerfile .
docker run -p 3000:80 numeros-por-extenso
```

### Run test with docker CLI

To run the tests with docker CLI, change to the root directory and execute the following command:

```console
docker build -t numeros-por-extenso-teste -f ./XUnitTestNumerosPorExtenso/Dockerfile .
docker run numeros-por-extenso-teste
```

## Run without docker

To build and run the sample, change to the *NumerosPorExtenso* directory and execute the following command:

```console
dotnet run
```

`dotnet run` builds the sample and runs the output executable. It implicitly runs `dotnet restore` (this happens ever since .NET Core 2.0 SDK) to restore the dependencies of the sample.

To run the tests, change to the *XUnitTestNumerosPorExtenso* directory and execute the following command:

```console
dotnet test
```

`dotnet test` builds the test project and runs the configured tests.
