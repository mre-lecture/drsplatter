using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO , This is a Stub Class

public class WoundGenerator : MonoBehaviour {

    private string[][] possibleWounds = new string[10][];
    private static string[] woundList;

	// Use this for initialization
	void Start () {
		
	}

    // generates List of possible Wounds
    private void Awake()
    {
        // Laceration
        possibleWounds[0] = new string[] { "laceration", "desinfectant", "needle", "bandage" };
    }

    public static string[] getWounds()
    {

        if(woundList != null)
        {
            return woundList;
        }
        else
        {
            woundList = new string[1];
            woundList[0] = "empty";
            return woundList;
        }
    }

    public static void generateWoundList(int numberOfWounds)
    {
        woundList = new string[numberOfWounds];
        for(int i=0; i<numberOfWounds; i++)
        {
            
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
