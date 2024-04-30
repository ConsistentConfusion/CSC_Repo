using System;
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
        private ISolver solver;
        public Form1()
        {
            InitializeComponent();
            //grid = new Grid(Quad1, Quad2, Quad3, 
            //                Quad4, Quad5, Quad6,
            //                Quad7, Quad8, Quad9);
            grid = new Grid();
            solver = new Backtrack();
        }

        private void UpdateUI()
        {
            // Update UI based on the grid in the Grid object
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    int value = grid.SudokuGrid[row, col].Value;
                    if (value != 0)
                    {
                        // Update the corresponding cell in the TableLayoutPanel controls
                        grid.SectionPanels[row / 3, col / 3].Controls[row % 3 * 3 + col % 3].Text = value.ToString();
                    }
                    else
                    {
                        // Clear the cell if the value is 0
                        grid.SectionPanels[row / 3, col / 3].Controls[row % 3 * 3 + col % 3].Text = "";
                    }
                }
            }

        }

        #region Buttons and Clicks
        private void ButtonSolve_Click(object sender, EventArgs e)
        {
           if(solver.Solve(grid))
            {
                UpdateUI();
            }
           else
            {
                MessageBox.Show("This puzzle is invalid or unsolvable.", "Sudoku Solver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            solver.GenerateBoard(grid);
            UpdateUI();

        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {

        }

        public void Quad1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Quad2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Quad3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Quad4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Quad5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Quad6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Quad7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Quad8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Quad9_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
    }
    public class Grid
    {
        //private int[,] SudokuGrid = new int[9, 9];
        private Random random = new Random();
        public TableLayoutPanel[,] SectionPanels { get; private set; }
        public Cell[,] SudokuGrid = new Cell[9, 9];

        public Grid()
        {
            InitializeGrid();
            SectionPanels = new TableLayoutPanel[3, 3];
        }

        private void InitializeGrid()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    SudokuGrid[row, col] = new Cell();
                }
            }
        }

        public void Clear(Grid grid)
        {
            foreach (var cell in grid.SudokuGrid)
            {
                cell.Value = 0;
                cell.IsFilled = false;
            }
        }
    }

    public interface ISolver
    {
        void GenerateBoard(Grid grid);
        bool Solve(Grid grid);
    }

    public class Backtrack : ISolver
    {
        private Random random = new Random();
        

        public void GenerateBoard(Grid grid, Valid valid)
        {
            // Implement Sudoku board generation logic if needed
            //Clear the existing grid
            grid.Clear(grid);

            // Generate board using random numbers
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    int num = random.Next(1, 10); // Generate a random number between 1 and 9
                    if (valid.IsValidPlacement(grid, row, col, num))
                    {
                        grid.SudokuGrid[row, col].Value = num;
                        grid.SudokuGrid[row, col].IsFilled = true;
                    }
                    else
                    {
                        // If the random number is not valid, try again until a valid number is found
                        col--; // Try the same column    again
                    }
                }
            }
        }

        public bool Solve(Grid grid, Valid valid)
        {
            return BacktrackSolve(grid, valid, 0, 0); // Start solving from the top-left cell
        }

        private bool BacktrackSolve(Grid grid, Valid valid, int row, int col)
        {
            // Check if we've reached the end of the grid (last row)
            if (row == 9)
            {
                row = 0;
                if (++col == 9) // Check if we've reached the last column
                {
                    return true; // Puzzle solved
                }
            }

            // Skip filled cells
            if (grid.SudokuGrid[row, col].IsFilled)
            {
                return BacktrackSolve(grid, valid, row + 1, col); // Move to the next cell
            }

            // Try placing numbers 1 to 9 in the current cell
            for (int num = 1; num <= 9; num++)
            {
                if (valid.IsValidPlacement(grid, row, col, num))
                {
                    grid.SudokuGrid[row, col].Value = num;
                    grid.SudokuGrid[row, col].IsFilled = true;

                    // Recursively solve for the next cell
                    if (BacktrackSolve(grid, valid, row + 1, col))
                    {
                        return true; // Puzzle solved
                    }

                    // Backtrack if placing 'num' doesn't lead to a solution
                    grid.SudokuGrid[row, col].Value = 0;
                    grid.SudokuGrid[row, col].IsFilled = false;
                }
            }

            return false; // No valid number found for this cell
        }
    }

    public class Cell
    {
        public int Value { get; set; }
        public bool IsFilled { get; set; }

        public Cell()
        {
            // Default value
            Value = 0;
            // Cell is initially empty
            IsFilled = false;
        }
    }

    public class Valid
    {
        public bool IsValidPlacement(Grid grid, int row, int col, int num)
        {
            // Check if placing 'num' at (row, col) is valid according to Sudoku rules
            return IsRowValid(grid, row, num) && IsColumnValid(grid, col, num) && IsSectionValid(grid, row, col, num);
        }

        private bool IsRowValid(Grid grid, int row, int num)
        {
            // Check if placing 'num' in the row 'row' is valid
            for (int col = 0; col < 9; col++)
            {
                if (grid.SudokuGrid[row, col].Value == num && num != 0)
                {
                    //MessageBox.Show("Number already exists in this row", "Sudoku Solver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false; // Number already exists in this row
                }
            }
            return true;
        }

        private bool IsColumnValid(Grid grid, int col, int num)
        {
            // Check if placing 'num' in the column 'col' is valid
            for (int row = 0; row < 9; row++)
            {
                if (grid.SudokuGrid[row, col].Value == num && num != 0)
                {
                    //MessageBox.Show("Number already exists in this column", "Sudoku Solver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false; // Number already exists in this column
                }
            }
            return true;
        }

        private bool IsSectionValid(Grid grid, int row, int col, int num)
        {
            // Check if placing 'num' in the 3x3 section containing (row, col) is valid
            int sectionStartRow = row - (row % 3);
            int sectionStartCol = col - (col % 3);
            for (int i = sectionStartRow; i < sectionStartRow + 3; i++)
            {
                for (int j = sectionStartCol; j < sectionStartCol + 3; j++)
                {
                    if (grid.SudokuGrid[i, j].Value == num && num != 0)
                    {
                        //MessageBox.Show("Number already exists in this section", "Sudoku Solver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false; // Number already exists in this section
                    }
                }
            }
            return true;
        }
    }
}    
    