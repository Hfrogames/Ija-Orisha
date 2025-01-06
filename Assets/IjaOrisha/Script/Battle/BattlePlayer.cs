using UnityEngine;

namespace IjaOrisha.Script.Battle
{
    public class BattlePlayer
    {
        public static string PlayerOneID { get; private set; }
        public string PlayerTwoID { get; private set; }

        public static BattleData PlayerOneBd { get; private set; }
        public static BattleData PlayerTwoBd { get; private set; }

        public static void SetPlayer(SocMessage messsage)
        {
        }

        public static void SetBattleData(SocMessage messsage)
        {
            PlayerOneID = "vwZEue";
            // create new SocMessage from this string
            string stringSoc =
                "{\"action\":\"battleData\",\"roomID\":\"hYNopRTi\",\"playerOne\":\"vwZEue\",\"playerOneBD\":{\"AttackCard\":\"sango\",\"DefenseCard\":\"yemoja\",\"AttackSpell\":\"doubleByTwo\",\"DefenseSpell\":\"None\",\"AttackPoint\":20,\"DefensePoint\":10,\"PlayerHealth\":20,\"AttackPointSpelled\":20,\"DefencePointSpelled\":10},\"playerTwo\":\"RnYL7w\",\"playerTwoBD\":{\"AttackCard\":\"ogun\",\"DefenseCard\":\"osun\",\"AttackSpell\":\"None\",\"DefenseSpell\":\"doubleByTwo\",\"AttackPoint\":8,\"DefensePoint\":16,\"PlayerHealth\":20,\"AttackPointSpelled\":8,\"DefencePointSpelled\":16},\"roundTimeout\":20,\"currentRound\":2,\"totalRounds\":3}";

            messsage = new SocMessage()
            {
                action = "battleData",
                roomID = "hYNopRTi",
                playerOne = "vwZEue",
                playerOneBD = new BattleData()
                {
                    AttackCard = "ogun",
                    DefenseCard = "yemoja",
                    AttackSpell = "doubleByTwo",
                    DefenseSpell = "None",
                    AttackPoint = 16,
                    DefensePoint = 10,
                    PlayerHealth = 20,
                },
                playerTwo = "RnYL7w",
                playerTwoBD = new BattleData()
                {
                    AttackCard = "ogun",
                    DefenseCard = "yemoja",
                    AttackSpell = "None",
                    DefenseSpell = "doubleByTwo",
                    AttackPoint = 8,
                    DefensePoint = 20,
                    PlayerHealth = 20,
                },
                roundTimeout = 20,
                currentRound = 2,
                totalRounds = 3
            };
            
            
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
            // messsage = JsonUtility.FromJson<SocMessage>(stringSoc); // TODO: demo only

        }
    }
}

public enum PlayerID
{
    Player1,
    Player2
}