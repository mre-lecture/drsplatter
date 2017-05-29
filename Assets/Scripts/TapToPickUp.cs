using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        if(GameLogicScript.selectedTool.Equals(" "))
        {
            GameLogicScript.selectedTool = this.gameObject.name;
            this.gameObject.transform.parent = Camera.main.transform;
        }
        else
        {
            GameObject selected = GameObject.Find(GameLogicScript.selectedTool);
            GameObject placer = GameObject.Find(GameLogicScript.selectedTool + " placer");
            selected.transform.parent = placer.transform;

            GameLogicScript.selectedTool = this.gameObject.name;
            this.gameObject.transform.parent = Camera.main.transform;
        }
        
    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }

    // Update is called once per frame
    void Update () {
		
	}
}
