using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPConsolev0._4
{
    public class Element
    {
        //array to hold cells that make up element
        public Cell[,] Cells { get; set; }
        //list of text strings present in the element
        public List<string> Text = new List<string>();
        public string Id { get; } //unique id of element
        public int Width { get; set; }
        public int Height { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public ConsoleColor BgColour { get; set; }
        public bool HasBorder { get; set; }
        public ConsoleColor BorderColour { get; set; }
       

        //constructor
        public Element(string id, int width, int height, int topPos, int leftPos, ConsoleColor bgColour)
        {
            this.Id = id;
            this.Width = width;
            this.Height = height;
            this.Top = topPos;
            this.Left = leftPos;
            this.BgColour = bgColour;
            this.HasBorder = false;

            Dimensions(width, height);           
        }

        //adds text to List
        public void AddText(string text)
        {
            foreach(string s in Text)
            {

            }
        }

        //changes the dimensions of Element
        public void Dimensions(int width, int height)
        {
            //build array of cells that make up the element
            Cells = new Cell[height, width];

            //create new Cells, add to Cells array        
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Cells[i, j] = new Cell(Id, i, j, i + Top, j + Left, BgColour);
                }
            }
        }

        //change Element position on _canvas, also changes the position attribute of the cells
        public void ChangePos(int top, int left)
        {
            this.Top = top;
            this.Left = left;

            //change the _canvas position of the Cells in Cell array of Element
            for(int i = 0; i < Cells.GetLength(0); i++)
            {
                for(int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i, j].CanvasTopPos = i + Top;
                    Cells[i, j].CanvasLeftPos = j + Left;
                }
            }
        }

        //changes the Element bgcolour and the bgcolour of the Cell's in the array
        public void ChangeBgColour(ConsoleColor col)
        {
            this.BgColour = col;

            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (!Cells[i, j].IsBorder)
                    {
                        Cells[i, j].BgColour = BgColour;
                    }
                                        
                }
            }
        }
    }
}
