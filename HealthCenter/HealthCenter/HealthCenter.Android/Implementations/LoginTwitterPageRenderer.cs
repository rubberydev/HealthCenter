[assembly: Xamarin.Forms.ExportRenderer(
    typeof(HealthCenter.Views.LoginTwitterPage),
    typeof(HealthCenter.Droid.Implementations.LoginTwitterPageRenderer))]


namespace HealthCenter.Droid.Implementations
{
    using Android.App;
    using HealthCenter.Domain;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Auth;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class LoginTwitterPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            var activity = this.Context as Activity;
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

            activity.StartActivity(auth.GetUI(activity));

            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    var profile = await GetTwitterProfileAsync(eventArgs.Account);
                    App.Navigate_ToProfile(profile, "Twitter");
                }
                else
                {
                    App.HideLoginView();
                }
            };

            
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