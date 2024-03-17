using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;

public class DrumstickR : MonoBehaviour
{
    private Animator animator_R;
    private NetworkContext context;

    // Public variable to manually assign ownership for testing
    public bool isOwnerForTesting;

    private struct DrumstickMessage
    {
        public string action;
        public Vector3 position;
        public Quaternion rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator_R = GetComponent<Animator>();
        context = NetworkScene.Register(this);

        // Set isOwner based on the public variable. This allows manual control for testing.
        isOwner = isOwnerForTesting;

        Debug.Log($"[DrumstickR] Start - isOwnerForTesting: {isOwnerForTesting}, isOwner: {isOwner}");
    }

    // Update is called once per frame
    void Update()
    {
        // Only allow action initiation if this instance is set as the owner for testing
        if (animator_R != null && isOwner)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator_R.SetTrigger("hit");
                SendAction("hit");
                Debug.Log("[DrumstickR] Hit action initiated.");
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator_R.SetTrigger("spin");
                SendAction("spin");
                Debug.Log("[DrumstickR] Spin action initiated.");
            }
        }
    }

    private void SendAction(string action)
    {
        // Send action along with current position and rotation over the network
        DrumstickMessage message = new DrumstickMessage 
        { 
            action = action,
            position = this.transform.position,
            rotation = this.transform.rotation
        };
        context.SendJson(message);
        Debug.Log($"[DrumstickR] Sending action: {action} with position and rotation.");
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage m)
    {
        // Process received action along with position and rotation
        var message = m.FromJson<DrumstickMessage>();

        // Set the drumstick's position and rotation to the received values
        this.transform.position = message.position;
        this.transform.rotation = message.rotation;

        Debug.Log($"[DrumstickR] Received action: {message.action} with position {message.position} and rotation {message.rotation}.");
    }

    // Optional: For future use if you decide to implement dynamic ownership.
    public bool isOwner;
    private void AttemptTakeOwnership()
    {
        // Placeholder for future ownership logic
        isOwner = false;
        Debug.Log("[DrumstickR] Attempting to take ownership.");
    }
}
