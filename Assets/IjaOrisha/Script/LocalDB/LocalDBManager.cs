using UnityEngine;

namespace IjaOrisha.Script.LocalDB
{
    public class LocalDBManager : MonoBehaviour
    {
        public static LocalDBManager Instance { get; private set; }

        [field:SerializeField] public LocalDB CardSoDB { get; private set; }
        [field:SerializeField] public LocalDB SpellSoDB { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
