using GHC.Parsing;
using GHC_Practice2017_Pizza.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GHC_Practice2017_Pizza
{
    public class Solver
    {
        #region Fields/Properties
        private string _inputFilePath;
        private string _outputFilePath;

        private Pizza pizza;
        private int nbRows;
        private int nbColumns;
        private int maxCellsPerSlice;
        private int minIngredientPerSlice;
        private int nbCellsMinPerSlice => minIngredientPerSlice * 2;

        private List<Slice> Slices = new List<Slice>();
        #endregion

        #region Constructor
        public Solver(string inputFilePath, string outputFilePath)
        {
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;
        }
        #endregion

        #region Public methods
        public void Solve()
        {
            ParseInput();
            Slice();
            CreateOutput();
            DisplayPoints();
        }
        #endregion

        #region Private methods
        private void ParseInput()
        {
            using (Parser parser = new Parser(_inputFilePath))
            {
                // Parse
                string line = parser.GetStringLine();
                var line1Tab = line.Split(' ');

                nbRows = int.Parse(line1Tab[0]);
                nbColumns = int.Parse(line1Tab[1]);
                minIngredientPerSlice = int.Parse(line1Tab[2]);
                maxCellsPerSlice = int.Parse(line1Tab[3]);

                pizza = new Pizza(nbRows, nbColumns);
                line = parser.GetStringLine();
                int indexRow = 0;
                while (!string.IsNullOrWhiteSpace(line))
                {
                    var lineTab = line.ToCharArray();
                    for (int c = 0; c < nbColumns; c++)
                    {
                        pizza.Cells[indexRow][c].Ingredient = lineTab[c] == 'T'
                            ? Ingredient.Tomatoe
                            : Ingredient.Mushroom;
                    }
                    indexRow++;
                    line = parser.GetStringLine();
                }
            }
        }
        
        private void Slice()
        {
            foreach (var row in pizza.Cells)
            {
                foreach (var cell in row)
                {
                    if (cell.IsInSlice)
                        continue;

                    int sliceSize = nbCellsMinPerSlice;

                    while (sliceSize <= maxCellsPerSlice)
                    {
                        foreach (Slice slice in GetCandidateSlices(cell, sliceSize))
                        {
                            if (IsSliceValid(slice))
                            {
                                Slices.Add(slice);
                                GetSliceCells(slice).ForEach(_ => _.IsInSlice = true);
                                break;
                            }
                        }
                        sliceSize++;
                    }
                }
            }
        }
                
        private List<Slice> GetCandidateSlices(PizzaCell cell, int size)
        {
            List<Slice> candidateSlices = new List<Slice>();

            for (int largeur = 1; largeur <= size; largeur++)
            {
                int reste = size % largeur;
                if (reste == 0)
                {
                    Slice slice = new Slice();
                    slice.SliceBegin = cell.Position;
                    slice.SliceEnd = new Point(
                        cell.Position.Row + ((size / largeur) - 1),
                        cell.Position.Column + largeur - 1
                        );

                    if (slice.SliceEnd.Row < pizza.NbRow
                        && slice.SliceEnd.Column < pizza.NbColumn)
                    {
                        candidateSlices.Add(slice);
                    }
                }
            }

            return candidateSlices;
        }

        private bool IsSliceValid(Slice slice)
        {
            List<PizzaCell> cells = GetSliceCells(slice);

            if (cells.Any(_ => _.IsInSlice))
                return false;

            int nbMushroom = cells.Count(_ => _.Ingredient == Ingredient.Mushroom);
            if (nbMushroom < minIngredientPerSlice)
                return false;

            int nbTomatoe = cells.Count(_ => _.Ingredient == Ingredient.Tomatoe);
            if (nbTomatoe < minIngredientPerSlice)
                return false;

            return true;
        }

        private List<PizzaCell> GetSliceCells(Slice slice)
        {
            List<PizzaCell> cells = new List<PizzaCell>();

            for (int r = slice.SliceBegin.Row; r <= slice.SliceEnd.Row; r++)
            {
                for (int c = slice.SliceBegin.Column; c <= slice.SliceEnd.Column; c++)
                {
                    cells.Add(pizza.Cells[r][c]);
                }
            }

            return cells;
        }

        private void CreateOutput()
        {
            using (FileCreator fc = new FileCreator(_outputFilePath))
            {
                fc.WriteLine(Slices.Count().ToString());

                foreach (Slice slice in Slices)
                {
                    string strSlice = $"{slice.SliceBegin.Row} {slice.SliceBegin.Column} {slice.SliceEnd.Row} {slice.SliceEnd.Column}";
                    fc.WriteLine(strSlice);
                }
            }
        }

        private void DisplayPoints()
        {
            int nbCellInSlice = pizza.Cells.SelectMany(_ => _).Count(_ => _.IsInSlice);
            Console.WriteLine(nbCellInSlice);
        }
        #endregion
    }
}