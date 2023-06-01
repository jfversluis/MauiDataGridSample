using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace MauiDataGridSample
{
    // Plugin repo: https://github.com/akgulebubekir/Maui.DataGrid
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient httpClient = new();

        public bool IsRefreshing { get; set; }
        public ObservableCollection<Monkey> Monkeys { get; set; } = new();
        public Command RefreshCommand { get; set; }
        public Monkey SelectedMonkey { get; set; }

        public MainPage()
        {
            RefreshCommand = new Command(async () =>
            {
                // Simulate delay
                await Task.Delay(2000);

                await LoadMonkeys();

                IsRefreshing = false;
                OnPropertyChanged(nameof(IsRefreshing));
            });

            BindingContext = this;

            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            await LoadMonkeys();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Monkeys.Clear();
        }

        private async Task LoadMonkeys()
        {
            var monkeys = await httpClient.GetFromJsonAsync<Monkey[]>("https://montemagno.com/monkeys.json");

            Monkeys.Clear();

            foreach (Monkey monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }
        }
    }
}