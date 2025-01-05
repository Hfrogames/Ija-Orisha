using System;
using IjaOrisha.Script.Utils;
using UnityEngine;

namespace IjaOrisha.Cards.Script
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        public string PlayerOneID { get; private set; }
        public string PlayerTwoID { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            PlayerOneID = GetPlayerID();
        }

        private string GetPlayerID()
        {
            string id = SaveData.GetItemString("PlayerID");

            if (id == String.Empty)
            {
                id = Random2.GenerateRandomString(6);
                SaveData.SetItem("PlayerID", id);
            }

            return id;
        }

        public void SetPlayerTwoID(SocMessage socMessage)
        {
            if (socMessage.playerOne == PlayerOneID)
                PlayerTwoID = socMessage.playerTwo;
            else if (socMessage.playerTwo == PlayerOneID)
                PlayerTwoID = socMessage.playerOne;
        }
    }
}