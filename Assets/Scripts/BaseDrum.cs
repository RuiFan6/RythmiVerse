using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseDrum : MonoBehaviour
{
    //Animator animator;
    private AudioSource aud;
    private Animator anim;
    public InputActionReference toggleReference = null;
    
    void Start()
    {
        aud = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    private void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy() 
    {
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        //Debug.Log("base drum !");
        anim.SetTrigger("hit");
        aud.Play();
    }
}
