using Laba4_Core.ViewModel;
using Laba4_Core.Interface;
using Laba4_web.Components;
using Laba_4;
using Laba4_Web.Services;
using Laba4_Core.Class;
using Blazored.Toast;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

namespace Laba4_web
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF5cWWJCeExwWmFZfVtgfV9EY1ZRRGYuP1ZhSXxWdkBjXn9XcnBVR2BYU0N9XUs=");
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddScoped<CommandFactory>(_ =>
    new         CommandFactory((exec, can) => new RelayCommand(exec)));
            builder.Services.AddBlazoredToast();
            builder.Services.AddScoped<ConcertState>();
            builder.Services.AddScoped<PerformanceState>();
            builder.Services.AddScoped<INavigationService, BlazorNavigationService>();
            builder.Services.AddSyncfusionBlazor();

            builder.Services.AddScoped<MainViewModel>(var  =>
            {
                var factory = var.GetRequiredService<CommandFactory>();
                var navigationService = var.GetRequiredService<INavigationService>();// ← правильно
                var vm = new MainViewModel(factory,navigationService);
                vm.LoadConcertsFromFile();
                return vm;
            });
            builder.Services.AddScoped<Concert_ViewModel>(var =>
            {
                var factory = var.GetRequiredService<CommandFactory>(); // ← правильно
                var vm = new Concert_ViewModel(factory);
                var navigationService = var.GetRequiredService<INavigationService>();
                vm.SetNavigationService(navigationService);
                return vm;
            });
            builder.Services.AddScoped<PerformanceViewModel>(var =>
            {
                var factory = var.GetRequiredService<CommandFactory>(); // ← правильно
                var vm = new PerformanceViewModel(factory);
                var navigationService = var.GetRequiredService<INavigationService>();
                vm.SetNavigationService(navigationService);
                return vm;
            });

           
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
