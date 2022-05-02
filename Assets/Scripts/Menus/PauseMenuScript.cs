using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseMenuScript : MonoBehaviour
{
   public GameObject pauseMenuUI;
   public GameObject optionsMenuUI;


   public void GoToMainMenu()
   {
      GameStateManager.Instance.SetState(GameState.GamePlay);
      SetGameObjectState(pauseMenuUI, false);
      SceneLoader.instance.Load(SceneLoader.SceneType.MainMenu);
      Debug.Log("Go To Main Menu");
   }

   public void OpenOptionsMenu()
   {
      SetPauseController(false);
      SceneLoader.instance.AudioVolumeSlider.value = SceneLoader.instance.AudioVolume;
      SceneLoader.instance.EFXVolumeSlider.value = SceneLoader.instance.EFXVolume;
      SceneLoader.instance.CameraSensitivitySlider.value = SceneLoader.instance.CameraSensitivity;
      SetGameObjectState(pauseMenuUI, false);
      SetGameObjectState(optionsMenuUI, true);
   }

   public void CloseOptionsMenu()
   {
      SetPauseController(true);
      SceneLoader.instance.AudioVolume = SceneLoader.instance.AudioVolumeSlider.value;
      SceneLoader.instance.EFXVolume = SceneLoader.instance.EFXVolumeSlider.value;
      SceneLoader.instance.CameraSensitivity = SceneLoader.instance.CameraSensitivitySlider.value;
      SetGameObjectState(pauseMenuUI, true);
      SetGameObjectState(optionsMenuUI, false);
   }
   void SetGameObjectState(GameObject obj, bool state)
   {
      if (obj)
      {
         obj.SetActive(state);
      }
   }
   void SetPauseController(bool option)
   {
      PauseController pauseController = FindObjectOfType<PauseController>();
      if (pauseController != null)
      {
         pauseController.CanChangeState = option;
      }
   }
}
