using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLegWoundScript : MonoBehaviour, IInputClickHandler
{

    private static int BodyPartBloodLoss;
    private static string woundType;
    private bool bandaged = false;
    private bool desinfected = false;
    private bool anesthetized = false;
    private int stitched = 0;

    public static RightLegWoundScript instance;

    public GameObject healthyBodyPart;
    public GameObject bandagedBodyPart;
    public GameObject woundedBodyPart;
    public GameObject stitchedOneWound;
    public GameObject stitchedTwoWounds;
    public GameObject stitchedThreeWounds;

    public GameObject blood1;
    public GameObject blood2;
    public GameObject blood3;

    // Audio Sources
    public AudioSource scissorsSound;
    public AudioSource bandageSound;
    public AudioSource anestheticsSound;
    public AudioSource desinfectantSound;
    public AudioSource stitchingSound;
    public AudioSource scalpelSound;

    private void Awake()
    {
        instance = this;

        healthyBodyPart.SetActive(true);
        bandagedBodyPart.SetActive(false);
        woundedBodyPart.SetActive(false);
        stitchedOneWound.SetActive(false);
        stitchedTwoWounds.SetActive(false);
        stitchedThreeWounds.SetActive(false);

        blood1.SetActive(false);
        blood2.SetActive(false);
        blood3.SetActive(false);

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
            blood1.SetActive(false);
            blood2.SetActive(false);
            blood3.SetActive(false);
        }
    }

    public static void SetWoundType(string wound)
    {
        woundType = wound;
        instance.healthyBodyPart.SetActive(false);
        instance.woundedBodyPart.SetActive(true);
        instance.blood1.SetActive(true);
        instance.blood2.SetActive(true);
        instance.blood3.SetActive(true);
    }

    public static void SetBodyPartBloodLoss(int bloodloss)
    {
        if (woundType.Equals("SmallCuts"))
        {
            BodyPartBloodLoss = bloodloss;
        }
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
                if (woundType != null && woundType.Length > 0)
                {

                    instance.blood1.SetActive(false);
                    instance.blood2.SetActive(false);
                    instance.blood3.SetActive(false);

                    if (woundType.Contains("Large"))
                    {
                        BodyPartBloodLoss += GameLogicScript.bandageHealLarge;
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
                        BodyPartBloodLoss += GameLogicScript.bandageHealSmall;
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

                if (woundType != null && woundType.Length > 0)
                {
                    BodyPartBloodLoss += GameLogicScript.desinfectantHeal;
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

                if (woundType != null && woundType.Length > 0)
                {
                    if (stitched == 0)
                    {
                        BloodBarScript.IncreaseBloodloss(GameLogicScript.scissorsEffect);
                        BodyPartBloodLoss += GameLogicScript.scissorsEffect;
                        instance.blood1.SetActive(true);
                        instance.blood2.SetActive(true);
                        instance.blood3.SetActive(true);
                    }
                    else if (stitched == 1)
                    {
                        BloodBarScript.IncreaseBloodloss(GameLogicScript.scissorsEffect/2);
                        BodyPartBloodLoss += GameLogicScript.scissorsEffect / 2;
                        instance.blood2.SetActive(true);
                        instance.blood3.SetActive(true);
                    }
                    else if (stitched == 2)
                    {
                        BloodBarScript.IncreaseBloodloss(GameLogicScript.scissorsEffect / 3);
                        BodyPartBloodLoss += GameLogicScript.scissorsEffect / 3;
                        instance.blood3.SetActive(true);
                    }else if(stitched == 3)
                    {

                    }

                }

                // Remove Bandage Model
                bandagedBodyPart.SetActive(false);
                
                scissorsSound.Play();

                DisplayFieldScript.Display("Bandages removed");
            }
            else if (GameLogicScript.selectedTool.Equals("syringe") && !anesthetized)
            {
                if (woundType != null && woundType.Length > 0)
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

                // remove all models for the left leg
                instance.healthyBodyPart.SetActive(false);
                instance.woundedBodyPart.SetActive(false);
                instance.stitchedOneWound.SetActive(false);
                instance.stitchedTwoWounds.SetActive(false);
                instance.stitchedThreeWounds.SetActive(false);

            }
            else if (GameLogicScript.selectedTool.Equals("needle") && stitched < 3 && !bandaged)
            {
                BloodBarScript.TakeDamage(15);

                if (woundType != null && woundType.Length > 0)
                {
                    if (BodyPartBloodLoss < (GameLogicScript.stitchingHeal/3))
                    {
                        BloodBarScript.ReduceBloodLoss(BodyPartBloodLoss);
                    }
                    else
                    {
                        BloodBarScript.ReduceBloodLoss((GameLogicScript.stitchingHeal / 3));
                        BodyPartBloodLoss -= (GameLogicScript.stitchingHeal/3);
                    }

                    print("BodyPartBloodLoss: " + BodyPartBloodLoss + ", Current Bloodloss: " + BloodBarScript.bloodLossRate + ", Healed amount: " + (GameLogicScript.stitchingHeal/3));

                    stitched++;

                    if(stitched == 1)
                    {
                        instance.woundedBodyPart.SetActive(false);
                        instance.blood1.SetActive(false);
                        instance.stitchedOneWound.SetActive(true);
                    }else if(stitched == 2)
                    {
                        instance.stitchedOneWound.SetActive(false);
                        instance.blood2.SetActive(false);
                        instance.stitchedTwoWounds.SetActive(true);
                    }
                    else if (stitched == 3)
                    {
                        instance.stitchedTwoWounds.SetActive(false);
                        instance.blood3.SetActive(false);
                        instance.stitchedThreeWounds.SetActive(true);
                    }


                }

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
        instance.stitched = 0;
        instance.healthyBodyPart.SetActive(true);
        instance.bandagedBodyPart.SetActive(false);
        instance.woundedBodyPart.SetActive(false);
        instance.stitchedOneWound.SetActive(false);
        instance.stitchedTwoWounds.SetActive(false);
        instance.stitchedThreeWounds.SetActive(false);
        instance.blood1.SetActive(false);
        instance.blood2.SetActive(false);
        instance.blood3.SetActive(false);

    }
}
