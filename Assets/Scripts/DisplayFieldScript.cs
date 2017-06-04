using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFieldScript : MonoBehaviour {

    public static Text displayText;
    private static DisplayFieldScript instance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Display(string message)
    {
        displayText.text = message;
        instance.StartCoroutine(instance.Wait());
        displayText.text = " ";
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }
}
