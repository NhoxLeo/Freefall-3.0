using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeMovement : MonoBehaviour
{

    private FlyingStates flyingStates;
    public Material capemovement;
    [SerializeField]
    private float Windspeed;
    [SerializeField]
    private float windSpeedDivide;


    private void Start()
    {
        flyingStates = GetComponent<FlyingStates>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Windspeed = flyingStates.Speed / windSpeedDivide;
        capemovement.SetFloat("Vector1_769F0E1", (Windspeed ));
    }
}

