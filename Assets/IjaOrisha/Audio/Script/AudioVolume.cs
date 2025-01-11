// using System.Globalization;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace Idan_Quest.Idan_Sounds.Script
// {
//     public class AudioVolume : MonoBehaviour
//     {
//         /**
//          * It responsible for controlling audio source volume
//          */
//         private enum VolumeID
//         {
//             SFX,
//             Theme
//         }
//
//         [SerializeField] private VolumeID volumeID;
//         [SerializeField] private AudioSource[] audioSource;
//         [SerializeField] private Slider volumeControl;
//
//         private float _volume;
//
//         private void Start()
//         {
//             volumeControl.onValueChanged.AddListener(SetVolume);
//
//             _volume = SavaData.GetItemFloat(volumeID.ToString(), .2f);
//             volumeControl.value = _volume;
//         }
//
//         private void SetVolume(float value)
//         {
//             _volume = value;
//             foreach (var source in audioSource)
//             {
//                 source.volume = _volume;
//             }
//
//             SavaData.SetItem(volumeID.ToString(), _volume.ToString(CultureInfo.CurrentCulture));
//         }
//     }
// }