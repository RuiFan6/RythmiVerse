using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class DrumKitManager : MonoBehaviour
{
    NetworkContext context;
    public static DrumKitManager Instance { get; private set; }

    public bool isOwner; // The centralized ownership flag

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Initialize isOwner here or elsewhere as needed
            isOwner = true;
        }
        else
        {
            Destroy(gameObject); // Ensures there is only one instance
        }
    }

    // Method to update ownership status
    public void UpdateOwnership(bool ownershipStatus)
    {
        isOwner = ownershipStatus;
        // Optionally, broadcast the update to all child components
        // // Example of changing ownership from some other part of your code
        // Update ownership with this command DrumKitManager.Instance.UpdateOwnership(newOwnershipStatus);

    }
}