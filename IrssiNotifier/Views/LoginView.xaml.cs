﻿using System;
using IrssiNotifier.Interfaces;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace IrssiNotifier.Views
{
	public partial class LoginView
	{
		public LoginView()
		{
			InitializeComponent();
			webBrowser.Navigate(new Uri(App.Baseaddress + "client/login?version="+App.Version));
		}

		private void BrowserNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			var browser = (WebBrowser)sender;
			if (e.Uri.AbsoluteUri.EndsWith("client/login/loginsuccess"))
			{
				var cookies = browser.GetCookies();
				var uri = browser.Source;
				PhoneApplicationService.Current.State["cookies"] = cookies;
				PhoneApplicationService.Current.State["cookiesUri"] = uri;
				var loginPage = App.GetCurrentPage() as IViewContainerPage;
				if (loginPage != null)
				{
					loginPage.View = new RegisterView();
				}
			}
			browser.IsEnabled = true;
			progressBar.IsIndeterminate = false;
		}

		private void WebBrowserNavigating(object sender, NavigatingEventArgs e)
		{
			progressBar.IsIndeterminate = true;
			((WebBrowser)sender).IsEnabled = false;
		}
	}
}
