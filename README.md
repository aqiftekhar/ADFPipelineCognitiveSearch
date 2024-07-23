# Azure AI Search & Data Factories Integration

## Description
Following tasks are performed in this project:
- Integration of Azure Cognitive Search to Search and filter Queries.
- Integration of Azure Data Factory.
- Trigger ETL pipeline to insert Products using Stored Procedure from C# Code.
- Implemented Change Data Capture in C#.
- For Change Data Capture, Copy recently added changes from Products table in Azure Database.
- For Change Data Capture, Write recently added Products to Destination Data Source (For Demo, I am using JSON File as Destination).
- Managing and securing Azure resources using environment variables in .env file.
- Building and running the application using .NET 7.

## Project Configurations
Following are the steps to configure and run the project.

## Azure Data Factory Pipeline
Following is the Azure Data Factory:
- Pipeline that Wait for 5 seconds.
- Execute Lookup Activity to Load the data from Excel file.
- ForEach Activity with nested Stored Procedure to insert Products

![ADF Pipeline](./images/adf_pipeline.png "ADF Pipeline")

### Clone the repository
```bash
git clone https://github.com/aqiftekhar/ADFPipelineCognitiveSearch.git
cd ADFPipelineCognitiveSearch
```

### Open in VSCode
```bash
code .
```

### Restore NuGet Packages
```bash
dotnet restore
```

### Add `.env` file with the following environment configurations
```env
# Azure configurations
SUBSCRIPTION_ID=
AZURE_CLIENT_ID=
AZURE_TENANT_ID=
AZURE_CLIENT_SECRET=
RESOURCE_GROUP_NAME=

# DB connection string
CONNECTION_STRING=

# Azure AI Search Configurations
SEARCH_SERVICE_NAME=
SEARCH_SERVICE_API_KEY=
SEARCH_SERVICE_URL=
SEARCH_INDEX_NAME=

# Azure Data Factory Configurations
DATA_FACTORY_NAME=
PIPELINE_NAME=

# Azure Blob Storage Configurations
BLOB_STORAGE_CONNECTION_STRING=
CONTAINER_NAME=
CDC_CONTAINER_NAME=
```

### Build Application using VSCode
```bash
dotnet build
```

### Run Application using VSCode
```bash
dotnet run
```

## Features
Enter a command:
1. Search products
2. Run ETL pipeline
3. Capture changes
4. Exit
