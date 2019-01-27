using System;
using System.Collections.Generic;
using System.Text;
using static Pizza.Pizza;

namespace Pizza
{
    public class Slice
    {
        public int locR { get; set; }
        public int locC { get; set; }
        public SliceShapePattern SliceShapePattern { get; }
        public Slice(int[] location, SliceShapePattern sliceShapePattern)
        {
            locR = location[0];
            locC = location[1];
            SliceShapePattern = sliceShapePattern;
        }


        public bool IsValidSliceForPizza(Pizza p)
        {
            if (this.SliceShapePattern.GetShapeSize() > p.MaxCellsSlice)
                return false;

            int rowLimit = locR + SliceShapePattern.Rows;
            int columnLimit = locC + SliceShapePattern.Columns;

            if (rowLimit > p.Rows || columnLimit > p.Columns)
                return false;

            int nTomatoes = 0;
            int nMushrooms = 0;

            int[,] pizzaMask = p.PizzaMask;
            for (int i = locR; i < rowLimit; i++)
            {
                for (int j = locC; j < columnLimit; j++)
                {
                    if (pizzaMask[i, j] == PizzaIngredient.TOMATO)
                        nTomatoes++;
                    if (pizzaMask[i, j] == PizzaIngredient.MUSHROOM)
                        nMushrooms++;

                    if (pizzaMask[i, j] == PizzaIngredient.NONE)
                        return false;
                }
            }

            return nTomatoes >= p.NumMinIngredientsSlice && nMushrooms >= p.NumMinIngredientsSlice;
        }

        public int GetSliceScore(Pizza p)
        {
            int[,] pizzaMask = p.PizzaMask;
            int score = 0;
            for (int i = locR; i < locR + SliceShapePattern.Rows; i++)
            {
                for (int j = locC; j < locC + SliceShapePattern.Columns; j++)
                {
                    if (pizzaMask[i, j] != PizzaIngredient.NONE)
                        score++;                  
                }
            }
            return score;
        }
    }
}
