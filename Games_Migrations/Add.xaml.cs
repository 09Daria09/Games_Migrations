using GamesLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Games_Migrations
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
            LoadGameModesAsync();
        }
        private async Task LoadGameModesAsync()
        {
            using (var context = new GamesContext())
            {
                var gameModes = await context.GameMode.ToListAsync();
                GameModeComboBox.ItemsSource = gameModes;
                GameModeComboBox.DisplayMemberPath = "Mode";
                GameModeComboBox.SelectedValuePath = "Id";
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var releaseDateParsed = DateTime.TryParse(ReleaseDateTextBox.Text, out var releaseDate);
                if (!releaseDateParsed)
                {
                    MessageBox.Show("Неверный формат даты. Используйте формат гггг-мм-дд.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(SoldCopiesTextBox.Text, out var soldCopies))
                {
                    MessageBox.Show("Неверный формат количества проданных копий. Используйте целое число.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (GameModeComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите режим игры.", "Необходимо выбрать режим", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var gameMode = GameModeComboBox.SelectedItem as GameMode;

                var game = new Game
                {
                    Name = NameTextBox.Text,
                    Studio = StudioTextBox.Text,
                    Style = StyleTextBox.Text,
                    ReleaseDate = releaseDate,
                    SoldCopies = soldCopies,
                    GameModeId = gameMode?.Id 
                };

                using (var context = new GamesContext())
                {
                    context.Games.Add(game);
                    await context.SaveChangesAsync();
                }

                MessageBox.Show($"Игра {game.Name} добавлена в базу данных.", "Игра добавлена", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении игры: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




    }
}
