using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBarScript : MonoBehaviour {

    public Image BloodBar;
    public Text ratioText;

    public static BloodBarScript instance;

    public static float bloodLevel = 0;
    public static float maxBloodLevel = 150;
    public static float bloodLossRate = 1;

	// Use this for initialization
	void Start () {
        bloodLevel = maxBloodLevel;
	}

    void Awake()
    {
        instance = this;
    }

    public static void Reset()
    {
        StopBloodLoss();
        bloodLevel = maxBloodLevel;
        bloodLossRate = 1;
        if(instance)
            instance.UpdateBloodBar();
    }

    // Update is called once per frame
    void Update () {
		
	}

    // Update visual presentation in GameSpace and calculate ratio, to make visualization better
    private void UpdateBloodBar()
    {
        float ratio = bloodLevel / maxBloodLevel;
        // currentBloodBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        BloodBar.fillAmount = ratio;
        ratioText.text = (ratio * 100).ToString("0") + '%';
    }

    public static void SetBloodLossRate(float lossRate)
    {
        bloodLossRate = lossRate;
    }

    public static void ModifyBloodLossRate(float modifier)
    {
        bloodLossRate += modifier;
    }

    public static void StartBloodLoss()
    {
        if (instance)
            instance.InvokeRepeating("TakeBloodLoss", 2f, 1f);
    }

    public static void StopBloodLoss()
    {
        if(instance)
        instance.CancelInvoke("TakeBloodLoss");
    }

    private static void TakeBloodLoss()
    {
        if(instance)
        TakeDamage(bloodLossRate);
    }

    public static void TakeDamage(float damage)
    {
        bloodLevel -= damage;
        if(bloodLevel < 0)
        {
            bloodLevel = 0;
        }
        if(instance)
            instance.UpdateBloodBar();
    }

    public static void healBloodLoss(float heal)
    {
        bloodLevel += heal;
        if (bloodLevel > maxBloodLevel)
        {
            bloodLevel = maxBloodLevel;
        }
        if(instance)
            instance.UpdateBloodBar();
    }
}
