using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerCollision : MonoBehaviour
{
    NetworkContext context;

     // Define all tags you're interested in
     string[] tagsOfInterest = new string[] { "snare_tom", "floor_tom", "rack_tom1", "rack_tom2", "ride_symbal1", "ride_symbal2", "hi_hat" };

    void start(){
        context = NetworkScene.Register(this);
    }
    private struct SoundMessage
    {
        public string tagOfHitObject;
        // public int token; // Token for ownership logic
    }
    void OnCollisionEnter(Collision collisionInfo)
    { 
        // Check if the collided object's tag is one of the tags of interest
        foreach (var tag in tagsOfInterest)
        {
            if (collisionInfo.gameObject.tag == tag)
            {
              
                // Get the Animator and AudioSource components
                Animator animator = collisionInfo.gameObject.GetComponent<Animator>();
                AudioSource aud = collisionInfo.gameObject.GetComponent<AudioSource>();

                // Trigger the animation and play the sound if components are found
                if (animator != null)
                {
                    animator.SetTrigger("hit");

                }
                if (aud != null)
                {
                    aud.Play();
                    var soundMessage = new SoundMessage { tagOfHitObject = tag };
                    context.SendJson(soundMessage);
                }

                // Since we found a match, no need to continue checking other tags
                break;
            }
        }
    }
    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var soundMessage = message.FromJson<SoundMessage>();
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(soundMessage.tagOfHitObject);
        foreach (var obj in objectsWithTag)
        {
            AudioSource aud = obj.GetComponent<AudioSource>();
            if (aud != null)
            {
                aud.Play();
                break; // Assuming you only want to play the sound once for each message.
            }
        }
        
    }
}
