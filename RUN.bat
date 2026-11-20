@echo off
cd /d "%~dp0EventHub"
echo Starting EventHub at http://localhost:5118 ...
start "" http://localhost:5118
dotnet run
pause
