using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPConsolev0._4
{
    public class Cell
    {
        public char Character { get; set; }
        public string Id { get; }
        public int TopPos { get; } //top position in the element
        public int LeftPos { get; }
        public int CanvasTopPos { get; set; } //top position on the canvas
        public int CanvasLeftPos { get; set; }
        public bool IsBorder { get; set; } //is this a border cell?
        public ConsoleColor BgColour { get; set; }
        

        public Cell(string id, int topPos, int leftPos, int canvasTopPos, int canvasLeftPos, ConsoleColor bgColour)
        {
            this.Id = id;
            this.TopPos = topPos;
            this.LeftPos = leftPos;
            this.CanvasTopPos = canvasTopPos;
            this.CanvasLeftPos = canvasLeftPos;
            this.BgColour = bgColour;
            this.Character = ' ';
            this.IsBorder = false;
            
        }
    }
}
