using System;
using IjaOrisha;
using UnityEngine;


public class DebugAction : MonoBehaviour
{
    [SerializeField] private CardLoader cardLoader;
    [SerializeField] private LocalDB soDB;
    
    
    public  void LoadCard()
    {
        cardLoader.SetCardSo(soDB.GetRandom());
    }
}