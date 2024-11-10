using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

namespace PokeroleBuddyHelper
{
    public class CustomPokemonMoveTemplate : ViewCell
    {
        private Picker _learnedRankPicker;

        public CustomPokemonMoveTemplate()
        {
            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(5),
            };

            var nameEntry = new Entry
            {
                WidthRequest = 150,
                Placeholder = "Move Name"
            };
            nameEntry.SetBinding(Entry.TextProperty, "Name");

            _learnedRankPicker = new Picker
            {
                WidthRequest = 100 // Adjust width as needed
            };
            _learnedRankPicker.SetBinding(Picker.ItemsSourceProperty, new Binding("TrainerRanks", source: new RelativeBindingSource(RelativeBindingSourceMode.FindAncestorBindingContext, typeof(EditPokemonPage))));
            _learnedRankPicker.SetBinding(Picker.SelectedItemProperty, "LearnedRank", BindingMode.TwoWay);

            var removeButton = new Button
            {
                Text = "Remove"
            };
            removeButton.SetBinding(Button.CommandProperty, new Binding("DeleteMoveCommand", source: new RelativeBindingSource(RelativeBindingSourceMode.FindAncestorBindingContext, typeof(EditPokemonPage))));
            removeButton.SetBinding(Button.CommandParameterProperty, ".");

            stackLayout.Children.Add(nameEntry);
            stackLayout.Children.Add(_learnedRankPicker);
            stackLayout.Children.Add(removeButton);

            View = stackLayout;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _learnedRankPicker.BindingContext = BindingContext; // Explicitly set the BindingContext for the Picker
        }
    }
}
