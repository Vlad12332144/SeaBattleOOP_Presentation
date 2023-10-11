using SeaBattleBL.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace SeaBattleBL
{
    // TODO: 2 использование собственного IFieldViev.
    public class Field : IFieldViev
    {
        const int DEFAULT_COUNT_ROW = 10;
        const int DEFAULT_COUNT_COLUMN = 10;

        int _countRow;
        int _countColumn;

        // TODO: 4 использование стандартных коллекций.
        private readonly Dictionary<Coordinate, Cell> _cells = new Dictionary<Coordinate, Cell>();

        private static int _index = 0;

        private FieldCellStateChanged _cellChanged;

        /// <summary>
        /// Cell Change Event.
        /// </summary>
        public event FieldCellStateChanged CellChanged
        {
            add
            {
                _cellChanged += value;
            }
            remove
            {
                _cellChanged -= value;
            }
        }

        private void OnCellChanged(Coordinate cord, Cell state)
        {
            if(_cellChanged != null)
            {
                _cellChanged(this, new CellChangedEventArgs(cord, state));
            }
        }

        private void OnCellChanged(int row, int column, Cell state)
        {
            OnCellChanged(new Coordinate(row, column), state);
        }

        #region -==- Return Size Field -==-

        /// <summary>
        /// Return the number of rows.
        /// </summary>
        public int CountRow
        {
            get { return _countRow; }
        }

        /// <summary>
        /// Return the number of columns.
        /// </summary>
        public int CountColumn
        {
            get { return _countColumn; }
        }

        #endregion

        #region -==- Return Cell -==-

        /// <summary>
        /// Returns a cell by coordinate.
        /// </summary>
        /// <param name="indexRow">Coordinate X.</param>
        /// <param name="indexColumn">Coordinate Y.</param>
        /// <returns>Cell.</returns>
        public Cell this[int indexRow, int indexColumn]
        {
            get
            {
                return _cells[new Coordinate(indexRow, indexColumn)];
            }
        }

        #endregion

        #region -==- InizializeField -==-

        /// <summary>
        /// Inizialize field.
        /// </summary>
        /// <param name="ownerShip">Player or bot ships.</param>
        /// <param name="row">Number of lines in the field.</param>
        /// <param name="column">Number of columns in the field.</param>
        public void InizializeField(Ship[] ownerShip, int row = DEFAULT_COUNT_ROW,
                int column = DEFAULT_COUNT_COLUMN)
        {
            _cells.Clear();

            _countRow = row;
            _countColumn = column;

            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < column; y++)
                {
                    _cells.Add(new Coordinate(x, y), null);
                }
            }

            GetShips(ownerShip);

            _index = 0;
        }

        /// <summary>
        /// Places ships.
        /// </summary>
        /// <param name="ownerShip">Player or bot ships.</param>
        public void GetShips(Ship[] ownerShip)
        {
            DoShips(ownerShip, 4, 1);
            DoShips(ownerShip, 3, 2);
            DoShips(ownerShip, 2, 3);
            DoShips(ownerShip, 1, 4);
        }

        /// <summary>
        /// Selects a coordinate and places the ship.
        /// </summary>
        /// <param name="ownerShip">Player or bot ships.</param>
        /// <param name="deck">Number of decks.</param>
        /// <param name="countShips">Number of ships.</param>
        public void DoShips(Ship[] ownerShip, int deck, int countShips)
        {
            for (int j = 0; j < countShips; j++)
            {
                int row;
                int column;

                int direction;

                bool placeShip = true;

                Deck[] decks = new Deck[deck];

                while (placeShip)
                {
                    row = Randomizer.RandomizePositionShip();    // Ряд.           -| Функция.
                    column = Randomizer.RandomizePositionShip();    // Колонка.    -|

                    direction = Randomizer.RandomizeDirectionShip();    // Направление корабля.

                    switch (direction)    // Направление корабля.
                    {
                        case 0:
                            if(!PlaceShipVerticale(column, row, deck, ownerShip, ref decks))
                            {
                                continue;
                            }

                            break;
                        case 1:
                            if (!PlaceShipGorizontal(column, row, deck, ownerShip, ref decks))
                            {
                                continue;
                            }

                            break;
                    }
                    placeShip = false;

                    ownerShip[_index] = Ship.CreateShip(decks);
                    _index++;
                }
            }
        }

        #region -==- Place Ship -==-

        private bool PlaceShipVerticale(int column, int row, int deck, Ship[] ownerShip, ref Deck[] decks)
        {
            if (!(column + deck - 1 >= 0 && column + deck - 1 <= 9))    // Помещается ли корабль.
            {
                return false;
            }

            // Есть ли другие корабли рядом.
            if (CheckAnotherShip(row, column, row, column + deck - 1))
            {
                return false;
            }

            for (int y = column; y < column + deck; y++)    // ставит корабль.
            {
                Deck newDeck = new Deck(new Coordinate(row, y), ownerShip[_index]);
                _cells[new Coordinate(row, y)] = newDeck;
                decks[y - column] = newDeck;
            }

            return true;
        }

        private bool PlaceShipGorizontal(int column, int row, int deck, Ship[] ownerShip, ref Deck[] decks)
        {
            if (!(row + deck - 1 >= 0 && row + deck - 1 <= 9))    // Помещается ли корабль.
            {
                return false;
            }

            // Есть ли другие корабли рядом.
            if (CheckAnotherShip(row, column, row + deck - 1, column))
            {
                return false;
            }

            for (int x = row; x < row + deck; x++)    // ставит корабль.
            {
                Deck newDeck = new Deck(new Coordinate(x, column), ownerShip[_index]);
                _cells[new Coordinate(x, column)] = newDeck;
                decks[x - row] = newDeck;
            }

            return true;
        }

        #endregion

        private bool CheckAnotherShip(int leftTopX, int leftTopY, int rightBottomX, int rightBottomY)
        {
            bool res = false;

            FindExtremePointShip(this, leftTopX, leftTopY, rightBottomX, rightBottomY,
                    out leftTopX, out leftTopY, out rightBottomX, out rightBottomY);

            for (int x = leftTopX; x < rightBottomX + 1; x++)
            {
                for(int y = leftTopY; y < rightBottomY + 1; y++)
                {
                    if (_cells[new Coordinate(x, y)] is Deck)
                    {
                        res = true;
                        break;
                    }
                }
                if (res)
                {
                    break;
                }
            }

            return res;
        }

        #endregion

        #region -==- Do Around Dead Ship -==-

        /// <summary>
        /// Fills with empty cells around a dead ship.
        /// </summary>
        /// <param name="ship">Ship</param>
        public void DoAnotherShip(Ship ship)
        {
            int leftTopX;
            int leftTopY;

            int rightBottomX;
            int rightBottomY;

            FindExtremePointShip(this, ship.XFirst, ship.YFirst, ship.XLast, ship.YLast,
                    out leftTopX, out leftTopY, out rightBottomX, out rightBottomY);

            for (int x = leftTopX; x < rightBottomX + 1; x++)
            {
                for (int y = leftTopY; y < rightBottomY + 1; y++)
                {
                    if(_cells[new Coordinate(x, y)] is null)
                    {
                        _cells[new Coordinate(x, y)] = new Shoot(new Coordinate(x, y));
                        OnCellChanged(x, y, _cells[new Coordinate(x, y)]);
                    }
                }
            }
        }

        private static void FindExtremePointShip(IFieldViev field, int xHead,
                int yHead, int xTail, int yTail, out int leftTopX, out int leftTopY,
                out int rightBottomX, out int rightBottomY)
        {
            if (xHead - 1 >= 0 && xHead - 1 < field.CountRow)
            {
                leftTopX = xHead - 1;
            }
            else
            {
                leftTopX = xHead;
            }

            if (yHead - 1 >= 0 && yHead - 1 < field.CountColumn)
            {
                leftTopY = yHead - 1;
            }
            else
            {
                leftTopY = yHead;
            }

            if (xTail + 1 >= 0 && xTail + 1 < field.CountRow)
            {
                rightBottomX = xTail + 1;
            }
            else
            {
                rightBottomX = xTail;
            }

            if (yTail + 1 >= 0 && yTail + 1 < field.CountColumn)
            {
                rightBottomY = yTail + 1;
            }
            else
            {
                rightBottomY = yTail;
            }
        }

        #endregion

        #region -==- Fire -==-

        /// <summary>
        /// Converts the coordinate entered by the players into the coordinate for the shot.
        /// </summary>
        /// <param name="y">Coordinate y the player entered.</param>
        /// <param name="x">Coordinate x the player entered.</param>
        /// <param name="returnStep">Was a shot fired.</param>
        /// <exception cref="HitingTheSamePointExeption">Hitting a coordinate that has already been shot through.</exception>
        public void DoFirePlayer(int y, int x, ref bool returnStep)
        {
            if(!DoFire(x, y, ref returnStep))
            {
                throw new HitingTheSamePointExeption("Hiting the samepoint!!!");
            }
        }

        /// <summary>
        /// Fired a salvo at the coordinate.
        /// </summary>
        /// <param name="x">Coordinate X.</param>
        /// <param name="y">Coordinate Y.</param>
        /// <param name="returnStep">Hitting a coordinate that has already been shot through.</param>
        public bool DoFire(int x, int y, ref bool returnStep)
        {
            bool doFire = false;

            if (_cells[new Coordinate(x, y)] is Deck deck)
            {
                if (!deck.State)
                {
                    doFire = true;
                    deck.State = true;

                    OnCellChanged(deck.X, deck.Y, deck);
                }
            }
            else if(_cells[new Coordinate(x, y)] is null)
            {
                returnStep = true;
                doFire = true;
                _cells[new Coordinate(x, y)] = new Shoot(new Coordinate(x, y));

                OnCellChanged(((Shoot)_cells[new Coordinate(x, y)]).X,
                        ((Shoot)_cells[new Coordinate(x, y)]).Y, _cells[new Coordinate(x, y)]);
            }
            
            return doFire;
        }

        #endregion
    }
}