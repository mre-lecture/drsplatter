using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundGeneratorScript : MonoBehaviour {

    static string bodyPartName;
    private int largeCut = 25;
    private int smallCut = 10;
    private int burns = 15;

    public static WoundGeneratorScript instance;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void GenerateWound(string bodyPart)
    {
        bodyPartName = bodyPart;
        int woundIndex = Random.Range(0, 2);
        if(woundIndex == 0)
        {
            // do nothing
            print("no wound added on " + bodyPartName);
        }
        else if (woundIndex == 1)
        {
            AddLargeCut();
            GameLogicScript.hasWound = true;
        }
        else if(woundIndex == 2)
        {
            AddSmallCuts();
            GameLogicScript.hasWound = true;
        }
        /*
         * 
         * else if(woundIndex == 3)
         * {
         *     AddBurns();
         * }
         *
         */
    }

    // Check what bodypart was given and add the wound and bloodloss to said bodypart
    private static void AddLargeCut()
    {
        if (bodyPartName.Contains("Torso")){
            // adds two cuts
            TorsoWoundScript.SetWoundType("LargeCut");
            TorsoWoundScript.SetBodyPartBloodLoss(instance.largeCut);
            BloodBarScript.ModifyBloodLossRate(instance.largeCut);
            print("Bloddloss=" + BloodBarScript.bloodLossRate+", LargeCut on Torso");
        }
        else if (bodyPartName.Contains("Left Arm"))
        {
            LeftArmWoundScript.SetWoundType("LargeCut");
            LeftArmWoundScript.SetBodyPartBloodLoss(instance.largeCut);
            BloodBarScript.ModifyBloodLossRate(instance.largeCut);
            print("Bloddloss=" + BloodBarScript.bloodLossRate + ", LargeCut on Left Arm");
        }
        else if (bodyPartName.Contains("Left Leg"))
        {
            // adds a pipe stuck in the leg
            LeftLegWoundScript.SetWoundType("LargeCut");
            LeftLegWoundScript.SetBodyPartBloodLoss(instance.largeCut);
            BloodBarScript.ModifyBloodLossRate(instance.largeCut);
            print("Bloddloss=" + BloodBarScript.bloodLossRate + ", Large Cut / Pipe stuck in the Left Leg");
        }
        else if (bodyPartName.Contains("Right Arm"))
        {
            RightArmWoundScript.SetWoundType("LargeCut");
            RightArmWoundScript.SetBodyPartBloodLoss(instance.largeCut);
            BloodBarScript.ModifyBloodLossRate(instance.largeCut);
            print("Bloddloss=" + BloodBarScript.bloodLossRate + ", LargeCut on Right Arm");
        }
        else if (bodyPartName.Contains("Right Leg"))
        {
            RightLegWoundScript.SetWoundType("LargeCut");
            RightLegWoundScript.SetBodyPartBloodLoss(instance.largeCut);
            BloodBarScript.ModifyBloodLossRate(instance.largeCut);
            print("Bloddloss=" + BloodBarScript.bloodLossRate + ", LargeCut on Right Leg");
        }

    }

    // Check what bodypart was given and add the wound and bloodloss to said bodypart
    private static void AddSmallCuts()
    {
        if (bodyPartName.Contains("Torso"))
        {
            TorsoWoundScript.SetWoundType("SmallCuts");
            TorsoWoundScript.SetBodyPartBloodLoss(instance.smallCut);
            BloodBarScript.ModifyBloodLossRate(instance.smallCut);
            print("Bloddloss=" + BloodBarScript.bloodLossRate + ", Small cut on Torso");
        }
        else if (bodyPartName.Contains("Left Arm"))
        {
            LeftArmWoundScript.SetWoundType("SmallCuts");
            LeftArmWoundScript.SetBodyPartBloodLoss(instance.smallCut);
            BloodBarScript.ModifyBloodLossRate(instance.smallCut);
            print("Bloddloss=" + BloodBarScript.bloodLossRate + ", Small cut on Left Arm");
        }
        else if (bodyPartName.Contains("Left Leg"))
        {
            LeftLegWoundScript.SetWoundType("SmallCuts");
            LeftLegWoundScript.SetBodyPartBloodLoss(instance.smallCut);
            BloodBarScript.ModifyBloodLossRate(instance.smallCut);
            print("Bloddloss=" + BloodBarScript.bloodLossRate + ", Small cut on Left Leg");
        }
        else if (bodyPartName.Contains("Right Arm"))
        {
            RightArmWoundScript.SetWoundType("SmallCuts");
            RightArmWoundScript.SetBodyPartBloodLoss(instance.smallCut);
            BloodBarScript.ModifyBloodLossRate(instance.smallCut);
            print("Bloddloss=" + BloodBarScript.bloodLossRate + ", Small cut on Right Arm");
        }
        else if (bodyPartName.Contains("Right Leg"))
        {
            RightLegWoundScript.SetWoundType("SmallCuts");
            RightLegWoundScript.SetBodyPartBloodLoss(instance.smallCut);
            BloodBarScript.ModifyBloodLossRate(instance.smallCut);
            print("Bloddloss=" + BloodBarScript.bloodLossRate + ", Small cut on Right Leg");
        }
    }

    // Check what bodypart was given and add the wound and bloodloss to said bodypart
    private static void AddBurns()
    {
        if (bodyPartName.Contains("Torso"))
        {
            TorsoWoundScript.SetWoundType("Burns");
            TorsoWoundScript.SetBodyPartBloodLoss(instance.burns);
            BloodBarScript.ModifyBloodLossRate(instance.burns);
        }
        else if (bodyPartName.Contains("Left Arm"))
        {
            LeftArmWoundScript.SetWoundType("Burns");
            LeftArmWoundScript.SetBodyPartBloodLoss(instance.burns);
            BloodBarScript.ModifyBloodLossRate(instance.burns);
        }
        else if (bodyPartName.Contains("Left Leg"))
        {
            LeftLegWoundScript.SetWoundType("Burns");
            LeftLegWoundScript.SetBodyPartBloodLoss(instance.burns);
            BloodBarScript.ModifyBloodLossRate(instance.burns);
        }
        else if (bodyPartName.Contains("Right Arm"))
        {
            RightArmWoundScript.SetWoundType("Burns");
            RightArmWoundScript.SetBodyPartBloodLoss(instance.burns);
            BloodBarScript.ModifyBloodLossRate(instance.burns);
        }
        else if (bodyPartName.Contains("Right Leg"))
        {
            RightLegWoundScript.SetWoundType("Burns");
            RightLegWoundScript.SetBodyPartBloodLoss(instance.burns);
            BloodBarScript.ModifyBloodLossRate(instance.burns);
        }
    }
}
