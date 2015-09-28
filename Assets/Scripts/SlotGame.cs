using UnityEngine;
using UnityEngine.UI;
using System;

public class SlotGame : MonoBehaviour
{

    public int rotationSpeed = -200;    //Speed at which the slot reels rotate
    public GameObject slotButton;       //Button used to stop the wheel from rotating
    public int numOfIcons = 7;          //The number of icons on the slot reel
    public String selectedIcon; 

    private Vector3 rotation = Vector3.zero;    //The rotation vector of the slot reel
    private string[] reelIcon = new string[7] {"Bell","Fruit Gum","Lemon","Cherry1","Grape","Orange","Cherry2"};
    private bool continueRotation = true;       //Flag used to stop rotation when the button is pressed
    private Vector3 updateRotation = Vector3.zero;  //Used to update the rotation vector of the slot reel after button is pressed

    private float currentLerpTime = 0;      //Variable that is updated to provide an argument to Math.Lerp
    private int stopSpeed = 3;              //How fast to stop the slot reel after the button is pressed
    private float degreesPerIcon;           //Used to find out which icon we are on when the buttin is pressed

    public slotManager captureResult;

    //Initialize the slot reel starting rotation
    void Start()
    {
        transform.eulerAngles = rotation;
        degreesPerIcon = 360 / numOfIcons;
    }

    // Rotate the slot reel based on Rotationspeed as long as continueRotation flag is true
    // Otherwise call smoothStop
    void Update()
    {
        if (continueRotation)
        {
            rotation.x += rotationSpeed * Time.deltaTime;
            transform.eulerAngles = rotation;
        }

        else
        {
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime > stopSpeed)
            {
                currentLerpTime = stopSpeed;
            }

            smoothStop();
        }
        
    }

    // This should be called when the button is pressed
    public void stopSpin()
    {
        continueRotation = false;
        slotButton.GetComponent<Button>().interactable = false;

        float revolutionsRemainder = rotation.x % 360;
        int iconIndex = Math.Abs((int)(revolutionsRemainder/degreesPerIcon));

        selectedIcon = reelIcon[iconIndex];
        captureResult.getResults(selectedIcon);
    }

    // Smoothly stops the slot reel rotation and centers on the icon the button was pressed on
    public void smoothStop()
    {
        float perc = currentLerpTime / stopSpeed;
        perc = Mathf.Sin(perc * Mathf.PI * 0.5f);

        float endRotation = System.Convert.ToSingle(rotation.x % 360 % degreesPerIcon + degreesPerIcon / 2);

        updateRotation.x = Mathf.Lerp(rotation.x, rotation.x - (360 + endRotation), perc);

        transform.eulerAngles = updateRotation;
    }
}