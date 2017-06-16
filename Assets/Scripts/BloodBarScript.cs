using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBarScript : MonoBehaviour {

    public Image BloodBar;
    public Text ratioText;

    public GameObject bloodParent;

    public static BloodBarScript instance;

    public static float bloodLevel = 0;
    public static float maxBloodLevel = 1500;
    public static float bloodLossRate = 1;

    void Awake()
    {
        instance = this;
    }


	// Use this for initialization
	void Start () {
        bloodLevel = maxBloodLevel;
	}

    public static void Reset()
    {
        StopBloodLoss();
        maxBloodLevel = 1500;
        bloodLevel = maxBloodLevel;
        bloodLossRate = 1;
        if(instance)
            instance.UpdateBloodBar();
    }

    // Update is called once per frame
    void Update () {
		if(bloodLevel < 1000)
        {
            // causes out of bounds
          //  bloodParent.SetActive(true);
          //  transform.GetChild(0).gameObject.SetActive(true);
          //  transform.GetChild(1).gameObject.SetActive(true);
          //  transform.GetChild(2).gameObject.SetActive(true);

        }
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

    private void TakeBloodLoss()
    {
        if(instance)
        TakeDamage(bloodLossRate);
        print("Bloodloss of " + bloodLossRate + " taken");
    }

    public static void TakeDamage(float damage)
    {
        instance.TakeInternalDamage(damage);
    }

    private void TakeInternalDamage(float damage)
    {
        bloodLevel -= damage;
        if(bloodLevel < 0)
        {
            bloodLevel = 0;
        }
        if(instance)
            instance.UpdateBloodBar();
    }

    public void HealBloodLoss(float heal)
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
