using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame1
{
    class Snake
    {
        class SnakeGame
        {
            static void Main(string[] args)
            {
                Console.WindowHeight = 32;
                Console.WindowWidth = 64;
                int screenwidth = Console.WindowWidth;
                int screenheight = Console.WindowHeight;

                Random randomnummer = new Random();

                int score = 5;
                int gameover = 0;

                pixel px = new pixel();

                px.xposition = screenwidth / 2;
                px.yposition = screenheight / 2;

                px.shead = ConsoleColor.Red;

                string movement = "RIGHT";

                List<int> xpositionlist = new List<int>();
                List<int> ypositionlist = new List<int>();

                int berryx = randomnummer.Next(0, screenwidth);
                int berryy = randomnummer.Next(0, screenheight);

                DateTime time = DateTime.Now;
                DateTime time2 = DateTime.Now;

                string buttonpressed = "no";

                // We only draw the border once. It doesn't change.
                DrawBorder(screenwidth, screenheight);

                while (true)
                {
                    ClearConsole(screenwidth, screenheight);
                    if (px.xposition == screenwidth - 1 || px.xposition == 0 || px.yposition == screenheight - 1 || px.yposition == 0)
                    {
                        gameover = 1;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    if (berryx == px.xposition && berryy == px.yposition)
                    {
                        score++;
                        berryx = randomnummer.Next(1, screenwidth - 2);
                        berryy = randomnummer.Next(1, screenheight - 2);
                    }
                    for (int i = 0; i < xpositionlist.Count(); i++)
                    {
                        Console.SetCursorPosition(xpositionlist[i], ypositionlist[i]);
                        Console.Write("¦");
                        if (xpositionlist[i] == px.xposition && ypositionlist[i] == px.yposition)
                        {
                            gameover = 1;
                        }
                    }
                    if (gameover == 1)
                    {
                        break;
                    }

                    Console.SetCursorPosition(px.xposition, px.yposition);
                    Console.ForegroundColor = px.shead;
                    Console.Write("■");
                    Console.SetCursorPosition(berryx, berryy);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("■");
                    Console.CursorVisible = false;
                    time = DateTime.Now;
                    buttonpressed = "no";
                    while (true)
                    {
                        time2 = DateTime.Now;
                        if (time2.Subtract(time).TotalMilliseconds > 500) { break; }
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo toets = Console.ReadKey(true);
                            //Console.WriteLine(toets.Key.ToString());
                            if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN" && buttonpressed == "no")
                            {
                                movement = "UP";
                                buttonpressed = "yes";
                            }
                            if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != "UP" && buttonpressed == "no")
                            {
                                movement = "DOWN";
                                buttonpressed = "yes";
                            }
                            if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT" && buttonpressed == "no")
                            {
                                movement = "LEFT";
                                buttonpressed = "yes";
                            }
                            if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT" && buttonpressed == "no")
                            {
                                movement = "RIGHT";
                                buttonpressed = "yes";
                            }
                        }
                    }
                    xpositionlist.Add(px.xposition);
                    ypositionlist.Add(px.yposition);
                    switch (movement)
                    {
                        case "UP":
                            px.yposition--;
                            break;
                        case "DOWN":
                            px.yposition++;
                            break;
                        case "LEFT":
                            px.xposition--;
                            break;
                        case "RIGHT":
                            px.xposition++;
                            break;
                    }
                    if (xpositionlist.Count() > score)
                    {
                        xpositionlist.RemoveAt(0);
                        ypositionlist.RemoveAt(0);
                    }
                }
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
                Console.WriteLine("Game over, Score: " + score);
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
            }

            private static void ClearConsole(int screenwidth, int screenheight)
            {
                var blackLine = string.Join("", new byte[screenwidth - 2].Select(b => " ").ToArray());
                Console.ForegroundColor = ConsoleColor.Black;
                for (int i = 1; i < screenheight - 1; i++)
                {
                    Console.SetCursorPosition(1, i);
                    Console.Write(blackLine);
                }
            }

            private static void DrawBorder(int screenwidth, int screenheight)
            {
                var horizontalBar = string.Join("", new byte[screenwidth].Select(b => "■").ToArray());

                Console.SetCursorPosition(0, 0);
                Console.Write(horizontalBar);
                Console.SetCursorPosition(0, screenheight - 1);
                Console.Write(horizontalBar);

                for (int i = 0; i < screenheight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                    Console.SetCursorPosition(screenwidth - 1, i);
                    Console.Write("■");
                }
            }

            class pixel
            {
                public int xposition { get; set; }
                public int yposition { get; set; }
                public ConsoleColor shead { get; set; }
            }
        }
    }
}