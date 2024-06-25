# Azure AI Search & Data Factories Integration

## Description
Following tasks are performed in this project:
- Integration of Azure Cognitive Search to perform search queries.
- Configured Data Factory Pipelines in Azure and Triggered ETL Pipelines.
- Implemented Change Data Capture from Azure SQL Database and post changes to Destination Database --> Simply a JSON file for testing.
- Building and running the application using .NET 7.

## Project Configurations

# 
```bash
# Clone the repository
git clone https://github.com/aqiftekhar/ADFPipelineCognitiveSearch.git
cd your-repository

```bash
## Open in VSCode
code .

```bash
# Restore Nuget Packages
dotnet restore

```bash
## Add .evn file with following environment configurations
#Azure configurations
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

#Azure Data Factory Configurations
DATA_FACTORY_NAME=
PIPELINE_NAME=

#Azure Blob Storage Configurations
BLOB_STORAGE_CONNECTION_STRING=
CONTAINER_NAME=
CDC_CONTAINER_NAME=

```bash
## Build Application using VSCode
dotnet build

```bash
## Run Application using VSCode
dotnet run


```bash
## Features:
Enter a command:
1. Search products
2. Run ETL pipeline
3. Capture changes
4. Exit
