using UnityEngine;
using UnityEngine.Audio;

namespace GameSettings
{
    public class MusicSetting : BoolSetting
    {
        private const string MixerValueName = "MusicVolume";

        [SerializeField] private AudioMixer _musicMixer;

        protected override void ApplySetting(bool value)
        {
            float volume = value ? 0f : -80f;

            _musicMixer.SetFloat(MixerValueName, volume);
        }
    }
}