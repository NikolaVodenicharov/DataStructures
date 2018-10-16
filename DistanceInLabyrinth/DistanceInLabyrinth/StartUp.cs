namespace DistanceInLabyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StartUp
    {
        private const string StartingPositionSymbol = "*";
        private const string EmptySymbol = "0";
        private const string UnreacheableSymbol = "u";

        private static string[,] matrix;
        private static int firstMatrixRowIndex = 0;
        private static int lastMatrixRowIndex;
        private static int firstMatrixColumnIndex = 0;
        private static int lastMatrixColumnIndex;

        public static void Main()
        {
            matrix = ReadMatrix();
            lastMatrixRowIndex = matrix.GetLength(0) - 1;
            lastMatrixColumnIndex = matrix.GetLength(1) - 1;

            FindCellsMinimalDistance();
            MarkUnreacheableMatrixFields();

            var result = MatrixToString(matrix);
            Console.WriteLine();
            Console.WriteLine(result);
        }

        private static string[,] ReadMatrix()
        {
            var size = int.Parse(Console.ReadLine());
            var matrix = new string[size, size];

            for (int row = 0; row < size; row++)
            {
                var line = Console.ReadLine();

                for (int col = 0; col < size; col++)
                {
                    matrix[row, col] = line[col].ToString();
                }
            }

            return matrix;
        }

        private static void FindCellsMinimalDistance()
        {
            var cells = new Queue<Cell>();
            cells.Enqueue(CreateStartingCell());

            while (cells.Count > 0)
            {
                var cell = cells.Dequeue();

                ExpandUp(cell, cells);
                ExpandRight(cell, cells);
                ExpandDown(cell, cells);
                ExpandLeft(cell, cells);
            }
        }
        private static Cell CreateStartingCell()
        {
            for (int row = 0; row <= lastMatrixRowIndex; row++)
            {
                for (int col = 0; col <= lastMatrixColumnIndex; col++)
                {
                    if (matrix[row, col] == StartingPositionSymbol)
                    {
                        return
                            new Cell
                            {
                                Row = row,
                                Column = col,
                                MovesCounter = 0
                            };
                    }
                }
            }

            throw new ArgumentException("No starting position found.");
        }
        private static void ExpandUp(Cell cell, Queue<Cell> cells)
        {
            var previousRow = cell.Row - 1;

            if (previousRow >= firstMatrixRowIndex &&
                matrix[previousRow, cell.Column] == EmptySymbol)
            {
                ExpandCell(previousRow, cell.Column, cell.MovesCounter + 1, cells);
            }
        }
        private static void ExpandRight(Cell cell, Queue<Cell> cells)
        {
            var nextColumn = cell.Column + 1;

            if (nextColumn <= lastMatrixColumnIndex &&
                matrix[cell.Row, nextColumn] == EmptySymbol)
            {
                ExpandCell(cell.Row, nextColumn, cell.MovesCounter + 1, cells);
            }
        }
        private static void ExpandDown(Cell cell, Queue<Cell> cells)
        {
            var nextRow = cell.Row + 1;

            if (nextRow <= lastMatrixRowIndex &&
                matrix[nextRow, cell.Column] == EmptySymbol)
            {
                ExpandCell(nextRow, cell.Column, cell.MovesCounter + 1, cells);
            }
        }
        private static void ExpandLeft(Cell cell, Queue<Cell> cells)
        {
            var previousColumn = cell.Column - 1;

            if (previousColumn >= firstMatrixColumnIndex &&
                matrix[cell.Row, previousColumn] == EmptySymbol)
            {
                ExpandCell(cell.Row, previousColumn, cell.MovesCounter + 1, cells);
            }
        }
        private static void ExpandCell(int row, int column, int moveCounter, Queue<Cell> cells)
        {
            matrix[row, column] = moveCounter.ToString();

            cells.Enqueue(
                new Cell
                {
                    Row = row,
                    Column = column,
                    MovesCounter = moveCounter
                });
        }

        private static void MarkUnreacheableMatrixFields()
        {
            for (int row = 0; row <= lastMatrixRowIndex; row++)
            {
                for (int col = 0; col <= lastMatrixColumnIndex; col++)
                {
                    if (matrix[row, col] == EmptySymbol)
                    {
                        matrix[row, col] = UnreacheableSymbol;
                    }
                }
            }
        }

        public static string MatrixToString(string[,] matrix)
        {
            var sb = new StringBuilder();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    sb.Append($"{matrix[row, col]}");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
