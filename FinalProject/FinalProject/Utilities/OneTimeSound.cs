using Microsoft.Xna.Framework.Audio;

namespace FinalProject.Utilities
{
    internal class OneTimeSound
    {
        private SoundEffect soundEffect;
        private float volume;

        public OneTimeSound(SoundEffect soundEffect, float volume)
        {
            this.soundEffect = soundEffect;
            this.volume = volume;
        }

        public void Play(float masterVolume, float pan)
        {
            soundEffect.Play(volume * masterVolume, 0, pan);
        }
    }
}