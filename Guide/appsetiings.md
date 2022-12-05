## Application configuration (appsettings.json file)

The appsettings.json file is located in the App_Data folder, which is in the Nop.Web project. If you don’t see this file in the mentioned folder, just run the solution, and this file will be created automatically. If it doesn’t help click the  **Show all files** button and the hidden files will be shown.

The appsettings.json file is used to store the application configuration settings, application scope global variables, and many other information.

Before starting to describe this file, I will note that you can also edit these settings from your nopCommerce store’s admin area. To do this, go to **admin area – Settings – App settings**. Switch to the advanced mode if needed and find the setting you want to edit. In case you edit the settings below from the admin area, the appsettings.json file will be overridden, and the application should be restarted.

Let’s get back to the appsettings.json file.

## ConnectionStrings

First of all, let’s discuss the **ConnectionStrings** section. This section contains information about the database connection.

The **“ConnectionString”** is filled in during the nopCommerce installation process. The “Data Source” is your server IP, URL, or name. The “Initial Catalog” is your database name. The other parameters represent the security settings.

The **“DataProvider”** setting represents your server type.

**“SQLCommandTimeout”** is the duration time-out for the SQL commands.

## Azure Blob configuration

Let’s move on to the Azure Blob configuration section. We can use Azure Blob Storage to store blob data. nopCommerce already has a feature integrated for that. You just need to set the following information correctly to use it. Values for these settings can be obtained from Azure while creating a storage account.

The **“ConnectionString“** setting expects a string value. In this setting, you need to write your AzureBlobStorage connection string.

**“ContainerName“** is also of string type. In this setting, we set the container name for the Azure BLOB storage.

In the **“EndPoint“** setting, we need to set the endpoint for the Azure BLOB storage.

The **“AppendContainerName“** setting expects a boolean value. Set the value to "true" or "false" based on whether the Container Name should be appended to the endpoint when constructing the URL.

The next setting is **“StoreDataProtectionKeys.“** This setting expects a boolean value as well. Set the value to "true” if you want to use the Windows Azure BLOB storage for Data Protection Keys.

The **“DataProtectionKeysContainerName“** setting expects a string value. Here, you need to set up an Azure container name for storing Data Protection Keys. This container should be private and separate from the container used for media.

The **“DataProtectionKeysVaultId“** is an optional parameter. This setting also expects a string value. Set the Azure key vault ID if you need to encrypt the Data Protection Keys.

## Cache configuration

Then, let’s discuss cache configuration. Three parameters allow you to configure the nopCommerce cache.

The **“DefaultCacheTime“** setting determines the lifetime of the cached data. The default value is 60 minutes.

**“ShortTermCacheTime.“** In some cases, a shorter period is required for cache data than the default. This setting is applied to caching addresses, generic attributes, customers, etc.

The **“BundledFilesCacheTime“** setting is used when activating the function of combining CSS and JS files into one. This setting allows you to control their cache lifetime.



## Common configuration

Next, we see the **“Common configuration”** section allowing you to change the behavior of nopCommerce itself. It contains such settings as:

**“DisplayFullErrorStack,“** which is set to "false" by default. You can set the value to "true" if you want to see the full error in the production environment. Which is actually not recommended. 

For the development environment, this setting is ignored and, whatever the value you set, a full error will be shown.

The **“UserAgentStringsPath“** setting stores the location/path for the “Browscap.xml” file indicating a browser capabilities database. It's essentially a list of all known browsers and bots, along with their default capabilities and limitations.

Then, the **“CrawlerOnlyUserAgentStringsPath“** setting stores the location/path of the “browscap.crawlersonly.xml” file. It stores crawler user agents only.

The **“UseSessionStateTempDataProvider“** setting is set to "false" because we use the cookie-based “TempData“ provider to store TempData in cookies by default. You might want to set the value to "true" if you want to store TempData in the session state.

The **“MiniProfilerEnabled“** setting allows you to enable MiniProfiler, a performance appraisal tool.

**“ScheduleTaskRunTimeout“** allows you to set up a running schedule task timeout in milliseconds. Set null to use the default value.

The **“StaticFilesCacheControl“** setting specifies the value of the “Cache-Control“ header for static content (in seconds).

The **“SupportPreviousNopcommerceVersions“** setting specifies the value indicating whether we should support previous nopCommerce versions. In this case, old URLs from previous nopCommerce versions will redirect to the new ones. Only enable it if you have upgraded from one of the previous nopCommerce versions.

In the **“PluginStaticFileExtensionsBlacklist“** setting, you can specify the blacklist of static file extensions for plugin directories.

The **“ServeUnknownFileTypes”** setting determines whether the file with the not a recognized content-type should be served.

## Distributed cache configuration
Then, in the “Distributed cache configuration” section, as you may guess, you can configure the distributed cache.

Distributed cache is a cache shared by multiple app servers and typically maintained as an external service to the app servers that access it. A distributed cache can improve the performance and scalability of an ASP.NET Core app, especially when the app is hosted by a cloud service or a server farm.

You can choose from three types of “DistributedCacheType” implementations:

The first option is “Memory.” This is a framework-provided implementation of IDistributedCache that stores items in memory. The Distributed Memory Cache isn't an actual distributed cache. Cached items are stored by the app instance on the server where the app is running.

The second option is “SQL Server.” The “Distributed SQL Server Cache” implementation allows the distributed cache to use an SQL Server database as its backing store. To create an SQL Server cached item table in an SQL Server instance, you can use the SQL-cache tool. The tool creates a table with the name and schema you specify. It is recommended to use a separate database for this purpose.

And the last option is “Redis.” Redis is an open-source in-memory data store often used as a distributed cache. nopCommerce supports Redis out of the box.


Then you can enable distributed cache by setting this value to “true.” By default, nopCommerce uses “In-Memory” caching, so if you want to use Redis, for example, set the value to “true.”

The “ConnectionString” is an optional setting. This setting is only used in conjunction with Redis or SQL Server. The setting expects a string value. The default value is the following.

The “SchemaName“ is an optional setting as well and is only used in conjunction with SQL Server.

The “TableName“ is an optional setting and is only used in conjunction with SQL Server as well. Enter the SQL Server database name here.

## Hosting configuration

Then comes the “Hosting configuration” section that contains the settings used to configure the hosting behavior.

The “UseProxy” setting expects a boolean value. Enable this setting to apply forwarded headers to their matching fields on the current HTTP request.

“ForwardedProtoHeaderName” expects a string value. Specify a custom HTTP header name to determine the originating IP address.

The “ForwardedForHeaderName” setting expects a string value. Specify a custom HTTP header name for identifying the protocol (HTTP or HTTPS) that a client should use to connect to your proxy or load balancer.

“KnownProxies” expects a string value. Specify a comma-separated list of IP addresses to accept forwarded headers.

## Installation configuration

The “Installation configuration” section is used to configure the behavior of nopCommerce during its installation process.

The “DisableSampleData“ setting indicates whether a store owner can install sample data during installation. If you don't want the store owner to install sample data during installation, just set the value to "true."

The “DisabledPlugins“ setting expects a string value. Specify a comma-separated list of plugins ignored during installation.

Enable the “InstallRegionalResources“ setting to download and set up the regional language pack during installation.



## Plugin configuration

Well, let’s move on to the “Plugin configuration” section. In this section, you can configure how to work with the plugin assemblies.

The “ClearPluginShadowDirectoryOnStartup“ setting allows you to clear the “Plugins/bin” directory during application startup when set to “true.”

The “CopyLockedPluginAssembilesToSubdirectoriesOnStartup“ setting can be set to "true" if you want to copy "locked" assemblies from the “Plugins/bin” directory to temporary subdirectories during application startup.

The “UseUnsafeLoadAssembly“ setting allows you to load an assembly into the load-from context bypassing some security checks when set to “true.”

The “UsePluginsShadowCopy“ is set to "true" in order to copy plugin libraries to the “Plugins/bin” directory during application startup.



## WebOptimizer

Well, the next section I want to discuss is called “WebOptimizer”. In this section, you can set up JavaScript bundling and minification and how to cache it. We use the WebOptimizer tool for minification and bundling of CSS and JavaScript code. This is an ASP.NET Core middleware. Optimization is performed at runtime with server-side and client-side caching for high performance. So let’s discuss the settings below.

EnableJavaScriptBundling expects a boolean value. You can set it to true if you want JavaScript file bundling and minification to be enabled.

EnableCssBundling expects a boolean value as well. Set it to true if you want CSS file bundling and minification to be enabled.

JavaScriptBundleSuffix expects a string. You can set a custom suffix for the js-file name of generated bundles (it is .scripts by default).

CssBundleSuffix expects a string as well. You can set a custom suffix for the CSS file name of generated bundles (it is .styles by default).

The EnableCaching setting indicates whether the server-side caching is enabled.

EnableMemoryCache indicates whether the Memory Cache is enabled. We will talk about caching in more detail later.

EnableDiskCache determines whether the pipeline assets should be cached to a disk. This can speed up application restarts by loading pipeline assets from the disk instead of re-executing the pipeline. It’s better to disable it in the Developer mode.

The CacheDirectory setting expects a string. It indicates the directory where assets will be stored if EnableDiskCache is true.

The other settings that I haven’t discussed are related to the WebOptimizer library internal processes. You can learn more about them in the WebOptimizer documentation - I have provided a link to it in the longread. I would not recommend editing them without a deep understanding of the WebOptimizer tool.



## Add your own parameters

Then, you can also add your own parameters to this appsettings.json file if needed. For example, specify “MySection” where the value of "MyParam" is “false.”

  "MySection": {

    "MyParam": false

  }



Then, these parameters can be accessed through the Configuration dictionary of the AppSettings class. This class is placed in the Libraries – Nop.Core – Configuration folder. Through the Configuration dictionary, you can access all the parameters of the appsettings.json file.

You can also override the appsettings parameters using the environment variables. To do this, open the Nop.Web properties. And in the Debug section click “Open debug launch profiles UI”. In this window, you can specify the environment variables separated by a comma. Variables names and values chould be separated with an equal sign, as here.

You can also create separate appsettings files for different hosting environments, such as Development, Staging, or Production. So the file name will look like this: appsettings.{Environment}.json, where Environment is the application’s current hosting environment. So:

In development, the “appsettings.Development.json” configuration overwrites values found in “appsettings.json”.

In production, the values from the “appsettings.json” file will be overwritten by the “appsettings.Production.json” file. For example, it can be used when deploying the app to Azure.


This way, using the appsettings.json file provides us the flexibility of the application setting.



