
using UnityEngine;

public class MainMenu : MonoBehaviour
{

   public GameObject mainMenu;
   public GameObject levelMenu;
   public GameObject optionsMenu;

   private void Awake()
   {
      SetGameObjectState(mainMenu, true);
      SetGameObjectState(levelMenu, false);
      SetGameObjectState(optionsMenu, false);
   }
   public void StartGame()
   {
      SceneLoader.instance.Load (SceneLoader.SceneType.Cemetery);
      Debug.Log("Start Game");
   }
   public void StartMenu()
   {
      SceneLoader.instance.Load(SceneLoader.SceneType.MainMenu);
      Debug.Log("Main Menu");
   }

   public void QuitGame()
   {
      Debug.Log("Quit Game");
      Application.Quit();
      //UnityEditor.EditorApplication.isPlaying = false;
   }

   public void GoToCemetery()
   {
      SceneLoader.instance.Load(SceneLoader.SceneType.Cemetery);
   }

   public void GoToCouncilRoom()
   {
      SceneLoader.instance.Load(SceneLoader.SceneType.CouncilRoom);
   }

   public void GoToFirstFloor()
   {
      SceneLoader.instance.Load(SceneLoader.SceneType.FirstFloor);
   }

   public void GoToSecondFloor()
   {
      SceneLoader.instance.Load(SceneLoader.SceneType.SecondFloor);
   }

   public void GoToThirdFloor()
   {
      SceneLoader.instance.Load(SceneLoader.SceneType.ThirdFloor);
   }

   public void GoToThrone()
   {
      SceneLoader.instance.Load(SceneLoader.SceneType.Throne);
   }

   public void SceneSelectorMenu()
   {
      if(mainMenu && levelMenu)
      {
         SetGameObjectState(mainMenu, false);
         SetGameObjectState(levelMenu, true);
      }
   }
   public void ShowMainMenu()
   {
      if (mainMenu && levelMenu && optionsMenu)
      {
         //SetPauseController(true);
         SceneLoader.instance.AudioVolume = SceneLoader.instance.AudioVolumeSlider.value;
         SceneLoader.instance.EFXVolume = SceneLoader.instance.EFXVolumeSlider.value;
         SceneLoader.instance.CameraSensitivity = SceneLoader.instance.CameraSensitivitySlider.value;
         SetGameObjectState(mainMenu, true);
         SetGameObjectState(levelMenu, false);
         SetGameObjectState(optionsMenu, false);
      }
   }
   public void ShowOptionsMenu()
   {
      if (mainMenu && optionsMenu)
      {
         //SetPauseController(false);
         SceneLoader.instance.AudioVolumeSlider.value = SceneLoader.instance.AudioVolume;
         SceneLoader.instance.EFXVolumeSlider.value = SceneLoader.instance.EFXVolume;
         SceneLoader.instance.CameraSensitivitySlider.value = SceneLoader.instance.CameraSensitivity;
         SetGameObjectState(mainMenu, false);
         SetGameObjectState(levelMenu, false);
         SetGameObjectState(optionsMenu, true);
      }
   }
   void SetGameObjectState(GameObject obj, bool state)
   {
      if (obj)
      {
         obj.SetActive(state);
      }
   }

   //void SetPauseController(bool option)
   //{
   //   PauseController pauseController = FindObjectOfType<PauseController>();
   //   if(pauseController != null)
   //   {
   //      pauseController.CanChangeState = option;
   //   }
   //}
}
