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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game_of_Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    struct Cell
    {
        public Button btn;
        public bool alive;


        public Cell(Button btn, bool alive)
        {
            this.btn = btn;
            this.alive = alive;

        }
    }
    public partial class MainWindow : Window
    {
        private const int SCALE = 12;
        private const int WIDTH = 800;
        private const int HEIGHT = 800;
        private const int wDs = WIDTH / SCALE;
        private const int hDs = HEIGHT / SCALE;
        public int maxPixel = hDs;

        DispatcherTimer timer;
        Random random;
        int rand;

        Button[,] matrix = new Button[hDs, wDs];
        B[,] bMatrix = new B[hDs, wDs];

        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < HEIGHT / SCALE; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < WIDTH / SCALE; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());

            }

            int btnCount = 1;
            for (int i = 0; i < hDs; i++)
            {
                for (int j = 0; j < wDs; j++)
                {
                    Button btn = new Button();
                    btn.MouseEnter += btn_Event;

                    btn.FontSize = SCALE / 3;
                    btn.Background = Brushes.Black;
                    btn.Name = "Button" + btnCount.ToString();
                    ////matrix[i, j] = btn;


                    B b = new B(btn, true, 0, 0);
                    bMatrix[i, j] = b;


                    Grid.SetColumn(btn, j);
                    Grid.SetRow(btn, i);
                    grid.Children.Add(btn);

                    btnCount++;
                }

            }
            this.Show();
            timer = new DispatcherTimer();
            random = new Random();

            timer.Tick += update;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Start();
        }
    }
}
