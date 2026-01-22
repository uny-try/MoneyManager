using MoneyManager.ViewModels;

namespace MoneyManager.Views;

public partial class AllTransactionsPage : ContentPage
{
    public AllTransactionsPage(AllTransactionsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AllTransactionsViewModel viewModel)
        {
            viewModel.LoadAllTransactionsCommand.Execute(null);
            viewModel.LoadMonthlyTransactionsCommand.Execute(null);
        }
    }
}
