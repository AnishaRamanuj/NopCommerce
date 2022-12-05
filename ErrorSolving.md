# Some common issues

## Disable captcha from Database when accidently Signed-out

Run following query


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

## Accidently removed access control List from Admin panel

check this on your SQL Server

```mysql
select * from PermissionRecord
where SystemName='ManageACL'
```

the record exists or not , if not exist then please add this thing into the Database,

- If exist then please check that id with PermissionRecord_Role_Mapping table
```mysql
select * from [dbo].[PermissionRecord_Role_Mapping]
```

- if not exist add this
```mysql
INSERT INTO PermissionRecord_Role_Mapping(PermissionRecord_Id,CustomerRole_Id) VALUES (39, 1);
```

also check with the Direct URL
<https://yourstore.com/Admin/Security/Permissions>


