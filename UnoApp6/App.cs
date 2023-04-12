namespace UnoApp6 {
    public class App : Application {
        private static Window? _window;
        public static IHost? Host { get; private set; }

        protected async override void OnLaunched(LaunchActivatedEventArgs args) {
            var builder = this.CreateBuilder(args)

                // Add navigation support for toolkit controls such as TabBar and NavigationView
                .UseToolkitNavigation()
                .Configure(host => host
#if DEBUG
                // Switch to Development environment when running in DEBUG
                .UseEnvironment(Environments.Development)
#endif
                    .UseConfiguration(configure: configBuilder =>
                        configBuilder
                            .EmbeddedSource<App>()
                            .Section<AppConfig>()
                    )
                    .ConfigureServices((context, services) => {
                        // TODO: Register your services
                        //services.AddSingleton<IMyService, MyService>();
                    })
                    .UseNavigation(RegisterRoutes)
                );
            _window = builder.Window;

            Host = await builder.NavigateAsync<Shell>();
        }

        private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes) {
            views.Register(
                new ViewMap(ViewModel: typeof(ShellViewModel)),
                new ViewMap<Presentation.MainPage, MainViewModel>(),
                new DataViewMap<Presentation.JSONListe, SecondViewModel, Entity>()
            );

            routes.Register(
                new RouteMap("", View: views.FindByViewModel<ShellViewModel>(),
                    Nested: new RouteMap[]
                    {
                    new RouteMap("MainPage", View: views.FindByViewModel<MainViewModel>()),
                    new RouteMap("JSONListe", View: views.FindByViewModel<SecondViewModel>()),
                    }
                )
            );
        }
    }
}