using HoloToolkit.Unity;
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
    public static float OverallBloodloss;

    public static int bandageHealLarge;
    public static int bandageHealSmall;
    public static int stitchingHeal;
    public static int anestheticsHeal;
    public static int desinfectantHeal;
    public static int scissorsEffect;

    public static GameLogicScript instance;

    private bool presentationModeEnabled = true;
    private int bloodLossLimit;
    private int endGameBloodloss;

    public AudioSource heartBeatMonitorSound;
    public AudioSource heartBeatMonitorFlatlineSound;

    public GameObject audioManager;
    private TextToSpeechManager textToSpeech;
    private bool ttsEnabled = true;

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

        hasWound = false;
        OverallBloodloss = 0;

        // Place Gamestage in Front of Playercamera - ! Currently NOT Working !
        gameStage.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 4;
    }

    // Use this for initialization
    void Start()
    {
        textToSpeech = audioManager.GetComponent<HoloToolkit.Unity.TextToSpeechManager>();
        textToSpeech.Voice = TextToSpeechVoice.Zira;

        textToSpeech.SpeakText("Welcome to Doctor Splatter!");
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
            if (presentationModeEnabled)
            {
                bandageHealLarge = 3;
                bandageHealSmall = 2;
                stitchingHeal = 6;
                anestheticsHeal = 1;
                desinfectantHeal = 2;
                scissorsEffect = 5;

                bloodLossLimit = 16;

                WoundGeneratorScript.SetWoundValues(8, 3, 5);
            }
            else
            {
                bandageHealLarge = 10;
                bandageHealSmall = 5;
                stitchingHeal = 20;
                anestheticsHeal = 2;
                desinfectantHeal = 5;
                scissorsEffect = 10;

                bloodLossLimit = 50;

                WoundGeneratorScript.SetWoundValues(25, 10, 15);
            }

            print("Current Values: " + bandageHealLarge + " , " + bandageHealSmall + " , " + stitchingHeal + " , " + anestheticsHeal + " , " + desinfectantHeal + " , " + scissorsEffect);

            counterBackground.SetActive(true);
            textBackground.SetActive(true);
            timerBackground.SetActive(true);
            bandageCount.text = "10";
            desinfectantCount.text = "10";
            numberOfBandages = 10;
            numberOfDesinfectants = 10;
            hasWound = false;
            OverallBloodloss = 0;

            gameStarted = true;
            startButton.SetActive(false);
            BloodBarScript.Reset();
            BloodBarScript.StartBloodLoss();

            bool torsoWounded = false;
            bool leftArmWounded = false;
            bool rightArmWounded = false;
            bool leftLegWounded = false;
            bool rightLegWounded = false;

            while (OverallBloodloss < bloodLossLimit)
            {
                int bodyPart = Random.Range(0, 5);

                if (OverallBloodloss < bloodLossLimit && !torsoWounded && bodyPart == 0)
                {
                    hasWound = false;
                    WoundGeneratorScript.GenerateWound("Torso");
                    if (hasWound)
                    {
                        torsoWounded = true;
                    }
                }
                bodyPart = Random.Range(0, 5);

                if (OverallBloodloss < bloodLossLimit && !leftArmWounded && bodyPart == 1)
                {
                    hasWound = false;
                    WoundGeneratorScript.GenerateWound("Left Arm");
                    if (hasWound)
                    {
                        leftArmWounded = true;
                    }
                }
                bodyPart = Random.Range(0, 5);

                if (OverallBloodloss < bloodLossLimit && !rightArmWounded && bodyPart == 2)
                {
                    hasWound = false;
                    WoundGeneratorScript.GenerateWound("Right Arm");
                    if (hasWound)
                    {
                        rightArmWounded = true;
                    }
                }
                bodyPart = Random.Range(0, 5);

                if (OverallBloodloss < bloodLossLimit && !leftLegWounded && bodyPart == 3)
                {
                    hasWound = false;
                    WoundGeneratorScript.GenerateWound("Left Leg");
                    if (hasWound)
                    {
                        leftLegWounded = true;
                    }
                }
                bodyPart = Random.Range(0, 5);

                if (OverallBloodloss < bloodLossLimit && !rightLegWounded && bodyPart == 4)
                {
                    hasWound = false;
                    WoundGeneratorScript.GenerateWound("Right Leg");
                    if (hasWound)
                    {
                        rightLegWounded = true;
                    }
                }
            }

            print("Bloodloss added: " + BloodBarScript.bloodLossRate);

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
            Invoke("HeartMonitorFlatline", 0f);
        }
        Invoke("EnableResetButton", 5f);

        if (!won)
        {
            GrimReaperScript.SetVisibility(true);
        }
    }

    void EnableResetButton()
    {
        resetButton.SetActive(true);
        textBackground.SetActive(false);
    }

    public void ResetGame()
    {
        TapToPutDown.CallPutDown();
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
        hasWound = false;
        OverallBloodloss = 0;
        numberOfBandages = 10;
        numberOfDesinfectants = 10;



        // SceneManager.LoadScene("Main");
    }

    public static void UseBandage()
    {
        numberOfBandages--;
        instance.bandageCount.text = numberOfBandages.ToString();
        if (numberOfBandages < 1)
        {
            instance.bandagesObject.SetActive(false);
        }
    }

    public static void UseDesinfectant()
    {
        numberOfDesinfectants--;
        instance.desinfectantCount.text = numberOfDesinfectants.ToString();
        if (numberOfDesinfectants < 1)
        {
            instance.desinfectantObject.SetActive(false);
        }
    }

    void HeartMonitor()
    {
        float ratio = (BloodBarScript.bloodLevel / BloodBarScript.maxBloodLevel);

        if (ratio < 0.08f)
        {
            ratio = 0.08f;
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

    public static void CallStopGame()
    {
        instance.StopGame(true);
    }

    // Easy Difficutly / Presentatio Mode Difficutly
    public void SetPresentationMode()
    {
        presentationModeEnabled = true;

        DisplayFieldScript.Display("Presentation Mode Activated");
        CallTextToSpeechOutput("Presentation Mode Activated");
    }

    // Normal Difficulty
    public void SetNormalMode()
    {
        presentationModeEnabled = false;

        DisplayFieldScript.Display("Presentation Mode Deactivated");
        CallTextToSpeechOutput("Presentation Mode Deactivated");
    }

    public static void CallTextToSpeechOutput(string message)
    {
        instance.textToSpeech.SpeakText(message);
    }

    public void ToggleTextToSpeech()
    {
        if (ttsEnabled)
        {
            textToSpeech.SpeakText("Muting Text to Speech");
            textToSpeech.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            textToSpeech.GetComponent<AudioSource>().mute = false;
            textToSpeech.SpeakText("Text to Speech unmuted");
        }
    }
}
