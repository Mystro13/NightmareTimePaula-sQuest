using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Text;

public class SceneLoader:MonoBehaviour
{
   public static SceneLoader instance;
   public enum SceneType
   {
      MainMenu,
      Cemetery,
      FirstFloor,
      CouncilRoom,
      SecondFloor,
      ThirdFloor,
      Throne,
   }

   //sliders
   public Slider AudioVolumeSlider;
   public Slider EFXVolumeSlider;
   public Slider CameraSensitivitySlider;
   public float MaxAudioVolume  = 10;
   public float MaxEFXVolume = 10;
   public float MaxCameraSensitivity = 10;
   public bool playerHasPickedSword;
   public int healthData;
   public int keySlotData;
   public int manaSlotData;
   public int ammoSlotData;
   public int healthPotionSlotData;
   public int permaHealthSlotData;
   public float AudioVolume;
   public float EFXVolume;
   public float CameraSensitivity;
   private SceneType _currentSceneType;
   public SceneType currentSceneType => _currentSceneType;
   //We may need to save the full player between scenes
   GameObject savedPlayer;
   PlayerInteraction playerInteraction;
   //When we save player between scene, we need to define where to spawn the player in the next scene.
   //We could use the scene loader object as the place to spawn the player
   public Transform playerSceneTarget;
   public void Load(SceneType scene)
   {
      _currentSceneType = scene;
      SceneManager.LoadScene(scene.ToString());
   }

   void Awake()
   {
      if (instance == null)
      {
         DontDestroyOnLoad(gameObject);
         instance = this;
         playerHasPickedSword = false;
         healthData = 0;
         keySlotData = 0;
         manaSlotData = 0;
         ammoSlotData = 0;
         healthPotionSlotData = 0;
         permaHealthSlotData = 0;
         AudioVolume = .2f;
         EFXVolume = .2f;
         CameraSensitivity = .2f;
         GetOptionsSliders();
         //if (AudioVolumeSlider != null)
         //{
         //   AudioVolumeSlider.value = AudioVolume;
         //}
         //if (EFXVolumeSlider != null)
         //{
         //   EFXVolumeSlider.value = EFXVolume;
         //}
         //if (CameraSensitivitySlider != null)
         //{
         //   CameraSensitivitySlider.value = CameraSensitivity;
         //}
         Scene currentScene = SceneManager.GetActiveScene();
         switch (currentScene.name)
         {
            case "MainMenu": _currentSceneType = SceneType.MainMenu; break;
            case "Cemetery": _currentSceneType = SceneType.Cemetery; break;
            case "FirstFloor": _currentSceneType = SceneType.FirstFloor; break;
            case "CouncilRoom": _currentSceneType = SceneType.CouncilRoom; break;
            case "SecondFloor": _currentSceneType = SceneType.SecondFloor; break;
            case "ThirdFloor": _currentSceneType = SceneType.ThirdFloor; break;
            case "Throne": _currentSceneType = SceneType.Throne; break;
         }

      }
      else if (instance != this)
      {
         GetOptionsSliders();
         Destroy(gameObject);
      }

      if (playerSceneTarget == null)
         playerSceneTarget = gameObject.transform;
   }

   private static void GetOptionsSliders()
   {
      GameObject canvas = GameObject.Find("Canvas");
      if (canvas)
      {
         foreach (var slider in canvas.transform.GetComponentsInChildren<Slider>(includeInactive: true))
         {
            string sliderName = slider.name;
            if (sliderName.ToUpper().Contains("AUDIOVOLUME"))
            {
               slider.value = instance.AudioVolume;
               instance.AudioVolumeSlider = slider;
            }
            else if (sliderName.ToUpper().Contains("EFX"))
            {
               slider.value = instance.EFXVolume;
               instance.EFXVolumeSlider = slider;
            }
            else if (sliderName.ToUpper().Contains("CAMERA"))
            {
               slider.value = instance.CameraSensitivity;
               instance.CameraSensitivitySlider = slider;
            }
         }
      }
   }

   public void AssignPlayer(GameObject player)
   {
      savedPlayer = player;
   }

   void Update()
   {
      //Debug.Log($"Mouse sensitivity {CameraSensitivitySlider.value * MaxCameraSensitivity}");
   }
}
