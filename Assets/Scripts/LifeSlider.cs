using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeSlider : MonoBehaviour
{

    public float lifeTime = 10;
    public bool increaseWrongMatch = false;

    private Slider lifeSlider;
    private WheelRotation wheelRotation;

    private float timeKeeper;
    private int correctMatches;
    private int previousCorrectMatches;
    private int incorrectMatches;
    private int previousIncorrectMatches;

    // Use this for initialization
    void Start()
    {
        GameObject temp = GameObject.Find("lifeSlider");
        GameObject temp_1 = GameObject.Find("wheel_01");

        if (temp != null) { lifeSlider = temp.GetComponent<Slider>();}
        if (lifeSlider != null) { lifeSlider.value = 0.5f; }

        if (temp_1 != null) { wheelRotation = temp.GetComponent<WheelRotation>(); }
        if (wheelRotation != null) 
        { 
            correctMatches = wheelRotation.match_count;
            previousCorrectMatches = correctMatches;
            incorrectMatches = wheelRotation.mismatched_count;
            previousIncorrectMatches = incorrectMatches;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeKeeper += Time.deltaTime;
        lifeSlider.value = (lifeTime - timeKeeper) / lifeTime;

        if (correctMatches > previousCorrectMatches)
        {
            timeKeeper -= 5;
        }

        if (incorrectMatches > previousIncorrectMatches)
        {
            timeKeeper += 5;
        }

        if (timeKeeper >= lifeTime)
        {
            Debug.Log("Game Over Sucka");
        }

        previousIncorrectMatches = correctMatches;
        previousIncorrectMatches = incorrectMatches;
    }

    public void decreaseSlider()
    {
        timeKeeper += 5;
    }

    public void increaseSlider()
    {
        timeKeeper -= 5;
    }
}
