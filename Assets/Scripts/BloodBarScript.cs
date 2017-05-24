using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBarScript : MonoBehaviour {

    public Image BloodBar;
    public Text ratioText;

    private float bloodLevel = 0;
    private float maxBloodLevel = 150;
    private float bloodLossRate = 1;

	// Use this for initialization
	void Start () {
        bloodLevel = maxBloodLevel;
	}

    public void Reset()
    {
        StopBloodLoss();
        bloodLevel = maxBloodLevel;
        bloodLossRate = 1;
        UpdateBloodBar();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void UpdateBloodBar()
    {
        float ratio = bloodLevel / maxBloodLevel;
        // currentBloodBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        BloodBar.fillAmount = ratio;
        ratioText.text = (ratio * 100).ToString("0") + '%';
    }

    public void SetBloodLossRate(float bloodLossRate)
    {
        this.bloodLossRate = bloodLossRate;
    }

    public void StartBloodLoss()
    {
        InvokeRepeating("TakeBloodLoss", 2f, 1f);
    }

    public void StopBloodLoss()
    {
        CancelInvoke("TakeBloodLoss");
    }

    private void TakeBloodLoss()
    {
        TakeDamage(bloodLossRate);
    }

    public void TakeDamage(float damage)
    {
        bloodLevel -= damage;
        if(bloodLevel < 0)
        {
            bloodLevel = 0;
        }
        UpdateBloodBar();
    }

    public void healBloodLoss(float heal)
    {
        bloodLevel += heal;
        if (bloodLevel > maxBloodLevel)
        {
            bloodLevel = maxBloodLevel;
        }
        UpdateBloodBar();
    }
}
