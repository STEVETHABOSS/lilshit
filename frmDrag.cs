using System;
using System.Windows.Forms;

namespace DragForm
{
    public partial class frmDrag : Form
    {
        int pos = 0;
        int idleTime = 0;
        int x = MousePosition.X;
        int sitting = 0; //3 = sit down idle, 2 = sitting down, 1 = standing up, 0 = stand up idle
        bool mousePressed = false;
        bool formClosing = false;

        public frmDrag()
        {
            InitializeComponent();
            pictureBox1.Image = Properties.Resources.idle;
            pos = MousePosition.X;
        }

        private void frmDrag_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed == true)
            {
                Left += e.X - 30;
                Top += e.Y - 28;

                if (pos - MousePosition.X < 0)
                {
                    pictureBox1.Image = Properties.Resources.move_right;
                    pos = MousePosition.X;
                }
                if (pos - MousePosition.X > 0)
                {
                    pictureBox1.Image = Properties.Resources.move_left;
                    pos = MousePosition.X;
                }
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = true;
            idleTime = 0;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.idle;
            mousePressed = false;
        }

        private void frmClose(object sender, EventArgs e)
        {
            if (sitting == 3)
            {
                formClosing = true;
                pictureBox1.Image = Properties.Resources.sit_reverse;
                timer4.Enabled = true;
            }
            else
            {
            formClosing = true;
            pictureBox1.Image = Properties.Resources.exit;
            timer2.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pos - MousePosition.X == 0 & mousePressed == true)
            {
                pictureBox1.Image = Properties.Resources.hang;
            }
            if (mousePressed == false)
            {
                idleTime++;
            }
            if (idleTime == 10 && pictureBox1.Image != Properties.Resources.sit_idle)
            {
                pictureBox1.Image = Properties.Resources.sit;
                timer3.Enabled = true;
                sitting = 2;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (sitting == 2)
            {
                pictureBox1.Image = Properties.Resources.sit_idle;
                sitting = 3;
                timer3.Enabled = false;
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (sitting == 3)
            {
                pictureBox1.Image = Properties.Resources.sit_reverse;
                timer4.Enabled = true;
                sitting = 1;
            }
            else
            {
                idleTime = 0;
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (sitting == 1)
            {
                pictureBox1.Image = Properties.Resources.idle;
                idleTime = 0;
                sitting = 0;
                timer4.Enabled = false;
            }
            if (formClosing == true)
            {
                pictureBox1.Image = Properties.Resources.exit;
                sitting = 1;
                timer2.Enabled = true;
            }
        }

        private void frmDrag_Load(object sender, EventArgs e)
        {

        }
    }
}