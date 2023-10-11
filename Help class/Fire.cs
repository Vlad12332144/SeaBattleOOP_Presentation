using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SeaBattleBL;
using SeaBattleBL.BL;

namespace SeaBattleOOP
{
    public class Fire
    {
        public static FireState queueFire = FireState.PlayerQueue;

        /// <summary>
        /// DoFire
        /// </summary>
        /// <param name="fieldPlayer"></param>
        /// <param name="fieldBot"></param>
        public static void DoFire(Field fieldPlayer, Field fieldBot)
        {
            if(queueFire == FireState.PlayerQueue)
            {
                DoFirePlayer(fieldBot);
            }
            else
            {
                DoFireBot(fieldPlayer);
            }
        }

        private static void DoFirePlayer(Field field)
        {
            bool doFire = false;

            while (!doFire)
            {
                char coordinateLetter;
                int coordinateNum;

                string coordinate = UI.GetCoordinatePlayer();

                coordinateLetter = coordinate[0];    // y координата выстрела.

                if (coordinate.Length == 2)
                {
                    string num = Convert.ToString(coordinate[1]);
                    coordinateNum = Convert.ToByte(num) - 1;    // x координата выстрела.
                }
                else
                {
                    string num = Convert.ToString(coordinate[1]) + Convert.ToString(coordinate[2]);
                    coordinateNum = Convert.ToByte(num) - 1;    // x координата выстрела.
                }

                bool returnStep = false;

                int y = Player.ConvertCoordinate(coordinateLetter);

                try
                {
                    field.DoFirePlayer(y, coordinateNum, ref returnStep);

                    doFire = true;

                    if (returnStep)
                    {
                        queueFire = FireState.BotQueue;
                    }
                }
                catch(HitingTheSamePointExeption ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
        }

        private static void DoFireBot(Field field)
        {
            bool doFire = false;
            bool returnStep = false;

            while (!doFire)
            {
                Coordinate cord = Bot.GetCoordinate();

                int x = cord.x;
                int y = cord.y;

                doFire = field.DoFire(x, y, ref returnStep);
            }

            if (returnStep)
            {
                queueFire = FireState.PlayerQueue;
            }
        }
    }
}
