Goal is to do Authentication and get user login name/email id, so NO Login screen will be shown in MVC app, it shouls redirect to okta for login using SAML2, no Authenraization needed.

Steps performed to create the solution

1) Create a blank MVC5 Solution using VVisual Studio 2013
2) install KentorIT and Dependent nugets
3) Created Developer okta account
4) Added code in Startup.Auth for Authentication, and Test user login details (PocAppUser@gmail.com/ Password:"Test123!" )
5) Added Metadata xml file in this solution 


I am newbie in external Authentication,
when user isn't logged-In it redirects to Okta, and but Okta sent the response back, it is getting null in loginInfo object always.

 var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
        {
            return RedirectToAction("Login");
        }

