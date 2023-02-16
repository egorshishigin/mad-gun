using UnityEngine;

namespace GameSettings
{
    public class SoundSetting : BoolSetting
    {
        protected override void ApplySetting(bool value)
        {
            AudioListener.pause = !value;
        }
    }
}