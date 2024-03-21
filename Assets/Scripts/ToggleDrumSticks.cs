using UnityEngine.InputSystem;
using UnityEngine;

public class ToggleDrumSticks : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    public GameObject drumstick;

    private void Awake()
    {
        toggleReference.action.started += ToggleL;
    }

    private void OnDestroy() 
    {
        toggleReference.action.started -= ToggleL;
    }

    private void ToggleL(InputAction.CallbackContext context)
    {
        if (drumstick.activeSelf)
        {
            drumstick.SetActive(false);
        }
        else
        {
            drumstick.SetActive(true);
        }
    }
}
