using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider_GestureHandler : MonoBehaviour, IInputClickHandler, IInputHandler
{
    private bool isRed = false;
    public BloodBarScript bbs;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        bbs.TakeDamage(20);
        Invoke("ChangeColor", 0f);

    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }

    private void ChangeColor()
    {
        if (!isRed)
        {
            Renderer rend = this.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.red);
            isRed = !isRed;
            bbs.TakeDamage(20);
        }
        else
        {
            Renderer rend = this.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.white);
            isRed = !isRed;
        }
    }

    void Update()
    {

    }
}