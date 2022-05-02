using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
   public Sound[] soundCatalog;

   public static AudioManager instance;
   private List<Sound> playList = new List<Sound>();
   void Awake()
   {
      GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
      OnGameStateChanged(GameStateManager.Instance.CurrentGameState);

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

      foreach (Sound sound in soundCatalog)
      {
         sound.source = gameObject.AddComponent<AudioSource>();
         sound.source.clip = sound.clip;
         sound.source.volume = sound.volume;
         sound.source.pitch = sound.pitch;
         sound.source.loop = sound.loop;
         playList.Add(sound);
      }
   }
   void OnDestroy()
   {
      GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
   }

   public void Play(GameSounds soundToPlay)
   {
      Sound soundFound = Array.Find(soundCatalog, sound => sound.whichSound == soundToPlay);
      if (soundFound == null)
      {
         return;
      }
      soundFound.source.Play();
   }

   private void OnGameStateChanged(GameState newGameState)
   {
      if (newGameState == GameState.GamePlay)
      {
         playList.ForEach(sound => sound.source.UnPause());
      }
      else
      {
         playList.ForEach(sound => sound.source.Pause());
      }
   }
}
