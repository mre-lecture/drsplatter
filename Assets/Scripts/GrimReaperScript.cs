using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class GrimReaperScript : MonoBehaviour, IInputClickHandler
{
    public GameObject reaper;
    public static GrimReaperScript instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameLogicScript.CallReset();
    }

    public static void SetVisibility(bool visible)
    {
        if (visible)
        {
            instance.reaper.SetActive(true);
        }
        else
        {
            instance.reaper.SetActive(false);
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
