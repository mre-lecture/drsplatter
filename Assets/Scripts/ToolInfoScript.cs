using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolInfoScript : MonoBehaviour, IInputClickHandler
{

    // Use this for initialization
    void Start()
    {
        HideToolHelp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }

    public void ShowToolHelp()
    {
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void HideToolHelp()
    {
        gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

}
