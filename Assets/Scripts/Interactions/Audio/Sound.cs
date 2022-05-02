using UnityEngine;
[System.Serializable]
public class Sound
{
   public GameSounds whichSound;
   public AudioClip clip;
   [Range(0f, 1f)]
   public float volume;
   [Range(.1f, 3f)]
   public float pitch;

   public bool loop;
   public bool EFX;
   [HideInInspector]
   public AudioSource source;
}
