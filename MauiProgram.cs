using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using OfficeAnywhere.Mobile.Services;
using OfficeAnywhere.Mobile.ViewModels;
using OfficeAnywhere.Mobile.Views;
using UraniumUI;

namespace OfficeAnywhere.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseFFImageLoading()
                .UseUraniumUI()
                .UseUraniumUIMaterial()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Poppins-Medium.ttf", "Poppins");
                    fonts.AddFont("fa-regular-400.ttf", "FARegular");
                    fonts.AddFont("fa-solid-900.ttf", "FASolid");
                });

            // Register services
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<TaskService>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<Login>();

            builder.Services.AddTransient<TaskViewModel>();
            builder.Services.AddTransient<TaskPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
