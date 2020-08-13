using System.Collections.Generic;
using MarcTron.Plugin;
using Xamarin.Forms;

namespace SampleMTAdmob
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CrossMTAdmob.Current.UserPersonalizedAds = true;
            CrossMTAdmob.Current.UseRestrictedDataProcessing = true;
            CrossMTAdmob.Current.AdsId = "ca-app-pub-3940256099942544/6300978111";
            CrossMTAdmob.Current.TestDevices = new List<string>() {"YOUR PHONE IDS HERE", "", ""};

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
