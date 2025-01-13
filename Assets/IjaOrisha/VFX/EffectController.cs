using UnityEngine;

namespace IjaOrisha
{
    public enum VFX
    {
        StartClick,
        EndClick,
    }

    public class EffectController : MonoBehaviour
    {
        private void Play(VFX vfx)
        {
            switch (vfx)
            {
                case VFX.StartClick:
                    StartClickFX();
                    break;
                case VFX.EndClick:
                    EndClickFX();
                    break;
            }
        }

        private void StartClickFX()
        {
        }

        private void EndClickFX()
        {
        }
    }
}