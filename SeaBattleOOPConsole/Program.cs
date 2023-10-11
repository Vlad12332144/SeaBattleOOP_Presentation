using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using SeaBattleBL;

namespace SeaBattleOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Beep();

            Game game = new Game();

            game.CellPlayerChanged += UI.RefreshPlayerCell;
            game.CellBotChanged += UI.RefreshBotCell;

            UI.DrawField(game.PlayerField, game.BotField);
            Console.Beep();

            do
            {
                Fire.DoFire(game.PlayerField, game.BotField);

                game.Player.CheckShips(game.PlayerShips, game.PlayerField);
                game.Player.CheckShips(game.BotShips, game.BotField);

                UI.ClearFireText();
            } while (game.Player.CheckWinner(game.PlayerShips, game.BotShips) == GameState.GameContinue);

            UI.WriteWinner(game.Player.CheckWinner(game.PlayerShips, game.BotShips));
        }
    }
}
