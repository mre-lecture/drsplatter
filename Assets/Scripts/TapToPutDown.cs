using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPutDown : MonoBehaviour, IInputClickHandler
{

	// Use this for initialization
	void Start () {
		
	}


    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        GameObject selected = GameObject.Find(GameLogicScript.selectedTool);
        GameObject placer = GameObject.Find(GameLogicScript.selectedTool + " placer");
        selected.transform.position = placer.transform.position;

        GameLogicScript.selectedTool = " ";
    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }

    // Update is called once per frame
    void Update () {
		
	}
}
