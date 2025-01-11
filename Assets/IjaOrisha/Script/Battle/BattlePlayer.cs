namespace IjaOrisha
{
    public class BattlePlayer
    {
        public static string PlayerOneID { get; private set; }
        public static string PlayerTwoID { get; private set; }

        public static BattleData PlayerOneBd { get; private set; }
        public static BattleData PlayerTwoBd { get; private set; }

        public static void LoadDummy() //TODO: Demo only
        {
            // TODO: demo only
            PlayerOneID = "vwZEue";
            SetBattleData(_demoMes);
        }

        public static void SetPlayer(SocMessage messsage)
        {
            PlayerOneID = PlayerManager.Instance.PlayerID;

            if (messsage.playerOne == PlayerOneID)
                PlayerTwoID = messsage.playerTwo;
            else if (messsage.playerTwo == PlayerOneID)
                PlayerTwoID = messsage.playerOne;
        }

        public static void SetBattleData(SocMessage messsage)
        {
            if (PlayerOneID == messsage.playerOne)
            {
                PlayerOneBd = messsage.playerOneBD;
                PlayerTwoBd = messsage.playerTwoBD;
            }
            else
            {
                PlayerOneBd = messsage.playerTwoBD;
                PlayerTwoBd = messsage.playerOneBD;
            }
        }

        public static void FindWinner()
        {
            if (!EventSub.IsSessionEnd) return;
            if (PlayerOneBd.PlayerHealth > PlayerTwoBd.PlayerHealth)
            {
                EventPub.Emit(PlayEvent.OnSessionWin);
            }
            else if (PlayerOneBd.PlayerHealth < PlayerTwoBd.PlayerHealth)
            {
                EventPub.Emit(PlayEvent.OnSessionLose);
            }
            else
            {
                EventPub.Emit(PlayEvent.OnSessionDraw);
            }
        }

        private static SocMessage _demoMes = new SocMessage()
        {
            action = "battleData",
            roomID = "hYNopRTi",
            playerOne = "vwZEue",
            playerOneBD = new BattleData()
            {
                AttackCard = "sango",
                DefenseCard = "osun",
                AttackSpell = "divideByTwo",
                DefenseSpell = "None",
                AttackPoint = 20,
                DefensePoint = 4,
                PlayerHealth = 10,
            },
            playerTwo = "RnYL7w",
            playerTwoBD = new BattleData()
            {
                AttackCard = "ogun",
                DefenseCard = "yemoja",
                AttackSpell = "divideByTwo",
                DefenseSpell = "doubleByTwo",
                AttackPoint = 8,
                DefensePoint = 20,
                PlayerHealth = 15,
            },
            roundTimeout = 20,
            currentRound = 2,
            totalRounds = 3
        };
    }
}

public enum PlayerID
{
    Player1,
    Player2
}