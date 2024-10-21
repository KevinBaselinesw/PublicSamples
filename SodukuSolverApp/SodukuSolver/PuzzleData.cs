using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukuSolver
{
  


    internal class PuzzleData
    {
        private int[,] puzzleData = new int[9, 9];

        static int[,] testPuzzle = new int[,]
        {
            { 0,0,0,5,8,0,0,0,0 },
            { 0,0,0,0,0,1,6,0,0 },
            { 0,9,3,0,6,0,0,4,0 },
            { 3,0,4,0,0,0,0,0,0 },
            { 0,5,0,6,0,0,9,3,2 },
            { 0,6,0,7,3,0,0,0,1 },
            { 0,0,8,1,2,6,0,0,0 },
            { 0,0,0,4,7,0,0,9,0 },
            { 0,0,0,0,5,0,0,6,4 }
        };

        public PuzzleData()
        {
            puzzleData = testPuzzle;
        }

        public int[] GetRowValues(int row)
        {
            if (row < 0 || row > 8)
            {
                throw new Exception("specified row is out of range (0-8)");
            }

            int[] Values = new int[9];

            for (int i = 0; i < 9; i++)
            {
                Values[i] = testPuzzle[row, i];
            }

            return Values;
        }

        public int[] GetColumnValues(int column)
        {
            if (column < 0 || column > 8)
            {
                throw new Exception("specified column is out of range (0-8)");
            }

            int[] Values = new int[9];

            for (int i = 0; i < 9; i++)
            {
                Values[i] = testPuzzle[i, column];
            }

            return Values;
        }

        public int[] GetBlockValues(int block)
        {
            if (block < 0 || block > 8)
            {
                throw new Exception("specified block is out of range (0-8)");
            }

            int[] Values = new int[9];

            int colIndex = (block % 3) * 3;
            int rowIndex = (block / 3) * 3;

            Values[0] = testPuzzle[rowIndex + 0, colIndex + 0];
            Values[1] = testPuzzle[rowIndex + 0, colIndex + 1];
            Values[2] = testPuzzle[rowIndex + 0, colIndex + 2];

            Values[3] = testPuzzle[rowIndex + 1, colIndex + 0];
            Values[4] = testPuzzle[rowIndex + 1, colIndex + 1];
            Values[5] = testPuzzle[rowIndex + 1, colIndex + 2];

            Values[6] = testPuzzle[rowIndex + 2, colIndex + 0];
            Values[7] = testPuzzle[rowIndex + 2, colIndex + 1];
            Values[8] = testPuzzle[rowIndex + 2, colIndex + 2];

            return Values;
        }

    }
}
