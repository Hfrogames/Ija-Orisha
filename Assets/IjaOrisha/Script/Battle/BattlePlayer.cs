namespace IjaOrisha
{
    public class BattlePlayer
    {
        public static string PlayerOneID { get; private set; }
        public static string PlayerTwoID { get; private set; }

        public static BattleData PlayerOneBd { get; private set; }
        public static BattleData PlayerTwoBd { get; private set; }

        private static bool roundComplete;

        public static int CurrentRound { get; private set; }
        public static int MaxRound { get; private set; }

        public static void LoadDummy() //TODO: Demo only
        {
            // TODO: demo only
            PlayerOneID = "vwZEue";
            CurrentRound = 1;
            MaxRound = 3;
            SetBattleData(_demoMes);
        }

        public static void UpdateBattleInfo(SocMessage messsage)
        {
            CurrentRound = messsage.currentRound;
            MaxRound = messsage.totalRounds;
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

            roundComplete = messsage.currentRound == messsage.totalRounds ? true : false;
        }

        public static void FindWinner()
        {
            if (EventSub.InSimulation || !EventSub.IsSessionEnd || !roundComplete) return;
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
                AttackCard = "oba",
                DefenseCard = "osun",
                AttackSpell = "divideByTwo",
                DefenseSpell = "None",
                AttackPoint = 4,
                DefensePoint = 4,
                PlayerHealth = 20,
            },
            playerTwo = "RnYL7w",
            playerTwoBD = new BattleData()
            {
                AttackCard = "olokun",
                DefenseCard = "yemoja",
                AttackSpell = "divideByTwo",
                DefenseSpell = "None",
                AttackPoint = 4,
                DefensePoint = 5,
                PlayerHealth = 20,
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