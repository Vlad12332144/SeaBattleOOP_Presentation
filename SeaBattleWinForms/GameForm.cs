using SeaBattleBL;
using SeaBattleBL.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattleOOPWinForms
{
    public partial class GameForm : Form
    {
        private static Button[,] _btnPlayer;
        private static Button[,] _btnBot;

        private Game _game;

        private GameState _state;

        public GameForm()
        {
            InitializeComponent();
        }

        #region -==- Timers -==-

        private void DoFireBot_Tick(object sender, EventArgs e)
        {
            if(Fire.DoFire(FireState.BotQueue, _game.PlayerField))
            {
                _game.stepsBot++;
                lblStepsBot.Text = _game.stepsBot.ToString();
            }
        }

        private void CheckWinner_Tick(object sender, EventArgs e)
        {
            switch (_game.Player.CheckWinner(_game.PlayerShips, _game.BotShips))
            {
                case GameState.PlayerWin:
                    checkWinner.Stop();
                    doTime.Stop();
                    ShowMessage("Player Win!!!");
                    btnStop_Click(this, EventArgs.Empty);

                    break;
                case GameState.BotWin:
                    checkWinner.Stop();
                    doTime.Stop();
                    ShowMessage("Bot Win!!!");
                    btnStop_Click(this, EventArgs.Empty);

                    break;
            }
        }

        private void doTime_Tick(object sender, EventArgs e)
        {
            _game.gameTime += 1;
            lblTime.Text = _game.gameTime.ToString();

            _game.Player.CheckShips(_game.PlayerShips, _game.PlayerField);
            _game.Player.CheckShips(_game.BotShips, _game.BotField);
        }

        #endregion

        #region -==- Buttons -==-

        private void btnPlay_Click(object sender, EventArgs e)
        {
            doFireBot.Start();
            checkWinner.Start();
            doTime.Start();

            btnStop.Enabled = true;
            btnRestart.Enabled = true;
            btnPlay.Enabled = false;
            btnShuffle.Enabled = false;

            _state = GameState.GameContinue;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            doFireBot.Stop();
            checkWinner.Stop();
            doTime.Stop();

            btnStop.Enabled = false;
            btnPlay.Enabled = true;

            _state = GameState.GameStop;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            btnPlay.Enabled = true;
            btnStop.Enabled = false;
            btnRestart.Enabled = false;
            btnShuffle.Enabled = true;

            _game.gameTime = 0;
            _game.stepsBot = 0;
            _game.stepsPlayer = 0;
            lblTime.Text = _game.gameTime.ToString();
            lblStepsBot.Text = _game.stepsBot.ToString();
            lblStepsPlayer.Text = _game.stepsPlayer.ToString();

            doTime.Stop();
            checkWinner.Stop();
            doFireBot.Stop();

            _game.ShuffleShips();

            UI.DrawFields(_game.PlayerField, _game.BotField);
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            _game.ShuffleShips();

            UI.DrawFields(_game.PlayerField, _game.BotField);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region -==- RefreshButton -==-

        /// <summary>
        /// Refresh One Player Button.
        /// </summary>
        /// <param name="row">Coordinate X.</param>
        /// <param name="column">Coordinate Y.</param>
        /// <param name="symbol">The symbol you want to change to.</param>
        public static void RefreshButtonPlayer(int row, int column, Image symbol)
        {
            _btnPlayer[row, column].Image = symbol;
        }

        /// <summary>
        /// Refresh One Bot Button.
        /// </summary>
        /// <param name="row">Coordinate X.</param>
        /// <param name="column">Coordinate Y.</param>
        /// <param name="symbol">The symbol you want to change to.</param>
        public static void RefreshButtonBot(int row, int column, Image symbol)
        {
            _btnBot[row, column].Image = symbol;
        }

        #endregion

        #endregion

        /// <summary>
        /// Show MessageBox.
        /// </summary>
        /// <param name="message">Message for output.</param>
        public static void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _btnPlayer = new Button[10, 10]
            {
                { btnPlayer0_0, btnPlayer0_1, btnPlayer0_2, btnPlayer0_3, btnPlayer0_4, btnPlayer0_5, btnPlayer0_6, btnPlayer0_7, btnPlayer0_8, btnPlayer0_9 },
                { btnPlayer1_0, btnPlayer1_1, btnPlayer1_2, btnPlayer1_3, btnPlayer1_4, btnPlayer1_5, btnPlayer1_6, btnPlayer1_7, btnPlayer1_8, btnPlayer1_9 },
                { btnPlayer2_0, btnPlayer2_1, btnPlayer2_2, btnPlayer2_3, btnPlayer2_4, btnPlayer2_5, btnPlayer2_6, btnPlayer2_7, btnPlayer2_8, btnPlayer2_9 },
                { btnPlayer3_0, btnPlayer3_1, btnPlayer3_2, btnPlayer3_3, btnPlayer3_4, btnPlayer3_5, btnPlayer3_6, btnPlayer3_7, btnPlayer3_8, btnPlayer3_9 },
                { btnPlayer4_0, btnPlayer4_1, btnPlayer4_2, btnPlayer4_3, btnPlayer4_4, btnPlayer4_5, btnPlayer4_6, btnPlayer4_7, btnPlayer4_8, btnPlayer4_9 },
                { btnPlayer5_0, btnPlayer5_1, btnPlayer5_2, btnPlayer5_3, btnPlayer5_4, btnPlayer5_5, btnPlayer5_6, btnPlayer5_7, btnPlayer5_8, btnPlayer5_9 },
                { btnPlayer6_0, btnPlayer6_1, btnPlayer6_2, btnPlayer6_3, btnPlayer6_4, btnPlayer6_5, btnPlayer6_6, btnPlayer6_7, btnPlayer6_8, btnPlayer6_9 },
                { btnPlayer7_0, btnPlayer7_1, btnPlayer7_2, btnPlayer7_3, btnPlayer7_4, btnPlayer7_5, btnPlayer7_6, btnPlayer7_7, btnPlayer7_8, btnPlayer7_9 },
                { btnPlayer8_0, btnPlayer8_1, btnPlayer8_2, btnPlayer8_3, btnPlayer8_4, btnPlayer8_5, btnPlayer8_6, btnPlayer8_7, btnPlayer8_8, btnPlayer8_9 },
                { btnPlayer9_0, btnPlayer9_1, btnPlayer9_2, btnPlayer9_3, btnPlayer9_4, btnPlayer9_5, btnPlayer9_6, btnPlayer9_7, btnPlayer9_8, btnPlayer9_9 }
            };

            _btnBot = new Button[10, 10]
            {
                { btnBotA_0, btnBotA_1, btnBotA_2, btnBotA_3, btnBotA_4, btnBotA_5, btnBotA_6, btnBotA_7, btnBotA_8, btnBotA_9 },
                { btnBotB_0, btnBotB_1, btnBotB_2, btnBotB_3, btnBotB_4, btnBotB_5, btnBotB_6, btnBotB_7, btnBotB_8, btnBotB_9 },
                { btnBotC_0, btnBotC_1, btnBotC_2, btnBotC_3, btnBotC_4, btnBotC_5, btnBotC_6, btnBotC_7, btnBotC_8, btnBotC_9 },
                { btnBotD_0, btnBotD_1, btnBotD_2, btnBotD_3, btnBotD_4, btnBotD_5, btnBotD_6, btnBotD_7, btnBotD_8, btnBotD_9 },
                { btnBotE_0, btnBotE_1, btnBotE_2, btnBotE_3, btnBotE_4, btnBotE_5, btnBotE_6, btnBotE_7, btnBotE_8, btnBotE_9 },
                { btnBotF_0, btnBotF_1, btnBotF_2, btnBotF_3, btnBotF_4, btnBotF_5, btnBotF_6, btnBotF_7, btnBotF_8, btnBotF_9 },
                { btnBotG_0, btnBotG_1, btnBotG_2, btnBotG_3, btnBotG_4, btnBotG_5, btnBotG_6, btnBotG_7, btnBotG_8, btnBotG_9 },
                { btnBotH_0, btnBotH_1, btnBotH_2, btnBotH_2, btnBotH_4, btnBotH_5, btnBotH_6, btnBotH_7, btnBotH_8, btnBotH_9 },
                { btnBotI_0, btnBotI_1, btnBotI_2, btnBotI_3, btnBotI_4, btnBotI_5, btnBotI_6, btnBotI_7, btnBotI_8, btnBotI_9 },
                { btnBotJ_0, btnBotJ_1, btnBotJ_2, btnBotJ_3, btnBotJ_4, btnBotJ_5, btnBotJ_6, btnBotJ_7, btnBotJ_8, btnBotJ_9 }
            };

            // TODO: 7 использование встроенного события кнопки
            foreach(Button btn in _btnBot)
            {
                btn.Click += new EventHandler(OnClick);
                btn.Text = btn.Name[6] + "_" + btn.Name[8];
                btn.Font = new Font("", (float)1.5);
            }

            _game = new Game();

            _game.CellPlayerChanged += UI.RefreshPlayerCell;
            _game.CellBotChanged += UI.RefreshBotCell;

            UI.DrawFields(_game.PlayerField, _game.BotField);

            _state = GameState.GameStop;
        }

        // TODO: 8 метод для события назатия кнопки.
        private void OnClick(object sender, EventArgs args)
        {
            if(_state == GameState.GameContinue)
            {
                if(Fire.DoFire(FireState.PlayerQueue, _game.BotField,
                    char.ToLower(((Button)sender).Text[0]), ((Button)sender).Text[2]))
                {
                    _game.stepsPlayer++;
                    lblStepsPlayer.Text = _game.stepsPlayer.ToString();
                }
            }
        }
    }
}
