[Back to README](../README.md)

## Project Configuration

### Prerequisites

Ensure that the following prerequisites are installed:
 - [.NET SDK 8+](https://dotnet.microsoft.com/en-us/download)
 - [PostgreSQL](https://www.postgresql.org/download/)
 - EF CLI:
	 - `dotnet  tool install --global dotnet-ef`


### How to run the project

1. Clone this repository:  
```bash
https://github.com/roothery/abi-gth-omnia-developer-evaluation.git
cd abi-gth-omnia-developer-evaluation
```

2. Make sure your database connection string is correctly configured:

```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DeveloperEvaluation;Username=your_user;Password=your_password;TrustServerCertificate=true"
}
```

3. Navigate to the project root directory and run:

```bash
dotnet restore
```

4. Make sure to apply the migrations from within the ORM project path:
```bash
dotnet ef database update
or
dotnet ef database update --startup-project ../Ambev.DeveloperEvaluation.WebApi
```

5. To start the application locally and test its functionality, run the following inside the project folder:
```bash
dotnet run --project ../Ambev.DeveloperEvaluation.WebApi
```

6. Navigate to the project folder where the tests are located and run:
```bash
dotnet test
```

---

<div style="display: flex; justify-content: space-between;">
  <a href="./frameworks.md">Previous: Frameworks</a>
  <a href="../README.md">Next: Read Me</a>
</div>