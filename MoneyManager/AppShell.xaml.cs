using MoneyManager.Views;

namespace MoneyManager;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(EditTransactionPage), typeof(EditTransactionPage));
	}
}
