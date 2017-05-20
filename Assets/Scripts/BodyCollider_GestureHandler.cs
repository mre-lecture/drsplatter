using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider_GestureHandler : MonoBehaviour {

    private bool isActive = false;
    private bool isRed = false;

        void Update()
        {

            if (isActive)
            {
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
                //this.transform.Rotate(0, 1, 0);
            }

        }

        void OnAirTapped()
        {
            isActive = !isActive;
        }

}
