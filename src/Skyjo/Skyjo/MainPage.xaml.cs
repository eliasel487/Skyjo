namespace Skyjo
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCreateGameClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateGame));
        }

        private async void OnContinueGameClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ContinueGame));
        }
    }
}
