using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();
            Valid valid = new Valid();
            Solve solve = new Solve();
            int N = grid.Puzzle.GetLength(0);

            Console.WriteLine("Welcome to the Sudoku Solver! \nThe initial board in this version is hard coded. \nThe project will be refined at a later date! \n\n");
            Console.WriteLine("Initial Board");
            grid.Print();

            if(solve.SolveSudoku(grid.Puzzle, valid, N))
            {
                Console.WriteLine("\n\n\nSolved!");
                grid.Print();
            }

            Console.ReadKey();
        }
    }

    public class Grid
    {
        public int[,] Puzzle = { { 8, 2, 3, 0, 7, 9, 0, 4, 0 },
                                 { 0, 7, 6, 3, 0, 4, 0, 0, 1 },
                                 { 0, 0, 1, 2, 6, 0, 7, 0, 9 },
                                 { 0, 0, 0, 5, 4, 0, 2, 0, 8 },
                                 { 4, 0, 0, 0, 0, 0, 1, 7, 0 },
                                 { 1, 6, 2, 0, 0, 0, 9, 0, 4 },
                                 { 0, 0, 0, 7, 2, 3, 0, 0, 0 },
                                 { 0, 5, 0, 9, 0, 0, 0, 8, 0 },
                                 { 7, 0, 9, 0, 0, 0, 6, 1, 0 } };
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("-------------------------------------");

            for (int i = 0; i < Puzzle.GetLength(0); i++)
            {
                Console.Write("\n");
                for (int j = 0; j < Puzzle.GetLength(1); j++)
                {
                    if (j % 3 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.Write("|");

                    Console.ResetColor();
                    var num = Puzzle[i, j].ToString();
                    if ( num == "0")
                    {
                        num = " ";
                    }
                    Console.Write(" " + num + " ");

                }

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("|");
                if ((i + 1) % 3 != 0)
                {
                    Console.ResetColor();
                }
                Console.Write("\n-------------------------------------");
            }

            Console.ResetColor();
        }
    }

    public class Valid
    {
        public bool IsValid(int[,] Puzzle, int row, int col, int num)
        {
            // check row
            for(int r = 0; r < Puzzle.GetLength(0); r++)
            {
                // check if num will be duplicate
                if (Puzzle[row, r] == num)
                {
                    // if the numbers match, it is invalid
                    return false;
                }
            }
            // check column
            for(int r = 0;  r < Puzzle.GetLength(0); r++)
            {
                // check if num will be duplicate
                if (Puzzle[r, col] == num)
                {
                    return false;
                }
            }
            // check 3x3 square
            int Square = (int)Math.Sqrt(Puzzle.GetLength(0));
            int RowSq = row - row % Square;
            int ColSq = col - col % Square;

            for(int r = RowSq; r < RowSq + Square; r++)
            {
                for (int c = ColSq; c < ColSq + Square; c++)
                {
                    if (Puzzle[r, c] == num) 
                    { 
                        return false; 
                    }
                }
            }

            return true;
        }
    }

    public class Solve
    { 
        public bool SolveSudoku(int[,] Puzzle, Valid valid, int n)
        {
            int row = -1;
            int col = -1;
            bool Filled = true;


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // if any cell is 0, there are remaining spaces that need values
                    if (Puzzle[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        Filled = false;
                        break;
                    }
                }
                if (!Filled)
                {
                    break;
                }
            }

            if (Filled)
            {
                return true;
            }

            for(int num = 1; num <= n; num++)
            {
                if(valid.IsValid(Puzzle, row, col, num))
                {
                    Puzzle[row, col] = num;
                    if(SolveSudoku(Puzzle, valid, n))
                    {
                        // solved!
                        return true;
                    }
                    else
                    {
                        Puzzle[row,col] = 0;
                    }                    
                }
            }
            return false;
        }
    }
}
