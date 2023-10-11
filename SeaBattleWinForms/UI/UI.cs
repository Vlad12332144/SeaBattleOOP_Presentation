using SeaBattleBL;
using SeaBattleBL.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace SeaBattleOOPWinForms
{
    public class UI
    {
        /// <summary>
        /// Refresh One Player Cell
        /// </summary>
        /// <param name="sender">The field in which the change was made</param>
        /// <param name="args">Parameters to change</param>
        public static void RefreshPlayerCell(object sender, CellChangedEventArgs args)
        {
            GameForm.RefreshButtonPlayer(args.Position.x, args.Position.y, GetSymbolsPlayer(args.State));
        }

        /// <summary>
        /// Refresh One Bot Cell
        /// </summary>
        /// <param name="sender">The field in which the change was made</param>
        /// <param name="args">Parameters to change</param>
        public static void RefreshBotCell(object sender, CellChangedEventArgs args)
        {
            GameForm.RefreshButtonBot(args.Position.x, args.Position.y, GetSymbolsBot(args.State));
        }

        #region -==- Draw Field -==-

        public static void DrawFields(IFieldViev playerField, IFieldViev botField)
        {
            CreateGridPlayer(playerField);

            CreateGridBot(botField);
        }

        #region -==- Create grid -==-

        private static void CreateGridPlayer(IFieldViev playerField)
        {
            for (int i = 0; i < playerField.CountRow; i++)
            {
                for(int j = 0; j < playerField.CountColumn; j++)
                {
                    Image symbol = GetSymbolsPlayer(playerField[i, j]);

                    GameForm.RefreshButtonPlayer(i, j, symbol);
                }
            }
        }

        private static void CreateGridBot(IFieldViev botField)
        {
            for (int i = 0; i < botField.CountRow; i++)
            {
                for (int j = 0; j < botField.CountColumn; j++)
                {
                    Image symbol = GetSymbolsPlayer(botField[i, j]);

                    GameForm.RefreshButtonBot(i, j, symbol);
                }
            }
        }

        #endregion

        #region -==- GetSymbols -==-

        private static Image GetSymbolsPlayer(Cell item)    // Задаёт символы для поля игрока.
        {
            Image symbol = null;

            if (item is null)
            {
                symbol = null;
            }
            else
            {
                if (item is Deck deck)
                {
                    if (deck.State)
                    {
                        symbol = Image.FromFile("..\\..\\Images\\deadDeck.jpg");
                    }
                    else
                    {
                        symbol = Image.FromFile("..\\..\\Images\\ship.jpg");
                    }
                }
                else
                {
                    if (item is Shoot)
                    {
                        symbol = Image.FromFile("..\\..\\Images\\blueDot.jpg");
                    }
                }
            }

            return symbol;
        }

        private static Image GetSymbolsBot(Cell item)    // Задаёт символы для поля бота.
        {
            Image symbol = null;

            if (item is null)
            {
                symbol = null;
            }
            else
            {
                if (item is Deck deck)
                {
                    if (deck.State)
                    {
                        symbol = Image.FromFile("..\\..\\Images\\deadDeck.jpg");
                    }
                    else
                    {
                        symbol = Image.FromFile("..\\..\\Images\\ship.jpg");
                    }
                }
                else
                {
                    if (item is Shoot)
                    {
                        symbol = Image.FromFile("..\\..\\Images\\blueDot.jpg");
                    }
                }
            }

            return symbol;
        }

        #endregion

        #endregion
    }
}
