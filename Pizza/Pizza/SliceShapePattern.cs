using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pizza
{
    public class SliceShapePattern
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public SliceShapePattern(int r, int c)
        {            
            this.Rows = r;
            this.Columns = c;
        }

        public int GetShapeSize()
        {            
            return this.Rows * this.Columns;
        }
        
        /// <summary>        
        /// </summary>
        /// <param name="mr">Max Row cells</param>
        /// <param name="mc">Max Columsn cells</param>
        /// <param name="numMaxCells"></param>
        /// <returns></returns>
        public static List<SliceShapePattern> GetAllPosibleSliceShapes(int pizzaNumRows, int pizzaNumColumns, int numMaxCells)
        {
            List<SliceShapePattern> patterns = new List<SliceShapePattern>();

            int maxRowLength = (pizzaNumRows <= numMaxCells) ? pizzaNumRows : numMaxCells;
            int maxColumnLength = (pizzaNumColumns <= numMaxCells) ? pizzaNumColumns : numMaxCells;
            for (int r = 1; r <= maxRowLength; r++)
            {
                for (int c = 1; c <= maxColumnLength; c++)
                {
                    bool isValid = (r*c) <= numMaxCells;
                    if (!isValid)
                        break;

                    patterns.Add(new SliceShapePattern(r,c));
                }
            }

            patterns = patterns.OrderBy(p => p.Rows * p.Columns).ToList();

            return patterns;
        }
    }
}
