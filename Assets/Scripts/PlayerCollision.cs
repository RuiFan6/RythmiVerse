using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        // Define all tags you're interested in
        string[] tagsOfInterest = new string[] { "snare_tom", "floor_tom", "rack_tom1", "rack_tom2", "ride_symbal1", "ride_symbal2", "hi_hat" };

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
                }

                // Since we found a match, no need to continue checking other tags
                break;
            }
        }
    }
}
