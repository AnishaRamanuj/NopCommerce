##  Understanding of the web.config file

The web.config is a text-based XML file. It is read by IIS and the ASP.NET Core Module to configure an application hosted with IIS. It contains various settings that define a website. These settings are separate from the application code to allow configuring them independently.

And the main benefit of this is that if we want to change some configuration settings, we do not need to restart the application to apply new changes. ASP.NET automatically detects the changes and applies them to the running application.

The ASP.NET framework uses a hierarchical configuration system. You can place a web.config file in any subdirectory of an application. The file then applies to any pages located in the same directory or any subdirectories.

In nopCommerce, you can find the web.config file in the Presentation/Nop.Web directory. If your solution is a fresh installation of nopCommerce, then the content of that file looks the following way.

1. Every configuration rule goes inside the "configuration" element.

2. The “system.webServer” element specifies the root element for many of the site-level and application-level configuration settings for IIS. It contains configuration elements that define the settings used by the webserver engine and modules.

3. The “modules” element defines the native-code and managed-code modules registered for an application. We commonly use modules to implement customized functionality.

The “modules” element can contain a collection of “add,” “remove,” and “clear” elements. In our web.config file, we use the “remove” element to remove the WebDAV module from the application.
The “handlers'' element defines the handlers registered for a specific file name extension or URL. They are configured to process requests to specific content, typically to generate a response for the requested resource. For example, an ASP.NET Web page is a handler type. You can use handlers to process requests to any resource that needs to return information to users and is not a static file.

4. The “handlers” element can also contain a collection of “add,” “remove,” and “clear” elements, each defining a handler mapping for the application. The “add” element adds a handler to the collection of handlers, the “remove” element removes references of a handler from the handler collection, and the “clear” element removes all references of handlers from the handler collection. As you can see, in our web.config file, we remove the WebDAV module from handlers and add a handler for AspNetCoreModuleV2.

5. In the “aspNetCore” element you can define the following stuff:

      1. The request timeout.

      2. Then, “processPath” and “arguments” with these default values, which will be automatically overridden during the publishing process.

      3. “forwardWindowsAuthToken” should be set to true if you want Windows Authentication to be working in IIS.

      4. Then, setting “stdoutLogEnabled” to true allows you to enable the application log. You can find the log files in the Presentation\Nop.Web\logs\stdout folder.

      5. Then, “startupTimeLimit” is already set to 3600.

      6. And, as you can see, we use the in-process hosting model.

6. Then, in the “httpProtocol” section, we see “customHeaders.” This element specifies custom HTTP headers that IIS will return in HTTP responses from the webserver.

HTTP headers are “name and value” pairs returned in the response from the webserver. Custom response headers are sent to the client together with the default ones. Unlike redirect response headers, which are only returned in response in case of redirection, custom response headers are returned in every response. Here, we also use “add,” “remove,” and “clear” elements to set up headers as needed. You can see why we added some custom headers by clicking the link in the appropriate comment. It is mainly made for security purposes. 


The next interesting part about the web.config is that you can set up your own configuration parameters in addition to the existing ones. For example, using the web.config file, you can configure the redirect rules in IIS.

A redirect rule enables more than one URL to point to a single web page. There may be several reasons why you would want to redirect requests from one page to another or even from one server to another. 

For example, your company name is changed, and you wish to register a new domain for the store. Then you move your website to this new domain but still want the links related to your old domain name to be available. In this case, you can set up redirect rules to allow redirection of all requests from your old domain to the new one.

To enable our website to use redirect rules, we need to [install the URL rewrite module](https://www.iis.net/downloads/microsoft/url-rewrite), which is an extension to IIS. 

Let’s say we have to redirect requests from our old site to the new one. To do this, we need to add the following rules to the web.config file:

```config
<rewrite>

  <rules>

     <rule name="MyRule" stopProcessing="true">

     <match url="(.*)" />

     <conditions logicalGrouping="MatchAny" trackAllCaptures="false">

        <add input="{HTTP_HOST}{REQUEST_URI}" pattern="myoldstore.com" />

     </conditions>

     <action type="Redirect" url="http://[mynewstore.com]/{R:1}" redirectType="Permanent"/>

     </rule>

  </rules>

</rewrite>

```

Here, the “name” is a rule name.

“stopProcessing” set to true means that IIS will stop executing additional rules if the condition specified inside the rule matches the current request.

This rule has a pattern, an action, and a set of conditions.

The pattern implemented in the “match” element tells the Rewrite extension which URLs to execute the rule for. Patterns are written using regular expressions in the URL attribute. In our case, we defined that this rule will match each URL.

The “conditions” collection in our example is used to specify additional logical operations to perform if a URL string matches the rule pattern. Here, we check for certain values of HTTP headers.

The “action” element tells IIS what to do about requests matching the pattern. In our case, we redirect the old matching URL to a new one.

For more information about the web-config file, see the [Microsoft documentation](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/web-config?view=aspnetcore-6.0).
