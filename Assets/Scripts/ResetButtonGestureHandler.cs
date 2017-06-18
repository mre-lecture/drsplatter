using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonGestureHandler : MonoBehaviour, IInputClickHandler, IInputHandler
{
    public GameLogicScript gls;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        gls.ResetGame();
    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }
}
