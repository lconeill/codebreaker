using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SlotGame : MonoBehaviour
{

    public Image[] powerup_images;
    public GameObject slotButton;
    private Image[] image_clone = new Image[3];
    private int index_tracker;
    private Vector2[] defaultPositions = new Vector2[3] { new Vector2(0, 200), new Vector2(0, 0), new Vector2(0, -200) };

    private float start_lerp_timer = 0.0f;  // Timer that determines when to exit lerp function
    private float lerp_duration = 1.0f;     // How long to lerp for
    private float wheel_speed = 12.5f;

    public slotManager captureResult;
    private bool startRoll = false;
    private bool continueRoll = false;
    private bool stopRoll = false;
    private float parent_transform = 0.0f;

    // Use this for initialization
    void Start()
    {
        //initializeWheel();
        slotButton.GetComponent<Button>().enabled = false;
        parent_transform = transform.position.x; // This script needs to be on the button that's clicked for this to work
    }

    // This is called everytime the gameobject is enabled
    void OnEnable()
    {
        initializeWheel();
    }

    // Update is called once per frame
    void Update()
    {

        if (startRoll)
        {
            start_lerp_timer += Time.deltaTime;

            if (start_lerp_timer > lerp_duration)
            {
                startRoll = false;
                continueRoll = true;
                start_lerp_timer = 0;
                slotButton.GetComponent<Button>().enabled = true;
            }

            smoothStart();
        }

        else if (continueRoll)
        {
            moveWheel(wheel_speed);
        }

        else if (stopRoll)
        {
            start_lerp_timer += Time.deltaTime;

            if (start_lerp_timer > lerp_duration)
            {
                stopRoll = false;
            }

            smoothStop();
        }
    }

    // This is called when the roll button is pressed
    public void startSpinSwtich()
    {
        startRoll = true;
    }

    // Randomizes slot wheel icons and instatiates buttons in correct position
    public void initializeWheel()
    {
        index_tracker = UnityEngine.Random.Range(0, 7);
        int index_one = (index_tracker + 1 == 7) ? 0 : index_tracker + 1;
        int index_two = (index_tracker + 2 >= 7) ? Math.Abs(7 - (index_tracker + 2)) : index_tracker + 2;

        int[] index_array = { index_tracker, index_one, index_two };

        for (int i = 0; i < 3; i++)
        {
            image_clone[i] = (Image)Instantiate(powerup_images[index_array[i]], defaultPositions[i], Quaternion.identity);
            image_clone[i].transform.SetParent(slotButton.transform, false);
            image_clone[i].enabled = false;
        }
    }

    // Move the icons down as if the slot wheel was rotating
    public void moveWheel(float speed)
    {
        for (int i = 0; i < 3; i++)
        {
            image_clone[i].transform.position -= new Vector3(0, speed, 0);
        }

        //Debug.Log(image_clone[2].transform.position);
        //Debug.Log(image_clone[2].transform.parent.transform.position);

        if (image_clone[0].transform.position.y <= 405)
        {
            Destroy(image_clone[2].gameObject); // Had to add ".gameObject" otherwise it would not be destroyed
            image_clone[2] = image_clone[1];
            image_clone[1] = image_clone[0];

            index_tracker = (index_tracker == 0) ? 6 : index_tracker - 1;

            image_clone[0] = (Image)Instantiate(powerup_images[index_tracker], new Vector2(0, 290), Quaternion.identity);
            image_clone[0].transform.SetParent(slotButton.transform, false);

            image_clone[0].enabled = false;
            image_clone[1].enabled = false;
            image_clone[2].enabled = false;
        }
    }

    // This should be called when the button over the slot reel is pressed
    public void stopSpin()
    {
        continueRoll = false;
        stopRoll = true;
        slotButton.GetComponent<Button>().interactable = false;

        string full_name = image_clone[1].name;
        char[] delimter = { '_' };
        string[] parsed_name = full_name.Split(delimter);
        string selectedIcon = parsed_name[0];

        captureResult.getResults(selectedIcon);
    }

    // Smoothly starts the slot wheel movement when the roll button is pressed
    public void smoothStart()
    {
        float perc = start_lerp_timer / lerp_duration;
        perc = 1 - Mathf.Cos(perc * Mathf.PI * 0.5f);

        float speed = Mathf.Lerp(0, wheel_speed, perc);

        moveWheel(speed);
    }

    public void smoothStop()
    {
        float perc = start_lerp_timer / lerp_duration;
        perc = Mathf.Sin(perc * Mathf.PI * 0.5f);

        float button_location_0 = Mathf.Lerp(image_clone[0].transform.position.y, 508.5f, perc);
        float button_location_1 = Mathf.Lerp(image_clone[1].transform.position.y, 317, perc);
        float button_location_2 = Mathf.Lerp(image_clone[2].transform.position.y, 108.5f, perc);

        image_clone[0].transform.position = new Vector3(parent_transform + 110, button_location_0, 0);
        image_clone[1].transform.position = new Vector3(parent_transform + 110, button_location_1, 0);
        image_clone[2].transform.position = new Vector3(parent_transform + 110, button_location_2, 0);
    }

    // TODO: Look into what needs to be reset
    public void resetReel()
    {
        stopRoll = false;
        startRoll = false;
        continueRoll = false;
        start_lerp_timer = 0;

        for (int i = 0; i < 3; i++)
        {
            Destroy(image_clone[i].gameObject);
        }
    }
}