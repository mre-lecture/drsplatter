using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider_GestureHandler : MonoBehaviour, IInputClickHandler, IInputHandler
{

    private bool isActive = false;
    private bool isRed = false;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        if (!isRed)
        {
            Renderer rend = this.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.red);
            isRed = !isRed;
        }
        else
        {
            Renderer rend = this.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.white);
            isRed = !isRed;
        }
}

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }

    void Update()
        {

        }
}
