using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.InputSystem.EnhancedTouch;

public class Playercontrol : MonoBehaviour
{
    private Rigidbody rb;//rb to control player object

    private float movementX;//float for joystick input
    private float movementY;//float for joystick input
    public float joystickSpeed = 10;//joy stick Speed
    public int count=0; //start with 0 value trashes
    private bool boost = false; // Flag indicating whether the boost effect is active
    //private bool coll = false; // Flag indicating whether the collide effect is active

    [SerializeField] public float timeLimit = 30f; // Total time for the level
    private float timeLeft; // Time left in the level, adjustable
    [SerializeField] public GameObject timeLeftText;//text object for timer
    [SerializeField] public TextMeshProUGUI countText;//text object for count
    [SerializeField] private GameObject joystickController; // the joystick controller
    public GameObject winTextObject;//text for winning
    public GameObject loseTextObject;//text for losing
    public GameObject hitTextObject;//text for hiting non-trashes

    public float speed = 0; // The speed of the spacecraft, adjustable
    public float thrustSpeed = 2; // The speed increase when the "thrust" button is pressed, adjustable
    public float thrustDuration = 1; // The duration of the thrust effect, adjustable
    private float currentSpeed; // The current speed of the spacecraft, adjustable
    private float thrustStartTime; // The time when the "thrust" button was pressed

    public float beamSpeed = 10f; // beam when finger touch the object
    public Color successColor = Color.green; //green color indicating success
    public Color failureColor = Color.red; //red color indicating failure
    public AudioClip successSound; //sound when successfully pick up trash
    public AudioClip failureSound; //sound when fail picking up a trash
    public Camera mainCamera; //front camera

    private AudioSource audioSource;//audio source

    public GameObject spaceTrashPrefab;//when spacecraft hit moon/planet, generate space trash
    public GameObject explosionEffect;//when spacecraft hit moon/planet, explosive effect

    private MeshRenderer meshRenderer;//To destroy objects out of camera view

    // Start to call all needed variables/methods
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        currentSpeed = speed;
        count = 0;
        SetCountText();
        timeLeft = timeLimit;
        SetTimeLeftText();

        // Set the text property of the Win Text and Lose Text UI to an empty string, making the 'You Win' and 'You Lose' messages blank
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        hitTextObject.SetActive(false);

        //Get audio playing
        audioSource = GetComponent<AudioSource>();

        //Simulate touch screen
        //TouchSimulation.Enable();
    }

    //Enable the touchsimulation on Mac
    //***Not sure why my mac is not working, but it does work on my phone, shown in video***
    void OnEnable()
    {
        TouchSimulation.Enable();
        Debug.Log("TouchSimulation.instance");
    }

    // Control the joystick
    public void OnMove(InputValue value)
    {
        Debug.Log("onMove joystick");
        Vector2 inputVector = value.Get<Vector2>(); // get the input value as a Vector2

        // calculate the movement direction based on the input value
        movementX = inputVector.x*joystickSpeed;
        movementY = inputVector.y*joystickSpeed;
    }

    //Update the trash count value 
    public void SetCountText()
    {
        string countstring = count.ToString();
        countText.text = $"Value Collected Trash: {countstring}$";

        if (count >= 1500) 
        {
            // Activate'winText'
            winTextObject.SetActive(true);
        }
    }

    //Timer, adjustable on unity
    void SetTimeLeftText()
    {
        string timeLeftString = Mathf.CeilToInt(timeLeft).ToString();
        timeLeftText.GetComponent<TextMeshProUGUI>().text = $"Time Left: {timeLeftString}s";
    }

    //OnTriggerEnter to detect collision.
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter called");

        //If player object hit planet trash
        if (other.gameObject.CompareTag("PT")) 
        {
            // Destroy trash
            other.gameObject.SetActive(false);
			// Add 100 to the score variable 'count'
			count = count + 100;

			// Run the 'SetCountText()' function 
			SetCountText ();
        }
        //If player object hit coca-cola can
        if (other.gameObject.CompareTag("CC")) 
        {
            // Destroy CC
            other.gameObject.SetActive(false);
			// Add 200 to the score variable 'count'
			count = count + 200;

			// Run the 'SetCountText()' function 
			SetCountText ();
        }

        //If player object hit Moon trash
        if (other.gameObject.CompareTag("MT")) 
        {
            other.gameObject.SetActive(false);
			// Add 300 to the score variable 'count'
			count = count + 300;

			// Run the 'SetCountText()' function
			SetCountText ();
        }

        //If player object hit any non-trash
        if (other.gameObject.CompareTag("BP")) 
        {
            // Find the collision coordinate
            Vector3 trashPos = other.ClosestPoint(transform.position);// + Random.insideUnitSphere * 10f;
            // Instantiate trash from the collision
            Instantiate(spaceTrashPrefab, trashPos, Quaternion.identity);
            // Instantiate explosion effect from the collision
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            // Penalty 50 to the score variable 'count'
			count = count - 50;
            // Turn on hit message
            hitTextObject.SetActive(true);
			// Run the 'SetCountText()' function (see below)
			SetCountText ();
        }
        else
        {
            //Keep hit message off
            hitTextObject.SetActive(false);
        }

    }

    //Boost method
    public void BoostStart()
    {
        boost = true;//Set flag to true, update() will do the boost
        currentSpeed += thrustSpeed;//basic math
        thrustStartTime = Time.time;//get start time
    }

    //CollectSpaceTrash method
    void CollectSpaceTrash(GameObject trash)
    {
        // Change the color of the trash to indicate success
        trash.GetComponent<MeshRenderer>().material.color = successColor;

        // Increment the score by 100
        count += 100;
        SetCountText ();

        // Play the success sound
        audioSource.PlayOneShot(successSound);

        // Move the trash towards the spacecraft
        Vector3 direction = transform.position - trash.transform.position;
        trash.GetComponent<Rigidbody>().velocity = direction.normalized * beamSpeed;

        //Destroy trash once its out of camera view
        if (!meshRenderer.isVisible)
        {
            trash.gameObject.SetActive(true);
        }
    }

    // If player touch planet/moon/satellites
    void DecrementScore(GameObject nontrash)
    {
        // Change the color of the trash to indicate success
        nontrash.GetComponent<MeshRenderer>().material.color = failureColor;

        // Decrement the score by 50
        count -= 50;
        SetCountText ();
        
        // Play the failure sound
        audioSource.PlayOneShot(failureSound);

    }


void Update()
{
    //Keep track of time
    timeLeft -= Time.deltaTime;
    SetTimeLeftText();

    // Check if the time has run out
    if (timeLeft <= 0)
    {
        // turn on lose text
        loseTextObject.SetActive(true);
    }

    //If player touch
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
    {
            // Cast a ray from the camera to the touch position
            Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object is space trash
                if (hit.collider.CompareTag("PT")||hit.collider.CompareTag("CC"))
                {
                    CollectSpaceTrash(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("BP"))
                {
                    // Touched non-space-trash, decreases the score
                    DecrementScore(hit.collider.gameObject);
                }
            }
    }
    else
    {
        joystickController.SetActive(true);
        // Move the player object with a joystick
        movementY = Mathf.Clamp(movementY, -45f, 45f);//strict joystick move
        movementX = Mathf.Clamp(movementX, -45f, 45f);//strict joystick move
        transform.Rotate(-movementY, movementX, 0);

        // Handle boost button input
        if (boost)
        {
            //Debug.Log("boosting");
            float timeSinceThrust = Time.time - thrustStartTime;
            currentSpeed += thrustSpeed;
            thrustStartTime = Time.time;
            rb.velocity = transform.forward * Mathf.Lerp(speed, currentSpeed, timeSinceThrust / thrustDuration);
            boost = false;
        }

        // Update the spacecraft's speed based on the current speed and thrust effect
        if (currentSpeed > speed)
        {
            float timeSinceThrust = Time.time - thrustStartTime;
            if (timeSinceThrust < thrustDuration)
            {
                // Apply acceleration during the thrust duration
                rb.velocity = transform.forward * Mathf.Lerp(speed, currentSpeed, timeSinceThrust / thrustDuration);
            }
            else
            {
                // Apply deceleration after the thrust duration
                currentSpeed = speed;
            }
        }
        else
        {
            // Apply the constant speed
            rb.velocity = transform.forward * speed;
            boost = false;
        }

    }
}


}



