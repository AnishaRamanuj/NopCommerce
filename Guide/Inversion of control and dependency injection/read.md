The Inversion of Control and Dependency Injection patterns mainly focus on providing a loose coupling between components or simply a way in which we decouple the conventional dependency relationships between objects so that the objects are not tight to each other.



- This approach allows nopCommerce to stay easy to change as it grows in size and complexity. 

- It facilitates creating a reusable, modular application that is easy to maintain. 

- This also makes it possible to override the existing nopCommerce functionality through plugins without touching the core.

To implement this, we use the built-in ASP.NET Core internal dependency injection and the **Autofac** library.

Let’s look at how we do this in the code. For example, the **ProductService** class. This class allows us to manipulate the **Product** table and other product-related tables of the database. It contains methods intended to insert, update, delete, and retrieve data from the database. This service also contains business logic needed to implement the product inventory management, discounts mapping, tier pricing functionality, and more.

This way, each entity in nopCommerce has the appropriate service that covers all possible ways to use the entity.

Coming back to the Dependency Injection, let’s look at the service constructor. Here, we have many dependencies going into this constructor. If we look at the implementation of one of those dependencies, for example, at the CustomerService, we will see that it contains a bunch of dependencies as well.

And to register these dependencies, we have created the NopStartup class in the Infrastructure folder of **Nop.Web.Framework**. This class implements the **INopStartup** interface. In this class, the AddScoped method registers services, including mentioned **ProductService** and **CustomerService**. 

Besides the **AddScoped** method, we also use such methods as **AddTransient** and **AddSingleton**:

1. **Scoped** objects are the same within a request but different across different requests;

2. **Transient** objects are always different; a new instance is provided to every controller and every service;

3. **Singleton** objects are the same for every object and every request.

This way, we have a service container responsible for creating an instance of the dependency and disposing it when it's no longer needed.

So, this allows us to create instances of objects that other objects rely on without knowing at compile time which class will be used to provide that functionality. We just specify the needed interface in the constructor, and then the container figures out which class implementing this interface should be used.

Note that the **INopStartup** interface has the **Order** property. It allows you to replace existing dependencies. To override nopCommerce dependencies, set the Order property to something higher than 0. nopCommerce orders dependency classes and runs them in ascending order. The higher the number, the later your objects will be registered.

Then, sometimes we need to resolve a dependency not through a constructor but in the code directly. For example, we do it this way in migrations. Let’s open the **LocalizationMigration** file from Nop.Web.Framework - Migrations - UpgradeTo450. Here, we use the **Resolve method** of **IEngine**. In this case, we avoid exceptions on the installation process that can be caused by injecting the **LocalizationService** in the constructor.

This way, instead of creating an object using the **new** operator, we let the container do that for us.

I mean, you can actually create your own **CustomerService** implementing the **ICustomerService** interface. And you don’t need to edit a single piece of code in the ProductService in this case. You just have to register your new implementation of the customer service in the container instead. And the container will inject the new implementation of the **CustomerService** to the **ProductService** for you. Isn’t this cool?

Not only services we register using **INopStartup**. The same way, we resolve factories, helpers, classes that provide context, and plugin managers.
