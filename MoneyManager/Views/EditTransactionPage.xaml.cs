using MoneyManager.ViewModels;

namespace MoneyManager.Views;

[QueryProperty(nameof(TransactionIdString), nameof(EditTransactionViewModel.TransactionId))]
public partial class EditTransactionPage : ContentPage
{
    public string? TransactionIdString { get; set; }
    public EditTransactionPage(EditTransactionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (TransactionIdString != null)
        {
            if (Guid.TryParse(TransactionIdString, out Guid transactionId))
            {
                ((EditTransactionViewModel)BindingContext).LoadTransaction(transactionId);
            }
        }
        else
        {
            ((EditTransactionViewModel)BindingContext).LoadTransaction();
        }
    }
}