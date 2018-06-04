# Investigations
A place to test things that i can access from both my laptop and work. 

Mainly .net Core & Vue bits and pieces.

## Things we are using 

* Swashbuckle

    Provides swagger documentstion.
    https://github.com/domaindrivendev/Swashbuckle.AspNetCore

    <pre>
    dotnet add package Swashbuckle.AspNetCore
    </pre>

    Notes:
    <pre>
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
    });
    </pre>
    needs to be
    <pre>
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "My API", Version = "v1" });
    });
    </pre>

* MySql

    Assess the DB
    <pre>
    dotnet add package MySql.Data --version 8.0.11
    </pre>

* Dapper

    Lightweight ORM

    https://github.com/StackExchange/Dapper
    <pre>
    dotnet add package Dapper --version 1.50.5
    </pre>


<pre>
dotnet add package Microsoft.Extensions.Logging.Console --version 2.1.0
dotnet add package Microsoft.Extensions.Logging.Debug --version 2.1.0	
</pre>