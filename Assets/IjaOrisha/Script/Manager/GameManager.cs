using UnityEngine;
using UnityEngine.SceneManagement;

namespace IjaOrisha.Cards.Script
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

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

            EventSub.Initialize();
        }

        public void ReloadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}