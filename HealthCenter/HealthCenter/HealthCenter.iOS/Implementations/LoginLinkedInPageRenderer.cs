[assembly: Xamarin.Forms.ExportRenderer(
    typeof(HealthCenter.Views.LoginLinkedInPage),
    typeof(HealthCenter.iOS.Implementations.LoginLinkedInPageRenderer))]


namespace HealthCenter.iOS.Implementations
{
    using HealthCenter.Domain;
    using HealthCenter.Services;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Auth;
    using Xamarin.Forms.Platform.iOS;

    public class LoginLinkedInPageRenderer : PageRenderer
    {
        bool done = false;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (done)
            {
                return;
            }

            var LinkedInKey = Xamarin.Forms.Application.Current.Resources["LinkedInKey"].ToString();
            var LinkedInSecret = Xamarin.Forms.Application.Current.Resources["LinkedInSecret"].ToString();
            var LinkedInScope = Xamarin.Forms.Application.Current.Resources["LinkedInScope"].ToString();
            var LinkedInAuthURL = Xamarin.Forms.Application.Current.Resources["LinkedInAuthURL"].ToString();
            var LinkedInCallbackURL = Xamarin.Forms.Application.Current.Resources["LinkedInCallbackURL"].ToString();
            var LinkedInURLAccess = Xamarin.Forms.Application.Current.Resources["LinkedInURLAccess"].ToString();

            var auth = new OAuth2Authenticator(
                clientId: LinkedInKey,
                clientSecret: LinkedInSecret,
                scope: LinkedInScope,
                authorizeUrl: new Uri(LinkedInAuthURL),
                redirectUrl: new Uri(LinkedInCallbackURL),
                accessTokenUrl: new Uri(LinkedInURLAccess));

            auth.Completed += async (sender, eventArgs) =>
            {
                DismissViewController(true, null);
                App.HideLoginView();

                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var profile = await GetLinkedInProfileAsync(accessToken);
                    App.__NavigateToProfile(profile);
                }
                else
                {
                    App.HideLoginView();
                }
            };

            done = true;
            PresentViewController(auth.GetUI(), true, null);
        }

        public async Task<LinkedInResponse> GetLinkedInProfileAsync(string accessToken)
        {
            var LinkedInProfileInfoURL = Xamarin.Forms.Application.Current.Resources["LinkedInProfileInfoURL"].ToString();
            var requestUrl = string.Format("{0}&oauth2_access_token={1}",
                LinkedInProfileInfoURL,
                accessToken);

            var apiService = new ApiService();
            return await apiService.GetLinkedIn(requestUrl);
        }
    }
}
