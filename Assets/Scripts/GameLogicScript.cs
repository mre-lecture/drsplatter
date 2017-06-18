using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour
{

    private static bool gameStarted = false;

    public GameObject startButton;
    public GameObject resetButton;
    public GameObject gameStage;
    private float internalTimer = 0;
    private static int numberOfBandages;
    private static int numberOfDesinfectants;
    public Text bandageCount;
    public Text desinfectantCount;
    public GameObject counterBackground;
    public GameObject timerBackground;
    public GameObject textBackground;
    public Text uiTimer;
    public GameObject bandagesObject;
    public GameObject desinfectantObject;
    public static bool hasWound;

    public static GameLogicScript instance;

    public AudioSource heartBeatMonitorSound;
    public AudioSource heartBeatMonitorFlatlineSound;

    public static string selectedTool;

    private void Awake()
    {
        instance = this;

        gameStarted = false;
        selectedTool = " ";

        resetButton.SetActive(false);

        counterBackground.SetActive(false);
        textBackground.SetActive(false);
        timerBackground.SetActive(false);

        bandagesObject.SetActive(true);
        desinfectantObject.SetActive(true);

        GrimReaperScript.SetVisibility(false);

        // Place Gamestage in Front of Playercamera - ! Currently NOT Working !
        gameStage.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 4;
    }

    // Use this for initialization
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (BloodBarScript.bloodLossRate < 5)
            {
                // give true , player has won
                StopGame(true);
            }
            if (BloodBarScript.bloodLevel <= 0)
            {
                // give false , player has lost
                StopGame(false);
            }

        }
    }

    public void StartGame()
    {
        if (gameStarted == false)
        {
            counterBackground.SetActive(true);
            textBackground.SetActive(true);
            timerBackground.SetActive(true);
            bandageCount.text = "10";
            desinfectantCount.text = "10";
            numberOfBandages = 10;
            numberOfDesinfectants = 10;

            gameStarted = true;
            startButton.SetActive(false);
            BloodBarScript.Reset();
            BloodBarScript.StartBloodLoss();

            while (!hasWound)
            {
                WoundGeneratorScript.GenerateWound("Torso");
                WoundGeneratorScript.GenerateWound("Left Arm");
                WoundGeneratorScript.GenerateWound("Right Arm");
                WoundGeneratorScript.GenerateWound("Left Leg");
                // WoundGeneratorScript.GenerateWound("Right Leg");
            }

            Invoke("HeartMonitor", 1f);
            InvokeRepeating("IncrementTimer", 0f, 0.1f);
        }
    }

    public void StopGame(bool won)
    {
        gameStarted = false;
        BloodBarScript.StopBloodLoss();

        CancelInvoke("IncrementTimer");

        if (won)
        {
            uiTimer.text = "Your Time: " + internalTimer.ToString("0.0");
        
            DisplayFieldScript.Display("Patient saved!");
        }
        else
        {
            uiTimer.text = "Patient died!";
            Invoke("HeartMonitorFlatline",0f);
        }
        Invoke("EnableResetButton", 5f);
        GrimReaperScript.SetVisibility(true);
    }

    void EnableResetButton()
    {
        resetButton.SetActive(true);
        textBackground.SetActive(false);
    }

    public void ResetGame()
    {
        selectedTool = " ";
        internalTimer = 0;
        uiTimer.text = " ";
        BloodBarScript.Reset();
        LeftLegWoundScript.ResetBodyPart();
        LeftArmWoundScript.ResetBodyPart();
        RightArmWoundScript.ResetBodyPart();
        RightLegWoundScript.ResetBodyPart();
        TorsoWoundScript.ResetBodyPart();
        counterBackground.SetActive(false);
        textBackground.SetActive(false);
        timerBackground.SetActive(false);
        bandagesObject.SetActive(true);
        desinfectantObject.SetActive(true);
        resetButton.SetActive(false);
        startButton.SetActive(true);
        GrimReaperScript.SetVisibility(false);

        SceneManager.LoadScene("Main");
    }

    public static void UseBandage()
    {
        numberOfBandages--;
        instance.bandageCount.text = numberOfBandages.ToString();
        if(numberOfBandages < 1)
        {
            instance.bandagesObject.SetActive(false);
        }
    }

    public static void UseDesinfectant()
    {
        numberOfDesinfectants--;
        instance.desinfectantCount.text = numberOfDesinfectants.ToString();
        if(numberOfDesinfectants < 1)
        {
            instance.desinfectantObject.SetActive(false);
        }
    }

    void HeartMonitor() {
        float ratio = (BloodBarScript.bloodLevel / BloodBarScript.maxBloodLevel);

        if (ratio < 0.8f)
        {
            ratio = 0.8f;
        }
        
        heartBeatMonitorSound.Play();

        if (gameStarted)
        {
                Invoke("HeartMonitor", ratio * 3);
        }
    }

    void HeartMonitorFlatline()
    {
        heartBeatMonitorFlatlineSound.Play();
    }

    private void IncrementTimer()
    {
        if (gameStarted)
        {
            internalTimer = internalTimer + 0.1f;
            uiTimer.text = internalTimer.ToString("0.0");
        }
    }

    public static bool GetGameState()
    {
        return gameStarted;
    }

    public static void CallReset()
    {
        instance.ResetGame();
    }
}
