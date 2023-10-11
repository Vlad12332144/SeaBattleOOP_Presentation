using SeaBattleBL;
using SeaBattleBL.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleOOPWinForms
{
    public class Fire
    {
        private static FireState fireState = FireState.PlayerQueue;

        public static bool DoFire(FireState queueFire, Field field, char row = ' ', char column = ' ')
        {
            bool doFire = false;

            if (fireState == queueFire && queueFire == FireState.PlayerQueue)
            {
                doFire = DoFirePlayer(row, column, field);
            }
            else if(fireState == queueFire && queueFire == FireState.BotQueue)
            {
                doFire = DoFireBot(field);
            }

            return doFire;
        }

        private static bool DoFirePlayer(char row, char column, Field field)
        {
            bool doFire = false;

            int coordinateLetter = Player.ConvertCoordinate(row);
            int coordinateNum = 0;

            switch (column)
            {
                case '1':
                    coordinateNum = 1;

                    break;
                case '2':
                    coordinateNum = 2;

                    break;
                case '3':
                    coordinateNum = 3;

                    break;
                case '4':
                    coordinateNum = 4;

                    break;
                case '5':
                    coordinateNum = 5;

                    break;
                case '6':
                    coordinateNum = 6;

                    break;
                case '7':
                    coordinateNum = 7;

                    break;
                case '8':
                    coordinateNum = 8;

                    break;
                case '9':
                    coordinateNum = 9;

                    break;
            }

            bool returnStep = false;

            try
            {
                field.DoFirePlayer(coordinateNum, coordinateLetter, ref returnStep);

                if (returnStep)
                {
                    fireState = FireState.BotQueue;
                }

                doFire = true;
            }
            catch (HitingTheSamePointExeption ex)
            {
                GameForm.ShowMessage(ex.Message);
            }

            return doFire;
        }

        private static bool DoFireBot(Field field)
        {
            bool doFire = false;
            bool returnStep = false;

            while (!doFire)
            {
                Coordinate cord = Bot.GetCoordinate();

                int x = cord.x;
                int y = cord.y;

                doFire = field.DoFire(x, y, ref returnStep);

                if (returnStep)
                {
                    fireState = FireState.PlayerQueue;
                }
            }

            return doFire;
        }
    }
}
