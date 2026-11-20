EventHub (CampusPass) - ASP.NET Core MVC, .NET 10

OPTION A - Visual Studio 2026
 1. Open EventHub.sln
 2. Press F5 (green button named "EventHub")

OPTION B - no Visual Studio needed
 Double-click RUN.bat (needs the .NET 10 SDK installed)
 Then open http://localhost:5118

If you only have .NET 8 or 9 installed, open EventHub\EventHub.csproj and change
 <TargetFramework>net10.0</TargetFramework>  to  net8.0  (or net9.0).

DATABASE
 Users and registrations are saved in SQLite: EventHub\App_Data\eventhub.db (created automatically
 on first run). Delete that file to reset all accounts and registrations.
 The first run needs internet once so NuGet can download Microsoft.EntityFrameworkCore.Sqlite.
 If you switched to net8.0 / net9.0, change the package Version in EventHub.csproj to 8.0.0 / 9.0.0.
