# Azure Function Demo
## Introduction
This repository contains a basic Azure Functions Application that illustrates the implementation concepts
necessary to create an advanced application. Some of the features of the demo include
- HTTP Trigger
- Queue Trigger
- Dependency Injection
- Service Library
- Custom Logging
- IOptions based configuration

## Getting Started
1. Install Visual Studio 2019
2. Add the Azure development workload to your Visual Studo installation
3. Clone the repository
4. Create the local.settings.json file
5. Add custom log categories to host.json file
6. Create the storage queue
7. Execute the Azure Functions

### Visual Studio Installation
The code is developed using Visual Studio 2019. Visual Studio 2022 is expected to be binary compatible, therefore,
I expect the code to work properly in 2022. VS Code may work, however, I prefer to develop in Visual Studio.
If someone who prefers VS Code messages me I will update the README with their experience.

### Visual Studio Work Loads
Ensure the Azure development workload is included in your Visual Studo installation. Open the Visual Studio
Installer, select the Modify button. Make sure the Azure Development workload is selected.

### Clone the Repository
Clone the code to a local repository.

### Create the Local Settings File
The application configuration file, local.settings.json, is excluded from the git repository by default. Create a new json
file in the root of the FunctionDemoApp called local.settings.json with the following content:
``` json
{
	"IsEncrypted": false,
	"Values": {
		"AzureWebJobsStorage": "UseDevelopmentStorage=true",
		"FUNCTIONS_WORKER_RUNTIME": "dotnet",
		"Service:Url": "www.contoso.com",
		"Service:UseHttps": true,
		"QueueName": "custom-messages"
	}
}
```

### Add Custom Log Category
The default logging configuration filters out custom log categories. There is an
[issue](https://github.com/Azure/azure-functions-host/issues/4345) on github to change the default behavior to allow
log entries from log categories created by the DI container. Currently, this is a manual change to the host.json file:
``` json
{
	"version": "2.0",
	"logging": {
		"logLevel": {
			"ServiceLib": "Information"
		},
		"applicationInsights": {
			"samplingSettings": {
				"isEnabled": true,
				"excludedTypes": "Request"
			}
		}
	}
}
```

### Create the Storage Queue
I recommend installing [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer)
on your workstation. It is the simpliest tool for creating the queue.
 
It is easiest to run the application to get the Storage Emulator running. The first time the application is run on your workstation may take several minutes to launch.
Visual Studio will try to install the Storage Emulator if it's not already installed on your workstation.

Once the application is running, right-click on Queues under Local & Attached => Storage Accounts => (Emulator -Default Ports) (Key).
Select the Create Queue context menu item. Enter the queue name to create. The queue name and QueueName setting in
local.settings.json must match.

### Execute the Azure Functions
There are two HTTP Trigger functions and one Queue Trigger function. Please note, the TCP port may be different on your machine.

To execute the Get Status HTTP Trigger function, send a HTTP GET requrest to the following endpoint
```
http://localhost:7071/api/GetStatus
```

To execute the Send Message HTTP Trigger function, send a HTTP POST request to the following endpoint
```
http://localhost:7071/api/SendMessage
```
with the following JSON request body
``` json
{
    "message": "Hello, World!",
    "level": "Information"
}
```

To execute the Process Message Queue Trigger function, create a new message on the queue with any string
using Azure Storage Explorer.
