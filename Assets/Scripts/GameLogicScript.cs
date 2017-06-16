using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour {

    private bool gameStarted = false;

    public GameObject startButton;
    public GameObject gameStage;
    public Text timerText;
    public static int numberOfBandages;
    public static int numberOfDesinfectants;

    float timer = 0.0f;

    public static string selectedTool;

    //private void Awake()
    //{
    //    selectedTool = " ";
    //}

    // Use this for initialization
    void Start () {
        gameStarted = false;
        selectedTool = " ";

        // Place Gamestage in Front of Playercamera - ! Currently NOT Working !
        gameStage.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 4;

    }

    // Update is called once per frame
    void Update () {
        if (gameStarted)
        {
            timer += Time.deltaTime;
            float minutes = (timer / 60);
            float seconds = timer - minutes * 60;
            timerText.text = seconds.ToString("0");
        }

        if(BloodBarScript.bloodLossRate < 5)
        {
            StopGame();
        }
        
    }

    public void StartGame()
    {
        if (gameStarted == false)
        {
            numberOfBandages = 10;
            numberOfDesinfectants = 10;
            gameStarted = true;
            startButton.SetActive(false);
            BloodBarScript.Reset();
            BloodBarScript.StartBloodLoss();

            WoundGeneratorScript.GenerateWound("Torso");
            WoundGeneratorScript.GenerateWound("Left Arm");
            WoundGeneratorScript.GenerateWound("Right Arm");
            WoundGeneratorScript.GenerateWound("Left Leg");
           // WoundGeneratorScript.GenerateWound("Right Leg");
        }
    }

    public void StopGame()
    {
        gameStarted = false;
        BloodBarScript.StopBloodLoss();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Main");
        selectedTool = " ";
        BloodBarScript.Reset();
    }
}
