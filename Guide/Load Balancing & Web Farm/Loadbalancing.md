#  Load balancing and web farms

Load balancing refers to efficiently distributing incoming network traffic across a group of backend servers. A load balancer acts as a “traffic cop” in front of your servers, routing client requests across all servers capable of fulfilling them, maximizing speed and capacity utilization, and ensuring that no server is overloaded, which could degrade performance. If a single server goes down, the load balancer redirects traffic to the remaining online servers.

A web farm, in turn, is a group of two or more web servers (or nodes) that host multiple instances of an app. When requests from users arrive to a web farm, a load balancer distributes the requests to the web farm's nodes.

There are several ways to configure load balancing in nopCommerce:

1. You can use cloud-based auto-scaling appliances like Microsoft's Azure Web Apps we discussed previously in the “Installing on Microsoft Azure” section;

2. Or you can configure load balancing with IIS web farms. And this approach we will discuss next.

-  I’m going to discuss the nopCommerce settings you need to consider when setting up web farms. And also the web farm configuration that will allow you to distribute a nopCommerce workload across many nodes.

So Microsoft suggests two ways to build a web farm with IIS servers:

Local content infrastructure;

And Shared network content infrastructure.

- nopCommerce supports both ways and handles content replication by using Distributed File System Replication if you use Local content infrastructure or a central location to manage the content with Shared network content infrastructure.

## nopCommerce configuration
First of all, you have to configure the initial settings of your web farm in IIS and add the first instance of your nopCommerce store there. For more information, I added a link to the Microsoft documentation in the attachments (https://docs.microsoft.com/en-us/iis/extensions/configuring-application-request-routing-arr/define-and-configure-an-application-request-routing-server-farm).

As you can see, for demonstration, I have created a web farm locally. 

So then, you have to configure a few settings in the nopCommerce admin area to allow nopCommerce to work with web farms:

1. Go to the **Configuration – Settings – All settings page**. Find the m**ediasettings.useabsoluteimagepath setting** and change its value to false;

2. Then, open the appsettings file or go to the **Configuration – Settings – App settings page** and find the **Distributed cache configuration** settings. Tick the **Use distributed cache** checkbox and choose the option you prefer:



1) **Redis**. In this case, you just need to enter the connection string to your Redis server below;

2) Or **SQL Server**. I will use this option. In this case, we need to prepare a new table in our database first. This is a special SQL Server cached item table that needs to be created manually in an SQL Server instance. It is recommended to use a separate database for this purpose. But just for demonstration, I will do this in my current database.

1. First, let’s install the “dotnet sql-cache” using the following command (dotnet tool install --global dotnet-sql-cache). Wait a bit untill it’s done;

2. And after this, create a new table for caching using the “dotnet sql-cache create” (dotnet sql-cache create "Data Source=DESKTOP-1HPG1O0;Initial Catalog=nopwf;Integrated Security=True" dbo DistributedCache) command the following way: first, we specify the connection string. In my case, it’s the same connection string as in my app. But I want to note one more time that it’s recommended to use a separate database for this purpose. Then come schema name and table name. 

Now our table is created successfully. Let’s continue configuring the distributed SQL Server cache. I’m going to use the same parameters as we used when creating the cache table;

Let’s specify the **Connection string**;
**Schema name**;
**Table name**;

3. And after setting this up, we can **use the distributed SQL Server cache** in our nopCommerce app.

4. Then, since our web farm will utilize Application Request Routing to control traffic using a proxy server, tick the **Use proxy servers** checkbox;

5. After we click the **Save** button the application will be restarted.

## Web farm configuration
Now, let’s proceed to the web farm configuration. The rules I will describe next can be added in two ways:

1. You can include them in the **applicationHost.config** file, in the s**ystem.webServer/rewrite/globalRules section**;

2. Or you can use the URL Rewrite script section in IIS Manager.



I will use the second way.

## Admin area redirection rule
So first, let’s set up the admin area redirection rule. Since a web farm hosts multiple instances of an application, we need to choose one of the nopCommerce instances as a primary one to avoid file locking. This primary node will process requests from the admin panel.

So let’s open the Routing Rules of our web farm. Then click Url Rewrite and choose Add Rule:

1.Choose blank rule;

2. Let’s name this rule “Admin area”;

3. The requested URL should match the pattern;

4. And we will use regular expressions to specify the pattern;

5. The pattern itself should look like this. This rule and the other rules I will describe later you can find in the attachments to this video;

6. The ignore case checkbox should be ticked;

7. And let’s proceed to Conditions:

1) The logical grouping is Match all;

2) Then click Add;

3) The condition input is HTTP HOST;

4) Choose the type Does not match the pattern;

5) And the pattern should look the following way. Where instance.webfarm.local is the address of our primary instance;

6) Click OK;

7) Then, tick the Track capture groups across conditions;

8) Then, add an action. It should have the Rewrite type;

9) And the rewrite URL is the following. Where instance.webfarm.local is also the address of our primary instance;

10) Don’t forget to tick the Stop processing of subsequent rules checkbox;

So, if we open the applicationHost.config file we will see that our rule looks the following way:

```csharp
<rule name="Admin Area" stopProcessing="true">

    <match url="^(admin(/.*)?)$|^(lib_npm/.+)$" />

    <conditions>

        <add input="{HTTP_HOST}" pattern="^instance\.webfarm\.local$" negate="true" />

    </conditions>

    <action type="Rewrite" url="http://instance.webfarm.local/{R:0}" />

</rule>
```

## Load balancing rules

After we set up the web farm, we need to configure a load balancing rule. Note that the order of the rules matters. So the next rule should be added after the previously discussed one. Well, we should add a condition that prevents handling requests intended for the primary node. These are admin area requests in our case. To do this, let’s specify the following rule:

1. In this name, ARR is Application Request Routing;

2. Then, enter the pattern;

3. Next, add a new condition. Where the condition input is PATH INFO;

4. Choose the type Does not match the pattern;

5. Then, specify the pattern;

6. And click OK;

7. Then, in the action, specify this URL. Where webfarm is the web farm name;

8. And choose Stop processing;

9. Then click Apply;

10. As you can see, in the applicationHost.config file, our new rule comes after the Admin area rule, and this is the correct order;


```csharp
<rule name="ARR_webfarm-local_loadbalance" stopProcessing="true">

    <match url=".*" />

    <conditions>

        <add input="{PATH_INFO}" pattern="^(/admin(/.*)?)$|^(/lib_npm/.+)$" negate="true" />

    </conditions>

    <action type="Rewrite" url="" />

</rule>
```

In some cases, Application Request Routing has issues handling URLs of javascript files containing more than one “.” symbol. For example, it can affect minified JavaScript files ending with “.min.js.” To avoid errors when processing such files, we recommend directly routing these requests to one of the nopCommerce instances. As you can see in the examples I provided, we do this for the whole lib_npm directory by routing to the primary instance.

So, when the configuration is finished, we can add new instances to our web farm.

As I previously mentioned, I created a local web farm for demonstration only. But the rules described previously are intended to use in a real web farm with multiple servers. So if you apply them locally, as I did, it could require the additional setup to make it work.

## File replication
And a few words about file replication. When you start the configuration of file replication, please make sure that the following folders of the primary instance are set up to support replication to all other nodes. They are the App_Data, Plugins, Themes, and wwwroot folders.





So, that’s all you need to do to configure load balancing for nopCommerce. And note that all actions that assume restarting the nopCommerce application (for example, plugin installation, updating of the app settings, etc.) require manual restarting of all application pools related to a web farm.
