using System;
using IjaOrisha.Script.Network;
using UnityEngine;

namespace IjaOrisha.Script.Utils
{
    public class DebugAction : MonoBehaviour
    {
        private void Start()
        {
            string socResp =
                "{\"action\":\"roundData\",\"roomID\":\"SRpXDMSi\",\"playerOne\":\"tNLmD0\",\"playerOneBD\":{\"AttackCard\":\"\",\"DefenseCard\":\"\",\"AttackSpell\":\"\",\"DefenseSpell\":\"\",\"AttackPoint\":0,\"DefensePoint\":0,\"PlayerHealth\":100},\"playerTwo\":\"3vmG3g\",\"playerTwoBD\":{\"AttackCard\":\"\",\"DefenseCard\":\"\",\"AttackSpell\":\"\",\"DefenseSpell\":\"\",\"AttackPoint\":0,\"DefensePoint\":0,\"PlayerHealth\":100},\"roundTimeout\":10,\"currentRound\":3,\"totalRounds\":3}";

            SocMessage socMessage = JsonUtility.FromJson<SocMessage>(socResp);

            Debug.Log(socMessage.playerOneBD.PlayerHealth.ToString());
        }
    }
}