namespace Skyjo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("page1", typeof(Main));
            Routing.RegisterRoute("//page1", typeof(Page1));
            Routing.RegisterRoute("page2", typeof(Page2));
            Routing.RegisterRoute("//page2", typeof(Page2));
            Routing.RegisterRoute(nameof(PartieEnCours), typeof(PartieEnCours));
        }
    }
}