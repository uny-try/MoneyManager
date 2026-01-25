using MoneyManager.ViewModels;

namespace MoneyManager.Views;

public partial class AccountSummaryPage : ContentPage
{
	public AccountSummaryPage(AccountSummaryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AccountSummaryViewModel viewModel)
        {
            viewModel.CulculateAccountSummaryCommand.Execute(null);
        }
    }
}