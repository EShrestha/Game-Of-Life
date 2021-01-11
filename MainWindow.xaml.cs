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

        public void Revive()
        {
            this.alive = true;
            this.btn.Background = Brushes.Red;
        }
        public void Kill()
        {
            this.alive = false;
            this.btn.Background = Brushes.Black;
        }
    }
    public partial class MainWindow : Window
    {
        private const int SCALE = 12;
        private const int WIDTH = 1000;
        private const int HEIGHT = 1000;
        private const int wDs = WIDTH / SCALE;
        private const int hDs = HEIGHT / SCALE;
        public int maxPixel = hDs;

        DispatcherTimer timer;
        Random random;
        int rand;

        //Button[,] matrix = new Button[hDs, wDs];
        Cell[,] matrix = new Cell[hDs, wDs];

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
                    btn.Click += btn_Event;
                    //btn.MouseEnter += btn_Event;

                    btn.FontSize = SCALE / 3;
                    btn.Background = Brushes.Black;
                    btn.Name = "Button" + btnCount.ToString();
                    ////matrix[i, j] = btn;


                    Cell cell = new Cell(btn, false);
                    matrix[i, j] = cell;


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
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            //timer.Start();
        }


        private void btn_Event(object sender, RoutedEventArgs e)
        {
            Button cell = (sender as Button);
            matrix[Grid.GetRow(cell), Grid.GetColumn(cell)].alive = true;
            matrix[Grid.GetRow(cell), Grid.GetColumn(cell)].btn.Background = Brushes.Red;

        }

        private void update(object sender, EventArgs e)
        {
            for(int row=0; row < hDs; row++)
            {
                for(int col=0; col<wDs; col++)
                {

                    /*
Any live cell with fewer than two live neighbours dies, as if by underpopulation.
Any live cell with two or three live neighbours lives on to the next generation.
Any live cell with more than three live neighbours dies, as if by overpopulation.
Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
These rules, which compare the behavior of the automaton to real life, can be condensed into the following:

Any live cell with two or three live neighbours survives.
Any dead cell with three live neighbours becomes a live cell.
All other live cells die in the next generation. Similarly, all other dead cells stay dead.
                    */

                    //Checking neighbors
                    if((row > 0 && row < wDs) && (col > 0 && col < hDs))
                    {
                        if (matrix[row, col].alive)
                        {
                            //Less than two neighbours dies by underpopulation 
                            if(cellNeighborCount(row,col) < 2)
                            {
                                matrix[row, col].Kill();
                            }

                            //More than three neighbours dies by over population
                            if (cellNeighborCount(row, col) > 3)
                            {
                                matrix[row, col].Kill();
                            }

                            //If two or three live neighbours, lives on to next generation
                            if (cellNeighborCount(row,col) == 2 || cellNeighborCount(row,col) == 3)
                            {

                            }

                        }
                        else if(cellNeighborCount(row, col) == 3) // Dead cell comes to life by reproduction
                        {
                            matrix[row, col].Revive();
                        }
                    }
                    


                }
            }
        }

        private int cellNeighborCount(int row, int col)
        {
            short aliveCells = 0;
            if(row+1 < wDs){if(matrix[row+1, col].alive) { aliveCells++; }}
            if(row - 1 > 0){if(matrix[row-1, col].alive) { aliveCells++; }}

            if (col + 1 < hDs) { if (matrix[row, col+1].alive) { aliveCells++; } }
            if (col - 1 > 0) { if (matrix[row, col-1].alive) { aliveCells++; } }

            if((row + 1 < wDs && row - 1 > 0) && (col + 1 < hDs && col - 1 > 0))
            {
                if (matrix[row + 1, col + 1].alive) { aliveCells++; }
                if (matrix[row + 1, col - 1].alive) { aliveCells++; }
                if (matrix[row - 1, col + 1].alive) { aliveCells++; }
                if (matrix[row - 1, col - 1].alive) { aliveCells++; }
            }
            return aliveCells;
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
    }


}
