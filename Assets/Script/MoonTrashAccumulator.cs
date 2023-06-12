using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonTrashAccumulator : MonoBehaviour
{
    public GameObject objectToDuplicate;  // The object to duplicate
    public float duplicateInterval = 5f;  // The time interval between duplicates
    public float duration = 60f;  // The duration of the duplication process

    private float elapsedTime = 0f;  // The elapsed time since the duplication process began

    // Start is called before the first frame update
    void Start()
    {
        // Start the duplication process
        StartCoroutine(DuplicateObject());
    }

    // Coroutine that duplicates the object at a regular interval
    private IEnumerator DuplicateObject()
    {
        // Loop for the specified duration
        while (elapsedTime < duration)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(duplicateInterval);

            // Create a new clone of the object
            GameObject newObject = Instantiate(objectToDuplicate, transform.position, transform.rotation);

            // Increment the elapsed time
            elapsedTime += duplicateInterval;
        }
    }
}
