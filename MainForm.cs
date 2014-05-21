using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace sudoku
{
    public partial class MainForm : Form
    {
        int[,] vectors;   // hold the list of cells to check for each position
        public TextBox[] board; // array to store the list of textboxes
        private bool exit;  // flag to exit the search routine if the app is closing

        public MainForm()
        {
            InitializeComponent();
        }

        // select all text in each textbox when the user tabs into it
        private void tb_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        // the only keys allowed are the digits 1 - 9 and backspace
        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("123456789\b".IndexOf(e.KeyChar) < 0)
                e.Handled = true;
        }

        // set references to all the textboxes into an array for easy iteration
        private void MainForm_Load(object sender, EventArgs e)
        {
            exit = false;
            board = new TextBox[81];
            board[0] = tb0; board[1] = tb1; board[2] = tb2; board[3] = tb3; board[4] = tb4; board[5] = tb5;
            board[6] = tb6; board[7] = tb7; board[8] = tb8; board[9] = tb9; board[10] = tb10; board[11] = tb11;
            board[12] = tb12; board[13] = tb13; board[14] = tb14; board[15] = tb15; board[16] = tb16; board[17] = tb17;
            board[18] = tb18; board[19] = tb19; board[20] = tb20; board[21] = tb21; board[22] = tb22; board[23] = tb23;
            board[24] = tb24; board[25] = tb25; board[26] = tb26; board[27] = tb27; board[28] = tb28; board[29] = tb29;
            board[30] = tb30; board[31] = tb31; board[32] = tb32; board[33] = tb33; board[34] = tb34; board[35] = tb35;
            board[36] = tb36; board[37] = tb37; board[38] = tb38; board[39] = tb39; board[40] = tb40; board[41] = tb41;
            board[42] = tb42; board[43] = tb43; board[44] = tb44; board[45] = tb45; board[46] = tb46; board[47] = tb47;
            board[48] = tb48; board[49] = tb49; board[50] = tb50; board[51] = tb51; board[52] = tb52; board[53] = tb53;
            board[54] = tb54; board[55] = tb55; board[56] = tb56; board[57] = tb57; board[58] = tb58; board[59] = tb59;
            board[60] = tb60; board[61] = tb61; board[62] = tb62; board[63] = tb63; board[64] = tb64; board[65] = tb65;
            board[66] = tb66; board[67] = tb67; board[68] = tb68; board[69] = tb69; board[70] = tb70; board[71] = tb71;
            board[72] = tb72; board[73] = tb73; board[74] = tb74; board[75] = tb75; board[76] = tb76; board[77] = tb77;
            board[78] = tb78; board[79] = tb79; board[80] = tb80;

            // 00 01 02  09 10 11  18 19 20
            // 03 04 05  12 13 14  21 22 23
            // 06 07 08  15 16 17  24 25 26
            // 
            // 27 28 29  36 37 38  45 46 47
            // 30 31 32  39 40 41  48 49 50
            // 33 34 35  42 43 44  51 52 53
            // 
            // 54 55 56  63 64 65  72 73 74
            // 57 58 59  66 67 68  75 76 77
            // 60 61 62  69 70 71  78 79 80

            vectors = new int[,] {
                {09, 18, 27, 54}, {09, 18, 28, 55}, {09, 18, 29, 56}, {12, 21, 27, 54}, {12, 21, 28, 55}, 
                {12, 21, 29, 56}, {15, 24, 27, 54}, {15, 24, 28, 55}, {15, 24, 29, 56}, {00, 18, 36, 63}, 
                {00, 18, 37, 64}, {00, 18, 38, 65}, {03, 21, 36, 63}, {03, 21, 37, 64}, {03, 21, 38, 65}, 
                {06, 24, 36, 63}, {06, 24, 37, 64}, {06, 24, 38, 65}, {00, 09, 45, 72}, {00, 09, 46, 73}, 
                {00, 09, 47, 74}, {03, 12, 45, 72}, {03, 12, 46, 73}, {03, 12, 47, 74}, {06, 15, 45, 72}, 
                {06, 15, 46, 73}, {06, 15, 47, 74}, {36, 45, 00, 54}, {36, 45, 01, 55}, {36, 45, 02, 56}, 
                {39, 48, 00, 54}, {39, 48, 01, 55}, {39, 48, 02, 56}, {42, 51, 00, 54}, {42, 51, 01, 55}, 
                {42, 51, 02, 56}, {27, 45, 09, 63}, {27, 45, 10, 64}, {27, 45, 11, 65}, {30, 48, 09, 63}, 
                {30, 48, 10, 64}, {30, 48, 11, 65}, {33, 51, 09, 63}, {33, 51, 10, 64}, {33, 51, 11, 65}, 
                {27, 36, 18, 72}, {27, 36, 19, 73}, {27, 36, 20, 74}, {30, 39, 18, 72}, {30, 39, 19, 73}, 
                {30, 39, 20, 74}, {33, 42, 18, 72}, {33, 42, 19, 73}, {33, 42, 20, 74}, {63, 72, 00, 27}, 
                {63, 72, 01, 28}, {63, 72, 02, 29}, {66, 75, 00, 27}, {66, 75, 01, 28}, {66, 75, 02, 29}, 
                {69, 78, 00, 27}, {69, 78, 01, 28}, {69, 78, 02, 29}, {54, 72, 09, 36}, {54, 72, 10, 37}, 
                {54, 72, 11, 38}, {57, 75, 09, 36}, {57, 75, 10, 37}, {57, 75, 11, 38}, {60, 78, 09, 36}, 
                {60, 78, 10, 37}, {60, 78, 11, 38}, {54, 63, 18, 45}, {54, 63, 19, 46}, {54, 63, 20, 47}, 
                {57, 66, 18, 45}, {57, 66, 19, 46}, {57, 66, 20, 47}, {60, 69, 18, 45}, {60, 69, 19, 46}, 
                {60, 69, 20, 47}
            };
        }

        // iterate through all the textboxes, clearing them
        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i].Text = "";
                board[i].ForeColor = Color.Black;
            }
            tb0.Focus();
        }

        // if the form is closing, set the flag so the search routine exits
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit = true;
        }

        private int ToInt(string s)
        {
            return (s.Length > 0) ? int.Parse(s) : 0;
        }

        private List<int> Allowed(int index)
        {
            // compile list of used digits in the same block/row/column
            List<int> used = new List<int>();
            // check cells in the same block
            int blockStart = index / 9 * 9;
            for (int i = 0; i < 9; i++)
            {
                if (blockStart + i != index)
                    used.Add(ToInt(board[blockStart + i].Text));
            }
            // check cells in the same row/col
            for (int i = 0; i < 3; i++)
            {
                used.Add(ToInt(board[vectors[index, 0] + i].Text));
                used.Add(ToInt(board[vectors[index, 1] + i].Text));
                used.Add(ToInt(board[vectors[index, 2] + (i * 3)].Text));
                used.Add(ToInt(board[vectors[index, 3] + (i * 3)].Text));
            }
            // add digits that have not been used
            List<int> allowed = new List<int>();
            for (int i = 1; i < 10; i++)
                if (!used.Contains(i))
                    allowed.Add(i);
            return allowed;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < board.Length; i++)
            {
                string s = board[i].Text.Trim();
                int t = (s.Length > 0) ? int.Parse(s) : 0;
                board[i].ForeColor = (t > 0) ? Color.Blue : Color.Black;
            }
            // start the search
            Search(0);
        }

        public bool Search(int index)
        {
            if (index == 81)
                return true;
            if (exit)
                return false;
            if (board[index].Text.Length > 0)
            {
                return Search(index + 1);
            }
            else
            {
                List<int> choices = Allowed(index);
                for (int i = 0; i < choices.Count; i++)
                {
                    board[index].Text = choices[i].ToString();
                    Application.DoEvents();
                    if (Search(index + 1))
                        return true;
                }
                board[index].Text = "";
                return false;
            }
        }
    }
}
