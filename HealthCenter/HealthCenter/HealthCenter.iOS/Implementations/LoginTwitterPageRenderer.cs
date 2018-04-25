[assembly: Xamarin.Forms.ExportRenderer(
    typeof(HealthCenter.Views.LoginTwitterPage),
    typeof(HealthCenter.iOS.Implementations.LoginTwitterPageRenderer))]

namespace HealthCenter.iOS.Implementations
{
    using HealthCenter.Domain;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Auth;
    using Xamarin.Forms.Platform.iOS;

    public class LoginTwitterPageRenderer : PageRenderer
    {
        bool done = false;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (done)
            {
                return;
            }

            var TwitterKey = Xamarin.Forms.Application.Current.Resources["TwitterKey"].ToString();
            var TwitterSecret = Xamarin.Forms.Application.Current.Resources["TwitterSecret"].ToString();
            var TwitterRequestURL = Xamarin.Forms.Application.Current.Resources["TwitterRequestURL"].ToString();
            var TwitterAuthURL = Xamarin.Forms.Application.Current.Resources["TwitterAuthURL"].ToString();
            var TwitterCallbackURL = Xamarin.Forms.Application.Current.Resources["TwitterCallbackURL"].ToString();
            var TwitterURLAccess = Xamarin.Forms.Application.Current.Resources["TwitterURLAccess"].ToString();

            var auth = new OAuth1Authenticator(
                consumerKey: TwitterKey,
                consumerSecret: TwitterSecret,
                requestTokenUrl: new Uri(TwitterRequestURL),
                authorizeUrl: new Uri(TwitterAuthURL),
                callbackUrl: new Uri(TwitterCallbackURL),
                accessTokenUrl: new Uri(TwitterURLAccess));

            auth.Completed += async (sender, eventArgs) =>
            {
                DismissViewController(true, null);
                App.HideLoginView();

                if (eventArgs.IsAuthenticated)
                {
                    var profile = await GetTwitterProfileAsync(eventArgs.Account);
                    App.Navigate_ToProfile(profile,"twitter");
                }
                else
                {
                    App.HideLoginView();
                }
            };

            done = true;
            PresentViewController(auth.GetUI(), true, null);
        }

        public async Task<TwitterResponse> GetTwitterProfileAsync(Account account)
        {
            var TwitterProfileInfoURL = Xamarin.Forms.Application.Current.Resources["TwitterProfileInfoURL"].ToString();
            var requestUrl = new OAuth1Request(
                "GET",
                new Uri(TwitterProfileInfoURL),
                null,
                account);

            var response = await requestUrl.GetResponseAsync();
            return JsonConvert.DeserializeObject<TwitterResponse>(response.GetResponseText());
        }
    }

}