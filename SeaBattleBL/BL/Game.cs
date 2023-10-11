using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBL
{
    public class Game
    {
        public int stepsPlayer = 0;
        public int stepsBot = 0;

        public int gameTime = 0;

        Field _playerField;
        Field _botField;

        Ship[] _playerShips;
        Ship[] _botShips;

        readonly Player _player;

        public event FieldCellStateChanged CellPlayerChanged
        {
            add
            {
                _playerField.CellChanged += value;
            }
            remove
            {
                _playerField.CellChanged -= value;
            }
        }

        public event FieldCellStateChanged CellBotChanged
        {
            add
            {
                _botField.CellChanged += value;
            }
            remove
            {
                _botField.CellChanged -= value;
            }
        }

        public Game()
        {
            _playerField = new Field();
            _botField = new Field();

            _playerShips = new Ship[10];
            _botShips = new Ship[10];

            _player = new Player();

            _playerField.InizializeField(_playerShips);
            _botField.InizializeField(_botShips);
        }

        /// <summary>
        /// Return player Field.
        /// </summary>
        public Field PlayerField { get { return _playerField; } set { _playerField = value; } }

        /// <summary>
        /// Return bot Field.
        /// </summary>
        public Field BotField { get { return _botField; } set { _playerField = value; } }

        /// <summary>
        /// Return player.
        /// </summary>
        public Player Player { get { return _player; } }

        // TODO: 2 + использование 
        /// <summary>
        /// Return player Ships.
        /// </summary>
        public IEnumerable<Ship> PlayerShips { get { return _playerShips; } }

        /// <summary>
        /// Return bot Ships.
        /// </summary>
        public IEnumerable<Ship> BotShips { get { return _botShips; } }

        /// <summary>
        /// Shuffle Player & Bot Ships.
        /// </summary>
        public void ShuffleShips()
        {
            _playerField.InizializeField(_playerShips);
            _botField.InizializeField(_botShips);
        }
    }
}
