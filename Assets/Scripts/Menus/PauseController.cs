using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseController : MonoBehaviour
{
   public bool CanChangeState;
   private void Awake()
   {
      CanChangeState = true;
   }
   void Update()
   {
      Keyboard keyboard = Keyboard.current;
      if (keyboard.backspaceKey.wasPressedThisFrame && CanChangeState)
      {
         GameState currentGameState = GameStateManager.Instance.CurrentGameState;
         GameState newGameState = currentGameState == GameState.GamePlay
         ? GameState.Paused : GameState.GamePlay;

         GameStateManager.Instance.SetState(newGameState);
      }
   }
}

