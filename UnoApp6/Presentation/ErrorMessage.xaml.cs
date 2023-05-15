namespace UnoApp6.Presentation {

    public sealed partial class ErrorMessage : Page {
        public ErrorMessage() {
            this.InitializeComponent();
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            Error.Text = e.Parameter.ToString();

            base.OnNavigatedTo(e);
        }
    }
}



