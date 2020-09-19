using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace covid_calculator
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new IncialPage
			{
				Title="Calculadora covid"
			});
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
