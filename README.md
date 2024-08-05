# ShipCrewsApiRestSwaggerER

## Aim
To demonstrate a REST API implementing swagger which accesses an underlying SQL DB using entity framework.
(Long term goal - but not yet realised: implment the RNLI crew stations using Blazor WebApp)

## Notes
Based on: https://medium.com/@cydexcode/build-a-net-8-restful-web-api-testing-with-swagger-and-sql-server-integration-8d18336ebaa2 
and ASP.NET Core Hosted (see https://codewithjulian.com/create-a-crud-blazor-webassembly-api-controller/#:~:text=Create%20a%20CRUD%20Blazor%20Webassembly%20API%20Controller%201,8.%20Add%20the%20UI%20mark-up%20for%20the%20list)  

Blazer Web App .net 8 not used as do not know how to add Swagger/OpenAI  

This uses the ShipCrews database (https://github.com/SimonV839/ShipCrews-2.git).

EF requires any handled value to have a primary key (i.e. CrewAssignments could not use a composite primary key made of the fks).

### Creation Notes
1) ASP.NET Core Web api
	note "Enable OpenAPI support" is ticked

2. Nuget:
	a) Microsoft.EntityFrameworkCore.Tools
	b) Microsoft.EntityFrameworkCore.SqlServer

3) Tools -> NuGet Package Manger -> Package Manger Console:
	scaffold-DbContext "server=(local)\SQLEXPRESS;database=ShipCrews;Integrated Security=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

4) Share the db context service throught the app:
	builder.Services.AddDbContext<ShipCrewsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ShipCrewsContext")));

5) API Controller
	Controllers [folder]->Add->Razor Component...
	API Controller - Empty (PeopleController)

6) Populate PeopleController

...

7) The EF created models contain more information than required. Eg Person contains Role (as well as RoleId)
	which is set to null. This causes a problem when referencing this API.
	As a hack (Simon: do not yet know what the proper solution is), create the HackedModels which are
	the models but with the minimum of fields (ie those in the db).
