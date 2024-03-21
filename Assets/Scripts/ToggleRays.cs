using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleRays : MonoBehaviour
{
    public GameObject ray_L;
    public GameObject ray_R;
    //public GameObject drumstick_L;
    //public GameObject drumstick_R;
    private bool enabled_L = false;
    private bool enabled_R = false; 
    public InputActionReference toggleReference_L = null;
    public InputActionReference toggleReference_R = null;
    void Start()
    {
        
    }
    private void Awake()
    {
        toggleReference_L.action.started += ToggleL;
        toggleReference_R.action.started += ToggleR;
    }

    private void OnDestroy() 
    {
        toggleReference_L.action.started -= ToggleL;
        toggleReference_R.action.started -= ToggleR;
    }

    private void ToggleL(InputAction.CallbackContext context)
    {
        if (enabled_L)
        {
            //drumstick_L.SetActive(false);
            ray_L.SetActive(false);
            enabled_L = false;
        }
        else
        {
            //drumstick_L.SetActive(true);
            ray_L.SetActive(true);
            enabled_L = true;
        }
    }

    private void ToggleR(InputAction.CallbackContext context)
    {
        if (enabled_R)
        {
            //drumstick_R.SetActive(false);
            ray_R.SetActive(false);
            enabled_R = false;
        }
        else
        {
            //drumstick_R.SetActive(true);
            ray_R.SetActive(true);
            enabled_R = true;
        }
    }
}
