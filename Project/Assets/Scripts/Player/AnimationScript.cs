using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator anim;
    public AnimationScript animationScript;
    private InputManager input;

    public bool rollLeft;
    public bool rollRight;




    // Start is called before the first frame update
    void Start()
    {
        animationScript = GetComponent<AnimationScript>();
        input = GetComponent<InputManager>();

        anim.SetBool("Rolling_Left", false);

    }

    // Update is called once per frame
    void Update()
    {
        //var x = Input.GetAxis("Horizontal");
        var x = input.hori;
        //var y = Input.GetAxis("Vertical");
        var y = input.vert;
        //Calling blend tree function

        y = Mathf.Clamp(y, -1, 1);
        x = Mathf.Clamp(x, -1, 1);
        Move(x, y);

        if (rollRight == true)
        {
            anim.SetBool("Rolling_Right", true);
        }
        else if (rollRight == false)
        {
            anim.SetBool("Rolling_Right", false);
        }

        if(rollLeft == true)
        {
            anim.SetBool("Rolling_Left", true);
        }
        else if(rollLeft == false)
        {
            anim.SetBool("Rolling_Left", false);
        }
        


    }

    //Blend tree Function
    public void Move(float x, float y)
    {
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);
    }


}
