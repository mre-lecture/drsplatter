using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLegWoundScript : MonoBehaviour, IInputClickHandler
{

    private static int BodyPartBloodLoss;
    private static string woundType;
    private bool bandaged = false;
    private bool desinfected = false;
    private bool anesthetized = false;
    private bool stitched = false;
    private bool pipeRemoved = false;

    public static RightLegWoundScript instance;

    public GameObject healthyBodyPart;
    public GameObject bandagedBodyPart;
    /*
    public GameObject woundedBodyPart;
    public GameObject woundedBodyPartWithPipe;
    public GameObject stitchedBodyPart;

    public GameObject blood;
    */

    // Audio Sources
    public AudioSource scissorsSound;
    public AudioSource bandageSound;
    public AudioSource anestheticsSound;
    public AudioSource desinfectantSound;
    public AudioSource stitchingSound;
    public AudioSource scalpelSound;

    private void Awake()
    {
        instance = this;

        healthyBodyPart.SetActive(true);
        /*
        woundedBodyPart.SetActive(false);
        woundedBodyPartWithPipe.SetActive(false);
        stitchedBodyPart.SetActive(false);
        bandagedBodyPart.SetActive(false);
        blood.SetActive(false);
        */
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void SetWoundType(string wound)
    {
        woundType = wound;
        if (wound.Equals("LargeCut"))
        {
            instance.healthyBodyPart.SetActive(false);
            /*
            instance.woundedBodyPart.SetActive(true);
            instance.woundedBodyPartWithPipe.SetActive(true);
            instance.blood.SetActive(true);
            */
        }
        else
        {
            instance.healthyBodyPart.SetActive(false);
            /*
            instance.woundedBodyPart.SetActive(true);
            instance.blood.SetActive(true);
            */
        }

    }

    public static void SetBodyPartBloodLoss(int bloodloss)
    {
        BodyPartBloodLoss = bloodloss;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {

        if (GameLogicScript.GetGameState())
        {
            // AirTap code goes here
            if (GameLogicScript.selectedTool.Equals("bandage") && !bandaged)
            {
                GameLogicScript.UseBandage();
                bandaged = true;

                //Apply Bandage Model
                bandagedBodyPart.SetActive(true);
                /*
                if (woundType.Contains("Large"))
                {
                    BloodBarScript.ModifyBloodLossRate(-10);
                    BodyPartBloodLoss -= 10;
                }
                else
                {
                    BloodBarScript.ModifyBloodLossRate(-5);
                    BodyPartBloodLoss -= 5;
                }
                */

                bandageSound.Play();

                DisplayFieldScript.Display("Bandages applied");
            }
            else if (GameLogicScript.selectedTool.Equals("desinfectant") && !desinfected)
            {
                GameLogicScript.UseDesinfectant();
                desinfected = true;

                // Change Model somehow? or not?
                /*
                BloodBarScript.ModifyBloodLossRate(-5);
                BodyPartBloodLoss -= 5;
                BloodBarScript.TakeDamage(20);
                */
                desinfectantSound.Play();

                DisplayFieldScript.Display("Desinfectant applied");
            }
            else if (GameLogicScript.selectedTool.Equals("scissors") && bandaged)
            {
                bandaged = false;
                /*
                BloodBarScript.ModifyBloodLossRate(10);
                BodyPartBloodLoss += 10;
                */

                // Remove Bandage Model
                bandagedBodyPart.SetActive(false);
                /*
                instance.blood.SetActive(true);
                */
                scissorsSound.Play();

                DisplayFieldScript.Display("Bandages removed");
            }
            else if (GameLogicScript.selectedTool.Equals("syringe") && !anesthetized)
            {
                BloodBarScript.TakeDamage(10);
                /*
                BloodBarScript.ModifyBloodLossRate(-2);
                BodyPartBloodLoss -= 2;
                
                anesthetized = true;
                */
                anestheticsSound.Play();

                DisplayFieldScript.Display("Anesthetics applied");
            }
            else if (GameLogicScript.selectedTool.Equals("bonesaw"))
            {
                BloodBarScript.TakeDamage(600);
                DisplayFieldScript.Display("Oh Really?");

                // remove all models for the left leg
                instance.healthyBodyPart.SetActive(false);
                /*
                instance.woundedBodyPart.SetActive(false);
                instance.woundedBodyPartWithPipe.SetActive(false);
                instance.stitchedBodyPart.SetActive(false);
                */
            }
            else if (GameLogicScript.selectedTool.Equals("needle") && !stitched && pipeRemoved && !bandaged)
            {
                BloodBarScript.TakeDamage(15);
                /*
                BloodBarScript.ModifyBloodLossRate(-20);
                BodyPartBloodLoss -= 20;
                stitched = true;
                */
                stitchingSound.Play();

                DisplayFieldScript.Display("Wound stitched");
                /*
                // change model from wounded to stitched and hide blood
                instance.woundedBodyPart.SetActive(false);
                instance.stitchedBodyPart.SetActive(true);
                instance.blood.SetActive(false);
                */
            }
            else if (GameLogicScript.selectedTool.Equals("scalpel") && !pipeRemoved)
            {
                /*
                BloodBarScript.TakeDamage(20);
                BloodBarScript.ModifyBloodLossRate(+10);
                pipeRemoved = true;

                scalpelSound.Play();
                */

            }
        }
    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }

    public static void ResetBodyPart()
    {
        BodyPartBloodLoss = 0;
        woundType = " ";
        instance.bandaged = false;
        instance.desinfected = false;
        instance.anesthetized = false;
        instance.stitched = false;
        instance.pipeRemoved = false;
        instance.healthyBodyPart.SetActive(true);
        instance.bandagedBodyPart.SetActive(false);
        /*
        instance.woundedBodyPart.SetActive(false);
        instance.woundedBodyPartWithPipe.SetActive(false);
        instance.stitchedBodyPart.SetActive(false);
        
        instance.blood.SetActive(false);
        */
    }
}
