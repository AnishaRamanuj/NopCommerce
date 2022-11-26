# Some common issues

## Disable captcha from Database when accidently Signed-out

-- Run following query


```mysql
Update Setting set Value= 'False' where Name = 'captchasettings.enabled'
```
