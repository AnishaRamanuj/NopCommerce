# Some common issues

## Disable captcha from Database when accidently Signed-out

-- Run following query


```mysql
Update Setting set Value= 'False' where Name = 'captchasettings.enabled'
```

## Edit contact us page
- In Admin Panel,
```csharp
Content Management> Topics> ContactUs
```

- In Codebase,
```csharp
Nop.Web > Views > Common > ContactUs.cshtml
```
- If you want to change the text of the default nopcommerce contact us page and mail box, then you can do that from:
```csharp
Admin panel > Configurations > Languages > Edit > String Resource
```
