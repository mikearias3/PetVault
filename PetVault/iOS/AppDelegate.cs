using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using UIKit;
using Microsoft.WindowsAzure;

namespace petvault.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            var statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                statusBar.BackgroundColor = UIColor.FromRGB(0, 69, 63);
                statusBar.TintColor = UIColor.White;
                app.StatusBarStyle = UIStatusBarStyle.LightContent;
            }

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
        public class iOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                
            }
        }
    }
}
