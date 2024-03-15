using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class DrumstickL : MonoBehaviour
{
    private Animator animator_L;
    private NetworkContext context;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    public NetworkId Id { get; } = new NetworkId();
    public bool owner = false;

    private struct DrumstickMessage
    {
        public string animationTrigger;
        public Vector3 position;
        public Quaternion rotation;
        public bool isOwner; // Added field to the message

    }

    // Start is called before the first frame update
    void Start()
    {
        animator_L = GetComponent<Animator>();
        context = NetworkScene.Register(this);
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void SendDrumstickUpdate(string animationTrigger = null)
    {
        var message = new DrumstickMessage
        {
            animationTrigger = animationTrigger,
            position = transform.position,
            rotation = transform.rotation,
            isOwner = owner // Set the isOwner field based on the owner variable
        };
        context.SendJson(message);
    }

    private void TriggerAnimation(string trigger)
    {
        animator_L.SetTrigger(trigger);
        SendDrumstickUpdate(trigger);
    }

    // Update is called once per frame
    void Update()
    {
        if (owner)
        {
            if (animator_L != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    TriggerAnimation("hit");
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    TriggerAnimation("spin");
                }

                if (lastPosition != transform.position || lastRotation != transform.rotation)
                {
                    SendDrumstickUpdate();
                    lastPosition = transform.position;
                    lastRotation = transform.rotation;
                }
            }
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var receivedMessage = message.FromJson<DrumstickMessage>();

        // Only update position and rotation if there's no animation trigger and the sender is not the owner
        if (string.IsNullOrEmpty(receivedMessage.animationTrigger) && !receivedMessage.isOwner)
        {
            transform.position = receivedMessage.position;
            transform.rotation = receivedMessage.rotation;
        }
        else
        {
            // If there's an animation trigger and the sender is not the owner, apply it
            if (!receivedMessage.isOwner)
            {
                animator_L.SetTrigger(receivedMessage.animationTrigger);
            }
        }
    }
}
