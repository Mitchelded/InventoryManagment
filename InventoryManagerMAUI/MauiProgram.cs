using CommunityToolkit.Maui;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace InventoryManagerMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseSkiaSharp(true)
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            // Инициализация LiveCharts
            LiveCharts.Configure(config =>
                config
                    .AddSkiaSharp() // Подключение визуализатора SkiaSharp
                    .AddDefaultMappers() // Использование стандартных мапперов
                    .AddLightTheme()); // Установка светлой темы

            EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
							handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}