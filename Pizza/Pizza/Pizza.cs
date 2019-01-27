using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza
{
    public class Pizza
    {
        public class PizzaIngredient
        {
            public static int NONE = 0;
            public static int TOMATO = 1;
            public static int MUSHROOM = 2;
        }

        public int[,] PizzaMask;
        public int Rows { get; }
        public int Columns { get; }
        public int NumMinIngredientsSlice { get; }
        public int MaxCellsSlice { get; }

        public List<SliceShapePattern> shapePatterns;

        public List<Slice> selectedSlices;

        public Pizza(int rows, int columns, int numMinIngredientsSlice, int maxCellsSlice, int[,] pizzaMask)
        {
            Rows = rows;
            Columns = columns;
            NumMinIngredientsSlice = numMinIngredientsSlice;
            MaxCellsSlice = maxCellsSlice;
            PizzaMask = pizzaMask;
            this.shapePatterns = SliceShapePattern.GetAllPosibleSliceShapes(rows, columns, maxCellsSlice);
            this.selectedSlices = new List<Slice>();
        }



        public Slice GetBestSliceForLocation(int r, int c)
        {
            int[] location = new int[2] { r, c };
            int bestScore = 0;
            Slice bestSlice = null;
            foreach (var pattern in shapePatterns)
            {
                Slice slice = new Slice(location, pattern);
                if (slice.IsValidSliceForPizza(this)){
                    if (bestSlice == null)
                    {
                        bestSlice = slice;
                    }

                    int sliceScore = slice.GetSliceScore(this);
                    if (bestScore < sliceScore)
                    {
                        bestScore = sliceScore;
                        bestSlice = slice;
                    }
                }      
            }

            //if (bestSlice != null)
            //    Console.WriteLine($"Checking {r} {c}  -> Best: {bestSlice.locR} {bestSlice.locC} {(bestSlice.SliceShapePattern.Rows + bestSlice.locR - 1)} {(bestSlice.SliceShapePattern.Columns + bestSlice.locC) - 1}");
            //else
            //    Console.WriteLine($"Checking {r} {c}  -> Best: None");            
            return bestSlice;
        }

        public void RemoveSliceFromPizza(Slice slice)
        {
            for (int i = slice.locR; i < slice.locR + slice.SliceShapePattern.Rows; i++)
            {
                for (int j = slice.locC; j < slice.locC + slice.SliceShapePattern.Columns; j++)
                {
                    this.PizzaMask[i, j] = PizzaIngredient.NONE; //Remove ingredient
                }
            }
        }

        public void Run()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if(this.PizzaMask[i,j] != PizzaIngredient.NONE)
                    {
                        Slice bestSlice = this.GetBestSliceForLocation(i, j);
                        if (bestSlice != null)
                        {
                            this.RemoveSliceFromPizza(bestSlice);
                            this.selectedSlices.Add(bestSlice);
                        }
                    }
                }
            }
        }
    }
}
