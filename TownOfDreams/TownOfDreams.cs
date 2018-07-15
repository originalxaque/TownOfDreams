using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;

namespace TownOfDreams
{
    class TownOfDreams
    {
        static void Main(string[] args)
        {
            Game.Start();
        }
    }
    class Game
    {
        /** Starts the game.
         */
        public static void Start()
        {
            // Initialize Everything
            Text screenPrint = new Text();
            Map map = new Map();
            Input playerInput = new Input();
            Inventory playerInventory = new Inventory();
            Room room = new Room();

            PlayTitle();
            
            /* - Code creates duplicate 0's, need to be able to have duplicates to fill in map spaces.
            int[] mapArray = map.BuildMap();
            Dictionary<int, string> mapContent = map.BuildRooms(mapArray);
            map.PrintAllRooms(mapContent);
            */

            ReadKey();

            // playerInventory.BuildInventory();
            // ReadKey();
            
            screenPrint.PrimeForPrint();
            screenPrint.PrintText("1.txt");
            
            string input = playerInput.ParseInput();
            string inputResult = playerInput.CheckKeywords(input);
            WriteLine(inputResult);
            ReadKey();
            GameOver();
        }

        /** Displays the title screen.
         */
        static void PlayTitle()
        {
            SetCursorPosition(WindowWidth / 2, WindowHeight / 2);
            Text screenPrint = new Text();
            screenPrint.PrimeForPrint();
            screenPrint.PrintText("titleScreen.txt");
            Input playerInput = new Input();
            string input = playerInput.ParseInput();
        }

        /** Get the player's name.
         */
        static string SetPlayerName(string playerInput)
        {
            return null ;
        }

        public string GetPlayerName()
        {
            string name = "Zack";
            return name;
        }
        /** Display the final screen of the game when game over is encountered.
         */
        static void GameOver()
        {
            Text screenPrint = new Text();
            screenPrint.PrimeForPrint();
            screenPrint.PrintText("gameOver.txt");
            Input playerInput = new Input();
            string input = playerInput.ParseInput();
        }

        /** Sets the game over flag from false to true.
         */
        bool GameEnd(bool isGameOver)
        {
            if (!isGameOver)
            {
                isGameOver = true;
            }
            return isGameOver;
        }
    }

    class Text
    {
        /// <summary>
        /// Prints text given to it formatted for the game screen.
        /// </summary>
        /// <param name="sourceFile"></param>
        public void PrintText(string sourceFile)
        {
            Clear();
            PrimeForPrint();
            string[] toPrint = ParseContent(sourceFile);
            foreach (string line in toPrint)
            {
                WriteLine(line);
            }
        }

        /** Prepares the screen for printing text.
         */
        public void PrimeForPrint()
        {
            HUD hud = new HUD();
            Clear();
            hud.DrawHUD();
            SetCursorDefault();
        }

        /** Sets the cursor to the default printing position.
         */
        void SetCursorDefault()
        {
            SetCursorPosition(0, 2);
        }

        /** Reads a text file and creates a string array.
         */
        public string[] ParseContent(string textFile)
        {
            string[] text = null;
            string sourceFile = textFile;
            try
            {
                text = File.ReadAllLines(sourceFile);
            }
            catch (Exception e)
            {
                WriteLine("The file could not be read:");
                WriteLine(e.Message);
            }
            return text;
        }
    }

    class HUD
    {
        /** Draws the Game's HUD when called.
         */
        public void DrawHUD()
        {
            Game game = new Game();
            string playerName = game.GetPlayerName();
            SetCursorPosition(0, 0);
            BackgroundColor = ConsoleColor.White;
            ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < 1; i++)
            {
                SetCursorPosition(0, i);
                for (int j = 0; j < WindowWidth; j++)
                {
                    WriteLine(" ");
                    SetCursorPosition(j, i);
                }
            }
            SetCursorPosition(0, 0);
            WriteLine("Town of Dreams");
            SetCursorPosition(WindowWidth - playerName.Length - 1, 0);
            WriteLine(playerName);
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
            
        }
    }
    class Inventory
    {
        Dictionary<string, int> inventory = new Dictionary<string, int>();

        /** Builds the inventory when called.
         */
         /*
        public void BuildInventory()
        {
            using (StreamReader sr = new StreamReader("inventory.txt"))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    line.Split(",");
                    inventory.Add(line[0], line[1]);
                }
            }
            PrintInventory();
        }

        /** Sets the player's starting inventory.
         */
        void InitializeInventory()
        {
            ;
        }

        /** Prints out the player's inventory.
         */
        void PrintInventory()
        {
            foreach (var item in inventory)
            {
                WriteLine(item.Key, item.Value);
            }
        }

        /** Returns whether the player can pick up an item or not.
         */
        bool CheckWeight(int playerWeight)
        {
            bool canPickUp = false;
            return canPickUp;
        }

        /** Drops an item from the inventory.
         */
        void DropItem()
        {
            ;
        }
    }

    class Map
    {
        /** Builds the map by reading in csv map file.
         */
        public int[] BuildMap()
        {
            string[] mapString = File.ReadAllText("map.csv").Split(new char[] { ',', '\r', '\n' });

            int[] mapArray = mapString.Where(x => x.Count() != 0).Select(x => int.Parse(x)).ToArray();

            return mapArray;
        }

        public Dictionary<int, string> BuildRooms(int[] mapArray)
        {
            Dictionary<int, string> mapContent = new Dictionary<int, string>();
            for (int i = 0; i < mapArray.Length; i++)
            {
                mapContent.Add(mapArray[i], File.ReadAllText(mapArray[i] + ".txt"));
            }

            return mapContent;
        }

        public void PrintAllRooms(Dictionary<int, string> mapContent)
        {
            foreach (var pair in mapContent)
            {
                WriteLine("{0}, {1}", pair.Key, pair.Value);
            }
        }

        public bool CheckMap(int[] mapArray, int index, int direction, string positiveOrNegative)
        {
            bool validDirection;
            if (positiveOrNegative == "positive")
            {
                if (mapArray[index + direction] != 0)
                {
                    validDirection = true;
                }
                else
                {
                    validDirection = false;
                }
            }
            else
            {
                if (mapArray[index - direction] != 0)
                {
                    validDirection = true;
                }
                else
                {
                    validDirection = false;
                }
            }
            return validDirection;
        }
    }

    class Room
    {
        int currentRoom;
        int lastRoom;

        public int CurrentRoom { get => currentRoom; set => currentRoom = value; }

        /** Set's last room location when moving rooms.
*/
        void SetLastRoom(int currentRoom)
        {
            lastRoom = currentRoom;
        }

        /** Sends player to the room they asked for.
         */
        public void GoToRoom()
        {
            SetLastRoom(currentRoom);
        }
    }

    class Input
    {
        const string NORTH = "north";
        const string SOUTH = "south";
        const string EAST = "east";
        const string WEST = "west";
        const string BAG = "bag";
        const string BACK = "back";
        const string EXIT = "exit";
        const string EXAMINE = "examine";
        const string PICK = "pick";
        const string NOT_RECOGNIZED = "I do not recognize that command.";

        /** Checks player input for keywords.
         */
        public string ParseInput()
        {
            Write(">");
            string input = ReadLine();
            input.ToLower();
            return input;
        }

        /** Take the keywords from parse input and check against current room keywords.
         */
        public string CheckKeywords(string input)
        {
            switch (input)
            {
                case NORTH:
                    //check cell up
                    return NORTH;
                case SOUTH:
                    //check cell south
                    return SOUTH;
                case EAST:
                    //check cell to the right
                    return EAST;
                case WEST:
                    //check cell to the west
                    return WEST;
                case BAG:
                    //open the inventory
                    return BAG;
                case BACK:
                    //go back to last room
                    return BACK;
                case EXAMINE:
                    return EXAMINE;
                case PICK:
                    return PICK;
                case EXIT:
                    //exit the game
                    return EXIT;
            }

            return NOT_RECOGNIZED;
        }

        public void ProcessInput()
        {
            string input = ParseInput();
            string inputResult = CheckKeywords(input);
        }

        /** Takes player's input and makes sure it is valid.
         */
        bool ValidateInput(string input)
        {
            bool isValid = false;
            return isValid;
        }
    }
}