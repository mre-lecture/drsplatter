using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLegWoundScript : MonoBehaviour, IInputClickHandler
{

    private static float BodyPartBloodLoss;
    private static string woundType;
    private bool bandaged = false;
    private bool desinfected = false;
    private bool anesthetized = false;
    private int stitched = 0;
    private bool pipeRemoved = false;

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
            GameLogicScript.CallStopGame();
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
            BodyPartBloodLoss = bloodloss * 2;
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
                if(woundType.Length > 0)
                {

                    instance.blood1.SetActive(false);
                    instance.blood2.SetActive(false);
                    instance.blood3.SetActive(false);

                    if (woundType.Contains("Large"))
                    {
                        BodyPartBloodLoss += GameLogicScript.bandageHealLarge;
                        if (BodyPartBloodLoss < 0)
                        {
                            BloodBarScript.ModifyBloodLossRate(GameLogicScript.bandageHealLarge + BodyPartBloodLoss);
                        }
                        else
                        {
                            BloodBarScript.ModifyBloodLossRate(GameLogicScript.bandageHealLarge);
                        }
                    }
                    else
                    {
                        BodyPartBloodLoss += GameLogicScript.bandageHealSmall;
                        if (BodyPartBloodLoss < 0)
                        {
                            BloodBarScript.ModifyBloodLossRate(GameLogicScript.bandageHealSmall + BodyPartBloodLoss);
                        }
                        else
                        {
                            BloodBarScript.ModifyBloodLossRate(GameLogicScript.bandageHealSmall);
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

                if(woundType.Length > 0)
                {
                    BodyPartBloodLoss += GameLogicScript.desinfectantHeal;
                    if (BodyPartBloodLoss < 0)
                    {
                        BloodBarScript.ModifyBloodLossRate(GameLogicScript.desinfectantHeal + BodyPartBloodLoss);
                    }
                    else
                    {
                        BloodBarScript.ModifyBloodLossRate(GameLogicScript.desinfectantHeal);
                    }
                    BloodBarScript.TakeDamage(20);
                }

                desinfectantSound.Play();

                DisplayFieldScript.Display("Desinfectant applied");
            }
            else if (GameLogicScript.selectedTool.Equals("scissors") && bandaged)
            {
                bandaged = false;

                if(woundType.Length > 0)
                {
                    BloodBarScript.ModifyBloodLossRate(GameLogicScript.scissorsEffect);
                    BodyPartBloodLoss += GameLogicScript.scissorsEffect;

                    instance.blood1.SetActive(true);
                    instance.blood2.SetActive(true);
                    instance.blood3.SetActive(true);

                }

                // Remove Bandage Model
                bandagedBodyPart.SetActive(false);
                
                scissorsSound.Play();

                DisplayFieldScript.Display("Bandages removed");
            }
            else if (GameLogicScript.selectedTool.Equals("syringe") && !anesthetized)
            {
                BloodBarScript.TakeDamage(10);
                
                if(woundType.Length > 0)
                {
                    BloodBarScript.ModifyBloodLossRate(GameLogicScript.anestheticsHeal);
                    BodyPartBloodLoss += GameLogicScript.anestheticsHeal;
                }

                anesthetized = true;

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

                if(woundType.Length > 0)
                {
                    BodyPartBloodLoss += GameLogicScript.stitchingHeal/3;
                    if(BodyPartBloodLoss < 0)
                    {
                        BloodBarScript.ModifyBloodLossRate(GameLogicScript.stitchingHeal / 3 + BodyPartBloodLoss);
                        print("overheal: " + BodyPartBloodLoss);
                    }
                    else
                    {
                        BloodBarScript.ModifyBloodLossRate(GameLogicScript.stitchingHeal / 3);
                    }
                    
                    
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
        instance.pipeRemoved = false;
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
