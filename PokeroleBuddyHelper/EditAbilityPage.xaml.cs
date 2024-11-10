using Microsoft.Maui.Controls;
using PokeroleBuddyHelper.Models;

namespace PokeroleBuddyHelper
{
    public partial class EditAbilityPage : ContentPage
    {
        private Ability _ability;
        private Ability unedittedAbility;

        public EditAbilityPage(Ability ability)
        {
            InitializeComponent();
            _ability = ability;
            BindingContext = _ability;
            unedittedAbility = (Ability)ability.Clone();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Save the ability details
            // You can add any additional logic here if needed
            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            _ability = unedittedAbility;
        }
    }
}