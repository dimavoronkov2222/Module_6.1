using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Module_6._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Game
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Studio { get; set; }
            public string Genre { get; set; }
            public DateTime ReleaseDate { get; set; }
        }
        public class GameDbContext : DbContext
        {
            public DbSet<Game> Games { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // Вкажіть свої дані підключення до бази даних
                optionsBuilder.UseSqlServer("YOUR_CONNECTION_STRING");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            using (var dbContext = new GameDbContext())
            {
                // Здесь можна виконати операції з базою даних, створення, видалення, оновлення записів
            }
        }
    }
}