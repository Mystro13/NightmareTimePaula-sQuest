using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioScript : MonoBehaviour
{
   public AudioSource audioSource;
   public AudioClip audioFX;

   public void Execute()
   {
      Debug.Log("Play Audio");
      audioSource.Play();
      new WaitForSeconds(audioFX.length);
      audioSource.clip = audioFX;
      audioSource.Stop();
   }
}
