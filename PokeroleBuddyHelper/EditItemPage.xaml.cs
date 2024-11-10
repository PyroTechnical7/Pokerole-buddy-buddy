using Microsoft.Maui.Controls;
using PokeroleBuddyHelper.Models;

namespace PokeroleBuddyHelper
{
    public partial class EditItemPage : ContentPage
    {
        private Item _item;
        private readonly Item unedittedItem;

        public Item Item
        {
            get { return _item; }
            set { _item = value; OnPropertyChanged(nameof(Item)); }
        }

        public EditItemPage(Item item)
        {
            InitializeComponent();
            _item = item;
            unedittedItem = (Item) item.Clone();
            BindingContext = this;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Save the item details
            // You can add any additional logic here if needed
            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            Item = unedittedItem;
        }
    }
}