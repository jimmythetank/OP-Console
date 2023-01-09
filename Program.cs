using OPConsolev0._4;
using System;
using System.Diagnostics;

namespace OPConsolev0._5
{
    internal class Program
    {
        static void Main()
        {
            Window w = new Window(200, 50, "OP Console v0.5");
            OPC opc = new OPC();

            opc.Start(w.GetWidth(), w.GetHeight(), "black");

            /*opc.New("box1", 20, 10, 25, 90, "red");

            opc.Border("box1", "yellow");
            Console.ReadLine();
            opc.Text("box1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc eget orci sapien. Proin at malesuada erat. Suspendisse blandit sed leo.");
            Console.ReadLine();
            opc.Border("box1", "green");
            Console.ReadLine();
            opc.Resize("box1", 30, 15);
            //opc.Move("box1", 5, 20);
            //Console.ReadLine();
            //opc.BgColour("box1", "blue");
            //Console.ReadLine();*/


            Demo(opc);


        }

        public static void Demo(OPC opc)
        {
            //change this for more or less boxes on screen
            int maxBoxes = 10;
            //instantiate Random object
            Random rand = new Random();
            //make array of ConsoleColors
            Array colours = Enum.GetValues(typeof(ConsoleColor));
            //add position values to array (for randomising positions)
            int[] pos = new int[100];

            //array of values for resizing
            int[] size = { 1, 2, 3, 4, 5, -1, -2, -3, -4, -5 };
            //make loop for filling pos array with random values
            for (int j = 0; j < pos.Length; j++)
            {
                pos[j] = rand.Next(35);
            }

            //array for boxes
            Element[] boxes = new Element[maxBoxes];

            //create random boxes and draw to screen in random positions
            for (int i = 0; i < maxBoxes; i++)
            {
                //get random ConsoleColor
                ConsoleColor col = (ConsoleColor)colours.GetValue(rand.Next(colours.Length));
                //parse ConsoleColor to string
                string colour = col.ToString();
                //if col doesnt equal black, draw boxes
                if (col != ConsoleColor.Black)
                {
                    //draw boxes at random positions on _canvas
                    opc.New($"box{i}", 20, 10, pos[rand.Next(pos.Length)], pos[rand.Next(pos.Length)] * 5, colour);
                    
                }
            }
            Console.ReadLine();
            //add random borders to elements
            for(int m = 0; m < opc.Screen.Count; m++)
            {
                //get random ConsoleColor
                ConsoleColor col = (ConsoleColor)colours.GetValue(rand.Next(colours.Length));
                //parse ConsoleColor to string
                string colour = col.ToString();

                //make borders random colours
                opc.Border(opc.Screen[m].Id, colour);
            }
            Console.ReadLine();
            //change bgcolour of boxes
            for (int l = 0; l < opc.Screen.Count; l++)
            {
                //get random ConsoleColor
                ConsoleColor col = (ConsoleColor)colours.GetValue(rand.Next(colours.Length));
                //parse ConsoleColor to string
                string colour = col.ToString();
                //change colour of box
                opc.BgColour(opc.Screen[l].Id, colour);
            }
            Console.ReadLine();
            //move boxes to other location
            for(int n = 0; n < opc.Screen.Count; n++)
            {
                opc.Move(opc.Screen[n].Id, pos[rand.Next(pos.Length)], pos[rand.Next(pos.Length)] * 4);
            }
            Console.ReadLine();
            //resize boxes
            for(int p = 0; p < opc.Screen.Count; p++)
            {
                opc.Resize(opc.Screen[p].Id, opc.Screen[p].Width + size[rand.Next(size.Length)], opc.Screen[p].Height + size[rand.Next(size.Length)]);
            }
            Console.ReadLine();
            //remove boxes in a random order
            for (int k = 0; k < opc.Screen.Count; k++)
            {
                //remove random box from _canvas
                opc.Remove(opc.Screen[k].Id);
                Console.ReadLine();
            }
        }
    }
}
