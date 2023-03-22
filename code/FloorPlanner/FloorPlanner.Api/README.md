# This is an auto-generated Visual Studio solution template by MA-Coding.

## Initial steps
1. Add initial migration with [Migration-Add.ps1](../FloorPlanner.Dal/Migration-Add.ps1) script.

2. Update database with [Migration-Run.ps1](../FloorPlanner.Dal/Migration-Run.ps1) script.

3. Check all TODO-s in Task List and execute them.

## Authentication

### Windows

1. Set `RequiredGroup` attribute in [appsettings.Development.json](./appsettings.Development.json) file.

### Azure ActiveDirectory

1. Generate keyvault by [this Quickstart](https://learn.microsoft.com/en-us/azure/key-vault/general/quick-create-portal). Use this documentation for [local debugging in VS](https://learn.microsoft.com/en-us/azure/key-vault/general/vs-key-vault-add-connected-service).

2. Set `AzureActiveDirectoryKeys` and `KeyVaultName` attributes in [appsettings.Development.json](./appsettings.Development.json) file. `KeyVaultName` attribute should contain only the specific part of this URI: "https://`KeyVaultName`.vault.azure.net/".

## FRONTEND

### Blazor

1. Generate Client.cs with [apigenerator.ps1](../FloorPlanner.Web.Blazor/Api/apigenerator.ps1) script.

2. In case of Windows authentication, fill the `AzureActiveDirectoryKeys` attributes in [appsettings.Development.json](../FloorPlanner.Web.Blazor/wwwroot/appsettings.Development.json) file.

### Angular

1. Generate Client.cs with [apigenerator.ps1](../FloorPlanner.Web.Angular/src/apigenerator.ps1) script.