using System.Collections;
using System.Data;

namespace CME1251__2
{
    internal class Program
    {
        // stack 
        public static Queue<string> queue = new Queue<string>();  

        public static string[,] matrix = new string[23 , 53];
        public static string[,] template_matrix;
        public static Random random = new Random();
        public static int cx = 1; 
        public static int cy = 1;
        public static string[] number_list = new string[] { "0" ,"1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public struct  walltypes
        {
            public static string[,] horizontal_small_wall = new string[3, 5]{ 
                        { " ", " ", " ", " ", " " },
                     { " ", "#", "#", "#", " " } ,
                    {"  " ," "," "," "," "}};
            public static string[,] vertical_small_wall = new string[5, 3]{
                        { " ", " ", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", " ", " " },
                    };
            public static string[,] horizontal_medium_wall = new string[3, 9]
            {
                { " ", " ", " ", " ", " "," "," " ," "," "},
                { " ", "#", "#", "#", "#","#","#" ,"#"," "},
                { " ", " ", " ", " ", " "," "," " ," "," "},
            };
            public static string[,] vertical_medium_wall = new string[9, 3]
            {
                        { " ", " ", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", " ", " " },
            };
            public static string[,] horizontal_big_wall = new string[3, 13]
            {
                { " ", " ", " ", " ", " "," "," " ," "," " ," "," "," "," "},
                { " ", "#", "#", "#", "#","#","#" ,"#","#","#","#","#"," "},
                { " ", " ", " ", " ", " "," "," " ," "," " ," "," "," "," "},
            };
            public static string[,] vertical_big_wall = new string[13, 3]
            {
                        { " ", " ", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", "#", " " },
                        { " ", " ", " " },
            };
        }

        public static DateTime Aimovetimer = DateTime.Now.AddSeconds(2);

        public enum lastDirectionType
        {
            left = 0, right = 1,
            top = 2, bottom = 3,
        }
        public static lastDirectionType lastDirection;

        public static bool isKilled = false;
        public static int Healt = 5;
        static void Main(string[] args)
        {
            while(Healt > 0)
            {
                Init();
                Console.SetCursorPosition(cx, cy);
                // game loop
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        bool flag = false;
                        ConsoleKeyInfo ckey = Console.ReadKey();
                        if (ckey.Key == ConsoleKey.UpArrow && CheckWall(cy - 1, cx))
                        {
                            if (IsDead(cy - 1, cx))
                            {
                                isKilled = false;
                                Healt--;
                                RoundEndScreen();
                                break;
                            }
                            ClearP();
                            flag = CollectUp(cy);
                            lastDirection = lastDirectionType.top;
                            cy--;
                        }
                        else if (ckey.Key == ConsoleKey.RightArrow && CheckWall(cy, cx + 1))
                        {
                            if (IsDead(cy, cx + 1))
                            {
                                isKilled = false;
                                Healt--;
                                RoundEndScreen();
                                break;
                            }
                            ClearP();
                            flag = CollectRight(cx);
                            lastDirection = lastDirectionType.right;
                            cx++;
                        }
                        else if (ckey.Key == ConsoleKey.DownArrow && CheckWall(cy + 1, cx))
                        {
                            if (IsDead(cy + 1, cx))
                            {
                                isKilled = false;
                                Healt--;
                                RoundEndScreen();
                                break;
                            }
                            ClearP();
                            flag = CollectDown(cy);
                            lastDirection = lastDirectionType.bottom;
                            cy++;
                        }
                        else if (ckey.Key == ConsoleKey.LeftArrow && CheckWall(cy, cx - 1))
                        {
                            if (IsDead(cy, cx - 1))
                            {
                                isKilled = false;
                                Healt--;
                                RoundEndScreen();
                                break;
                            }
                            ClearP();
                            flag = CollectLeft(cx);
                            lastDirection = lastDirectionType.left;
                            cx--;
                        }
                        if (flag)
                        {
                            if (!IsDescanding(queue))
                            {
                                if (lastDirection == lastDirectionType.right)
                                {
                                    cx--;
                                }
                                else if (lastDirection == lastDirectionType.left)
                                {
                                    cx++;
                                }
                                else if (lastDirection == lastDirectionType.top)
                                {
                                    cy++;
                                }
                                else if (lastDirection == lastDirectionType.bottom)
                                {
                                    cy--;
                                }
                            }

                            if (lastDirection == lastDirectionType.top)
                            {
                                int count = queue.Count();
                                if (count == 1 && matrix[cy - 1, cx] == "#")
                                {
                                    string num = queue.Dequeue();
                                    matrix[cy, cx] = num;
                                    Console.SetCursorPosition(cx, cy);
                                    Console.Write(num);
                                    cy++;
                                }
                                else
                                {
                                    for (int i = 1; i <= count; i++)
                                    {
                                        string num = queue.Dequeue();
                                        if (matrix[cy - i, cx] == "#") break;
                                        else
                                        {
                                            matrix[cy - i, cx] = num;
                                            Console.SetCursorPosition(cx, cy - i);
                                            Console.Write(num);
                                        }
                                    }
                                }

                            }
                            else if (lastDirection == lastDirectionType.bottom)
                            {
                                int count = queue.Count();
                                if (count == 1 && matrix[cy + 1, cx] == "#")
                                {
                                    string num = queue.Dequeue();
                                    matrix[cy, cx] = num;
                                    Console.SetCursorPosition(cx, cy);
                                    Console.Write(num);
                                    cy--;
                                }
                                else
                                {
                                    for (int i = 1; i <= count; i++)
                                    {
                                        string num = queue.Dequeue();
                                        if (matrix[cy + i, cx] == "#") break;
                                        else
                                        {
                                            matrix[cy + i, cx] = num;
                                            Console.SetCursorPosition(cx, cy + i);
                                            Console.Write(num);
                                        }
                                    }
                                }
                            }
                            else if (lastDirection == lastDirectionType.left)
                            {
                                int count = queue.Count();
                                if (count == 1 && matrix[cy, cx - 1] == "#")
                                {
                                    string num = queue.Dequeue();
                                    matrix[cy, cx] = num;
                                    Console.SetCursorPosition(cx, cy);
                                    Console.Write(num);
                                    cx++;
                                }
                                else
                                {
                                    for (int i = 1; i <= count; i++)
                                    {
                                        string num = queue.Dequeue();
                                        if (matrix[cy, cx - i] == "#") break;
                                        else
                                        {
                                            matrix[cy, cx - i] = num;
                                            Console.SetCursorPosition(cx - i, cy);
                                            Console.Write(num);
                                        }
                                    }
                                }

                            }
                            else if (lastDirection == lastDirectionType.right)
                            {
                                int count = queue.Count();
                                if (count == 1 && matrix[cy, cx + 1] == "#")
                                {
                                    string num = queue.Dequeue();
                                    matrix[cy, cx] = num;
                                    Console.SetCursorPosition(cx, cy);
                                    Console.Write(num);
                                    cx--;
                                }
                                else
                                {
                                    for (int i = 1; i <= count; i++)
                                    {
                                        string num = queue.Dequeue();
                                        if (matrix[cy, cx + i] == "#") break;
                                        else
                                        {
                                            matrix[cy, cx + i] = num;
                                            Console.SetCursorPosition(cx + i, cy);
                                            Console.Write(num);
                                        }
                                    }
                                }


                            }




                            queue = new Queue<string>();
                        }
                        PrintP();
                    }
                    if (Aimovetimer <= DateTime.Now)
                    {
                        Moveallzero();
                        Aimovetimer = DateTime.Now.AddSeconds(2);
                    }
                    if (isKilled)
                    {
                        RoundEndScreen();
                        isKilled = false;
                        Healt--;
                        break;
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("game endend");
        }

        public static void Init()
        {
            Console.Clear();// clearing previous games trash
            // board
            CreateBorder();
            CreateObstacle();
            // player
            matrix[cy, cx] = "P";
            // numbers
            AddNumbers();

            Render();


        }
        public static void CreateBorder()
        {
            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 53; j++)
                {
                    if(i == 0 ||i == 22 || j == 0 ||j == 52)
                    {
                        matrix[i, j] = "#";
                    }
                    else matrix[i, j] = " ";
                }
            }
        }
        public static void Render()
        {
            for (int i = 0; i< 23; i++)
            {
                for (int j = 0; j < 53; j++)
                {
                    if (matrix[i, j] == "#") Console.Write("#");
                    else if(matrix[i, j] == "P") {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("P");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (number_list.Contains(matrix[i, j]))
                    {
                        Console.Write(matrix[i,j]);
                    }
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public static void CreateObstacle()
        {
            // create small walls
            int counter = 0;
            while (counter <= 28)
            {
                int top_x = random.Next(1, 51);
                int top_y = random.Next(1, 21);
                // horizontal
                if (random.Next(0,2) == 0)
                {
                    if(counter <= 20) template_matrix = walltypes.horizontal_small_wall;
                    else if(counter <= 25) template_matrix = walltypes.horizontal_medium_wall;
                    else template_matrix = walltypes.horizontal_big_wall;
                }
                // vertical
                else
                {
                    if (counter <= 20) template_matrix = walltypes.vertical_small_wall;
                    else if (counter <= 25) template_matrix = walltypes.vertical_medium_wall;
                    else template_matrix = walltypes.vertical_big_wall;
                }
                if (CheckCollision(top_y , top_x , template_matrix))
                {
                    AddObstacle(top_y, top_x, template_matrix);
                    counter++;
                }
            }
            // creawte medium walls
       
        }

        public static bool CheckCollision(int topy , int topx , string[,] teplate_matrix)
        {
            if (topy + template_matrix.GetLength(0) >= 23 || topx + template_matrix.GetLength(1) >= 53) return false;
            for (int i = 0; i < teplate_matrix.GetLength(0); i++)
            {
                for (int j = 0; j < teplate_matrix.GetLength(1); j++)
                {
                    if (matrix[topy + i , topx + j] != " ")
                    {
                        return false;   
                    }
                }
            }

            return true;
        }
        public static void AddObstacle(int topy, int topx, string[,] teplate_matrix)
        {
            for (int i = 0; i < teplate_matrix.GetLength(0); i++)
            {
                for (int j = 0; j < teplate_matrix.GetLength(1); j++)
                {
                    matrix[topy + i, topx + j] = template_matrix[i, j];
                }
            }
        }

        public static bool CheckIsSquareEmpty(int y , int x)
        {
            if (matrix[y, x] != " ") return true;
            else return false;
        }

        public static void PrintP()
        {
            matrix[cy, cx] = "P";
            Console.SetCursorPosition(cx, cy);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("P");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void ClearP()
        {
            matrix[cy, cx] = " ";
            Console.SetCursorPosition(cx, cy);
            Console.Write(" ");
        }

        public static void AddNumbers()
        {
            int coutner = 0;
            while (coutner <= 70)
            {
                int x = random.Next(1, 51);
                int y = random.Next(1, 21);
                if (matrix[y,x] == " ")
                {
                    matrix[y,x] = random.Next(0, 10).ToString();
                    coutner++;
                }
            }
        }

        public static void ClearChar(int x , int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
            Console.SetCursorPosition(cx ,cy);
        }
        // collecting
        public static bool CollectDown(int cy)
        {
            bool flag = false;
            for (int i = cy + 1; i <= 53 ; i++)
            {
                if (number_list.Contains(matrix[i, cx]))
                {
                    queue.Enqueue(matrix[i, cx]);
                    matrix[i, cx] = " ";
                    ClearChar(cx, i);
                    flag = true;
                }
                else
                {
                    break;
                };
            }
            return flag;
        }
        public static bool CollectUp(int cy) {
            bool flag = false;
            for (int i = cy - 1; i >= 0; i--)
            {
                if (number_list.Contains(matrix[i, cx]))
                {
                    queue.Enqueue(matrix[i, cx]);
                    matrix[i, cx] = " ";
                    ClearChar(cx, i);
                    flag = true;
                }
                else break;
            }
            return flag;
        }
        public static bool CollectLeft(int cx)
        {
            bool flag = false;
            for (int i = cx - 1; i >= 0; i--)
            {
                if (number_list.Contains(matrix[cy, i]))
                {
                    queue.Enqueue(matrix[cy, i]);
                    matrix[cy, i] = " ";
                    ClearChar(i, cy);
                    flag = true;
                }
                else break;
            }
            return flag;
        }
        public static bool CollectRight(int cx)
        {
            bool flag = false;
            for (int i = cx + 1; i <= 53; i++)
            {
                if (number_list.Contains(matrix[cy, i]))
                {
                    queue.Enqueue(matrix[cy, i]);
                    matrix[cy, i] = " ";
                    ClearChar(i , cy);
                    flag = true;
                }
                else break;
            }
            return flag;
        }

        public static bool CheckWall(int cy , int cx)
        {
            if (matrix[cy, cx] == "#") return false;
            return true;
        }

        public static bool ChecksquareEmpty(int cy , int cx , bool for_ai = false)
        {
            if (matrix[cy, cx] == " " || matrix[cy, cx] == "P") return true;
            return false;
        }
        public static bool IsDescanding(Queue<string> queue)
        {
            bool flag = true;
            Queue<string> temp = new Queue<string>();
            int count = queue.Count;
            int prev = 99;
            for (int i = 0; i < count; i++)
            {
                int item = Convert.ToInt32(queue.Dequeue());
                if(item > prev)
                {
                    flag = false;  
                }
                prev = item;
                temp.Enqueue(item.ToString());
            }
            while (temp.Count() > 0)
            {
               string num = temp.Dequeue();
               queue.Enqueue((string)num); 
            }
            return flag;
        }

        public static void Moveallzero()
        {
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 53; j++)
                {
                    if (matrix[i, j] == "0")
                    {
                        list.Add(new int[] { i, j });
                    }
                }
            }
            foreach (var item in list)
            {
                int y = item[0] + random.Next(-1,2);
                int x = item[1] + random.Next(-1, 2);
                if (ChecksquareEmpty(y, x))
                {
                    if(IsAiKillPlayer(y, x))
                    {
                        isKilled = true;
                        return;
                    }
                    matrix[item[0], item[1]] = " ";
                    matrix[y, x] = "0";
                    Console.SetCursorPosition(item[1], item[0]);
                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(x, y);
                    Console.Write("0");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
        }

        // check collide wiht 0
        public static bool IsDead(int cy , int cx)
        {
            if (matrix[cy, cx] == "0") return true;
            return false;
        }
        // chekc collide with P
        public static bool IsAiKillPlayer(int cy, int cx)
        {
            if (matrix[cy, cx] == "P") return true;
            return false;
        }

        // utility 
        public static void RoundEndScreen()
        {
            Console.Clear();
            Console.WriteLine("collide wiht number 0");
            Console.ReadKey();
        }
    }
}