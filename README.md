# Demo - LubenSoft App

This is LubeSoft's Vehicles Datastore App, that manages Vehicles Sales Management etc. Step by step instructions below will help you to run Demo LubeSoft App.
Please follow the below instructions.

## Requirements

- .NET 5.0
- Visual Studio 2019

## Run in Visual Studio
First clone the project using any suitabel tool or via using windows powershell and git clone commands.
Use develop or master branch to clone.
After cloning code, open `\DemoRazorPageApp.sln` in Visual Studio and press `F5`.

## Run in CLI
Move to Now open `\DemoRazorPageApp.sln` folder and Run following commands in windows powershell. 
```bash
dotnet restore
dotnet build
dotnet run
```

Visit https://localhost:5001

## Known Issues
- This issue is about vehicle json file deletion and recreation with updated data and to reload new json file. 
  As we are not using any database so, used a json file data. And the issue is, system sometime deletes the file 
  and create an empty one and it doesn't show any data in grid.I have created a backup folder with backup data file, 
  you can copy from `./DemoRazorPageApp/wwwroot/data/backup` to `./DemoRazorPageApp/wwwroot/data` to test your basic flow.
