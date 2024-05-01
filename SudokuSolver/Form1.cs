using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        private Grid grid;
        public Form1()
        {
            InitializeComponent();
        }        

        #region Buttons and Clicks
        private void ButtonSolve_Click(object sender, EventArgs e)
        {
           
        }

        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            

        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {

        }

        
        #endregion
    }
    public class Grid
    {
        //private int[,] SudokuGrid = new int[9, 9];
        private Random random = new Random();
        public int[,] Puzzle = { { 8, 2, 3, 0, 7, 9, 0, 4, 0 },
                                 { 0, 7, 6, 3, 0, 4, 0, 0, 1 },
                                 { 0, 0, 1, 2, 6, 0, 7, 0, 9 },
                                 { 0, 0, 0, 5, 4, 0, 2, 0, 8 },
                                 { 4, 0, 0, 0, 0, 0, 1, 7, 0 },
                                 { 1, 6, 2, 0, 0, 0, 9, 0, 4 },
                                 { 0, 0, 0, 7, 2, 3, 0, 0, 0 },
                                 { 0, 5, 0, 9, 0, 0, 0, 8, 0 },
                                 { 7, 0, 9, 0, 0, 0, 6, 1, 0 } };

        public Grid()
        {
            
        }

        public void GenerateBoard ()
        {
            Grid grid = new Grid();

            
                        
        }

    }

    public class Valid
    {
        public bool IsValid()
        {
            
            return false;
        }
    }
    
}    