using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicScript : MonoBehaviour {

    private bool gameStarted = false;
    public BloodBarScript bloodBarScript;

    public GameObject startButton;
    public GameObject gameStage;

	// Use this for initialization
	void Start () {
        gameStarted = false;

        gameStage.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        gameStarted = true;
        startButton.SetActive(false);
        bloodBarScript.Reset();
        bloodBarScript.StartBloodLoss();
    }

    public void StopGame()
    {
        gameStarted = false;

        bloodBarScript.StopBloodLoss();
    }
}
