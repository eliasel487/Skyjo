namespace Skyjo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateGame), typeof(CreateGame));
            Routing.RegisterRoute(nameof(ContinueGame), typeof(ContinueGame));
            Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));
            Routing.RegisterRoute(nameof(RulesPage), typeof(RulesPage));
        }
    }
}