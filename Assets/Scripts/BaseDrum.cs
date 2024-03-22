using UnityEngine;
using UnityEngine.InputSystem;

public class BaseDrum : MonoBehaviour
{
    //Animator animator;
    private AudioSource aud;
    private Animator anim;
    public InputActionReference toggleReference = null;
    public GameObject RayR;
    public bool isTeacher;
    
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
        if (gameObject.layer == LayerMask.NameToLayer("teacher_drum") && isTeacher)
        {
            if (RayR.activeSelf == false)
            {
                //Debug.Log("base drum !");
                anim.SetTrigger("hit");
                aud.Play();
            }
        }
        if (gameObject.layer == LayerMask.NameToLayer("student_drum") && isTeacher == false)
        {
            if (RayR.activeSelf == false)
            {
                //Debug.Log("base drum !");
                anim.SetTrigger("hit");
                aud.Play();
            }
        }
        
    }
}
