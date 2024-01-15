using GameDatabase;
using GameLibrary;
using Microsoft.EntityFrameworkCore;
using System.Windows;
namespace Module_6._1
{
    public partial class MainWindow : Window
    {
        public GameContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new GameContext();
            LoadGames();
        }
        private void DeleteGamesWithZeroSales(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var gamesToDelete = context.Games.Where(g => g.NumberOfSales == 0);
                context.Games.RemoveRange(gamesToDelete);
                context.SaveChanges();
            }
        }
        private void DeleteGamesWithSalesEqualTo(object sender, RoutedEventArgs e, int sales)
        {
            using (var context = new GameContext())
            {
                var gamesToDelete = context.Games.Where(g => g.NumberOfSales == sales);
                context.Games.RemoveRange(gamesToDelete);
                context.SaveChanges();
            }
        }
        private void DisplayTop3GenresBySales(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var top3Genres = context.Games
                    .GroupBy(g => g.Genre)
                    .Select(group => new
                    {
                        Genre = group.Key,
                        Sales = group.Sum(g => g.NumberOfSales)
                    })
                    .OrderByDescending(g => g.Sales)
                    .Take(3)
                    .ToList();

                MessageBox.Show($"Top 3 Genres by Sales:\n\n1. {top3Genres[0].Genre}: {top3Genres[0].Sales} sales\n2. {top3Genres[1].Genre}: {top3Genres[1].Sales} sales\n3. {top3Genres[2].Genre}: {top3Genres[2].Sales} sales", "Top 3 Genres", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void DisplayMostPopularGenreBySales(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var mostPopularGenre = context.Games
                    .GroupBy(g => g.Genre)
                    .Select(group => new
                    {
                        Genre = group.Key,
                        Sales = group.Sum(g => g.NumberOfSales)
                    })
                    .OrderByDescending(g => g.Sales)
                    .FirstOrDefault();

                MessageBox.Show($"Most Popular Genre by Sales:\n\n{mostPopularGenre.Genre}: {mostPopularGenre.Sales} sales", "Most Popular Genre", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void DisplayTop3SinglePlayerGamesBySales(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var top3SinglePlayerGames = context.Games
                    .Where(g => g.GameMode == "SinglePlayer")
                    .OrderByDescending(g => g.NumberOfSales)
                    .Take(3)
                    .ToList();

                MessageBox.Show($"Top 3 Single Player Games by Sales:", "Top 3 Single Player Games", MessageBoxButton.OK, MessageBoxImage.Information);
                foreach (var game in top3SinglePlayerGames)
                {
                    MessageBox.Show($"{game.Title}: {game.NumberOfSales} sales", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private void DisplayTop3MultiplayerGamesBySales(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var top3MultiplayerGames = context.Games
                    .Where(g => g.GameMode == "Multiplayer")
                    .OrderByDescending(g => g.NumberOfSales)
                    .Take(3)
                    .ToList();

                MessageBox.Show($"Top 3 Multiplayer Games by Sales:", "Top 3 Multiplayer Games", MessageBoxButton.OK, MessageBoxImage.Information);
                foreach (var game in top3MultiplayerGames)
                {
                    MessageBox.Show($"{game.Title}: {game.NumberOfSales} sales", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private void DisplayMostPopularSinglePlayerGameBySales(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var mostPopularSinglePlayerGame = context.Games
                    .Where(g => g.GameMode == "SinglePlayer")
                    .OrderByDescending(g => g.NumberOfSales)
                    .FirstOrDefault();

                MessageBox.Show($"Most Popular Single Player Game by Sales:\n\n{mostPopularSinglePlayerGame.Title}: {mostPopularSinglePlayerGame.NumberOfSales} sales", "Most Popular Single Player Game", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void DisplayMostPopularMultiplayerGameBySales(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var mostPopularMultiplayerGame = context.Games
                    .Where(g => g.GameMode == "Multiplayer")
                    .OrderByDescending(g => g.NumberOfSales)
                    .FirstOrDefault();

                MessageBox.Show($"Most Popular Multiplayer Game by Sales:\n\n{mostPopularMultiplayerGame.Title}: {mostPopularMultiplayerGame.NumberOfSales} sales", "Most Popular Multiplayer Game", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void AddNewGame(object sender, RoutedEventArgs e, Game newGame)
        {
            using (var context = new GameContext())
            {
                var existingGame = context.Games.FirstOrDefault(g => g.Title == newGame.Title && g.Studio == newGame.Studio);
                if (existingGame == null)
                {
                    context.Games.Add(newGame);
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Such a game already exists", "Error",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void DisplayTop3StudiosWithMostGames(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var top3Studios = context.Games
                    .GroupBy(g => g.Studio)
                    .Select(group => new
                    {
                        Studio = group.Key,
                        GamesCount = group.Count()
                    })
                    .OrderByDescending(g => g.GamesCount)
                    .Take(3)
                    .ToList();

                MessageBox.Show($"Top 3 Studios with Most Games:\n\n1. {top3Studios[0].Studio}: {top3Studios[0].GamesCount} games\n2. {top3Studios[1].Studio}: {top3Studios[1].GamesCount} games\n3. {top3Studios[2].Studio}: {top3Studios[2].GamesCount} games", "Top 3 Studios", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void DisplayStudioWithMostGames(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var studioWithMostGames = context.Games
                    .GroupBy(g => g.Studio)
                    .Select(group => new
                    {
                        Studio = group.Key,
                        GamesCount = group.Count()
                    })
                    .OrderByDescending(g => g.GamesCount)
                    .FirstOrDefault();

                MessageBox.Show($"Studio with Most Games:\n\n{studioWithMostGames.Studio}: {studioWithMostGames.GamesCount} games", "Studio with Most Games", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void DisplayTop3PopularStyles(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var top3Styles = context.Games
                    .GroupBy(g => g.Genre)
                    .Select(group => new
                    {
                        Style = group.Key,
                        GamesCount = group.Count()
                    })
                    .OrderByDescending(g => g.GamesCount)
                    .Take(3)
                    .ToList();

                MessageBox.Show($"Top 3 Popular Styles:\n\n1. {top3Styles[0].Style}: {top3Styles[0].GamesCount} games\n2. {top3Styles[1].Style}: {top3Styles[1].GamesCount} games\n3. {top3Styles[2].Style}: {top3Styles[2].GamesCount} games", "Top 3 Styles", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void DisplayMostPopularStyle(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var mostPopularStyle = context.Games
                    .GroupBy(g => g.Genre)
                    .Select(group => new
                    {
                        Style = group.Key,
                        GamesCount = group.Count()
                    })
                    .OrderByDescending(g => g.GamesCount)
                    .FirstOrDefault();

                MessageBox.Show($"Most Popular Style:\n\n{mostPopularStyle.Style}: {mostPopularStyle.GamesCount} games", "Most Popular Style", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private bool StudioExists(string studioName)
        {
            using (var context = new GameContext())
            {
                return context.Games.Any(g => g.Studio == studioName);
            }
        }
        private void AddNewStudio(object sender, RoutedEventArgs e, string studioName)
        {
            using (var context = new GameContext())
            {
                if (!StudioExists(studioName))
                {
                    context.Games.Add(new Game(title: null, studio: studioName, genre: null, releaseDate: DateTime.Now, gameMode: null, numberOfSales: 0));
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Studio already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void UpdateStudio(object sender, RoutedEventArgs e, string oldStudioName, string newStudioName)
        {
            using (var context = new GameContext())
            {
                var studioToUpdate = context.Games.Where(g => g.Studio == oldStudioName);

                foreach (var game in studioToUpdate)
                {
                    game.Studio = newStudioName;
                }

                context.SaveChanges();
            }
        }
        private void DeleteStudio(object sender, RoutedEventHandler e, string studioName)
        {
            using (var context = new GameContext())
            {
                if (StudioExists(studioName))
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this studio?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        var gamesInStudio = context.Games.Where(g => g.Studio == studioName);
                        context.Games.RemoveRange(gamesInStudio);
                        context.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Studio not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void UpdateGame(object sender, RoutedEventArgs e, int gameId, Game updatedGame, string releaseyear, string gamemode, string genre, int numberofsales, string description, string platform, decimal rating)
        {
            using (var context = new GameContext())
            {
                var gameToUpdate = context.Games.FirstOrDefault(g => g.Id == gameId);
                if (gameToUpdate != null)
                {
                    gameToUpdate.Title = updatedGame.Title;
                    gameToUpdate.Studio = updatedGame.Studio;
                    gameToUpdate.ReleaseDate = updatedGame.ReleaseDate;
                    gameToUpdate.GameMode = updatedGame.GameMode;
                    gameToUpdate.Genre = updatedGame.Genre;
                    gameToUpdate.NumberOfSales = updatedGame.NumberOfSales;
                    gameToUpdate.Description = updatedGame.Description;
                    gameToUpdate.Platform = updatedGame.Platform;
                    gameToUpdate.Rating = updatedGame.Rating;
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Game not found","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }
        private void DeleteGame(object sender, RoutedEventArgs e, string title, string studio)
        {
            using (var context = new GameContext())
            {
                var gameToDelete = context.Games.FirstOrDefault(g => g.Title == title && g.Studio == studio);
                if (gameToDelete != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        context.Games.Remove(gameToDelete);
                        context.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Game not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void LoadGames()
        {
            using (var context = new GameContext())
            {
                List<Game> games = _context.Games.ToList();
                gameListBox.ItemsSource = games;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var games = context.Games.Where(g => g.Title.Contains(nameTextBox.Text)).ToList();
                gameListBox.ItemsSource = games;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var games = context.Games.Where(g => g.Studio.Contains(studioTextBox.Text)).ToList();
                gameListBox.ItemsSource = games;
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var games = context.Games.Where(g => g.Studio.Contains(studioGameTextBox.Text) && g.Title.Contains(nameTextBox.Text)).ToList();
                gameListBox.ItemsSource = games;
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var games = context.Games.Where(g => g.Genre.Contains(styleTextBox.Text)).ToList();
                gameListBox.ItemsSource = games;
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var year = Int32.Parse(yearTextBox.Text);
                var games = context.Games.Where(g => g.ReleaseDate.Year == year).ToList();
                gameListBox.ItemsSource = games;
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string studio = txtStudio.Text;
            DeleteGame(sender, e, title, studio);
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string studio = txtStudio.Text;
            string releaseyear = txtReleaseDate.Text;
            string gamemode = txtGameMode.Text;
            string genre = txtGenre.Text;
            int numberofsales = Convert.ToInt32(txtNumberOfSales.Text);
            string description = txtDescription.Text;
            string platform = txtPlatform.Text;
            decimal rating = Convert.ToDecimal(txtRating.Text);
            int gameID = GetGameId(title, studio);
            Game updatedGame = new Game(
                title,
                studio,
                genre,
                Convert.ToDateTime(releaseyear),
                gamemode,
                numberofsales,
                description,
                platform,
                Convert.ToDecimal(rating)
            );
            UpdateGame(sender, e, gameID, updatedGame,releaseyear,gamemode,genre,numberofsales,description,platform,rating);
        }
        public int GetGameId(string title, string studio)
        {
            using (var context = new GameContext())
            {
                var game = context.Games.FirstOrDefault(g => g.Title == title && g.Studio == studio);
                if (game != null)
                {
                    return game.Id;
                }
            }
            return -1;
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string studio = txtStudio.Text;
            string releaseyear = txtReleaseDate.Text;
            string gamemode = txtGameMode.Text;
            string genre = txtGenre.Text;
            int numberofsales = Convert.ToInt32(txtNumberOfSales.Text);
            string description = txtDescription.Text;
            string platform = txtPlatform.Text;
            decimal rating = Convert.ToDecimal(txtRating.Text);
            Game newGame = new Game(
                title,
                studio,
                genre,
                Convert.ToDateTime(releaseyear),
                gamemode,
                numberofsales,
                description,
                platform,
                Convert.ToDecimal(rating)
            );
            AddNewGame(sender, e, newGame);
        }
        private void DisplayGameWithMaxSalesByStyle(object sender, RoutedEventArgs e)
        {
            string style = styleTextBoxForMaxSales.Text;
            using (var context = new GameContext())
            {
                var gameWithMaxSales = context.Games.Where(g => g.Genre == style).OrderByDescending(g => g.NumberOfSales).FirstOrDefault();
                gameListBox.ItemsSource = new List<Game>() { gameWithMaxSales };
            }
        }
        private void DisplayTopFivePopularGamesByStyle(object sender, RoutedEventArgs e)
        {
            string style = styleTextBoxForPopular.Text;
            using (var context = new GameContext())
            {
                var topFivePopularGames = context.Games.Where(g => g.Genre == style).OrderByDescending(g => g.NumberOfSales).Take(5).ToList();
                gameListBox.ItemsSource = topFivePopularGames;
            }
        }
        private void DisplayTopFiveUnpopularGamesByStyle(object sender, RoutedEventArgs e)
        {
            string style = styleTextBoxForUnpopular.Text;
            using (var context = new GameContext())
            {
                var topFiveUnpopularGames = context.Games.Where(g => g.Genre == style).OrderBy(g => g.NumberOfSales).Take(5).ToList();
                gameListBox.ItemsSource = topFiveUnpopularGames;
            }
        }
        private void DisplayFullGameInfo(object sender, RoutedEventArgs e)
        {
            int gameId = int.Parse(gameIdTextBox.Text);
            using (var context = new GameContext())
            {
                var game = context.Games.FirstOrDefault(g => g.Id == gameId);
                gameListBox.ItemsSource = new List<Game>() { game };
            }
        }
        private void DisplayStudioAndMostCreatedStyle(object sender, RoutedEventArgs e)
        {
            using (var context = new GameContext())
            {
                var studioAndStyle = context.Games
                    .GroupBy(g => new { g.Studio, g.Genre })
                    .Select(group => new
                    {
                        Studio = group.Key.Studio,
                        Style = group.Key.Genre,
                        Count = group.Count()
                    })
                    .OrderByDescending(g => g.Count)
                    .GroupBy(g => g.Studio)
                    .Select(g => g.First())
                    .ToList();
            }
        }
        public class GameContext : DbContext
        {
        public DbSet<Game> Games { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-2;Initial Catalog=GameDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False");
            }
        }
        }
    }
}