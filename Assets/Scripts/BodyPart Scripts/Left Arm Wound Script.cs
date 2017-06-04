using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmWoundScript : MonoBehaviour {

    private static int BodyPartBloodLoss;
    private static string woundType;
    private bool bandaged;

	// Use this for initialization
	void Start () {
		
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
    }

    public static void SetBodyPartBloodLoss(int bloodloss)
    {
        BodyPartBloodLoss = bloodloss;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        if (GameLogicScript.selectedTool.Equals("bandage"))
        {
            GameLogicScript.numberOfBandages--;
            bandaged = true;

            //Apply Bandage Model

            BloodBarScript.ModifyBloodLossRate(-10);
            BodyPartBloodLoss -= 10;
        }
        else if (GameLogicScript.selectedTool.Equals("desinfectant"))
        {
            GameLogicScript.numberOfDesinfectants--;

            // Change Model somehow? or not?

            BloodBarScript.ModifyBloodLossRate(-5);
            BodyPartBloodLoss -= 5;
            BloodBarScript.TakeDamage(10);
        }
        else if (GameLogicScript.selectedTool.Equals("scissors") && bandaged)
        {
            bandaged = false;
            BloodBarScript.ModifyBloodLossRate(10);
            BodyPartBloodLoss += 10;

            // Remove Bandage Model

        }
        else if (GameLogicScript.selectedTool.Equals("syringe"))
        {
            // meh ?
        }
        else if (GameLogicScript.selectedTool.Equals("bonesaw"))
        {
            BloodBarScript.TakeDamage(100);
            // Remove Limb?
        }
    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }
}
