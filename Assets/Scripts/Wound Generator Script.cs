using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundGeneratorScript : MonoBehaviour {

    static string bodyPartName;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void GenerateWound(GameObject bodyPart)
    {
        bodyPartName = bodyPart.name;
        int woundIndex = Random.Range(0, 3);
        if(woundIndex == 0)
        {
            // do nothing
        }
        else if (woundIndex == 1)
        {
            AddLargeCut();
        }
        else if(woundIndex == 2)
        {
            AddSmallCuts();
     
        }
        else if(woundIndex == 3)
        {
            AddBurns();
        }
    }

    // Check what bodypart was given and add the wound and bloodloss to said bodypart
    private static void AddLargeCut()
    {
        if (bodyPartName.Contains("Torso")){
            TorsoWoundScript.SetWoundType("LargeCut");
            TorsoWoundScript.SetBodyPartBloodLoss(20);
            BloodBarScript.ModifyBloodLossRate(20);
        }
        else if (bodyPartName.Contains("Head"))
        {
            HeadWoundScript.SetWoundType("LargeCut");
            HeadWoundScript.SetBodyPartBloodLoss(20);
            BloodBarScript.ModifyBloodLossRate(20);
        }
        else if (bodyPartName.Contains("Left Arm"))
        {
            LeftArmWoundScript.SetWoundType("LargeCut");
            LeftArmWoundScript.SetBodyPartBloodLoss(20);
            BloodBarScript.ModifyBloodLossRate(20);
        }
        else if (bodyPartName.Contains("Left Leg"))
        {
            LeftLegWoundScript.SetWoundType("LargeCut");
            LeftLegWoundScript.SetBodyPartBloodLoss(20);
            BloodBarScript.ModifyBloodLossRate(20);
        }
        else if (bodyPartName.Contains("Right Arm"))
        {
            RightArmWoundScript.SetWoundType("LargeCut");
            RightArmWoundScript.SetBodyPartBloodLoss(20);
            BloodBarScript.ModifyBloodLossRate(20);
        }
        else if (bodyPartName.Contains("Right Leg"))
        {
            RightLegWoundScript.SetWoundType("LargeCut");
            RightLegWoundScript.SetBodyPartBloodLoss(20);
            BloodBarScript.ModifyBloodLossRate(20);
        }

    }

    // Check what bodypart was given and add the wound and bloodloss to said bodypart
    private static void AddSmallCuts()
    {
        if (bodyPartName.Contains("Torso"))
        {
            TorsoWoundScript.SetWoundType("SmallCuts");
            TorsoWoundScript.SetBodyPartBloodLoss(10);
            BloodBarScript.ModifyBloodLossRate(10);
        }
        else if (bodyPartName.Contains("Head"))
        {
            HeadWoundScript.SetWoundType("SmallCuts");
            HeadWoundScript.SetBodyPartBloodLoss(10);
            BloodBarScript.ModifyBloodLossRate(10);
        }
        else if (bodyPartName.Contains("Left Arm"))
        {
            LeftArmWoundScript.SetWoundType("SmallCuts");
            LeftArmWoundScript.SetBodyPartBloodLoss(10);
            BloodBarScript.ModifyBloodLossRate(10);
        }
        else if (bodyPartName.Contains("Left Leg"))
        {
            LeftLegWoundScript.SetWoundType("SmallCuts");
            LeftLegWoundScript.SetBodyPartBloodLoss(10);
            BloodBarScript.ModifyBloodLossRate(10);
        }
        else if (bodyPartName.Contains("Right Arm"))
        {
            RightArmWoundScript.SetWoundType("SmallCuts");
            RightArmWoundScript.SetBodyPartBloodLoss(10);
            BloodBarScript.ModifyBloodLossRate(10);
        }
        else if (bodyPartName.Contains("Right Leg"))
        {
            RightLegWoundScript.SetWoundType("SmallCuts");
            RightLegWoundScript.SetBodyPartBloodLoss(10);
            BloodBarScript.ModifyBloodLossRate(10);
        }
    }

    // Check what bodypart was given and add the wound and bloodloss to said bodypart
    private static void AddBurns()
    {
        if (bodyPartName.Contains("Torso"))
        {
            TorsoWoundScript.SetWoundType("Burns");
            TorsoWoundScript.SetBodyPartBloodLoss(5);
            BloodBarScript.ModifyBloodLossRate(5);
        }
        else if (bodyPartName.Contains("Head"))
        {
            HeadWoundScript.SetWoundType("Burns");
            HeadWoundScript.SetBodyPartBloodLoss(5);
            BloodBarScript.ModifyBloodLossRate(5);
        }
        else if (bodyPartName.Contains("Left Arm"))
        {
            LeftArmWoundScript.SetWoundType("Burns");
            LeftArmWoundScript.SetBodyPartBloodLoss(5);
            BloodBarScript.ModifyBloodLossRate(5);
        }
        else if (bodyPartName.Contains("Left Leg"))
        {
            LeftLegWoundScript.SetWoundType("Burns");
            LeftLegWoundScript.SetBodyPartBloodLoss(5);
            BloodBarScript.ModifyBloodLossRate(5);
        }
        else if (bodyPartName.Contains("Right Arm"))
        {
            RightArmWoundScript.SetWoundType("Burns");
            RightArmWoundScript.SetBodyPartBloodLoss(5);
            BloodBarScript.ModifyBloodLossRate(5);
        }
        else if (bodyPartName.Contains("Right Leg"))
        {
            RightLegWoundScript.SetWoundType("Burns");
            RightLegWoundScript.SetBodyPartBloodLoss(5);
            BloodBarScript.ModifyBloodLossRate(5);
        }
    }
}
