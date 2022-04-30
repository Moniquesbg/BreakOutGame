namespace BreakoutGame
{
    public partial class Form1 : Form
    {

        bool goLeft;
        bool goRight;
        bool isGameOve

        int score;
        int ballx;
        int bally;
        int playerSpeed;

        Random rnd = new Random();


        public Form1()
        {
            InitializeComponent();

            setupGame();
        }

        private void setupGame()
        {
            score = 0;
            ballx = 6;
            bally = 6;
            playerSpeed = 12;
            txtScore.Text = "Score: " + score;

            gameTimer.Start();

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }
        }

        private void gameOver(String message)
        {
            isGameOver = true;
            gameTimer.Stop();

            txtScore.Text = "Score: " + score + " " + message;
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            if(goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }

            if(goRight == true && player.Left < 1485)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballx;
            ball.Top += bally;

            if(ball.Left < 0 || ball.Left > 1683)
            {
                ballx = -ballx;
            }
            if(ball.Top < 0)
            {
                bally = -bally;
            }
            
            if(ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = rnd.Next(6, 12) * -1;

                if(ballx < 0)
                {
                    ballx = rnd.Next(6, 12) * -1;
                }
                else
                {
                    ballx = rnd.Next(6, 12);
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if(ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;

                        bally = -bally;

                        this.Controls.Remove(x);
                    }
                }
            }


            if(score == 15)
            {
                gameOver("You win!!");
            }

            if(ball.Top > 1305)
            {
                gameOver("You lost loser");
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            else
            {
                goRight = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            else
            {
                goRight = false;
            }
        }
    }
}
