using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace MauiDataGridSample
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Monkey> Monkeys { get; set; } = new();
        private readonly HttpClient httpClient = new();

        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                var monkeys = await httpClient.GetFromJsonAsync<Monkey[]>("https://montemagno.com/monkeys.json");

                foreach(Monkey monkey in monkeys)
                {
                    Monkeys.Add(monkey);
                }
            });
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}