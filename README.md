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
### Create User Secrets
Create new user secret to be able to use JWT features

![image](https://github.com/user-attachments/assets/15217e6f-15dc-4fa4-a2d5-a6c4c57b3d28)

Fill it like this 
```json
{
  "Jwt": {
    "Key": "<Your Secret Key>",
    "Issuer": "<Your Local Server:Port>",
    "API": "<Your Local Server:Port>",
  }
}
```

