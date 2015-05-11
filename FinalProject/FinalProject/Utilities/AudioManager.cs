using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;

namespace FinalProject.Utilities
{
    public class AudioManager
    {
        private BackgroundMusic backgroundMusic;
        private float masterVolume;
        private Dictionary<string, OneTimeSound> oneTimeSounds;

        public AudioManager(ContentManager content1, ContentManager content2)
        {
            oneTimeSounds = new Dictionary<string, OneTimeSound>();
            if (!LoadConfig())
            {
                masterVolume = .5f;
            }
            backgroundMusic = new BackgroundMusic(content1, content2);
        }

        public float GetVolume()
        {
            return masterVolume;
        }

        public void LoadContent(ContentManager content)
        {
            oneTimeSounds["Menu Sound"] = new OneTimeSound(content.Load<SoundEffect>("Blip_Select"), 1f);
            backgroundMusic.LoadContent();
        }

        public void PlayOneTimeSound(string name)
        {
            PlayOneTimeSound(name, 0);
        }

        public void PlayOneTimeSound(string name, float pan)
        {
            oneTimeSounds[name].Play(masterVolume, pan);
        }

        public void SetVolume(float volume)
        {
            masterVolume = volume;
            SaveConfig();
        }

        public void Update()
        {
            backgroundMusic.Update(masterVolume);
        }

        private bool LoadConfig()
        {
            if (File.Exists("Audio.config"))
            {
                using (StreamReader reader = new StreamReader(File.OpenRead("Audio.config")))
                {
                    string contents = reader.ReadToEnd();
                    if (!float.TryParse(contents, out masterVolume))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SaveConfig()
        {
            if (File.Exists("Audio.config"))
            {
                File.Delete("Audio.config");
            }
            using (StreamWriter writer = new StreamWriter(File.Create("Audio.config")))
            {
                writer.Write(masterVolume);
            }
        }
    }
}