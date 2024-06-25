Here is the code for your `README.md` with the correct formatting and structure:

```markdown
# Azure AI Search & Data Factories Integration

## Description
Following tasks are performed in this project:
- Integration of Azure Cognitive Search with Azure Data Factory.
- Setting up and configuring Azure resources required for the project.
- Creating and configuring Azure Data Factory pipelines.
- Implementing data ingestion and indexing workflows.
- Managing and securing Azure resources using environment variables.
- Building and running the application using .NET 7.

## Project Configurations

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