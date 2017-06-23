using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmWoundScript : MonoBehaviour, IInputClickHandler
{

    private static int BodyPartBloodLoss;
    private static string woundType;
    private bool bandaged = false;
    private bool desinfected = false;
    private bool anesthetized = false;
    private bool stitched = false;

    public static LeftArmWoundScript instance;

    public GameObject healthyBodyPart;
    public GameObject woundedBodyPart;
    public GameObject stitchedBodyPart;
    public GameObject bandagedBodyPart;

    public GameObject blood;

    // Audio Sources
    public AudioSource scissorsSound;
    public AudioSource bandageSound;
    public AudioSource anestheticsSound;
    public AudioSource desinfectantSound;
    public AudioSource stitchingSound;

    private void Awake()
    {
        instance = this;

        healthyBodyPart.SetActive(true);
        woundedBodyPart.SetActive(false);
        stitchedBodyPart.SetActive(false);
        bandagedBodyPart.SetActive(false);
        blood.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (BodyPartBloodLoss < 0)
        {
            blood.SetActive(false);
        }
    }

    public static void SetWoundType(string wound)
    {
        woundType = wound;
        instance.healthyBodyPart.SetActive(false);
        instance.woundedBodyPart.SetActive(true);
        instance.blood.SetActive(true);
    }

    public static void SetBodyPartBloodLoss(int bloodloss)
    {
        BodyPartBloodLoss = bloodloss;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {

        if (GameLogicScript.GetGameState())
        {
            // AirTap code goes here
            if (GameLogicScript.selectedTool.Equals("bandage") && !bandaged)
            {
                GameLogicScript.UseBandage();
                bandaged = true;

                //Apply Bandage Model
                bandagedBodyPart.SetActive(true);
                instance.blood.SetActive(false);

                if (woundType.Length > 0)
                {
                    if (woundType.Contains("Large"))
                    {
                        if (BodyPartBloodLoss < GameLogicScript.bandageHealLarge)
                        {
                            BloodBarScript.ReduceBloodLoss(BodyPartBloodLoss);
                        }
                        else
                        {
                            BloodBarScript.ReduceBloodLoss(GameLogicScript.bandageHealLarge);
                            BodyPartBloodLoss -= GameLogicScript.bandageHealLarge;
                        }
                    }
                    else
                    {
                        if (BodyPartBloodLoss < GameLogicScript.bandageHealSmall)
                        {
                            BloodBarScript.ReduceBloodLoss(BodyPartBloodLoss);
                        }
                        else
                        {
                            BloodBarScript.ReduceBloodLoss(GameLogicScript.bandageHealSmall);
                            BodyPartBloodLoss -= GameLogicScript.bandageHealSmall;
                        }
                    }
                }

                bandageSound.Play();

                DisplayFieldScript.Display("Bandages applied");
            }
            else if (GameLogicScript.selectedTool.Equals("desinfectant") && !desinfected)
            {
                GameLogicScript.UseDesinfectant();
                desinfected = true;

                if (woundType.Length > 0)
                {
                    if (BodyPartBloodLoss < GameLogicScript.desinfectantHeal)
                    {
                        BloodBarScript.ReduceBloodLoss(BodyPartBloodLoss);
                    }
                    else
                    {
                        BloodBarScript.ReduceBloodLoss(GameLogicScript.desinfectantHeal);
                        BodyPartBloodLoss -= GameLogicScript.desinfectantHeal;
                    }
                    BloodBarScript.TakeDamage(20);
                }

                desinfectantSound.Play();

                DisplayFieldScript.Display("Desinfectant applied");
            }
            else if (GameLogicScript.selectedTool.Equals("scissors") && bandaged)
            {
                bandaged = false;

                if (woundType.Length > 0)
                {
                    BloodBarScript.IncreaseBloodloss(GameLogicScript.scissorsEffect);
                    BodyPartBloodLoss += GameLogicScript.scissorsEffect;
                    instance.blood.SetActive(true);
                }

                scissorsSound.Play();

                // Remove Bandage Model
                bandagedBodyPart.SetActive(false);

                DisplayFieldScript.Display("Bandages removed");
            }
            else if (GameLogicScript.selectedTool.Equals("syringe") && !anesthetized)
            {
                if (woundType.Length > 0)
                {
                    if (BodyPartBloodLoss < GameLogicScript.anestheticsHeal)
                    {
                        BloodBarScript.ReduceBloodLoss(BodyPartBloodLoss);
                    }
                    else
                    {
                        BloodBarScript.ReduceBloodLoss(GameLogicScript.anestheticsHeal);
                        BodyPartBloodLoss -= GameLogicScript.anestheticsHeal;
                    }
                }
                anesthetized = true;
                BloodBarScript.TakeDamage(10);

                anestheticsSound.Play();

                DisplayFieldScript.Display("Anesthetics applied");
            }
            else if (GameLogicScript.selectedTool.Equals("bonesaw"))
            {
                BloodBarScript.TakeDamage(600);
                DisplayFieldScript.Display("Oh Really?");
            }
            else if (GameLogicScript.selectedTool.Equals("needle") && !stitched && !bandaged)
            {
                BloodBarScript.TakeDamage(15);

                if (woundType.Length > 0)
                {
                    if (BodyPartBloodLoss < GameLogicScript.stitchingHeal)
                    {
                        BloodBarScript.ReduceBloodLoss(BodyPartBloodLoss);
                    }
                    else
                    {
                        BloodBarScript.ReduceBloodLoss(GameLogicScript.stitchingHeal);
                        BodyPartBloodLoss -= GameLogicScript.stitchingHeal;
                    }

                    instance.woundedBodyPart.SetActive(false);
                    instance.stitchedBodyPart.SetActive(true);
                    instance.blood.SetActive(false);
                }

                print("BodyPartBloodLoss: " + BodyPartBloodLoss + ", Current Bloodloss: " + BloodBarScript.bloodLossRate + ", Healed amount: " + GameLogicScript.stitchingHeal);

                stitched = true;

                stitchingSound.Play();

                DisplayFieldScript.Display("Wound stitched");
            }
        }
    }

    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }

    public static void ResetBodyPart()
    {
        BodyPartBloodLoss = 0;
        woundType = " ";
        instance.bandaged = false;
        instance.desinfected = false;
        instance.anesthetized = false;
        instance.stitched = false;
        instance.healthyBodyPart.SetActive(true);
        instance.woundedBodyPart.SetActive(false);
        instance.stitchedBodyPart.SetActive(false);
        instance.bandagedBodyPart.SetActive(false);
        instance.blood.SetActive(false);
    }
}
