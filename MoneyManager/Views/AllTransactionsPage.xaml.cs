using MoneyManager.ViewModels;

namespace MoneyManager.Views;

public partial class AllTransactionsPage : ContentPage
{
    public AllTransactionsPage(AllTransactionsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
