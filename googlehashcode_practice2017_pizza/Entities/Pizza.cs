using System.Collections.Generic;

namespace GHC_Practice2017_Pizza.Entities
{
    public class Pizza
    {
        public int NbRow { get; private set; }
        public int NbColumn { get; private set; }
        public List<List<PizzaCell>> Cells;

        public Pizza(int nbRow, int nbColumn)
        {
            NbRow = nbRow;
            NbColumn = nbColumn;

            Cells = new List<List<PizzaCell>>();
            for(int r=0; r<nbRow; r++)
            {
                var cells = new List<PizzaCell>();
                for (int c = 0; c < nbColumn; c++)
                {
                    var pizzaCell = new PizzaCell();
                    pizzaCell.Position = new Point(r, c);
                    cells.Add(pizzaCell);
                }
                Cells.Add(cells);
            }
        }
    }
}