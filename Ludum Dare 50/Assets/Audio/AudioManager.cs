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

        private KnownedMusics currentMusic;

        private Dictionary<KnownedMusics, string[]> musicDefinition = new Dictionary<KnownedMusics, string[]>()
        {
            { KnownedMusics.bump_1, new string [] { "bump_1" } },
            { KnownedMusics.night_tense_acoustic, new string [] { "night_tense_acoustic", "water_waves_1" } },
            { KnownedMusics.adventure_serious_cinematic, new string [] { "adventure_serious_cinematic" } },
            { KnownedMusics.village_sad_acoustic, new string [] { "village_sad_acoustic" } },
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

            var musics = musicDefinition[music];
            if (musics == null || musics.Length <= 0)
            {
                return;
            }

            // Stop all sounds
            Stop(sounds.Select(s => s.name).ToArray());

            var firstSound = Play(musics).First();

            currentMusic = music;
        }

        private Sound[] Play(params string[] names)
        {
            var soundsToPlay = names.Select(n => sounds.Where(s => s.name == n).First());
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
    }
}