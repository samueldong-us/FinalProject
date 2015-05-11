using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Threading;

namespace FinalProject.Utilities
{
    internal class BackgroundMusic
    {
        private const float Volume = 1f;
        private int bufferIndex;
        private ContentManager content;
        private ContentManager[] contentBuffer;
        private SoundEffectInstance currentSong;
        private SoundState lastSoundState;
        private List<string> playlist;
        private int playlistIndex;
        private SoundEffect[] songBuffer;
        private string[] songs = { "Acrylics", "Zoooom", "Goooo", "Twistclip" };

        public BackgroundMusic(ContentManager content1, ContentManager content2)
        {
            songBuffer = new SoundEffect[2];
            InitializeVolumes();
            playlist = new List<string>();
            bufferIndex = 0;
            playlistIndex = 0;
            lastSoundState = SoundState.Stopped;
            GeneratePlaylist();
            contentBuffer = new ContentManager[2];
            contentBuffer[0] = content1;
            contentBuffer[1] = content2;
        }

        public void LoadContent()
        {
            LoadSong(playlist[0], FrontBuffer());
            LoadSong(playlist[1], BackBuffer());
            Play();
        }

        public void Update(float masterVolume)
        {
            if (currentSong.State == SoundState.Stopped && lastSoundState == SoundState.Playing)
            {
                playlistIndex = (playlistIndex + 1) % playlist.Count;
                SwapBuffers();
                contentBuffer[BackBuffer()].Unload();
                if (playlistIndex == playlist.Count - 1)
                {
                    GeneratePlaylist();
                    LoadSongAsynchronously(playlist[0], BackBuffer());
                }
                else
                {
                    LoadSongAsynchronously(playlist[playlistIndex + 1], BackBuffer());
                }
                Play();
            }
            currentSong.Volume = masterVolume * 1;
            lastSoundState = currentSong.State;
        }

        private int BackBuffer()
        {
            return (bufferIndex + 1) % 2;
        }

        private int FrontBuffer()
        {
            return bufferIndex;
        }

        private void GeneratePlaylist()
        {
            int lastSongIndex = -1;
            List<int> indicies = new List<int>();
            for (int i = 0; i < songs.Length; i++)
            {
                if (playlist.Count > 0 && playlist[playlist.Count - 1].Equals(songs[i]))
                {
                    lastSongIndex = i;
                }
                else
                {
                    indicies.Add(i);
                }
            }
            playlist.Clear();
            while (indicies.Count > 0)
            {
                int index = indicies[GameMain.RNG.Next(indicies.Count)];
                indicies.Remove(index);
                playlist.Add(songs[index]);
            }
            if (lastSongIndex != -1)
            {
                playlist.Insert(GameMain.RNG.Next(1, playlist.Count), songs[lastSongIndex]);
            }
        }

        private void InitializeVolumes()
        {
        }

        private void LoadSong(string name, int index)
        {
            songBuffer[index] = contentBuffer[index].Load<SoundEffect>(name);
        }

        private void LoadSongAsynchronously(string name, int index)
        {
            new Thread(() => LoadSong(name, index)).Start();
        }

        private void Play()
        {
            currentSong = songBuffer[FrontBuffer()].CreateInstance();
            currentSong.Play();
        }

        private void SwapBuffers()
        {
            bufferIndex = BackBuffer();
        }
    }
}