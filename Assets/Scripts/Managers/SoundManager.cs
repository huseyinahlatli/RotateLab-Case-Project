using Singleton;
using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        public AudioClip collectSound;
        public AudioClip dropSound;
        public AudioClip moneySound;
        private const float MaxVolume = 1.0f;

        public void PlayCollectSound(AudioClip audioClip, Vector3 position)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, MaxVolume);
        }
    }
}
