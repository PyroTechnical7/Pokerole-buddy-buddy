using Microsoft.Maui.Controls;

namespace PokeroleBuddyHelper
{
    public partial class MultiLineInputPage : ContentPage
    {
        public string InputText { get; private set; }

        public MultiLineInputPage()
        {
            InitializeComponent();
        }

        private async void OnOkClicked(object sender, EventArgs e)
        {
            InputText = InputEditor.Text;
            await Navigation.PopModalAsync(true);
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            InputText = null;
            await Navigation.PopModalAsync(true);
        }
    }
}