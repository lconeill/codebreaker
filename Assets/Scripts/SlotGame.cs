using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SlotGame : MonoBehaviour
{

    public int rotationSpeed = -200;
    public GameObject[] slotReels;
    public GameObject[] slotButtons;

    private Vector3 rotation = Vector3.zero;
    private string[] reelIcon = new string[7] {"Bell","Fruit Gum","Lemon","Cherry1","Grape","Orange","Cherry2"};
    private bool[] continueRotation = new bool[3] {true, true, true};

    private float currentLerpTime = 0;

    // Use this for initialization
    void Start()
    {
        slotReels[0].transform.eulerAngles = rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (continueRotation[0])
        {
            rotation.x += rotationSpeed * Time.deltaTime;
            slotReels[0].transform.eulerAngles = rotation;
        }

        else
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > 3)
            {
                currentLerpTime = 3;
            }

            smoothStop(rotation);
        }
        
    }

    public void stopSpin(string buttonName)
    {
        char char_index = buttonName[buttonName.Length - 1];
        int index = (int)char.GetNumericValue(char_index);

        float revolutionsRemainder = rotation.x % 360;

        int iconIndex = Math.Abs((int)(revolutionsRemainder/51.4));

        continueRotation[index] = false;

        Debug.Log(reelIcon[iconIndex]);
    }

    public void SendMyName(Button button)
    {
        stopSpin(button.name);
    }

    public void smoothStop(Vector3 rotationWhenStopped)
    {
        float perc = currentLerpTime / 3;
        perc = Mathf.Sin(perc * Mathf.PI * 0.5f);

        float endRotation = System.Convert.ToSingle(rotationWhenStopped.x % 360 % 51.428 + 51.428/2);

        float xrot = Mathf.Lerp(rotationWhenStopped.x, rotationWhenStopped.x - (360 + endRotation), perc);

        slotReels[0].transform.eulerAngles = new Vector3 (xrot, 0, 0);
    }
}