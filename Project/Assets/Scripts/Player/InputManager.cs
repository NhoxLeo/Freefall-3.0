using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    private GliderController gliderController;
    private FlyingStates flyingStates;
    private RotationController rotationController;
    public AnimationScript animationScript;
    public FuelUI fuelUIScript;
    public AudioManager audioManager;
    public ParticleSystem boostPop;

    [SerializeField]
    private bool isRollingRight;
    [SerializeField]
    private bool canRollRight;

    [SerializeField]
    private float canRollRightCounter;
    [SerializeField]
    private float canRollRightTarget;
    [SerializeField]
    private float canRollRightStep;

    [SerializeField]
    private bool isRollingLeft;
    [SerializeField]
    private bool canRollLeft;

    [SerializeField]
    private float canRollLeftCounter;
    [SerializeField]
    private float canRollLeftTarget;
    [SerializeField]
    private float canRollLeftStep;

    public float minBoost;
    public float maxBoost;
    public float boostVariable;
    private float boostUpdater;

    //input variables
    [SerializeField]
    public float pitch;
    public float yaw;

    [SerializeField]
    private float xRotationSpeed;
    [SerializeField]
    private float yRotationSpeed;

    public bool playerControlsInverted;

    public float horiCounter;
    [SerializeField]
    private float horiRate;

    public float vertCounter;
    [SerializeField]
    private float vertRate;


    public float hori;
    public float vert;

    public int horiSensitivity;
    public int vertSensitivity;



    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<MenuController>();
        canRollLeft = true;
        canRollRight = true;
        animationScript = GetComponent<AnimationScript>();
        rotationController = GetComponent<RotationController>();
        flyingStates = GetComponent<FlyingStates>();
        gliderController = GetComponent<GliderController>();
        audioManager = GetComponent<AudioManager>();
        
        if(FindObjectOfType<MenuController>().controlsInverted == true)
        {
            playerControlsInverted = true;
        }
       

       
    }


    void Update()
    {
        boostVariable = boostUpdater;
        LeftRollCounter();
        RightRollCounter();
        AxisCounter();
    }

    public void AxisCounter()
    {
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        if (hori >= 0.5f)
        {
            horiCounter = Mathf.Clamp(horiCounter, -1, 1);
            horiCounter += horiRate;
        }
        else if (hori <= -0.5f)
        {
            horiCounter = Mathf.Clamp(horiCounter, -1, 1);
            horiCounter -= horiRate;
        }
        else if (hori <= 0.5f || hori >= -0.5f)
        {
            if (horiCounter > 0)
            {
                horiCounter -= horiRate * 8;
                horiCounter = Mathf.Clamp(horiCounter, 0, 1);
            }
            else if (horiCounter < 0)
            {
                horiCounter += horiRate * 8;
                horiCounter = Mathf.Clamp(horiCounter, -1, 0);
            }

        }

        if (vert >= 0.5f)
        {
            vertCounter = Mathf.Clamp(vertCounter, -1, 1);
            vertCounter += vertRate;
        }
        else if (vert <= -0.5f)
        {
            vertCounter = Mathf.Clamp(vertCounter, -1, 1);
            vertCounter -= vertRate;
        }
        else if (vert <= 0.5f || vert >= -0.5f)
        {

            if (vertCounter > 0)
            {
                vertCounter -= vertRate * 8;
                vertCounter = Mathf.Clamp(vertCounter, 0, 1);
            }
            else if (vertCounter < 0)
            {
                vertCounter += vertRate * 8;
                vertCounter = Mathf.Clamp(vertCounter, -1, 0);
            }


        }



    }

    public void InputData()
    {
        if(playerControlsInverted == false)
        {
            yaw = yRotationSpeed * hori * rotationController.currentYawRotationSpeed * Time.deltaTime;
            if (flyingStates.canTurnUp == true)
            {
                pitch = xRotationSpeed * vert * rotationController.currentPitchRotationSpeed * Time.deltaTime;
            }
            else if (flyingStates.canTurnUp == false)
            {
                pitch = Mathf.Clamp(pitch, 0, 1);
            }
        }
        if (playerControlsInverted == true)
        {
            yaw = yRotationSpeed * hori * rotationController.currentYawRotationSpeed * Time.deltaTime;

            if (flyingStates.canTurnUp == true)
            {
                pitch = xRotationSpeed * -vert * rotationController.currentPitchRotationSpeed * Time.deltaTime;
            }
            else if (flyingStates.canTurnUp == false)
            {
                pitch = Mathf.Clamp(pitch, 0, 1);
            }
        }


        if (flyingStates.boostFuel <= 0)
        {
            Debug.Log("Out of fuel");
            if (Input.GetButtonDown("Shift"))
            {
                FindObjectOfType<AudioManager>().PlayAudio("OutOfFuel");
            }
            animationScript.anim.SetBool("Boosting", false);
            flyingStates.isBoosting = false;
            boostUpdater = minBoost;
            FindObjectOfType<AudioManager>().StopPlayingAudio("Boost");



        }
        else if (flyingStates.boostFuel > 0)
        {
            if (Input.GetButtonDown("Shift"))
            {
                flyingStates.isBoosting = true;
                fuelUIScript.Flashing = true;
                boostUpdater = maxBoost;
                FindObjectOfType<AudioManager>().PlayAudio("Boost");
                boostPop.Play();
                animationScript.anim.SetBool("Boosting", true);
            }

            else if (Input.GetButtonUp("Shift"))
            {
                flyingStates.isBoosting = false;
                fuelUIScript.Flashing = false;
                boostUpdater = minBoost;
                FindObjectOfType<AudioManager>().StopPlayingAudio("Boost");
                animationScript.anim.SetBool("Boosting", false);
            }

        }

        if (Input.GetButtonDown("Roll_Right"))
        {
            if(canRollRight == true)
            {
                canRollRight = false;
                animationScript.rollRight = true;
                flyingStates.addedVelocity += transform.right * 150;
            }

        }
        else if (Input.GetButtonUp("Roll_Right"))
        {
            animationScript.rollRight = false;
        }

        if (Input.GetButtonDown("Roll_Left"))
        {
            if(canRollLeft == true)
            {
                canRollLeft = false;
                animationScript.rollLeft = true;
                flyingStates.addedVelocity += -transform.right * 150;
            }

        }
        else if (Input.GetButtonUp("Roll_Left"))
        {
            animationScript.rollLeft = false;
        }
    }

    void RightRollCounter()
    { 
        
      if (canRollRight == false)
        {
            canRollRightCounter += canRollRightStep;
            if(canRollRightCounter >= canRollRightTarget)
            {
                canRollRight = true;
                canRollRightCounter = 0;
            }
        }
    }
    void LeftRollCounter()
    {
        if(canRollLeft == false)
        {
            canRollLeftCounter += canRollLeftStep;
            if(canRollLeftCounter >= canRollLeftTarget)
            {
                canRollLeft = true;
                canRollLeftCounter = 0;
            }
        }
    }
}
