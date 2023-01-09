using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OPConsolev0._4
{
    public class OPC
    {
        //main window - an array of Cell Lists
        private List<Cell>[,] _canvas;
        //List of Elements on screen
        public List<Element> Screen { get; set; }
        
        //load black screen/canvas
        public void Start(int width, int height, string col)
        {
            //get string col as ConsoleColor
            ConsoleColor colour = GetConsoleColor(col);

            //make array correct dimensions
            _canvas = new List<Cell>[height, width];

            //initialise Screen List
            Screen = new List<Element>();

            //make new Element - id, widht, height, topPos, leftPos, colour
            Element canvas = new Element("Canvas", width, height, 0, 0, colour);

            //put Cells from Element into Canvas Lists
            for(int i = 0; i < _canvas.GetLength(0); i++)
            {
                for(int j = 0; j < _canvas.GetLength(1); j++)
                {
                    //initialise correct List in _canvas
                    _canvas[i, j] = new List<Cell>();
                    
                    //get Cell from Element to be drawn
                    Cell cell = canvas.Cells[i, j];                   
                    //add Cell to canvas List at correct coords
                    _canvas[i, j].Add(cell);
                    //send each Cell to Draw function
                    Draw(cell);
                }
            }
        }

        public void New(string id, int width, int height, int topPos, int leftPos, string col)
        {
            //get string as ConsoleColor
            ConsoleColor c = GetConsoleColor(col);

            //build Element
            Element e = new Element(id, width, height, topPos, leftPos, c);

            //add new Element to Screen List
            Screen.Add(e);

            //draw element to _canvas
            Draw(e);
        }

        //draw new element to _canvas
        public void Draw(Element e)
        {
            foreach(Cell c in e.Cells)
            {
                //get List at correct coords
                List<Cell> l = _canvas[c.CanvasTopPos, c.CanvasLeftPos];
                
                //if result is true, remove Cell from List
                if (l.Exists(x => x.Id == c.Id))
                {
                    //find cell with the same id as new cell (c)
                    Cell cell = l.Find(item => item.Id.Equals(c.Id));
                    //delete cell from List
                    l.Remove(cell);
                }

                //add Cell to end of List
                l.Add(c);

                //draw Cell to _canvas
                Draw(c);
            }   
        }

        //used for drawing a single Cell to _canvas
        private void Draw(Cell c)
        {
            Console.CursorTop = c.CanvasTopPos;
            Console.CursorLeft = c.CanvasLeftPos;
            Console.BackgroundColor = c.BgColour;
            Console.Write(c.Character);
            Console.ResetColor();
        }

        //remove Element from _canvas using id of Element
        public void Remove(string id)
        {
            //find correct Element in Screen List using the Id
            Element e = GetElementById(id);

            //send Element to Remove function
            Remove(e);
        }

        private void Remove(Element e)
        {
            
            //remove Cells at correct _canvas position and List position
            foreach (Cell c in e.Cells)
            {
                //get List at correct _canvas position
                List<Cell> l = _canvas[c.CanvasTopPos, c.CanvasLeftPos];
                //remove c from List
                l.Remove(c);
                //Draw topmost Cell in List
                Draw(l.Last());
            }
        }

        //Move the Element to another position on the _canvas
        public void Move(string id, int top, int left)
        {
            //get correct Element from Screen List
            Element e = GetElementById(id);

            //remove Cells from current position in List on _canvas
            Remove(e);

            //set new _canvas coords of the Element. this method also changes the CanvasTop/Left of each Cell in the Element
            e.ChangePos(top, left);

            //draw Cells in new position
            Draw(e);
        }

        //resizes an element
        public void Resize(string id, int width, int height)
        {
            //get element from id
            Element e = GetElementById(id);

            //remove element from _canvas
            Remove(e);

            //change dimensions of Element
            e.Dimensions(width, height);

            //check if element had a border, if so, draw it
            if (e.HasBorder)
            {
                //border function uses Draw(e)
                Border(id, e.BorderColour.ToString());
            }
            else
            {
                //if no border, draw(e)
                Draw(e);
            }
   
        }
       
        public void BgColour(string id, string col)
        {
            //get Element from Screen
            Element e = GetElementById(id);

            //get ConsoleColour from string colour
            ConsoleColor c = GetConsoleColor(col);
           
            //change the background colour of Element - method will change the bgcolour of the cells too
            //and takes into account borders 
            e.ChangeBgColour(c);

            //draw Element to _canvas again
            Draw(e);
        }
       
        public void Border(string id, string col)
        {
            //get element and consolecolor, set HasBorder in Element class to true
            Element e = GetElementById(id);
            ConsoleColor colour = GetConsoleColor(col);
            e.HasBorder = true;
            e.BorderColour = colour;

            //add border to top and bottom row
            for(int i = 0; i < e.Cells.GetLength(1); i++)//across
            {
                for(int j = 0; j < e.Cells.GetLength(0); j++)//down
                {
                    //top: 0 down, i across
                    e.Cells[0, i].BgColour = colour;
                    //set border cell to Border = true. this is so that we have a way to identify the border cells, so that if the bgcolour is changed it doesnt overwrite border
                    e.Cells[0, i].IsBorder = true;
                    //bottom: height -1 down, i across
                    e.Cells[e.Cells.GetLength(0) - 1, i].BgColour = colour;
                    e.Cells[e.Cells.GetLength(0) - 1, i].IsBorder = true;
                    //left: j down, 0 across; j down, 1 across
                    e.Cells[j, 0].BgColour = colour;
                    e.Cells[j, 1].BgColour = colour;
                    e.Cells[j, 0].IsBorder = true;
                    e.Cells[j, 1].IsBorder = true;
                    //right: j down, width -1 across; j down, width -2 across
                    e.Cells[j, e.Cells.GetLength(1) - 1].BgColour = colour;
                    e.Cells[j, e.Cells.GetLength(1) - 2].BgColour = colour;
                    e.Cells[j, e.Cells.GetLength(1) - 1].IsBorder = true;
                    e.Cells[j, e.Cells.GetLength(1) - 2].IsBorder = true;
                }
            }

            Draw(e);

        }

        public void Text(string id, string text)
        {
            Element e = GetElementById(id);

            //save the text string in the Text List in Element
            e.AddText(text);

            //a counter for the index of text char
            int counter = 0;

            //add text to element, takes into account borders
            foreach(Cell cell in e.Cells)
            {
                if (!cell.IsBorder && counter != text.Length)
                {
                    cell.Character = text[counter];
                    counter++;
                }
                
            }
            
            /*for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < e.Cells.GetLength(1); j++)
                {
                    if(counter == text.Length)
                    {
                        break;
                    }

                    if(e.Cells[j, i].IsBorder == false)
                    {
                        e.Cells[i, j].Character = text[counter];
                        counter++;
                    }
                    
                    
                }
            }*/

            Draw(e);
        }

        public Element GetElementById(string id)
        {
            Element e = Screen.Find(x => x.Id == id);

            return e;
        }

        public ConsoleColor GetConsoleColor(string col)
        {
            if(Enum.TryParse(col, true, out ConsoleColor colour))
            {
                return colour;
            }

            //if the parsing of the string does not return a valid ConsoleColor, default to black
            return ConsoleColor.Black;
        }
    }
}
