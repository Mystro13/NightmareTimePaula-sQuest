using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
   public GameObject pauseMenuUI;
   void Awake()
   {
      if (pauseMenuUI)
      {
         pauseMenuUI.SetActive(false);
      }
      GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
      OnGameStateChanged(GameStateManager.Instance.CurrentGameState);
   }

   void OnDestroy()
   {
      GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
   }

   private void OnGameStateChanged(GameState newGameState)
   {
      Debug.Log($"Pause Menu {newGameState}");
      if (pauseMenuUI)
      {
         pauseMenuUI.SetActive(newGameState == GameState.Paused);
      }
   }
}

