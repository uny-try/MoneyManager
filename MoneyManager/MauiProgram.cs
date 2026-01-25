using System.Globalization;
using Microsoft.Extensions.Logging;
using MoneyManager.Models.Interfaces;
using MoneyManager.Models.Services;
using MoneyManager.ViewModels;
using MoneyManager.ViewModels.Interfaces;
using MoneyManager.ViewModels.Services;

namespace MoneyManager;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
        builder.Services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();
        builder.Services.AddSingleton<ITransactionRepository>(
            _ => new FileTransactionRepository(FileSystem.AppDataDirectory,
                                                _.GetRequiredService<IAccountRepository>(),
                                                _.GetRequiredService<ICategoryRepository>()));
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<AllTransactionsViewModel>();
        builder.Services.AddTransient<EditTransactionViewModel>();
        builder.Services.AddTransient<AccountSummaryViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
