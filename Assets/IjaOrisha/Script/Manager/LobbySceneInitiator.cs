using DG.Tweening;
using UnityEngine;

namespace IjaOrisha
{
    public class LobbySceneInitiator : MonoBehaviour
    {
        [SerializeField] private GameObject environment;

        [Header("Game Manager")] [SerializeField]
        private GameObject gameManager;

        [SerializeField] private LobbySocket lobbySocket;

        [Header("UI")] [SerializeField] private GameObject canvasWrap;
        [SerializeField] private RectTransform lobbyNetworkUI;
        [SerializeField] private RectTransform gameMenuUI;

        private void Start()
        {
            InstantiateData();
            DOVirtual.DelayedCall(3f, LoadData);
        }

        private void InstantiateData()
        {
            environment.SetActive(true);
            gameManager.SetActive(true);
            canvasWrap.SetActive(true);
            lobbyNetworkUI.gameObject.SetActive(true);
            lobbySocket.gameObject.SetActive(true);
        }

        private void LoadData()
        {
            lobbySocket.Connect();
            gameMenuUI.gameObject.SetActive(true);
        }
    }
}