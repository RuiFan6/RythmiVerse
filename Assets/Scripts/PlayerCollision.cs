using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerCollision : MonoBehaviour
{
    NetworkContext context;

    // Define all tags 
    string[] tagsOfInterest = new string[] { "snare_tom", "floor_tom", "rack_tom1", "rack_tom2", "crash", "ride", "hi_hat" };

    void Start() // Corrected from 'void start()'
    {
        context = NetworkScene.Register(this);
    }

    private struct SoundMessage
    {
        public string tagOfHitObject;
    }

    void OnCollisionEnter(Collision collisionInfo)
    { 
        foreach (var tag in tagsOfInterest)
        {
            if (collisionInfo.gameObject.tag == tag)
            {
                Animator animator = collisionInfo.gameObject.GetComponent<Animator>();
                AudioSource aud = collisionInfo.gameObject.GetComponent<AudioSource>();

                if (animator != null)
                {
                    animator.SetTrigger("hit");
                }
                if (aud != null)
                {
                    //float impactforce = collisionInfo.relativeVelocity.magnitude;
                    //Debug.Log("aaa" + impactforce);

                    // Calculate the total force applied during the collision
                    Vector3 totalImpulse = collisionInfo.impulse;
                    float timeInterval = Time.fixedDeltaTime; // Time over which the impulse was applied
                    
                    // Force = Impulse / Time
                    Vector3 totalForce = totalImpulse / timeInterval;
                    
                    // Assuming you want to do something with the magnitude of the force
                    float forceMagnitude = totalForce.magnitude;
                    
                    //Debug.Log($"Total force applied during collision: {forceMagnitude} Newtons");

                    // Assuming forceMagnitude varies from about 0.1 to 4.0
                    float minForce = 0.1f;
                    float maxForce = 4.0f;

                    // Normalize forceMagnitude to a 0.0 - 1.0 scale
                    float normalizedForceMagnitude = (forceMagnitude - minForce) / (maxForce - minForce);

                    // Define minimum and maximum volume
                    float minVolume = 0.1f;
                    float maxVolume = 1.0f;

                    // Calculate the volume based on normalizedForceMagnitude, ensuring it's at least minVolume
                    float volume = normalizedForceMagnitude * (maxVolume - minVolume) + minVolume;

                    // Ensure volume is within the bounds
                    volume = Mathf.Clamp(volume, minVolume, maxVolume);

                    // Set the volume
                    aud.volume = volume;
                    aud.Play();
                    var soundMessage = new SoundMessage { tagOfHitObject = tag };
                    context.SendJson(soundMessage);



                    //CreateAndModifyClone(collisionInfo.gameObject);


                }
                break;
            }
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {   
        var soundMessage = message.FromJson<SoundMessage>();
        Debug.Log($"Received sound trigger for tag: {soundMessage.tagOfHitObject}");
        GameObject[] obj = GameObject.FindGameObjectsWithTag(soundMessage.tagOfHitObject);
        AudioSource aud = obj[0].GetComponent<AudioSource>();
        Animator animator = obj[0].GetComponent<Animator>();
        if (aud != null)
        {
            aud.Play();
        }
        if (animator != null)
        {
            animator.SetTrigger("hit");
        } 
    }
}
