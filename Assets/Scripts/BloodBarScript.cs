using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBarScript : MonoBehaviour {

    public Image BloodBar;
    public Text ratioText;

    private float bloodLevel = 0;
    private float maxBloodLevel = 150;

	// Use this for initialization
	void Start () {
        bloodLevel = maxBloodLevel;
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

    private void TakeDamage(float damage)
    {
        bloodLevel -= damage;
        if(bloodLevel < 0)
        {
            bloodLevel = 0;
        }
        UpdateBloodBar();
    }

    private void HealBloodLoss(float heal)
    {
        bloodLevel += heal;
        if (bloodLevel > maxBloodLevel)
        {
            bloodLevel = maxBloodLevel;
        }
        UpdateBloodBar();
    }
}
