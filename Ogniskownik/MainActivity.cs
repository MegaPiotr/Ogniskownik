using Android.App;
using Android.Widget;
using Android.OS;

namespace Ogniskownik
{
    [Activity(Label = "Ogniskownik", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Title = "Initial commit";
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}

