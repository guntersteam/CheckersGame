using System.Windows.Forms.VisualStyles;

namespace Checkers
{
    public partial class Form1 : Form
    {
        const int MapSize = 8;
        const int CellSize = 50;
        int[,] Map = new int[MapSize, MapSize];
        int currectPlayer;
        Button prevButton;

        bool IsMoving;

        Image WhiteFigure;
        Image BlackFigure;
        public Form1()
        {
            InitializeComponent();

            this.Text = "Checkers";

            WhiteFigure = new Bitmap(new Bitmap(@"C:\Users\Евгений\source\repos\Checkers\Checkers\Sprites\white.png"), new Size(CellSize - 10, CellSize - 10));
            BlackFigure = new Bitmap(new Bitmap(@"C:\Users\Евгений\source\repos\Checkers\Checkers\Sprites\black.png"), new Size(CellSize - 10, CellSize - 10));
            Init();
        }

        public void Init()
        {
            currectPlayer = 1;
            IsMoving = false;
            prevButton = null;
            Map = new int[MapSize, MapSize]
            {
                {0,1,0,1,0,1,0,1 },
                {1,0,1,0,1,0,1,0 },
                {0,1,0,1,0,1,0,1 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {2,0,2,0,2,0,2,0 },
                {0,2,0,2,0,2,0,2 },
                {2,0,2,0,2,0,2,0 },
            };
            CreateMap();
        }
        public void CreateMap()
        {
            this.Width = (MapSize + 1) * CellSize;
            this.Height = (MapSize + 1) * CellSize;

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(j * CellSize, i * CellSize);
                    button.Size = new Size(CellSize, CellSize);
                    button.Click += new EventHandler(OnFigurePress);
                    if (Map[i, j] == 1)
                        button.Image = WhiteFigure;
                    else if (Map[i, j] == 2)
                        button.Image = BlackFigure;
                    button.BackColor = GetPrevButtonColor(button);
                    this.Controls.Add(button);
                      
                }
            }
        }
        public void SwitchPlayer()
        {
            currectPlayer = currectPlayer == 1 ? 2 : 1;
        }
        public Color GetPrevButtonColor(Button prevButton)
        {
            if ((prevButton.Location.Y / CellSize) % 2 != 0)
            {
                if ((prevButton.Location.X / CellSize) % 2 == 0)
                {
                    return Color.Gray;
                }
            }
            if ((prevButton.Location.Y / CellSize) % 2 == 0)
            {
                if ((prevButton.Location.X / CellSize) % 2 != 0)
                {
                    return Color.Gray;
                }
            }
            return Color.White;
        }
        public void OnFigurePress(object sender, EventArgs e)
        {
            if (prevButton != null)
                prevButton.BackColor = GetPrevButtonColor(prevButton);

            Button pressedButton = sender as Button;
            if (Map[pressedButton.Location.Y / CellSize, pressedButton.Location.X / CellSize] != 0 && Map[pressedButton.Location.Y / CellSize, pressedButton.Location.X / CellSize] == currectPlayer)
            {
                pressedButton.BackColor = Color.Red;
                IsMoving = true;
            }
            else
            {
                if (IsMoving)
                {
                    (Map[pressedButton.Location.Y / CellSize, pressedButton.Location.X / CellSize], Map[prevButton.Location.Y / CellSize, prevButton.Location.X / CellSize]) = (Map[prevButton.Location.Y / CellSize, prevButton.Location.X / CellSize], Map[pressedButton.Location.Y / CellSize, pressedButton.Location.X / CellSize]);
                    pressedButton.Image = prevButton.Image;
                    prevButton.Image = null;
                    IsMoving = false;

                }
            }
            prevButton = pressedButton;

        }
    }
}