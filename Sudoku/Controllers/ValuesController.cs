﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Sudoku.Controllers
{
    public class ValuesController : ApiController
    {
        readonly int[,] board = new int[,]
            {
            {0, 0, 6, 5, 0, 8, 4, 0, 0},
            {5, 2, 0, 0, 0, 0, 0, 0, 0},
            {0, 8, 7, 0, 0, 0, 0, 3, 1},
            {0, 0, 3, 0, 1, 0, 0, 8, 0},
            {9, 0, 0, 0, 6, 3, 0, 0, 5},
            {0, 5, 0, 0, 9, 0, 6, 0, 0},
            {1, 3, 0, 0, 0, 0, 2, 5, 0},
            {0, 0, 0, 0, 0, 0, 0, 7, 4},
            {0, 0, 5, 2, 0, 6, 3, 0, 0}
            };
        private static bool isSafe(int[,] board,
                                 int row, int col,
                                 int num)
        {
            // row check 
            for (int d = 0; d < board.GetLength(0); d++)
            {
                
                if (board[row, d] == num)
                {
                    return false;
                }
            }

            // column check
            for (int r = 0; r < board.GetLength(0); r++)
            {
                
                if (board[r, col] == num)
                {
                    return false;
                }
            }

            //box check 
            int sqrt = (int)Math.Sqrt(board.GetLength(0));
            int boxRowStart = row - row % sqrt;
            int boxColStart = col - col % sqrt;

            for (int r = boxRowStart;
                    r < boxRowStart + sqrt; r++)
            {
                for (int d = boxColStart;
                        d < boxColStart + sqrt; d++)
                {
                    if (board[r, d] == num)
                    {
                        return false;
                    }
                }
            }

            //safe return
            return true;
        }

        private static bool solveSudoku(int[,] board, int n)
        {
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i, j] == 0)
                    {
                        row = i;
                        col = j;

                        
                        // missing values return false
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    break;
                }
            }

            // no empty space left 
            if (isEmpty)
            {
                return true;
            }

            // else for each-row back
            for (int num = 1; num <= n; num++)
            {
                if (isSafe(board, row, col, num))
                {
                    board[row, col] = num;
                    if (solveSudoku(board, n))
                    {
                        return true;
                    }
                    else
                    {
                        board[row, col] = 0; // replace it 
                    }
                }
            }
            return false;
        }

        private string ReturnString(int[,]returnBoard,int N) {
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < N; r++)
            {
                for (int d = 0; d < N; d++)
                {
                    sb.Append(returnBoard[r, d]);
                }
            }

                return sb.ToString();
        }

        private int[,] RecieveString(string recievedBoard, int N)
        {
            int[,] returnBoard = new int[N,N];
            int count = 0;
            char[] digits = recievedBoard.ToCharArray();
            try
            {
                for (int r = 0; r < N; r++)
                {
                    for (int d = 0; d < N; d++)
                    {
                        returnBoard[r, d] = System.Convert.ToInt32(digits[count].ToString());
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnBoard;
        }

        // GET api/values
        public string Get()
        {
            // return intial board
            return ReturnString(board,board.GetLength(0));

        }
        // GET api/values/5
        
        public string get(string recieveString,int id)
        {
            int [,] solvedBoard = RecieveString(recieveString, id);

            if (solveSudoku(solvedBoard, id))
            {
                return ReturnString(solvedBoard, id);
            }
            else
            {
                return ("No Solution");
            }
        }
    }
}
