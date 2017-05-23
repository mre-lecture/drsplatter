using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton_GestureHandler : MonoBehaviour, IInputClickHandler, IInputHandler
{
    public GameLogicScript gls;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        gls.StartGame();
    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }
}
