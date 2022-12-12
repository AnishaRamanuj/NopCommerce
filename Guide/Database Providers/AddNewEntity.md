## Steps to add a new entity

- we need to add a new table that will store support requests to the database and create the appropriate user interface. To achieve this, we need to go through the following steps:

1. First of all, we have to create a new domain entity in the **Nop.Core** project;

2. Then, we need to add the appropriate entity builder that will provide our new database table with the entity configuration. We have to update the **Nop.Data** project to do this;

3. After this, we have to u**pdate the database schema**;

4. Then, in the **Nop.Services** project, we will create a new service with CRUD methods to manipulate the new table data;

5. Our next steps all require only **Nop.Web** project updates;

So, in the public store, we need to create two new pages: one where a customer can see all the support requests and one where the customer will be allowed to create a new support request. To do this, we will also have to create two new models, a factory and a controller;

6. We will also create a **validation class** for the model used to create a support request;

7. Then, we will create a support request model for the admin area;

8. After this, we will configure the mapping between the entity and this model using the **Automapper** library;

9. We will add a new **controller** and a **factory** allowing a store owner to see all the support requests in the admin area. For this purpose, we will also create a new view displaying these data;

10. We will add a possibility for the store owner to answer the existing return requests. This requires creating another view and a few more methods in the controller;
