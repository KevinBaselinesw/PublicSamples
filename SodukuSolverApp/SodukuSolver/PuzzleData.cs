using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
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

        public List<int>[,] GetPuzzleNotes() 
        {
            List<int>?[,] Notes = new List<int>[9, 9];

            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                {
                    if (puzzleData[rowIndex, columnIndex] > 0)
                    {
                        Notes[rowIndex, columnIndex] = null;
                        continue;
                    }

                    // start with all possible values
                    Notes[rowIndex, columnIndex] = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                    // remove any that exist in the row
                    var rowValues = GetRowValues(rowIndex);
                    foreach (var rval in rowValues)
                    {
                        if (rval > 0)
                        {
                            Notes[rowIndex, columnIndex]?.Remove(rval);
                        }
                    }

                    // remove any that exist in the column
                    var colValues = GetColumnValues(columnIndex);
                    foreach (var colVal in colValues)
                    {
                        if (colVal > 0)
                        {
                            Notes[rowIndex, columnIndex]?.Remove(colVal);
                        }
                    }


                    // calculate the block index
                    int blockIndex = 0;

                    if (rowIndex < 3)
                        blockIndex = 0;
                    else if (rowIndex < 6)
                        blockIndex = 3;
                    else if (rowIndex < 9)
                        blockIndex = 6;

                    blockIndex += (columnIndex / 3);

                    // remove any that exist in the block
                    var blockValues = GetBlockValues(blockIndex);
                    foreach (var blkVals in blockValues)   
                    {
                        if (blkVals > 0)
                        {
                            Notes[rowIndex, columnIndex]?.Remove(blkVals);
                        }
                    }

                }
            }

            return Notes!;
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
                Values[i] = puzzleData[row, i];
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
                Values[i] = puzzleData[i, column];
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

            Values[0] = puzzleData[rowIndex + 0, colIndex + 0];
            Values[1] = puzzleData[rowIndex + 0, colIndex + 1];
            Values[2] = puzzleData[rowIndex + 0, colIndex + 2];

            Values[3] = puzzleData[rowIndex + 1, colIndex + 0];
            Values[4] = puzzleData[rowIndex + 1, colIndex + 1];
            Values[5] = puzzleData[rowIndex + 1, colIndex + 2];

            Values[6] = puzzleData[rowIndex + 2, colIndex + 0];
            Values[7] = puzzleData[rowIndex + 2, colIndex + 1];
            Values[8] = puzzleData[rowIndex + 2, colIndex + 2];

            return Values;
        }

    }
}
