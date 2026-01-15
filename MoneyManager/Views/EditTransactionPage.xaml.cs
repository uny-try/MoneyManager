using MoneyManager.ViewModels;

namespace MoneyManager.Views;

public partial class EditTransactionPage : ContentPage
{
    public EditTransactionPage(EditTransactionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}