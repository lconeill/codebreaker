using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


// Solution for preserving aspect ration and supporting multiple resolutions and multiple aspect ratios
// Create a button image that is exactly the same aspect ratio as the button in the slot game
// Then drop that image into the button and turn on preserve aspect ratio
public class SlotGame : MonoBehaviour
{

    public Image[] powerup_images;
    private Image[] image_clone = new Image[3];

    private Vector2[] defaultAnchorPositions = new Vector2[6] { new Vector2(0, 0), new Vector2(1, 1), 
                                                                new Vector2(0, 0), new Vector2(1, 1),
                                                                new Vector2(0, 0), new Vector2(1, 1),
                                                              };
    // Based on WXGA 1280 x 800 resolution
    private Vector2[] defaultRectPositions = new Vector2[6]{ new Vector2(16.5f, 119.5f), new Vector2(-16.5f, 408.5f), //positive number
                                                             new Vector2(16.5f, -144.5f), new Vector2(-16.5f, 144.5f),
                                                             new Vector2(16.5f, -408.5f), new Vector2(-16.5f, -119.5f),
                                                           };

    private int index_tracker;
    private float start_lerp_timer = 0.0f;  // Timer that determines when to exit lerp function
    private float lerp_duration = 1.0f;     // How long to lerp for
    private float wheel_speed = 9.5f;
    public slotManager captureResult;
    private bool startRoll = false;
    private bool continueRoll = false;
    private bool stopRoll = false;
    private float parent_transform = 0.0f;

    public Button roll_button;

    // Use this for initialization
    void Start()
    {
        // initializeWheel();
        gameObject.GetComponent<Button>().enabled = false;
        roll_button.enabled = true;
    }

    // Wheel needs to be initialized each time the slot game is enabled
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
            roll_button.enabled = false;

            if (start_lerp_timer > lerp_duration)
            {
                startRoll = false;
                continueRoll = true;
                start_lerp_timer = 0;

                gameObject.GetComponent<Button>().enabled = true;
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

    public void initializeWheel()
    {
        index_tracker = UnityEngine.Random.Range(0, 7);
        int index_one = (index_tracker + 1 == 7) ? 0 : index_tracker + 1;
        int index_two = (index_tracker + 2 >= 7) ? Math.Abs(7 - (index_tracker + 2)) : index_tracker + 2;

        int[] index_array = { index_tracker, index_one, index_two };
        int j = 0;

        for (int i = 0; i < 3; i++)
        {
            image_clone[i] = Instantiate(powerup_images[index_array[i]]) as Image;
            image_clone[i].transform.SetParent(gameObject.transform, false);

            image_clone[i].rectTransform.anchorMin = defaultAnchorPositions[j];
            image_clone[i].rectTransform.anchorMax = defaultAnchorPositions[j + 1];

            image_clone[i].rectTransform.offsetMax = defaultRectPositions[j];
            image_clone[i].rectTransform.offsetMin = defaultRectPositions[j + 1];

            j = j + 2;
        }
    }

    public void moveWheel(float speed)
    {
        for (int i = 0; i < 3; i++)
        {
            image_clone[i].transform.position -= new Vector3(0, speed, 0);
        }

        // Must check the position of clone zero - it is the only clone whose position from its anchors will be consistent
        if (image_clone[0].rectTransform.offsetMax.y < -26.5)
        {
            swapClones();
        }
    }

    public void swapClones()
    {
        Destroy(image_clone[2].gameObject);

        // Order is important!
        image_clone[2] = image_clone[1];
        image_clone[1] = image_clone[0];

        // Instantiate new power up item
        index_tracker = (index_tracker == 0) ? 6 : index_tracker - 1;

        image_clone[0] = Instantiate(powerup_images[index_tracker]) as Image;
        image_clone[0].transform.SetParent(gameObject.transform, false);

        image_clone[0].rectTransform.anchorMin = defaultAnchorPositions[0];
        image_clone[0].rectTransform.anchorMax = defaultAnchorPositions[1];

        image_clone[0].rectTransform.offsetMax = new Vector2(0, 222.5f);
        image_clone[0].rectTransform.offsetMin = new Vector2(0, 511.5f);
    }


    // This should be called when the button over the slot reel is pressed
    public void stopSpin()
    {
        continueRoll = false;
        stopRoll = true;

        gameObject.GetComponent<Button>().interactable = false;

        string full_name = image_clone[1].name;
        char[] delimter = { '_' };
        string[] parsed_name = full_name.Split(delimter);
        string selectedIcon = parsed_name[0];


        // TODO: Determine if the names are still correct
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

        Vector2 image_clone0_offsetMax_lerp_value = Vector2.Lerp(image_clone[0].rectTransform.offsetMax, defaultRectPositions[0], perc);
        Vector2 image_clone0_offsetMin_lerp_value = Vector2.Lerp(image_clone[0].rectTransform.offsetMin, defaultRectPositions[1], perc);

        Vector2 image_clone1_offsetMax_lerp_value = Vector2.Lerp(image_clone[1].rectTransform.offsetMax, defaultRectPositions[2], perc);
        Vector2 image_clone1_offsetMin_lerp_value = Vector2.Lerp(image_clone[1].rectTransform.offsetMin, defaultRectPositions[3], perc);

        Vector2 image_clone2_offsetMax_lerp_value = Vector2.Lerp(image_clone[2].rectTransform.offsetMax, defaultRectPositions[4], perc);
        Vector2 image_clone2_offsetMin_lerp_value = Vector2.Lerp(image_clone[2].rectTransform.offsetMin, defaultRectPositions[5], perc);

        image_clone[0].rectTransform.offsetMax = image_clone0_offsetMax_lerp_value;
        image_clone[0].rectTransform.offsetMin = image_clone0_offsetMin_lerp_value;

        image_clone[1].rectTransform.offsetMax = image_clone1_offsetMax_lerp_value;
        image_clone[1].rectTransform.offsetMin = image_clone1_offsetMin_lerp_value;

        image_clone[2].rectTransform.offsetMax = image_clone2_offsetMax_lerp_value;
        image_clone[2].rectTransform.offsetMin = image_clone2_offsetMin_lerp_value;
    }


    // TODO: Look into what needs to be reset
    public void resetReel()
    {
        stopRoll = false;
        startRoll = false;
        continueRoll = false;
        start_lerp_timer = 0;
        roll_button.enabled = true;
        gameObject.GetComponent<Button>().enabled = false;

        for (int i = 0; i < 3; i++)
        {
            Destroy(image_clone[i].gameObject);
        }
    }
}