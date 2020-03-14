# Flight Tracker with CosmosDb, Functions and other serverless resources
Serverless solution showing how flights can be tracked using Azure Cosmos DB, Functions and SignalR.

# Quick Deploy to Azure
[![Deploy to Azure](http://azuredeploy.net/deploybutton.svg)](https://azuredeploy.net/)

This template created the resources required.

Below are the parameters which can be user configured in the parameters file including:

- **Application Name:** Enter the name you wish to call the application. This name will be used as a base for the resources which will get created..
- **Tenant Id:** Enter the Azure Active Directory tenant ID that should be used for authenticating requests to the key vault. The default is `[subscription().tenantId]` which should be sufficient in most cases.
- **Multi-Master:** Enter the Object Id of a user, service principal or security group in the Azure Active Directory tenant for the Key Vault. The object ID must be unique for the list of access policies. Get it by using Get-AzADUser or Get-AzADServicePrincipal cmdlets. You can also use the following CLI command to get the objectID of the loged in user: `az ad signed-in-user show --query objectId`
