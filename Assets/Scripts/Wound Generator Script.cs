using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundGeneratorScript : MonoBehaviour {

    static string bodyPartName;
    private static int largeCut = 25;
    private static int smallCut = 10;
    private static int burns = 15;

    public static WoundGeneratorScript instance;

    public static void SetWoundValues(int lCut, int sCut, int burn)
    {
        largeCut = lCut;
        smallCut = sCut;
        burns = burn;
    }

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
        int woundIndex = Random.Range(0, 3);
        if(woundIndex == 0)
        {
            // do nothing
        }
        else if (woundIndex == 1)
        {
            AddLargeCut();
            GameLogicScript.hasWound = true;
            GameLogicScript.OverallBloodloss += largeCut;
        }
        else if(woundIndex == 2)
        {
            AddSmallCuts();
            GameLogicScript.hasWound = true;
            GameLogicScript.OverallBloodloss += smallCut;
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
            TorsoWoundScript.SetBodyPartBloodLoss(largeCut);
            BloodBarScript.IncreaseBloodloss(largeCut);
            print("LargeCut on Torso = +" + largeCut + " Bloodloss");
        }
        else if (bodyPartName.Contains("Left Arm"))
        {
            LeftArmWoundScript.SetWoundType("LargeCut");
            LeftArmWoundScript.SetBodyPartBloodLoss(largeCut);
            BloodBarScript.IncreaseBloodloss(largeCut);
            print("LargeCut on Left Arm = +" + largeCut + " Bloodloss");
        }
        else if (bodyPartName.Contains("Left Leg"))
        {
            // adds a pipe stuck in the leg
            LeftLegWoundScript.SetWoundType("LargeCut");
            LeftLegWoundScript.SetBodyPartBloodLoss(largeCut);
            BloodBarScript.IncreaseBloodloss(largeCut);
            print("Large Cut / Pipe stuck in the Left Leg = +" + largeCut + " Bloodloss");
        }
        else if (bodyPartName.Contains("Right Arm"))
        {
            RightArmWoundScript.SetWoundType("LargeCut");
            RightArmWoundScript.SetBodyPartBloodLoss(largeCut);
            BloodBarScript.IncreaseBloodloss(largeCut);
            print("LargeCut on Right Arm = +" + largeCut + " Bloodloss");
        }
        else if (bodyPartName.Contains("Right Leg"))
        {
            RightLegWoundScript.SetWoundType("LargeCut");
            RightLegWoundScript.SetBodyPartBloodLoss(largeCut);
            BloodBarScript.IncreaseBloodloss(largeCut);
            print("LargeCut on Right Leg = +" + largeCut + " Bloodloss");
        }

    }

    // Check what bodypart was given and add the wound and bloodloss to said bodypart
    private static void AddSmallCuts()
    {
        if (bodyPartName.Contains("Torso"))
        {
            TorsoWoundScript.SetWoundType("SmallCuts");
            TorsoWoundScript.SetBodyPartBloodLoss(smallCut);
            BloodBarScript.IncreaseBloodloss(smallCut);
            print("small cuts on Torso");
        }
        else if (bodyPartName.Contains("Left Arm"))
        {
            LeftArmWoundScript.SetWoundType("SmallCuts");
            LeftArmWoundScript.SetBodyPartBloodLoss(smallCut);
            BloodBarScript.IncreaseBloodloss(smallCut);
            print("Small cut on Left Arm");
        }
        else if (bodyPartName.Contains("Left Leg"))
        {
            LeftLegWoundScript.SetWoundType("SmallCuts");
            LeftLegWoundScript.SetBodyPartBloodLoss(smallCut);
            BloodBarScript.IncreaseBloodloss(smallCut);
            print("Small cut on Left Leg");
        }
        else if (bodyPartName.Contains("Right Arm"))
        {
            RightArmWoundScript.SetWoundType("SmallCuts");
            RightArmWoundScript.SetBodyPartBloodLoss(smallCut);
            BloodBarScript.IncreaseBloodloss(smallCut);
            print("Small cut on Right Arm");
        }
        else if (bodyPartName.Contains("Right Leg"))
        {
            RightLegWoundScript.SetWoundType("SmallCuts");
            RightLegWoundScript.SetBodyPartBloodLoss(smallCut);
            BloodBarScript.IncreaseBloodloss(smallCut);
            print("Small cut on Right Leg");
        }
    }

    // Check what bodypart was given and add the wound and bloodloss to said bodypart
    private static void AddBurns()
    {
        if (bodyPartName.Contains("Torso"))
        {
            TorsoWoundScript.SetWoundType("Burns");
            TorsoWoundScript.SetBodyPartBloodLoss(burns);
            BloodBarScript.IncreaseBloodloss(burns);
        }
        else if (bodyPartName.Contains("Left Arm"))
        {
            LeftArmWoundScript.SetWoundType("Burns");
            LeftArmWoundScript.SetBodyPartBloodLoss(burns);
            BloodBarScript.IncreaseBloodloss(burns);
        }
        else if (bodyPartName.Contains("Left Leg"))
        {
            LeftLegWoundScript.SetWoundType("Burns");
            LeftLegWoundScript.SetBodyPartBloodLoss(burns);
            BloodBarScript.IncreaseBloodloss(burns);
        }
        else if (bodyPartName.Contains("Right Arm"))
        {
            RightArmWoundScript.SetWoundType("Burns");
            RightArmWoundScript.SetBodyPartBloodLoss(burns);
            BloodBarScript.IncreaseBloodloss(burns);
        }
        else if (bodyPartName.Contains("Right Leg"))
        {
            RightLegWoundScript.SetWoundType("Burns");
            RightLegWoundScript.SetBodyPartBloodLoss(burns);
            BloodBarScript.IncreaseBloodloss(burns);
        }
    }
}
