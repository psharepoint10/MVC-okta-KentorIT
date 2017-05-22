using Kentor.AuthServices.Configuration;
using Kentor.AuthServices;
using Kentor.AuthServices.Owin;
using System.IdentityModel.Metadata;

using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using Kentor.AuthServices.WebSso;
using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;
using System.Globalization;
using Kentor.AuthServices.Metadata;

namespace Ken.MVC
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            
            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();

            app.UseKentorAuthServicesAuthentication(CreateAuthServicesOptions());

            //pocappuser@gmail.com  - Test123! - 

        }

        private static KentorAuthServicesAuthenticationOptions CreateAuthServicesOptions()
        {
            var spOptions = CreateSPOptions();
            var authServicesOptions = new KentorAuthServicesAuthenticationOptions(false)
            { SPOptions = spOptions };

            var idp = new IdentityProvider(new EntityId("https://dev-415807-admin.oktapreview.com/app/exkaifpiteoDPMgs40h7/sso/saml/metadata"), spOptions)
            {
                AllowUnsolicitedAuthnResponse = true,
                Binding = Saml2BindingType.HttpRedirect,
                SingleSignOnServiceUrl = new Uri("https://dev-415807-admin.oktapreview.com/")
            };

            idp.SigningKeys.AddConfiguredKey(
                new X509Certificate2(
                    HostingEnvironment.MapPath(
                        "~/App_Data/Kentor.AuthServices.StubIdp.cer")));
            authServicesOptions.IdentityProviders.Add(idp);

            // It's enough to just create the federation and associate it
            // wit the hoptions. The federation will load the metadata and
            // update the options with any identity providers found.
            new Federation("https://localhost:44305/Federation", true, authServicesOptions);
            return authServicesOptions;
        }

        private static SPOptions CreateSPOptions()
        {
            var swedish = CultureInfo.GetCultureInfo("sv-se");

            var organization = new Organization();
            organization.Names.Add(new LocalizedName("Kentor", swedish));
            organization.DisplayNames.Add(new LocalizedName("Kentor IT AB", swedish));
            organization.Urls.Add(new LocalizedUri(new Uri("http://www.kentor.se"), swedish));

            var spOptions = new SPOptions
            {
                EntityId = new EntityId("https://dev-415807.oktapreview.com/app/ginddev415807_kentorauth_1/exkaifpiteoDPMgs40h7/sso/saml"),//
                //EntityId = new EntityId("https://localhost:44305/AuthServices"),//http://www.okta.com/exkaifpiteoDPMgs40h7
                ReturnUrl = new Uri("https://localhost:44305/Account/ExternalLoginCallback"),
               // DiscoveryServiceUrl = new Uri("https://localhost:44305/DiscoveryService"),
                Organization = organization
            };

            var techContact = new ContactPerson
            {
                Type = ContactType.Technical
            };
            techContact.EmailAddresses.Add("authservices@GIT.com");
            spOptions.Contacts.Add(techContact);

            var supportContact = new ContactPerson
            {
                Type = ContactType.Support
            };
            supportContact.EmailAddresses.Add("support@GIT.com");
            spOptions.Contacts.Add(supportContact);

            var attributeConsumingService = new AttributeConsumingService("AuthServices")
            {
                IsDefault = true,
            };

            attributeConsumingService.RequestedAttributes.Add(
                new RequestedAttribute("urn:email")
                {
                    FriendlyName = "email",
                    IsRequired = true,
                    NameFormat = RequestedAttribute.AttributeNameFormatUri
                });

            attributeConsumingService.RequestedAttributes.Add(
                new RequestedAttribute("Minimal"));

            spOptions.AttributeConsumingServices.Add(attributeConsumingService);

            spOptions.ServiceCertificates.Add(new X509Certificate2(
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/App_Data/Kentor.AuthServices.Tests.pfx"));

            return spOptions;
        }
    }
}