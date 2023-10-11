using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

using SeaBattleBL;
using SeaBattleBL.BL.Interfaces;

namespace SeaBattleOOP
{
    public class UI
    {
        // TODO: 6 delegate
        /// <summary>
        /// Refresh One Player Cell
        /// </summary>
        /// <param name="sender">The field in which the change was made</param>
        /// <param name="args">Parameters to change</param>
        public static void RefreshPlayerCell(object sender, CellChangedEventArgs args)
        {
            int oofSetPlayerX = 1;
            int offSetPlayerY = 3;

            Console.SetCursorPosition(args.Position.y + offSetPlayerY, args.Position.x + oofSetPlayerX);
            Console.ForegroundColor = GetColorForSymbols(args.State);
            Console.Write(GetSymbolsPlayer(args.State));
        }

        /// <summary>
        /// Refresh One Bot Cell
        /// </summary>
        /// <param name="sender">The field in which the change was made</param>
        /// <param name="args">Parameters to change</param>
        public static void RefreshBotCell(object sender, CellChangedEventArgs args)
        {
            int offSetBotX = 1;
            int offSetBotY = 23;

            Console.SetCursorPosition(args.Position.y + offSetBotY, args.Position.x + offSetBotX);
            Console.ForegroundColor = GetColorForSymbols(args.State);
            Console.Write(GetSymbolsBot(args.State));
        }

        /// <summary>
        /// Get Anything String From User.
        /// </summary>
        /// <param name="outputString">Write String for User.</param>
        /// <returns></returns>
        public static string GetStringFromUser(string outputString = "")
        {
            Console.Write(outputString);

            string str = Console.ReadLine();

            return str;
        }

        #region -==- Draw Frield -==-

        /// <summary>
        /// Draw Bot and Player field.
        /// </summary>
        /// <param name="fieldPlayer">Player Field.</param>
        /// <param name="fieldBot">Bot Field.</param>
        public static void DrawField(IFieldViev fieldPlayer, IFieldViev fieldBot)
        {
            Console.Clear();

            DrawWord();    // Рисует буквы.
            CreateGridPlayer(fieldPlayer);     // Создаёт решётку поля для кораблей.

            int setCursorX = 0;

            int oofSetLetterY = 11;
            int y = 0 + oofSetLetterY;
            DrawWord(setCursorX, y);    // Рисует буквы.

            setCursorX = 20;    // Координата курсора по Х(горизонтали).

            DrawWord(setCursorX);    // Рисует буквы.
            CreateGridBot(fieldBot, setCursorX);     // Создаёт решётку поля для стрельбы.

            DrawWord(setCursorX, y);    // Рисует буквы.
        }

        private static void DrawWord(int x = 0, int y = 0)    // Рисует буквы.
        {
            Console.SetCursorPosition(x, y);
            Console.Write("   ");
            Console.Write("ABCDEFGHIJ");
            Console.WriteLine();
        }

        #region -==- Grid -==-

        #region -==- Create Grid -==-

        private static void CreateGridPlayer(IFieldViev field, int x = 0, int y = 1)     // Создаёт решётку поля для игрока.
        {
            int num = 1;

            for (int i = 0; i < field.CountRow; i++)    // Координата для строк.
            {
                Console.SetCursorPosition(x, y);
                Console.Write("{0,2} ", num);

                for (int j = 0; j < field.CountColumn; j++)    // Координаты для столбцов.
                {
                    ConsoleColor color = GetColorForSymbols(field[i, j]);    // Получение цвета символов.
                    Console.ForegroundColor = color;

                    char symbol = GetSymbolsPlayer(field[i, j]);    // Получение символа для заполнения.
                    Console.Write(symbol);

                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write(" {0,1}", num);
                Console.WriteLine();

                ++y;
                ++num;
            }
        }

        private static void CreateGridBot(IFieldViev field, int x = 0, int y = 1)     // Создаёт решётку поля для бота.
        {
            int num = 1;

            for (int i = 0; i < field.CountRow; i++)    // Координата для строк.
            {
                Console.SetCursorPosition(x, y);
                Console.Write("{0,2} ", num);

                for (int j = 0; j < field.CountColumn; j++)    // Координаты для столбцов.
                {
                    ConsoleColor color = GetColorForSymbols(field[i, j]);    // Получение цвета символов.
                    Console.ForegroundColor = color;

                    char symbol = GetSymbolsBot(field[i, j]);    // Получение символа для заполнения.
                    Console.Write(symbol);

                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(" {0,1}", num);
                Console.WriteLine();

                ++y;
                ++num;
            }
        }

        #endregion

        private static ConsoleColor GetColorForSymbols(Cell cell)
        {
            ConsoleColor color = ConsoleColor.White;

            if(cell is Shoot)
            {
                color = ConsoleColor.Blue;
            }
            else
            {
                if(cell is Deck deck)
                {
                    if (deck.State)
                    {
                        color = ConsoleColor.Red;
                    }
                }
            }

            return color;
        }

        #region -==- Get Symbol For Cell -==-

        private static char GetSymbolsPlayer(Cell item)    // Задаёт символы для поля игрока.
        {
            char symbol = ' ';

            if(item is null)
            {
                symbol = '.';
            }
            else
            {
                if (item is Deck deck)
                {
                    if (deck.State)
                    {
                        symbol = 'X';
                    }
                    else
                    {
                        symbol = 'V';
                    }
                }
                else
                {
                    if(item is Shoot)
                    {
                        symbol = '.';
                    }
                }
            }

            return symbol;
        }

        private static char GetSymbolsBot(Cell item)    // Задаёт символы для поля бота.
        {
            char symbol = ' ';

            if (item is null)
            {
                symbol = '.';
            }
            else
            {
                if (item is Deck deck)
                {
                    if (deck.State)
                    {
                        symbol = 'X';
                    }
                    else
                    {
                        symbol = 'V';
                    }
                }
                else
                {
                    if (item is Shoot)
                    {
                        symbol = '.';
                    }
                }
            }

            return symbol;
        }

        #endregion

        #endregion

        #endregion

        #region -==- Fire -==-

        /// <summary>
        /// Get Player Fire Coordinate.
        /// </summary>
        /// <returns>Player Fire Coordinate.</returns>
        public static string GetCoordinatePlayer()
        {
            bool enterCoordinate = false;
            string coordinate = "";

            while (!enterCoordinate)
            {
                coordinate = GetStringFromUser("Please Enter Coordinate: ");

                if (Validator.CheckCoordinatePlayer(coordinate))
                {
                    enterCoordinate = true;
                }
                else
                {
                    Console.WriteLine("Please enter correct coordinate!");
                    continue;
                }

                coordinate.ToLower();
            }

            return coordinate;
        }

        /// <summary>
        /// Set Cursor in Fire Position in Console.
        /// </summary>
        public static void SetCursorInFire()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 12);
        }

        /// <summary>
        /// Clear Fire Text in Console.
        /// </summary>
        public static void ClearFireText()
        {
            for(int column = 12; column < 20; column++)
            {
                for (int row = 0; row < 70; row++)
                {
                    Console.SetCursorPosition(row, column);
                    Console.Write(' ');
                }
            }
            SetCursorInFire();
        }

        #endregion

        #region -==- Write Winner -==-

        /// <summary>
        /// Write in Console Who Win.
        /// </summary>
        /// <param name="winner">Who Win in the Game.</param>
        public static void WriteWinner(GameState winner)
        {
            if(winner == GameState.PlayerWin)
            {
                Console.WriteLine("Player Win!!!");
            }
            else if(winner == GameState.BotWin)
            {
                Console.WriteLine("Bot Win!!!");
            }
        }

        #endregion
    }
}