# Everbridge Control Center Technical Challenge Project

This project purpose is to provide a base launching point for candidates to start their challenge projects. This project helps by providing a rough standalone environment to begin their work spending more time on the project than the environment.

What this project is not is a recommended boilerplate. It represents a rough prototype grade environment intended only to provide a simple starting point for candidates intending to take the challenge.


## The Challenge
React Web Client
Build a small Typescript React Web Client and C# .Net Server application providing a simple door management system for a facility. 







### Everbridge.ControlCenter.TechnicalChallenge

This C# Core 5.0 ASP.Net server supports a single DoorController based on */api/door* which uses the *DoorDatabase* library to access the MS SQL database configured in the docker environment.

The project also hosts the React WEb client within the */ClientApp* directory. This is a typescript template application built using [Create-React-App](https://create-react-app.dev/docs/adding-typescript/). This is just a stub application to be built upon by candidates that is hosted by the server. 

The server is hosted on port **12443** and only supports HTTPS. All non-static or controller requests will be directed to the React SPA. If the application is run with Environment Variables *ASPNETCORE_ENVIRONMENT* = *Development* and *USE_DEVELOPMENT_SERVER* = *true* then the server will use the React development server to host the web client allowing hot-reload editing of the web client.

The configured dockerfile takes a build argument to switch the final image based on the build-configuration of the project. The launch settings has both setups configured giving 'Docker (Debug)' to be used with the Debug build configuration and supports hot-reloading of the web client. 'Docker (Release)' is intended to be used with the Release build configuration and will used the optimised web client build.


### DoorDatabase

This C# project provides a simple base Entity Framework database context for storing Door records.


### Docker Compose

Docker compose providing a MS SQL 2019 server on host *sqlserver* and the ASP.Net Everbridge.ControlCenter.TechnicalChallenge service. The run configuration in visual studio provides the development build of the Everbridge.ControlCenter.TechnicalChallenge service with hosting the react application via the development server.
