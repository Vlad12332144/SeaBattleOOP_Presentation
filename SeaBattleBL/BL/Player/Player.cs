using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattleBL
{
    public class Player
    {
        /// <summary>
        /// Checks the status of all ships.
        /// </summary>
        /// <param name="ships">Bot or Player Ships.</param>
        /// <param name="field">Bot or Player Field.</param>
        public void CheckShips(IEnumerable<Ship> ships, Field field)
        {
            foreach (Ship ship in ships)
            {
                if (ship.IsAlive())
                {
                    field.DoAnotherShip(ship);
                }
            }
        }

        #region -==- Check Winner -==-

        /// <summary>
        /// Checks the status of all ships.
        /// </summary>
        /// <param name="ships">Bot or Player Ships.</param>
        /// <param name="field">Bot or Player Field.</param>
        public bool CheckShips(IEnumerable<Ship> ships)
        {
            bool res = true;

            foreach (Ship ship in ships)
            {
                if (!res)
                {
                    break;
                }

                res = ship.IsAlive() && res;
            }

            return res;
        }

        /// <summary>
        /// Checks the winner.
        /// </summary>
        /// <param name="player">Player Ships.</param>
        /// <param name="bot">Bot Ships.</param>
        /// <returns></returns>
        public GameState CheckWinner(IEnumerable<Ship> player, IEnumerable<Ship> bot)
        {
            GameState state = GameState.GameContinue;

            if (CheckShips(player))
            {
                state = GameState.BotWin;
            }

            if (CheckShips(bot))
            {
                state = GameState.PlayerWin;
            }

            return state;
        }

        #endregion

        public static int ConvertCoordinate(char letter)
        {
            int coordinate = 0;

            switch (letter)
            {
                case 'b':
                    coordinate = 1;

                    break;
                case 'c':
                    coordinate = 2;

                    break;
                case 'd':
                    coordinate = 3;

                    break;
                case 'e':
                    coordinate = 4;

                    break;
                case 'f':
                    coordinate = 5;

                    break;
                case 'g':
                    coordinate = 6;

                    break;
                case 'h':
                    coordinate = 7;

                    break;
                case 'i':
                    coordinate = 8;

                    break;
                case 'j':
                    coordinate = 9;

                    break;
            }

            return coordinate;
        }
    }
}