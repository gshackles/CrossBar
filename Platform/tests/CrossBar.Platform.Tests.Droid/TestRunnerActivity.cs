using System;
using System.Reflection;
using Android.App;
using Android.NUnitLite.UI;
using Android.OS;

namespace CrossBar.Platform.Tests.Droid
{
    [Activity(Label = "CrossBar.Platform.Tests.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class TestRunnerActivity : RunnerActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Add(Assembly.GetExecutingAssembly());
            Add(typeof(Amarillo.IntegrationTests.IntegrationTestsBase).Assembly);

            base.OnCreate(bundle);
        }
    }
}