# Flight Tracker with CosmosDb, Functions and other serverless resources
Serverless solution showing how flights can be tracked using Azure Cosmos DB, Functions and SignalR.

## Quick Deploy to Azure
[![Deploy to Azure](http://azuredeploy.net/deploybutton.svg)](https://azuredeploy.net/)

This template created the resources required.

Below are the parameters which can be user configured in the parameters file including:

- **Application Name:** Enter the name you wish to call the application. This name will be used as a base for the resources which will get created..
- **Tenant Id:** Enter the Azure Active Directory tenant ID that should be used for authenticating requests to the key vault. The default is `[subscription().tenantId]` which should be sufficient in most cases.
- **Multi-Master:** Enter the Object Id of a user, service principal or security group in the Azure Active Directory tenant for the Key Vault. The object ID must be unique for the list of access policies. Get it by using Get-AzADUser or Get-AzADServicePrincipal cmdlets. You can also use the following CLI command to get the objectID of the loged in user: `az ad signed-in-user show --query objectId`

## Architecture
This sample is a real-time app that displays flight status details. This can be achieved through the use of [Azure Functions](https://docs.microsoft.com/en-gb/azure/azure-functions/) and [Azure SignalR](https://docs.microsoft.com/en-gb/azure/azure-signalr/). The following image describes the solution that will provide this ability:
![signalr-cosmosdb-functions](assets/signalr-cosmosdb-functions.png).

More details can be found in the [documentation](https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-concept-azure-functions), but an overview of the above is:
1. A change is made in a Cosmos DB collection
2. The change event is propagated to the Cosmos DB change feed
3. An Azure Functions is triggered by the change event using the Cosmos DB trigger
4. The SignalR Service output binding publishes a message to SignalR Service
5. SignalR Service publishes the message to all connected clients

## Flight Data
As the name suggests, status of flights will be required. There is a non-profit organisation called [OpenSky Network](https://opensky-network.org) which will be used to source the data for this sample. Here is a description of the [OpenSky Network](https://opensky-network.org):

>The OpenSky Network is a non-profit association based in Switzerland. It aims at improving the security, reliability and efficiency of the air space usage by providing open access of real-world air traffic control data to the public. The OpenSky Network consists of a multitude of sensors connected to the Internet by volunteers, industrial supporters, and academic/governmental organizations. All collected raw data is archived in a large historical database. The database is primarily used by researchers from different areas to analyze and improve air traffic control technologies and processes.

[OpenSky Network](https://opensky-network.org) provide a [REST API](https://opensky-network.org/apidoc/rest.html) that provides several functions to retrieve [state vectors](https://opensky-network.org/apidoc/index.html#state-vectors), flights and tracks for the whole network, a particular sensor, or a particular aircraft. In this sample, the `GET /states/all` operation will be used.

The request on it's own would return a lot of data. Therefore, we can limit to the bounds of, for example, the United Kindom. The following is an example of such a request:

`https://opensky-network.org/api/states/all?lamin=49.9599&lomin=-7.5721&lamax=58.6350&lomax=1.6815`

These bounds are a rough estimate so some tweeking may be required.

The details of the response can be found on the [OpenSky REST API](https://opensky-network.org/apidoc/rest.html) page, but for this sample, the following fields are used:

| Index | Property          | Type      | Description
| ---   | ---               | ---       | ---
| 0     | icao24            | string    | Unique ICAO 24-bit address of the transponder in hex string representation.
| 1     | callsign	        | string	  | Callsign of the vehicle (8 chars). Can be null if no callsign has been received.
| 2     | origin_country	  | string	  | Country name inferred from the ICAO 24-bit address.
| 5     | longitude	        | float	    | WGS-84 longitude in decimal degrees. Can be null.
| 6     | latitude	        | float	    | WGS-84 latitude in decimal degrees. Can be null.
| 7     | baro_altitude 	  | float	    | Barometric altitude in meters. Can be null.
| 9     | velocity	        | float	    | Velocity over ground in m/s. Can be null.
| 11    | vertical_rate     | float	    | Vertical rate in m/s. A positive value indicates that the airplane is climbing, a negative value indicates that it descends. Can be null.
| 14    | squawk    	      | string    | The transponder code aka Squawk. Can be null.
