using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicScript : MonoBehaviour {

    private bool gameStarted = false;

    public GameObject startButton;
    public GameObject gameStage;

    public static string selectedTool;

	// Use this for initialization
	void Start () {
        gameStarted = false;
        selectedTool = " ";

        // Place Gamestage in Front of Playercamera - ! Currently NOT Working !
        gameStage.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        gameStarted = true;
        startButton.SetActive(false);
        BloodBarScript.Reset();
        BloodBarScript.StartBloodLoss();
    }

    public void StopGame()
    {
        gameStarted = false;

        BloodBarScript.StopBloodLoss();
    }
}
