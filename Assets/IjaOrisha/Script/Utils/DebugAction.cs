using System;
using IjaOrisha.Script.LocalDB;
using IjaOrisha.Script.Network;
using UnityEngine;


public class DebugAction : MonoBehaviour
{
    [SerializeField] private LocalDB loca;

    public void Check()
    {
        // string socResp =
        // "{\"action\":\"roundData\",\"roomID\":\"SRpXDMSi\",\"playerOne\":\"tNLmD0\",\"playerOneBD\":{\"AttackCard\":\"\",\"DefenseCard\":\"\",\"AttackSpell\":\"\",\"DefenseSpell\":\"\",\"AttackPoint\":0,\"DefensePoint\":0,\"PlayerHealth\":100},\"playerTwo\":\"3vmG3g\",\"playerTwoBD\":{\"AttackCard\":\"\",\"DefenseCard\":\"\",\"AttackSpell\":\"\",\"DefenseSpell\":\"\",\"AttackPoint\":0,\"DefensePoint\":0,\"PlayerHealth\":100},\"roundTimeout\":10,\"currentRound\":3,\"totalRounds\":3}";

        // SocMessage socMessage = JsonUtility.FromJson<SocMessage>(socResp);

        Debug.Log(loca.Count);
    }
}