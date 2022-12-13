## Entity

To do this, let’s find the Nop.Core project and the Domain directory there.

1. In this directory, I will create a folder named **Support Requests**. This folder will contain the entity class file;

2. So, I will create this class file with the name **SupportRequest**;

3. It should be a public class, so let’s specify this keyword;

4. nopCommerce has an abstract **BaseEntity** class, which all our entities inherit. This class provides common fields for all entities. It is actually the only field named **Id** that represents the database record identifier;

5. Then, let’s decide which fields we need in the **SupportRequest** entity. In other words, which columns our new database table should contain;

6. First of all, we need a **CustomerId** to determine which customer sent this request. The type of this field is integer;

7. Then, we need to know to which store this support request is related. So we add a **StoreId**. The type of this field is integer;

8. The other field is **MessageText**, which represents the support request message sent by the customer. The type of this field is string;

9. Then, we need to add a field that will store the reply from the support team. Let’s name it **ReplyText**. The type of this field is string as well;

10. We will also store the date and time when the support request was created. So let’s add a **CreatedOnUtc** property with the DateTime type;

11. And we will also store the date and time when the support request was updated. This will actually be the date when the support request was replied to. So the last field is **UpdatedOnUtc** with the DateTime type.

![Annotation 2022-12-12 181933](https://user-images.githubusercontent.com/98104395/207238093-ecaf7b54-e171-4d5f-a964-524154b9390f.png)


That’s all! Our entity is ready. Let’s just remove unused references.

## Entity builder

Now, it’s time to create an entity builder that provides the database table with the entity configuration.

1. Let’s open the Builders directory of Nop.Data;

2. Then create a SupportRequests folder there;

3. And add a new class named SupportRequestBuilder;

4. This class is public;

5. This class should inherit NopEntityBuilder with our entity type –  SupportRequest;

6. Then, in this class, let’s implement the MapEntity method, which will apply entity configuration;

7. In this method, we will configure CustomerId and StoreId fields as foreign keys, which means these columns will be related to the appropriate Customer and Store tables of the database;



That’s all! Now we have an entity builder set up.

## Database schema

1. The last step to finish with the data access layer is to update the database schema. For such purposes, we have already created our own **SchemaMigration** class. Then, in its **Up** method, let’s check if the **SupportRequest** table doesn’t exist yet and, if so, create it in the database.

2. So, we have finished setting up the domain and the data access layer for our new table.

3. Let’s run our solution. And then, let’s look at the database. We will see that the database now contains the new table named **SupportRequest**. The new table is right here. It has seven columns. The **Id** column is a primary key. The **CustomerId** and **StoreId** columns are foreign keys.


![Annotation 2022-12-13 112733](https://user-images.githubusercontent.com/98104395/207238279-e33c4381-726e-421d-b748-514fd181831d.png)


Refresh Database and check.
![Annotation 2022-12-13 112933](https://user-images.githubusercontent.com/98104395/207238553-d41a31e4-a805-45d0-99d4-427dfa5b452f.png)

-----------------------------------------------------------------------------------------------------------------------------------------
Let’s summarize what has been done. To add a new table to the database:

We have created a new domain entity with all the needed fields;

We have added the appropriate entity builder, which provides the entity configuration to the database;

And finally, we have updated the database schema to allow creating the new table;



The next step is to create a service that allows us to retrieve, insert, update, and delete support requests in the database. You will learn how to do this in the following sections.
