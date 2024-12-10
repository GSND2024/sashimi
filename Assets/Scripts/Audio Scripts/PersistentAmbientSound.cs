using UnityEngine;

public class PersistAmbientSound : MonoBehaviour
{
    // This ensures the object persists across scene loads.
    void Awake()
    {
        // Prevent this object from being destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);

        // Optional: Check if an instance of the ambient sound already exists
        // If it does, destroy this one to avoid duplicates
        if (FindObjectsOfType<PersistAmbientSound>().Length > 1)
        {
            Debug.Log("Duplicate ambient sound found. Destroying this instance.");
            Destroy(gameObject); // Destroy this duplicate instance
        }
    }
}
