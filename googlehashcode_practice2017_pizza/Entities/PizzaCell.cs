using System.Diagnostics;

namespace GHC_Practice2017_Pizza.Entities
{
    public enum Ingredient { Tomatoe, Mushroom};

    [DebuggerDisplay("{Ingredient} {IsInSlice}")]
    public class PizzaCell
    {
        public Point Position;
        public Ingredient Ingredient;

        public bool IsInSlice;
    }
}