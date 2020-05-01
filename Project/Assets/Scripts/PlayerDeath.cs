using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Animator anim;
    private GliderController gliderController;
   

    private void Start()
    {
        gliderController = GetComponent<GliderController>();
        FindObjectOfType<AudioManager>();
    }
    public void Death()
    {
        anim.SetTrigger("Death_Fade");
        Invoke("ReloadCheckpoint", 2f);
        
    }

    void ReloadCheckpoint()
    {
        FindObjectOfType<AudioManager>().PlayAudio("Dead");
        gliderController.creditsMenu.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
