[assembly: Xamarin.Forms.ExportRenderer(
    typeof(HealthCenter.Views.LoginLinkedInPage),
    typeof(HealthCenter.Droid.Implementations.LoginLinkedInPageRenderer))]


namespace HealthCenter.Droid.Implementations
{
    using Android.App;
    using Domain;
    using Services;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Auth;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class LoginLinkedInPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            var activity = this.Context as Activity;

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

            activity.StartActivity(auth.GetUI(activity));
        }

        public async Task<LinkedInResponse> GetLinkedInProfileAsync(string accessToken)
        {
            var LinkedInProfileInfoURL = Xamarin.Forms.Application.Current.Resources["LinkedInProfileInfoURL"].ToString();
            var requestUrl = string.Format("{0}&oauth2_access_token={1}",
                LinkedInProfileInfoURL,
                accessToken);

            var apiService = new ApiService();
            return await apiService.GetLinkedIn(requestUrl);

            //var httpClient = new HttpClient();
            //var userJson = await httpClient.GetStringAsync(requestUrl);
            //return JsonConvert.DeserializeObject<LinkedInResponse>(userJson);
        }
    }

}