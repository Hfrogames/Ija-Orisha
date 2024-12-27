using System;
using MatchIt.Script.Utils;
using UnityEngine;

namespace MatchIt.Player.Script
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        public string PlayerID { get; private set; }

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
            PlayerID = GetPlayerID();
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
    }
}