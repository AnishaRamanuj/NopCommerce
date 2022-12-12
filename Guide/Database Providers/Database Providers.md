The support of different databases is implemented using a set of the following tools:

1. **Linq2DB**, which is the LINQ database access library that offers a simple, light, fast, and type-safe layer between objects and the database. It enables work with a database using .NET objects;

2. And, **FluentMigrator** that allows us to control the database object creation and provides us with a structured way to alter the database schema.

Now, let’s look at the code and see how the different database types are supported:

**1.** The DataProviders folder from the Nop.Data project contains three classes representing different database providers: MsSqlDataProvider, MySqlDataProvider, and PostgreSqlDataProvider;

**2.** They all inherit the **BaseDataProvider** class that contains the methods common for all database providers:

1)) The **InitializeDatabase** method is used in the nopCommerce installation process. Let’s consider this process in more detail:

  - As you remember, on the installation screen, you can choose the database management system;

  - Then, using the **GetDataProvider** method of the **DataProviderManager**, we resolve the appropriate database provider;

  - Then, we build the connection string using the appropriate method of the chosen provider;

  - And save this data to the **appsettings.json** file;

  - Then, we create the database and initialize it using the appropriate data provider methods;

2)) So, let’s get back to the **BaseDataProvider** class and consider the next method – **CreateTempDataStorage**. This method creates new temporary storage and populates it using data from the provided query;

3))The next common method is **GetEntityDescriptor**, which returns the mapped entity descriptor. Database providers use this method when building the database schema, mapping entities, and getting database constraints for model validation;

4))The **GetFieldHashes** method is used to get hash values of a stored entity field via database management system tools. It’s used during the import process;

5))The **GetTable** method, as you may guess, is used when we need to get a table associated with an entity;

6))Then come the methods that allow us to insert, update, and delete entities from the database;

7))The **ExecuteNonQuery** and Query methods allow to execute a specified SQL command;

8))The **QueryProc** method allows executing a stored procedure; 

9))And **Truncate** method, as you may guess, truncates a database table;

**3.** Then, let’s consider **INopDataProvider** that represents the data provider interface:

**It contains definitions of the methods that each database provider should implement. Besides the already discussed methods from the BaseDataProvider class, there are also the following ones:**

- The **CreateForeignKeyName** method that gets the compatible name of a foreign key based on the tables’ and columns’ names;

- The **GetIndexName** used when creating indexes;

- The **GetTable** methods we already discussed;

- So then come the methods allowing to make sure the database exists;

- The next methods allow us to backup and restore a database and reindex a table. They also require different implementations for different database types;

- The rest of the methods we also already discussed;

**4.** So let’s say you wish to connect to the database type not supported by nopCommerce out of the box. In this case, you need to create your own database provider that implements the **INopDataProvider** interface. If it fits your requirements, you can also use the **BaseDataProvider** class with its default methods. Finally, add your database type to the **DataProviderType** and extend the **GetDataProvider** method of the **DataProviderManager**, which associates the **DataProviderType** with the appropriate data provider.
