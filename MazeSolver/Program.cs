using System;
using System.IO;


namespace MazeSolver
{
    class Program
    {
        public static void Main(string[] args)
        {
            string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\MazeSolver\Log.txt";
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }
            StreamWriter log = new StreamWriter(logFilePath);

            string filePath = @"C:\Users\Robertas\Desktop\MazeSolver\RPAMaze[4197].txt";
            if (File.Exists(filePath))
            {
                string[] mazeFileInfo = File.ReadAllLines(filePath);
                string[] mazeSize = mazeFileInfo[0].Split(' ');
                int mazeHeightInner = int.Parse(mazeSize[0]);
                int mazeHeight = mazeHeightInner + 2;
                int mazeWidthInner = int.Parse(mazeSize[1]);
                int mazeWidth = mazeWidthInner + 2;
                int[,] maze = new int[mazeHeight + 2, mazeWidth + 2];
                int currentPosHeight = 0;
                int currentPosWidth = 0;
                var userAnswer = "";
                string direction = "North";
                int futurePos = 0;

                for (int i = 0; i < mazeHeight; i++)
                {
                    for (int j = 0; j < mazeWidth; j++)
                    {
                        maze[i, j] = 8;
                    }
                }

                for (int i = 0; i < mazeHeightInner; i++)
                {
                    for (int j = 0; j < mazeWidthInner; j++)
                    {
                        string[] splitNumbers = mazeFileInfo[1 + i].Split(' ');
                        int mazeElem = int.Parse(splitNumbers[j]);
                        maze[i + 1, j + 1] = mazeElem;
                        if (mazeElem.Equals(2))
                        {
                            currentPosHeight = i + 1;
                            currentPosWidth = j + 1;
                        }
                    }
                    Console.WriteLine(mazeFileInfo[1 + i]);
                    }

                Console.WriteLine("2 marks starting position [" + currentPosWidth + "," + currentPosHeight + "] in the maze. ");
                Console.WriteLine("Would you like to change it? Please write YES or NO and press Enter.");
                userAnswer = Console.ReadLine();

                if(userAnswer.ToLower() == "no")
                {
                    Console.WriteLine("You selected to keep current position[" + currentPosWidth + "," + currentPosHeight + "].");
                }
                if(userAnswer.ToLower() == "yes")
                {
                    maze[currentPosHeight, currentPosWidth] = 0;
                    bool valid = false;
                    while (!valid)
                    {
                        Console.WriteLine("Please type in new width from 1 (counting from the left side) to " + mazeWidthInner + " and only instead of 0.");
                        string userInputCoordJ = Console.ReadLine();
                        Console.WriteLine("Please type in new height from 1 (counting from the top) to " + mazeHeightInner + " and only instead of 0.");
                        string userInputCoordI = Console.ReadLine();

                        if (int.TryParse(userInputCoordI, out currentPosHeight) && userInputCoordI.Length == 1
                            && int.TryParse(userInputCoordJ, out currentPosWidth) && userInputCoordJ.Length == 1
                            && maze[currentPosHeight, currentPosWidth] == 0)
                        {
                            valid = true;
                            Console.WriteLine("You successfully changed starting position to [" + currentPosWidth + "," + currentPosHeight + "].");
                        }
                        else
                        {
                            Console.WriteLine("Provided coordinates were incorrect or new position is occupied by the wall. Please try again.");
                        }
                    }
                    maze[currentPosHeight, currentPosWidth] = 2;
                }
                if(userAnswer.ToLower() != "yes" & userAnswer.ToLower() != "no")
                {
                    Console.WriteLine("Provided input was incorrect. Current starting position [" + currentPosWidth + "," + currentPosHeight + "] will stay the same.");
                }

                while (futurePos != 8)
                {
                    switch (direction)
                    {
                        case "North":
                            if (maze[currentPosHeight, currentPosWidth + 1] != 1)
                            {
                                maze[currentPosHeight, currentPosWidth] = 0;
                                futurePos = maze[currentPosHeight, currentPosWidth + 1];
                                maze[currentPosHeight, currentPosWidth + 1] = 2;
                                if (futurePos == 8)
                                {
                                    Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                    log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                }
                                else
                                {
                                    Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + (currentPosWidth + 1) + "," + currentPosHeight + "]");
                                    log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + (currentPosWidth + 1) + "," + currentPosHeight + "]");
                                }
                                currentPosWidth = currentPosWidth + 1;
                                direction = "East";
                            }
                            else
                            {
                                if(maze[currentPosHeight - 1, currentPosWidth] != 1)
                                {
                                    maze[currentPosHeight, currentPosWidth] = 0;
                                    futurePos = maze[currentPosHeight - 1, currentPosWidth];
                                    maze[currentPosHeight - 1, currentPosWidth] = 2;
                                    if (futurePos == 8)
                                    {
                                        Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                        log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + currentPosWidth + "," + (currentPosHeight - 1) + "]");
                                        log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + currentPosWidth + "," + (currentPosHeight - 1) + "]");
                                    }
                                    currentPosHeight = currentPosHeight - 1;
                                }
                                else
                                {
                                    direction = "West";
                                }
                            }
                            break;
                        case "South":
                            if (maze[currentPosHeight, currentPosWidth - 1] != 1)
                            {
                                maze[currentPosHeight, currentPosWidth] = 0;
                                futurePos = maze[currentPosHeight, currentPosWidth - 1];
                                maze[currentPosHeight, currentPosWidth - 1] = 2;
                                if (futurePos == 8)
                                {
                                    Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                    log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                }
                                else
                                {
                                    Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + (currentPosWidth - 1) + "," + currentPosHeight + "]");
                                    log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + (currentPosWidth - 1) + "," + currentPosHeight + "]");
                                }
                                currentPosWidth = currentPosWidth - 1;
                                direction = "West";
                            }
                            else
                            {
                                if (maze[currentPosHeight + 1, currentPosWidth] != 1)
                                {
                                    maze[currentPosHeight, currentPosWidth] = 0;
                                    futurePos = maze[currentPosHeight + 1, currentPosWidth];
                                    maze[currentPosHeight + 1, currentPosWidth] = 2;
                                    if (futurePos == 8)
                                    {
                                        Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                        log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + currentPosWidth + "," + (currentPosHeight + 1) + "]");
                                        log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + currentPosWidth + "," + (currentPosHeight + 1) + "]");
                                    }
                                    currentPosHeight = currentPosHeight + 1;
                                }
                                else
                                {
                                    direction = "East";
                                }
                            }
                            break;
                        case "East":
                            if (maze[currentPosHeight + 1, currentPosWidth] != 1)
                            {
                                maze[currentPosHeight, currentPosWidth] = 0;
                                futurePos = maze[currentPosHeight + 1, currentPosWidth];
                                maze[currentPosHeight + 1, currentPosWidth] = 2;
                                if (futurePos == 8)
                                {
                                    Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                    log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                }
                                else
                                {
                                    Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + currentPosWidth + "," + (currentPosHeight + 1) + "]");
                                    log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + currentPosWidth + "," + (currentPosHeight + 1) + "]");
                                }
                                currentPosHeight = currentPosHeight + 1;
                                direction = "South";
                            }
                            else
                            {
                                if (maze[currentPosHeight, currentPosWidth + 1] != 1)
                                {
                                    maze[currentPosHeight, currentPosWidth] = 0;
                                    futurePos = maze[currentPosHeight, currentPosWidth + 1];
                                    maze[currentPosHeight, currentPosWidth + 1] = 2;
                                    if (futurePos == 8)
                                    {
                                        Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                        log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + (currentPosWidth + 1) + "," + currentPosHeight + "]");
                                        log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + (currentPosWidth + 1) + "," + currentPosHeight + "]");
                                    }
                                    currentPosWidth = currentPosWidth + 1;
                                }
                                else
                                {
                                    direction = "North";
                                }
                            }
                            break;
                        case "West":
                            if (maze[currentPosHeight - 1, currentPosWidth] != 1)
                            {
                                maze[currentPosHeight, currentPosWidth] = 0;
                                futurePos = maze[currentPosHeight - 1, currentPosWidth];
                                maze[currentPosHeight - 1, currentPosWidth] = 2;
                                if (futurePos == 8)
                                {
                                    Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                    log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                }
                                else
                                {
                                    Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + currentPosWidth + "," + (currentPosHeight - 1) + "]");
                                    log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + currentPosWidth + "," + (currentPosHeight - 1) + "]");
                                }
                                currentPosHeight = currentPosHeight - 1;
                                direction = "North";
                            }
                            else
                            {
                                if (maze[currentPosHeight, currentPosWidth - 1] != 1)
                                {
                                    maze[currentPosHeight, currentPosWidth] = 0;
                                    futurePos = maze[currentPosHeight, currentPosWidth - 1];
                                    maze[currentPosHeight, currentPosWidth - 1] = 2;
                                    if (futurePos == 8)
                                    {
                                        Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                        log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to exit.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + (currentPosWidth - 1) + "," + currentPosHeight + "]");
                                        log.WriteLine("Moved from position [" + currentPosWidth + "," + currentPosHeight + "] to position [" + (currentPosWidth - 1) + "," + currentPosHeight + "]");
                                    }
                                    currentPosWidth = currentPosWidth - 1;
                                }
                                else
                                {
                                    direction = "South";
                                }
                            }
                            break;
                    }
                }

            }
            else {
                Console.WriteLine("Maze file was not found.");
            }

            log.Close();
            Console.ReadKey();
        }
    }
}
