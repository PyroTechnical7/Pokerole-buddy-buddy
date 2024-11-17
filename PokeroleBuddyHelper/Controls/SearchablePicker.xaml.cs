using System.Collections.ObjectModel;
using System.Linq;

namespace PokeroleBuddyHelper.Controls
{
    public partial class SearchablePicker : ContentView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(ObservableCollection<string>), typeof(SearchablePicker), new ObservableCollection<string>());

        public ObservableCollection<string> ItemsSource
        {
            get => (ObservableCollection<string>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(string), typeof(SearchablePicker), default(string), BindingMode.TwoWay);

        public string SelectedItem
        {
            get => (string)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public SearchablePicker()
        {
            InitializeComponent();
            SuggestionsListView.ItemsSource = ItemsSource;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                SuggestionsListView.IsVisible = false;
            }
            else
            {
                var filteredItems = ItemsSource.Where(item => item.ToLower().Contains(searchText.ToLower())).ToList();
                SuggestionsListView.ItemsSource = filteredItems;
                SuggestionsListView.IsVisible = filteredItems.Any();
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                SelectedItem = e.SelectedItem.ToString();
                SearchEntry.Text = SelectedItem;
                SuggestionsListView.IsVisible = false;
            }
        }
    }
}