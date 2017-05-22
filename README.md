# MVC-okta-KentorIT
The Goal is to do Authentication and get user login details (name/email), so No Login screen will be shown in MVC app, it should redirect users to okta for login which will be using SAML2 for Authentication, Authorization doesn't need.

Steps performed to create the solution

1) Create a blank MVC5 Solution using VVisual Studio 2013
2) install KentorIT and Dependent nugets
3) Created Developer okta account.
4) Added code in Startup.Auth for Authentication, and Test user login details (PocAppUser@gmail.com/ Password:"Test123!" )
5) Added Metadata .xml file in this solution 

when the user isn't logged-In it will be redirected to Okta, and Okta sends the response back, but it is getting null in loginInfo object always.

 var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
        {
            return RedirectToAction("Login");
        }

