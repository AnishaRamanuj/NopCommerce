## Configure Server Farm with IIS Manager

IS Prerequisites

First of all, we will need to install 2 modules into IIS:

1. ARR  (https://www.iis.net/downloads/microsoft/application-request-routing) and
2. Url Rewrite  (https://www.iis.net/downloads/microsoft/url-rewrite)
Application Request Routing (aka ARR)

ARR enables IIS to work as a reverse proxy in front of a farm of IIS instances. In normal cases the other instances would be on other servers like a true server farm. This is a valid option for blue/green deployments but in our case, we wanted to keep it in our existing infrastructure, so the instances will all be on the same machine.

Url Rewrite

The server farm is not exposed on a port to the outside world by IIS. We need to use the Url Rewrite module to route traffic to the Server Farm instead of directly to the regular web applications. This is what gives us the power to take an instance offline without affecting users.

### NOTE : Once both modules are installed, we restart the IIS management console.

## Configuring IIS

Before we get started with the new IIS configuration, we need to decide a few things:

- What do we call our server farm? There isn’t really a science to choosing this name, it will be used in a few places though so keep it simple and descriptive. (in this example I used “my-website”)
- What ports will each of the instances run on? Make sure they’re available on your server. (e.g. 8888,9999)
- Where will each instance be deployed? IIS uses “C:\inetpub\wwwroot” by default but your company may have different standards.


### Refer following links for more.
- (https://www.offerzen.com/blog/zero-downtime-deployments-in-an-iis-world)
- (https://learn.microsoft.com/en-us/iis/extensions/configuring-application-request-routing-arr/define-and-configure-an-application-request-routing-server-farm)
