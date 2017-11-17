Sample based on https://docs.asp.net/en/latest/getting-started.html

1. Create directory

mkdir aspnetcoreapp
cd aspnetcoreapp
dotnet new

2. Update project.json file

{
  "version": "1.0.0-*",
  "buildOptions": {
    "debugType": "portable",
    "emitEntryPoint": true
  },
  "dependencies": {},
  "frameworks": {
    "netcoreapp1.0": {
      "dependencies": {
        "Microsoft.NETCore.App": {
          "type": "platform",
          "version": "1.0.0"
        },
        "Microsoft.AspNetCore.Server.Kestrel": "1.0.0" // Add this line
      },
      "imports": "dnxcore50"
    }
  }
}

3. Run command line: dotnet restore

4. Run command line: dotnet run

5. Browse: http://localhost:5000: