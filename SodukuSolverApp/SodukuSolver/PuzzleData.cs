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


        public PuzzleData(int[,] puzzleData)
        {
            this.puzzleData = puzzleData;
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

            int colIndex = blockIndexes[block].ColumnIndex;
            int rowIndex = blockIndexes[block].RowIndex;

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

        public IEnumerable<string> CheckHiddenSingles(List<int>[,] Notes)
        {
            List<string> Messages = new List<string>();


            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                {
                    if (Notes[rowIndex, columnIndex] != null)
                    {
                        if (Notes[rowIndex, columnIndex].Count == 1)
                        {
                            Messages.Add($"Naked single {Notes[rowIndex, columnIndex][0]} in row {rowIndex} at col {columnIndex}");
                        }
                    }
                }
            }

            // check rows for single hidden 
            for (int i = 1; i <= 9; i++)
            {
                int totalRowCount = 0;
                string singleRowMessage = string.Empty;
                for (int rowIndex = 0; rowIndex < 9; rowIndex++)
                {
                    totalRowCount = 0;

                    for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                    {
                        if (Notes[rowIndex, columnIndex] != null)
                        {
                            if (Notes[rowIndex, columnIndex].Contains(i))
                            {
                                totalRowCount++;
                                singleRowMessage = $"Hidden single {i} in row {rowIndex} at col {columnIndex}";
                            }
                        }
                    }

                    if (totalRowCount == 1)
                    {
                        Messages.Add(singleRowMessage);
                    }
                }
   
            }

            // check columns for single hidden
            for (int i = 1; i <= 9; i++)
            {
                int totalColumnCount = 0;
                string singleRowMessage = string.Empty;
                for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                {
                    totalColumnCount = 0;

                    for (int rowIndex = 0; rowIndex < 9; rowIndex++)
                    {
                        if (Notes[rowIndex, columnIndex] != null)
                        {
                            if (Notes[rowIndex, columnIndex].Contains(i))
                            {
                                totalColumnCount++;
                                singleRowMessage = $"Hidden single {i} in col {columnIndex} at row {rowIndex}";
                            }
                    
                        }
                    }

                    if (totalColumnCount == 1)
                    {
                        Messages.Add(singleRowMessage);
                    }
                }

    
            }

            // check the blocks

            foreach (var blockIndex in blockIndexes)
            {
                for (int i = 1; i <= 9; i++)
                {
                    int totalBlockCount = 0;
                    string singleBlockMessage = string.Empty;
                    for (int rowIndex = blockIndex.RowIndex; rowIndex < blockIndex.RowIndex+3; rowIndex++)
                    {
                        for (int columnIndex = blockIndex.ColumnIndex; columnIndex < blockIndex.ColumnIndex+3; columnIndex++)
                        {
                            if (Notes[rowIndex, columnIndex] != null)
                            {
                                if (Notes[rowIndex, columnIndex].Contains(i))
                                {
                                    totalBlockCount++;
                                    singleBlockMessage = $"Hidden single {i} block {blockIndex.BlockNumber} at row {rowIndex}, col {columnIndex}";
                                }
                            }
                        }
                    }

                    if (totalBlockCount == 1)
                    {
                        Messages.Add(singleBlockMessage);
                    }
                }
            }


            return Messages;
        }

 
        public static BlockIndex[] blockIndexes = new BlockIndex[9]
        {
                new BlockIndex() {BlockNumber = 1, RowIndex = 0, ColumnIndex = 0},
                new BlockIndex() {BlockNumber = 2, RowIndex = 0, ColumnIndex = 3},
                new BlockIndex() {BlockNumber = 3, RowIndex = 0, ColumnIndex = 6},
                new BlockIndex() {BlockNumber = 4, RowIndex = 3, ColumnIndex = 0},
                new BlockIndex() {BlockNumber = 5, RowIndex = 3, ColumnIndex = 3},
                new BlockIndex() {BlockNumber = 6, RowIndex = 3, ColumnIndex = 6},
                new BlockIndex() {BlockNumber = 7, RowIndex = 6, ColumnIndex = 0},
                new BlockIndex() {BlockNumber = 8, RowIndex = 6, ColumnIndex = 3},
                new BlockIndex() {BlockNumber = 9, RowIndex = 6, ColumnIndex = 6}
        };
        


        internal class BlockIndex
        {
            public int BlockNumber;
            public int RowIndex;
            public int ColumnIndex;
        }

    }

  
}
