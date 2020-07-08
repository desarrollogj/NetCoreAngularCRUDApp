# NetCore & Angular CRUD App example

Requirements:
- Visual Studio 2019 (or VS Code, or command line)
- Angular 10 (Angular Cli)
- Net Core 3.1
- A SQL database

# Database
Review the connection options into `appsetting.json` file. Once the connection string is set, run the migration using:

`dotnet ef database update`

# Run
Run both projects from VS Ide, or use: 

`dotnet run`

Optional you can run the Angular frontend going to ClientApp directory and use: 

`ng serve`

# Test
The backend has a few unit tests using xUnit, Moq and coverlet.

To run the tests, use:

`dotnet test`

To run the tests with coverage report, use:

`dotnet test /p:CollectCoverage=true`

# Known issues
There is a timeout problem running Net Core 3 and Angular 10 using Visual Studio. More info at https://github.com/dotnet/aspnetcore/issues/17277
If the problem persist, simply run the app from Visual Studio and run the Client using `ng serve`. CORS is enabled to support different ports :)
