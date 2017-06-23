using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBarScript : MonoBehaviour
{

    public Image BloodBar;
    public Text ratioText;

    public GameObject bloodParent;

    public static BloodBarScript instance;

    public static float bloodLevel = 0;
    public static float maxBloodLevel = 2200;
    public static float bloodLossRate = 1;

    public AudioSource bloodSpillingSound;

    private bool bloodied = false;

    void Awake()
    {
        instance = this;
        bloodParent.SetActive(false);
    }


    // Use this for initialization
    void Start()
    {
        bloodLevel = maxBloodLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (bloodLevel < 1000 && !bloodied)
        {
            bloodParent.SetActive(true);
            bloodSpillingSound.Play();
            bloodied = true;
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

    public static void ModifyBloodLossRate(int modifier)
    {
        bloodLossRate += modifier;
       // print("ModBloodLoss: modifier = " + modifier);
       print("Current Bloodloss: " + bloodLossRate);
    }

    public static void ReduceBloodLoss(int amount)
    {
        bloodLossRate -= amount;
    }

    public static void IncreaseBloodloss(int amount)
    {
        bloodLossRate += amount;
    }

    public static void StartBloodLoss()
    {
        if (instance)
            instance.InvokeRepeating("TakeBloodLoss", 2f, 1f);
    }

    public static void StopBloodLoss()
    {
        if (instance)
            instance.CancelInvoke("TakeBloodLoss");
    }

    private void TakeBloodLoss()
    {
        if (bloodLossRate > 0)
        {
            TakeDamage(bloodLossRate);
            //print("Bloodloss of " + bloodLossRate + " taken");
        }
        else
        {
            GameLogicScript.CallStopGame();
        }

    }

    public static void TakeDamage(float damage)
    {
        instance.TakeInternalDamage(damage);
    }

    private void TakeInternalDamage(float damage)
    {
        bloodLevel -= damage;
        if (bloodLevel < 0)
        {
            bloodLevel = 0;
        }
        if (instance)
            instance.UpdateBloodBar();
    }

    public void HealDamage(float heal)
    {
        bloodLevel += heal;
        if (bloodLevel > maxBloodLevel)
        {
            bloodLevel = maxBloodLevel;
        }
        if (instance)
            instance.UpdateBloodBar();
    }

    public static void Reset()
    {
        StopBloodLoss();
        maxBloodLevel = 1500;
        bloodLevel = maxBloodLevel;
        bloodLossRate = 1;
        if (instance)
            instance.UpdateBloodBar();
        instance.bloodParent.SetActive(false);
        instance.bloodied = false;
    }
}
