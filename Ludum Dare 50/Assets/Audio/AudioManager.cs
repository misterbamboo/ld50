using UnityEngine.Audio;
using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Assets.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        public static AudioManager instance;

        private float lastTimeOfBaseMusic;
        private float loopLenght;

        private double timeSinceLastPlay;

        private KnownedMusics currentMusic;

        private Dictionary<KnownedMusics, string[]> musicDefinition = new Dictionary<KnownedMusics, string[]>()
        {
            { KnownedMusics.bump_1, new string [] { "bump_1" } },
            { KnownedMusics.seagulls_1, new string [] { "seagulls_1" } },
            { KnownedMusics.water_waves_1, new string [] { "water_waves_1" } },
        };

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);

            foreach (var s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void PlayMusic(KnownedMusics music)
        {
            if (currentMusic == music)
            {
                return;
            }

            if (!musicDefinition.ContainsKey(music))
            {
                return;
            }

            var firstMusic = musicDefinition[music].FirstOrDefault();
            if (firstMusic == null)
            {
                return;
            }

            // Stop all sounds
            Stop(sounds.Select(s => s.name).ToArray());

            var firstSound = Play(firstMusic).First();

            currentMusic = music;
            timeSinceLastPlay = 0;

            loopLenght = firstSound.clip.length;
        }

        private Sound[] Play(params string[] names)
        {
            var soundsToPlay = sounds.Where(s => names.Contains(s.name));
            if (!soundsToPlay.Any()) return new Sound[0];

            foreach (var sound in soundsToPlay)
            {
                sound.source.Play();
                sound.playing = true;
            }
            return soundsToPlay.ToArray();
        }

        private void Stop(params string[] names)
        {
            var soundsToStop = sounds.Where(s => names.Contains(s.name));
            if (!soundsToStop.Any()) return;

            foreach (var sound in soundsToStop)
            {
                sound.source.Stop();
                sound.playing = false;
            }
        }

        private void Update()
        {
            if (currentMusic != KnownedMusics.None)
            {
                timeSinceLastPlay += Time.deltaTime;
            }
            else
            {
                timeSinceLastPlay = 0;
                return;
            }

            var firstSoundName = musicDefinition[currentMusic][0];
            var baseSound = sounds.Where(s => s.name == firstSoundName).FirstOrDefault();

            var possibleSoundNames = musicDefinition[currentMusic];

            // When base music just looped to start, add or remove a music
            var loopTime = baseSound.source.time % loopLenght;
            if (lastTimeOfBaseMusic > loopTime)
            {
                var possibleSounds = sounds.Where(p => possibleSoundNames.Contains(p.name));

                var numberOfPlayingSounds = possibleSounds.Where(s => s.playing).Count();

                // should reduce
                if (numberOfPlayingSounds == possibleSoundNames.Length && possibleSoundNames.Length > 1)
                {
                    var playingSounds = possibleSounds.Where(p => p.playing).ToArray();
                    var indexToStop = UnityEngine.Random.Range(1, playingSounds.Length);
                    Stop(playingSounds[indexToStop].name);
                }
                else
                {
                    var nextPlayingSound = possibleSounds.Where(p => !p.playing).FirstOrDefault();
                    if (nextPlayingSound != null)
                    {
                        var newPlayingSounds = Play(nextPlayingSound.name);
                        foreach (var newPlayingSound in newPlayingSounds)
                        {
                            newPlayingSound.source.timeSamples = baseSound.source.timeSamples;
                        }
                    }
                }
            }

            lastTimeOfBaseMusic = loopTime;
        }
    }
}