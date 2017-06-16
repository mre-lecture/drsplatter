using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFieldScript : MonoBehaviour {

    public  Text displayText;

    public static DisplayFieldScript instance;

    private void Awake()
    {
        instance = this;
        Color white = new Color(1, 1, 1, 1);
        displayText.color = white;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Display(string message)
    {
        instance.displayText.text = message;
        instance.StartCoroutine(instance.Wait());
        instance.displayText.text = " ";
        print(message);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
    }
}
