## Instalation
If you first time cloning this repo, please adjust with the requirement
 ### Create Appsettings
 Create `appsettings.json` to the main directory of the VS project
 ```json
 {
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "JobseekBerca": "Server=<Your SQLServer Connection>;Database=JobseekBerca;user id=<Your SQLServer username>;password=<Your SQLServer password>;TrustServerCertificate=true;"
  }
}
```
