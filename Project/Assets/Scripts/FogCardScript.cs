using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class FogCardScript : MonoBehaviour
{
    public GameObject player;
    public GameObject fogCard;
    public float fog1Distance;
    public float fogDensity;
    public Material materialFogDensity;
    public Renderer rendererFogDensity;
    void Start()
    {
        fogCard = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        fog1Distance = Vector3.Distance(player.transform.position, fogCard.transform.position);
        fogDensity = -fog1Distance * 0.03f;


        fogDensity = Mathf.Clamp(fogDensity, -10, 30);
        materialFogDensity.SetFloat("Vector1_C9555341", fogDensity + 10);
    }
}
