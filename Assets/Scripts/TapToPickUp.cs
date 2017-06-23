using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPickUp : MonoBehaviour, IInputClickHandler {

    public static TapToPickUp instance;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (GameLogicScript.GetGameState())
        {
            // AirTap code goes here
            if (GameLogicScript.selectedTool.Equals(" "))
            {
                GameLogicScript.selectedTool = this.gameObject.name;
                this.gameObject.transform.position = Camera.main.transform.position;
            }
            else
            {
                GameObject selected = GameObject.Find(GameLogicScript.selectedTool);
                GameObject placer = GameObject.Find(GameLogicScript.selectedTool + " placer");
                selected.transform.position = placer.transform.position;

                GameLogicScript.selectedTool = this.gameObject.name;
                this.gameObject.transform.position = Camera.main.transform.position;
            }
        }  
    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }

    // Update is called once per frame
    void Update () {
		
	}

    public static void ResetTool()
    {
       
    }
}
