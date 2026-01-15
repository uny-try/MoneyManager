using Microsoft.Extensions.DependencyInjection;

namespace MoneyManager;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		// グローバル例外ハンドラーの設定
		AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
		{
			System.Diagnostics.Debug.WriteLine($"Unhandled exception: {e.ExceptionObject}");
		};

		TaskScheduler.UnobservedTaskException += (sender, e) =>
		{
			System.Diagnostics.Debug.WriteLine($"Unobserved task exception: {e.Exception}");
			e.SetObserved();
		};
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}