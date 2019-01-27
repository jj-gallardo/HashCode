using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Pizza.Pizza;

namespace Pizza
{
    class Program
    {


        static void Main(string[] args)
        {
            string[] files = new string[] { "example", "small", "medium", "big" };
            foreach (var file in files)
            {
                Console.WriteLine($"Processing {file}");
                var pizza = LoadPizza(file);
                pizza.Run();
                Save(file, pizza);
            }
         
        }

        static Pizza LoadPizza(string file)
        {
            string filename = file + ".in";
            string[] fileContent = File.ReadAllLines(filename);
            int[] pizzaParams = fileContent[0].Split(' ').Select((s) => int.Parse(s)).ToArray();

            int rows = pizzaParams[0];
            int columns = pizzaParams[1];
            int ingredientsMin = pizzaParams[2];
            int cellsMax = pizzaParams[3];

            int[,] pizzaMask = new int[rows, columns];

            for (int i = 1; i < fileContent.Length; i++)
            {
                for (int j = 0; j < fileContent[i].Length; j++)
                {
                    switch (fileContent[i][j])
                    {
                        case 'T':
                            pizzaMask[i - 1, j] = PizzaIngredient.TOMATO;
                            break;
                        case 'M':
                            pizzaMask[i - 1, j] = PizzaIngredient.MUSHROOM;
                            break;
                    }
                }
            }

            return new Pizza(rows, columns, ingredientsMin, cellsMax, pizzaMask);
        }


        public static void Save(string file, Pizza p)
        {
            string filename = file + ".out";
            List<string> content = new List<string>();
            content.Add(p.selectedSlices.Count.ToString());
            foreach (var sc in p.selectedSlices)
            {
                content.Add($"{sc.locR} {sc.locC} {(sc.SliceShapePattern.Rows + sc.locR - 1)} {(sc.SliceShapePattern.Columns + sc.locC) - 1}");
            }

            File.WriteAllLines(filename, content.ToArray());
        }


        public static void PrintPizza(Pizza p)
        {

            for (int i = 0; i < p.Rows; i++)
            {
                for (int j = 0; j < p.Columns; j++)
                {
                    switch (p.PizzaMask[i, j])
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                        default:
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;
                    }
                    Console.Write(p.PizzaMask[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}
