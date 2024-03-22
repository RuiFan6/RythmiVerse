using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public Transform target; // Target to move towards
    public float speed = 1.0f; // Adjustable speed

    void Update()
    {
        if (target != null)
        {
            // Move towards the target
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Check if the position of the clone and the target are approximately equal
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                Destroy(gameObject); // Destroy the clone once it reaches the target
            }
        }
    }
}