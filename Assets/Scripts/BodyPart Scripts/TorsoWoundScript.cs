using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoWoundScript : MonoBehaviour, IInputClickHandler
{

    private static int BodyPartBloodLoss;
    private static string woundType;
    private bool bandaged = false;
    private bool desinfected = false;
    private bool anesthetized = false;
    private int stitched = 0;

    public static TorsoWoundScript instance;

    public GameObject healthyBodyPart;
    public GameObject woundedBodyPart;
    public GameObject stitchedBodyPart1;
    public GameObject stitchedBodyPart2;
    public GameObject bandagedBodyPart;

    public GameObject blood;
    public GameObject blood2;

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
        stitchedBodyPart1.SetActive(false);
        stitchedBodyPart2.SetActive(false);
        bandagedBodyPart.SetActive(false);
        blood.SetActive(false);
        blood2.SetActive(false);

    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BodyPartBloodLoss < 2)
        {
            // Change to healthy Model
        }
    }

    public static void SetWoundType(string wound)
    {
        woundType = wound;
        instance.healthyBodyPart.SetActive(false);
        instance.woundedBodyPart.SetActive(true);
        instance.blood.SetActive(true);
        instance.blood2.SetActive(true);


    }

    public static void SetBodyPartBloodLoss(int bloodloss)
    {
        BodyPartBloodLoss = bloodloss * 2; // Torso takes double damage because 2 wounds 
    }


    // ------ AirTap Function ------ //

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
                instance.bandagedBodyPart.SetActive(true);
                instance.blood.SetActive(false);
                instance.blood2.SetActive(false);

                bandageSound.Play();

                BloodBarScript.ModifyBloodLossRate(-10);
                BodyPartBloodLoss -= 10;
                DisplayFieldScript.Display("Bandages applied");
            }
            else if (GameLogicScript.selectedTool.Equals("desinfectant") && !desinfected)
            {
                GameLogicScript.UseDesinfectant();
                desinfected = true;

                BloodBarScript.ModifyBloodLossRate(-5);
                BodyPartBloodLoss -= 5;
                BloodBarScript.TakeDamage(20);

                desinfectantSound.Play();

                DisplayFieldScript.Display("Desinfectant applied");
            }
            else if (GameLogicScript.selectedTool.Equals("scissors") && bandaged)
            {
                bandaged = false;
                BloodBarScript.ModifyBloodLossRate(10);
                BodyPartBloodLoss += 10;

                // Remove Bandage Model
                instance.bandagedBodyPart.SetActive(false);
                instance.blood.SetActive(true);
                instance.blood2.SetActive(true);

                scissorsSound.Play();

                DisplayFieldScript.Display("Bandages removed");
            }
            else if (GameLogicScript.selectedTool.Equals("syringe") && !anesthetized)
            {
                BloodBarScript.ModifyBloodLossRate(-2);
                BodyPartBloodLoss -= 2;
                BloodBarScript.TakeDamage(10);
                anesthetized = true;

                anestheticsSound.Play();

                DisplayFieldScript.Display("Anesthetics applied");
            }
            else if (GameLogicScript.selectedTool.Equals("bonesaw"))
            {
                BloodBarScript.TakeDamage(50);
                DisplayFieldScript.Display("Oh Really?");
            }
            else if (GameLogicScript.selectedTool.Equals("needle") && stitched < 2 && !bandaged)
            {
                // Torso can be stitched twice because 2 wounds

                BloodBarScript.TakeDamage(5);
                BloodBarScript.ModifyBloodLossRate(-20);
                BodyPartBloodLoss -= 20;
                stitched++;

                stitchingSound.Play();

                DisplayFieldScript.Display("Wound stitched");

                if (stitched == 1)
                {
                    instance.woundedBodyPart.SetActive(false);
                    instance.stitchedBodyPart1.SetActive(true);
                    instance.blood.SetActive(false);
                }
                else if (stitched == 2)
                {
                    instance.stitchedBodyPart1.SetActive(false);
                    instance.stitchedBodyPart2.SetActive(true);
                    instance.blood2.SetActive(false);
                }

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
        instance.woundedBodyPart.SetActive(false);
        instance.stitchedBodyPart1.SetActive(false);
        instance.stitchedBodyPart2.SetActive(false);
        instance.bandagedBodyPart.SetActive(false);
        instance.blood.SetActive(false);
        instance.blood2.SetActive(false);
    }

}
