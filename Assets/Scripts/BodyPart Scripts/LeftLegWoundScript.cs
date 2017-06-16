using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLegWoundScript : MonoBehaviour, IInputClickHandler
{

    private static int BodyPartBloodLoss;
    private static string woundType;
    private bool bandaged = false;
    private bool desinfected = false;
    private bool anesthetized = false;
    private bool stitched = false;
    private bool pipeRemoved = false;

    public static LeftLegWoundScript instance;

    public GameObject healthyBodyPart;
    public GameObject woundedBodyPart;
    public GameObject woundedBodyPartWithPipe;
    public GameObject stitchedBodyPart;
    public GameObject bandagedBodyPart;

    public GameObject blood;

    private void Awake()
    {
        instance = this;

        healthyBodyPart.SetActive(true);
        woundedBodyPart.SetActive(false);
        woundedBodyPartWithPipe.SetActive(false);
        stitchedBodyPart.SetActive(false);
        bandagedBodyPart.SetActive(false);
        blood.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (BodyPartBloodLoss < 2)
        {
            // Change to healthy Model
        }
    }

    public static void SetWoundType(string wound)
    {
        woundType = wound;
        if (wound.Equals("LargeCut"))
        {
            instance.healthyBodyPart.SetActive(false);
            instance.woundedBodyPart.SetActive(true);
            instance.woundedBodyPartWithPipe.SetActive(true);
            instance.blood.SetActive(true);
        }
        else
        {
            instance.healthyBodyPart.SetActive(false);
            instance.woundedBodyPart.SetActive(true);
            instance.blood.SetActive(true);
        }
        
    }

    public static void SetBodyPartBloodLoss(int bloodloss)
    {
        BodyPartBloodLoss = bloodloss;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        if (GameLogicScript.selectedTool.Equals("bandage") && !bandaged)
        {
            GameLogicScript.numberOfBandages--;
            bandaged = true;

            //Apply Bandage Model
            bandagedBodyPart.SetActive(true);
            instance.blood.SetActive(false);

            BloodBarScript.ModifyBloodLossRate(-10);
            BodyPartBloodLoss -= 10;
            DisplayFieldScript.Display("Bandages applied");
        }
        else if (GameLogicScript.selectedTool.Equals("desinfectant") && !desinfected)
        {
            GameLogicScript.numberOfDesinfectants--;
            desinfected = true;

            // Change Model somehow? or not?

            BloodBarScript.ModifyBloodLossRate(-5);
            BodyPartBloodLoss -= 5;
            BloodBarScript.TakeDamage(20);
            DisplayFieldScript.Display("Desinfectant applied");
        }
        else if (GameLogicScript.selectedTool.Equals("scissors") && bandaged)
        {
            bandaged = false;
            BloodBarScript.ModifyBloodLossRate(10);
            BodyPartBloodLoss += 10;

            // Remove Bandage Model
            bandagedBodyPart.SetActive(false);
            instance.blood.SetActive(true);

            DisplayFieldScript.Display("Bandages removed");
        }
        else if (GameLogicScript.selectedTool.Equals("syringe") && !anesthetized)
        {
            BloodBarScript.ModifyBloodLossRate(-2);
            BodyPartBloodLoss -= 2;
            BloodBarScript.TakeDamage(10);
            anesthetized = true;
            DisplayFieldScript.Display("Anesthetics applied");
        }
        else if (GameLogicScript.selectedTool.Equals("bonesaw"))
        {
            BloodBarScript.TakeDamage(BloodBarScript.maxBloodLevel/5);
            DisplayFieldScript.Display("Oh Really?");

            // remove all models for the left leg
            instance.healthyBodyPart.SetActive(false);
            instance.woundedBodyPart.SetActive(false);
            instance.woundedBodyPartWithPipe.SetActive(false);
            instance.stitchedBodyPart.SetActive(false);
        }
        else if (GameLogicScript.selectedTool.Equals("needle") && !stitched && pipeRemoved && !bandaged)
        {
            BloodBarScript.TakeDamage(5);
            BloodBarScript.ModifyBloodLossRate(-20);
            BodyPartBloodLoss -= 20;
            stitched = true;
            DisplayFieldScript.Display("Wound stitched");

            // change model from wounded to stitched and hide blood
            instance.woundedBodyPart.SetActive(false);
            instance.stitchedBodyPart.SetActive(true);
            instance.blood.SetActive(false);
        }
        else if(GameLogicScript.selectedTool.Equals("scalpel") && !pipeRemoved)
        {
            BloodBarScript.TakeDamage(20);
            BloodBarScript.ModifyBloodLossRate(+10);
            pipeRemoved = true;
            DisplayFieldScript.Display("Foreign Body removed");

            // hide pipe model
            instance.woundedBodyPartWithPipe.SetActive(false);
        }

    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }
}
