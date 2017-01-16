using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munkres
{
    public class Munkres
    {
        const int N = 1000;
        public static int[,] C = new int[N, N];
        public static int[,] C_orig = new int[N, N];
        public static int[,] M = new int[N, N];
        public static int[,] path = new int[N*2+1, 2];
        public static int[] RowCover = new int[N];
        public static int[] ColCover = new int[N];
        public static int nrow;
        public static int ncol;
        public static int path_count = 0;
        public static int path_row_0;
        public static int path_col_0;
        public static int asgn = 0;
        public static int step;

        public static void loadMatrix(string data)
        {
            string text;
            try
            {
                var tr = new StringReader(data);
                nrow = 0;
                do
                {
                    text = tr.ReadLine();
                    if (text != null)
                    {
                        ncol = 0;
                        foreach (string subString in text.Split(' '))
                        {
                            if (subString.Length > 0)
                            {
                                C[nrow, ncol] = Int32.Parse(subString);
                                ncol += 1;
                            }
                        }
                        nrow += 1;
                    }
                } while (text != null);
                tr.Close();
                resetMaskandCovers();
            }
            catch
            {
                Console.WriteLine("File Read Error");
            }
        }

        public static void Main(string[] args)
        {
            var res = Run("1 3 0\n 2 1 3\n");
            for(int i=0;i<res.Count;i++) 
            {
                Console.WriteLine(i.ToString() + " " + res[i].ToString());
            }
            Console.ReadKey();
        }

        private static void resetMaskandCovers()
        {
            for (int r = 0; r < nrow; r++)
            {
                RowCover[r] = 0;
                for (int c = 0; c < ncol; c++)
                {
                    M[r, c] = 0;
                }
            }
            for (int c = 0; c < ncol; c++)
                ColCover[c] = 0;
        }

        //For each row of the cost matrix, find the smallest element and subtract
        //it from every element in its row.  When finished, Go to Step 2.
        private static void step_one(ref int step)
        {
            int min_in_row;

            for (int r = 0; r < nrow; r++)
            {
                min_in_row = C[r, 0];
                for (int c = 0; c < ncol; c++)
                    if (C[r, c] < min_in_row)
                        min_in_row = C[r, c];
                for (int c = 0; c < ncol; c++)
                    C[r, c] -= min_in_row;
            }
            step = 2;
        }

        //Find a zero (Z) in the resulting matrix.  If there is no starred 
        //zero in its row or column, star Z. Repeat for each element in the 
        //matrix. Go to Step 3.
        private static void step_two(ref int step)
        {
            for (int r = 0; r < nrow; r++)
                for (int c = 0; c < ncol; c++)
                {
                    if (C[r, c] == 0 && RowCover[r] == 0 && ColCover[c] == 0)
                    {
                        M[r, c] = 1;
                        RowCover[r] = 1;
                        ColCover[c] = 1;
                    }
                }
            for (int r = 0; r < nrow; r++)
                RowCover[r] = 0;
            for (int c = 0; c < ncol; c++)
                ColCover[c] = 0;
            step = 3;
        }

        //Cover each column containing a starred zero.  If K columns are covered, 
        //the starred zeros describe a complete set of unique assignments.  In this 
        //case, Go to DONE, otherwise, Go to Step 4.
        private static void step_three(ref int step)
        {
            int colcount;
            for (int r = 0; r < nrow; r++)
                for (int c = 0; c < ncol; c++)
                    if (M[r, c] == 1)
                        ColCover[c] = 1;

            colcount = 0;
            for (int c = 0; c < ncol; c++)
                if (ColCover[c] == 1)
                    colcount += 1;
            if (colcount >= ncol || colcount >=nrow)
                step = 7;
            else
                step = 4;
        }

        //methods to support step 4
        private static void find_a_zero(ref int row, ref int col)
        {
            int r = 0;
            int c;
            bool done;
            row = -1;
            col = -1;
            done = false;
            while (!done)
            {
                c = 0;
                while (true)
                {
                    if (C[r, c] == 0 && RowCover[r] == 0 && ColCover[c] == 0)
                    {
                        row = r;
                        col = c;
                        done = true;
                    }
                    c += 1;
                    if (c >= ncol || done)
                        break;
                }
                r += 1;
                if (r >= nrow)
                    done = true;
            }
        }

        private static bool star_in_row(int row)
        {
            bool tmp = false;
            for (int c = 0; c < ncol; c++)
                if (M[row, c] == 1)
                    tmp = true;
            return tmp;
        }

        private static void find_star_in_row(int row, ref int col)
        {
            col = -1;
            for (int c = 0; c < ncol; c++)
                if (M[row, c] == 1)
                    col = c;
        }

        //Find a noncovered zero and prime it.  If there is no starred zero 
        //in the row containing this primed zero, Go to Step 5.  Otherwise, 
        //cover this row and uncover the column containing the starred zero. 
        //Continue in this manner until there are no uncovered zeros left. 
        //Save the smallest uncovered value and Go to Step 6.
        private static void step_four(ref int step)
        {
            int row = -1;
            int col = -1;
            bool done;

            done = false;
            while (!done)
            {
                find_a_zero(ref row, ref col);
                if (row == -1)
                {
                    done = true;
                    step = 6;
                }
                else
                {
                    M[row, col] = 2;
                    if (star_in_row(row))
                    {
                        find_star_in_row(row, ref col);
                        RowCover[row] = 1;
                        ColCover[col] = 0;
                    }
                    else
                    {
                        done = true;
                        step = 5;
                        path_row_0 = row;
                        path_col_0 = col;
                    }
                }
            }
        }

        // methods to support step 5
        private static void find_star_in_col(int c, ref int r)
        {
            r = -1;
            for (int i = 0; i < nrow; i++)
                if (M[i, c] == 1)
                    r = i;
        }

        private static void find_prime_in_row(int r, ref int c)
        {
            for (int j = 0; j < ncol; j++)
                if (M[r, j] == 2)
                    c = j;
        }

        private static void augment_path()
        {
            for (int p = 0; p < path_count; p++)
                if (M[path[p, 0], path[p, 1]] == 1)
                    M[path[p, 0], path[p, 1]] = 0;
                else
                    M[path[p, 0], path[p, 1]] = 1;
        }

        private static void clear_covers()
        {
            for (int r = 0; r < nrow; r++)
                RowCover[r] = 0;
            for (int c = 0; c < ncol; c++)
                ColCover[c] = 0;
        }

        private static void erase_primes()
        {
            for (int r = 0; r < nrow; r++)
                for (int c = 0; c < ncol; c++)
                    if (M[r, c] == 2)
                        M[r, c] = 0;
        }


        //Construct a series of alternating primed and starred zeros as follows.  
        //Let Z0 represent the uncovered primed zero found in Step 4.  Let Z1 denote 
        //the starred zero in the column of Z0 (if any). Let Z2 denote the primed zero 
        //in the row of Z1 (there will always be one).  Continue until the series 
        //terminates at a primed zero that has no starred zero in its column.  
        //Unstar each starred zero of the series, star each primed zero of the series, 
        //erase all primes and uncover every line in the matrix.  Return to Step 3.
        private static void step_five(ref int step)
        {
            bool done;
            int r = -1;
            int c = -1;

            path_count = 1;
            path[path_count - 1, 0] = path_row_0;
            path[path_count - 1, 1] = path_col_0;
            done = false;
            while (!done)
            {
                find_star_in_col(path[path_count - 1, 1], ref r);
                if (r > -1)
                {
                    path_count += 1;
                    path[path_count - 1, 0] = r;
                    path[path_count - 1, 1] = path[path_count - 2, 1];
                }
                else
                    done = true;
                if (!done)
                {
                    find_prime_in_row(path[path_count - 1, 0], ref c);
                    path_count += 1;
                    path[path_count - 1, 0] = path[path_count - 2, 0];
                    path[path_count - 1, 1] = c;
                }
            }
            augment_path();
            clear_covers();
            erase_primes();
            step = 3;
        }

        //methods to support step 6
        private static void find_smallest(ref int minval)
        {
            for (int r = 0; r < nrow; r++)
                for (int c = 0; c < ncol; c++)
                    if (RowCover[r] == 0 && ColCover[c] == 0)
                        if (minval > C[r, c])
                            minval = C[r, c];
        }

        //Add the value found in Step 4 to every element of each covered row, and subtract 
        //it from every element of each uncovered column.  Return to Step 4 without 
        //altering any stars, primes, or covered lines.
        private static void step_six(ref int step)
        {
            int minval = int.MaxValue;
            find_smallest(ref minval);
            for (int r = 0; r < nrow; r++)
                for (int c = 0; c < ncol; c++)
                {
                    if (RowCover[r] == 1)
                        C[r, c] += minval;
                    if (ColCover[c] == 0)
                        C[r, c] -= minval;
                }
            step = 4;
        }

        private static void step_seven(ref int step)
        {
           
        }

        private static void genRandomMatrix()
        {
            Random rnd = new Random();

            C[0, 0] = rnd.Next();
            for (int r = 0; r < nrow; r++)
                for (int c = 0; c < ncol; c++)
                    C[r, c] = rnd.Next(100);
            resetMaskandCovers();
        }

        private static void genTestMatrix()
        {
            for (int r = 0; r < nrow; r++)
                for (int c = 0; c < ncol; c++)
                    C[r, c] = (r + 1) * (c + 1);
            resetMaskandCovers();
        }

        private static void InitMunkres()
        {
            string fname;
            string cmat;

            Console.Write("Enter file name (or press enter to generate test matrix)...");
            fname = Console.ReadLine();
            if (fname.Length > 0)
                loadMatrix(fname);
            else
            {
                Console.Write("Enter nrow....");
                nrow = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter ncol....");
                ncol = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter 1 for rnd matrix, enter 2 for c(i,j)=i*j [test] matrix...");
                cmat = Console.ReadLine();
                if (cmat == "1")
                {
                    genRandomMatrix();
                }
                else
                {
                    genTestMatrix();
                }
            }
            step = 1;
        }

        private static void RunMunkres()
        {
            bool done = false;
            while (!done)
            {
                //ShowCostMatrix();
                //ShowMaskMatrix();
                switch (step)
                {
                    case 1:
                        step_one(ref step);
                        break;
                    case 2:
                        step_two(ref step);
                        break;
                    case 3:
                        step_three(ref step);
                        break;
                    case 4:
                        step_four(ref step);
                        break;
                    case 5:
                        step_five(ref step);
                        break;
                    case 6:
                        step_six(ref step);
                        break;
                    case 7:
                        step_seven(ref step);
                        done = true;
                        break;
                }
            }
        }

        private static void ShowCostMatrix()
        {
            Console.WriteLine("\n");
            Console.WriteLine("------------Step {0}-------------", step);
            for (int r = 0; r < nrow; r++)
            {
                Console.WriteLine();
                Console.Write("     ");
                for (int c = 0; c < ncol; c++)
                {
                    Console.Write(Convert.ToString(C[r, c]) + " ");
                }
            }
        }

        private static void ShowMaskMatrix()
        {
            Console.WriteLine();
            Console.Write("\n    ");
            for (int c = 0; c < ncol; c++)
                Console.Write(" " + Convert.ToString(ColCover[c]));
            for (int r = 0; r < nrow; r++)
            {
                Console.Write("\n  " + Convert.ToString(RowCover[r]) + "  ");
                for (int c = 0; c < ncol; c++)
                {
                    Console.Write(Convert.ToString(M[r, c]) + " ");
                }
            }
        }

        public static List<int> Run(string matrix)
        {
            loadMatrix(matrix);
            step = 1;
            RunMunkres();
            var res = new List<int>();
            for (int i = 0; i < nrow; i++)
            {
                for (int c = 0; c < ncol; c++)
                {
                    if (M[i, c] != 0)
                    {
                        res.Add(c);
                        break;
                    }
                }
            }
            //ShowMaskMatrix();
            return res;
        }
    }
}
