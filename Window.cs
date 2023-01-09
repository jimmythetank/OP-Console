using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPConsolev0._4
{
    public class Window
    {
        private int windowWidth;
        private int windowHeight;
        private int maxHeight = Console.LargestWindowHeight;
        private int maxWidth = Console.LargestWindowWidth;
        private string title;



        public Window(int width, int height, string title)
        {
            this.windowWidth = width;
            this.windowHeight = height;
            this.title = title;

            Console.Title = title;

            try
            {
                Console.WindowWidth = windowWidth;
                Console.WindowHeight = windowHeight;
                Console.BufferWidth = windowWidth;
                Console.BufferHeight = windowHeight;
                Console.CursorVisible = false;
                Console.SetWindowPosition(0, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"The defined Window dimensions are out of range, your dimensions are: Width ({windowWidth}), Height ({windowHeight}). Max ranges are: Width ({maxWidth}), Height ({maxHeight})");
            }


        }

        public int GetWidth()
        {
            return windowWidth;
        }

        public int GetHeight()
        {
            return windowHeight;
        }
    }
}
