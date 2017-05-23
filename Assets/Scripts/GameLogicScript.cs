using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicScript : MonoBehaviour {

    private bool gameStarted = false;
    public BloodBarScript bloodBarScript;

    public GameObject startButton;

	// Use this for initialization
	void Start () {
        gameStarted = false;
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
