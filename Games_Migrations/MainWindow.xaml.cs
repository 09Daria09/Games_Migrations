using GamesLibrary;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Games_Migrations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Game> Games { get; set; }
        public List<GameMode> GameMode { get; set; }  
        public MainWindow()
        {
            InitializeComponent();
            LoadGameData();
            LoadGameModesAsync();
        }
        private async void LoadGameModesAsync()
        {
            using (var context = new GamesContext())
            {
                if (!await context.GameMode.AnyAsync())
                {
                    var gameModes = new List<GameMode>
            {
                new GameMode { Mode = "Однопользовательский" },
                new GameMode { Mode = "Многопользовательский" }
            };

                    context.GameMode.AddRange(gameModes);
                    await context.SaveChangesAsync();
                }
            }
        }

        private async void LoadGameData()
        {
            using (var context = new GamesContext())
            {
                if (!await context.Games.AnyAsync())
                {
                    var games = new List<Game>
                    {
                       new Game { Name = "Ведьмак 3: Дикая Охота", Studio = "CD Projekt Red", Style = "Экшен RPG", ReleaseDate = new DateTime(2015, 5, 19) },
                       new Game { Name = "Киберпанк 2077", Studio = "CD Projekt Red", Style = "Экшен RPG", ReleaseDate = new DateTime(2020, 12, 10) },
                       new Game { Name = "Элден Ринг", Studio = "FromSoftware", Style = "Экшен RPG", ReleaseDate = new DateTime(2022, 2, 25) },
                       new Game { Name = "Хало Бесконечность", Studio = "343 Industries", Style = "Шутер", ReleaseDate = new DateTime(2021, 12, 8) },
                       new Game { Name = "Бог Войны Рагнарёк", Studio = "Santa Monica Studio", Style = "Приключенческий экшен", ReleaseDate = new DateTime(2022, 11, 9) },
                       new Game { Name = "Red Dead Redemption 2", Studio = "Rockstar Games", Style = "Приключенческий экшен", ReleaseDate = new DateTime(2018, 10, 26) },
                       new Game { Name = "The Legend of Zelda: Breath of the Wild", Studio = "Nintendo", Style = "Приключенческий экшен", ReleaseDate = new DateTime(2017, 3, 3) },
                       new Game { Name = "Майнкрафт", Studio = "Mojang Studios", Style = "Песочница", ReleaseDate = new DateTime(2011, 11, 18) },
                       new Game { Name = "Grand Theft Auto V", Studio = "Rockstar Games", Style = "Приключенческий экшен", ReleaseDate = new DateTime(2013, 9, 17) },
                       new Game { Name = "Фортнайт", Studio = "Epic Games", Style = "Королевская битва", ReleaseDate = new DateTime(2017, 7, 21), SoldCopies = 20000000, GameModeId = 1}
                    };

                    context.Games.AddRange(games);
                    await context.SaveChangesAsync();
                }
                Games = await context.Games.Include(g => g.GameMode).ToListAsync();

                GamesListView.ItemsSource = Games;
            }
        }

        private void MenuItem_Add_Click(object sender, RoutedEventArgs e)
        {
            var addGame = new Add();
            addGame.ShowDialog();

            LoadGameData();
        }
        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedGame = GamesListView.SelectedItem as Game;
            if (selectedGame != null)
            {
                var result = System.Windows.MessageBox.Show($"Вы действительно хотите удалить игру \"{selectedGame.Name}\"?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new GamesContext())
                    {
                        context.Games.Remove(selectedGame);
                        await context.SaveChangesAsync();
                    }

                    var gamesList = GamesListView.ItemsSource as IList<Game>;
                    if (gamesList != null)
                    {
                        gamesList.Remove(selectedGame);
                        GamesListView.ItemsSource = null;
                        GamesListView.ItemsSource = gamesList; 
                    }

                    System.Windows.MessageBox.Show($"Игра \"{selectedGame.Name}\" удалена.", "Игра удалена", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


    }
}
