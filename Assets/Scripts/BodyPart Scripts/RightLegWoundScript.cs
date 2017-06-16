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

    public static RightLegWoundScript instance;

    public GameObject healthyBodyPart;
    public GameObject woundedBodyPart;
    public GameObject stitchedBodyPart;

    private void Awake()
    {
        instance = this;

        healthyBodyPart.SetActive(true);
        // TODO: implement Bodyparts
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(BodyPartBloodLoss < 2)
        {
            // Change to healthy Model
        }
	}

    public static void SetWoundType(string wound)
    {
        woundType = wound;
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
            BloodBarScript.TakeDamage(50);
            DisplayFieldScript.Display("Oh Really?");
        }
        else if (GameLogicScript.selectedTool.Equals("needle") && !stitched && !bandaged)
        {
            BloodBarScript.TakeDamage(5);
            BloodBarScript.ModifyBloodLossRate(-20);
            BodyPartBloodLoss -= 20;
            stitched = true;
            DisplayFieldScript.Display("Wound stitched");

            // Change Model accordingly
        }

    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }
}
