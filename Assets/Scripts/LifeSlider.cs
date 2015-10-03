using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeSlider : MonoBehaviour
{

    public float lifeTime = 10;
    public bool decrease = false;
    public bool increase = false;


    private Slider lifeSlider;
    private float timeKeeper;


    // Use this for initialization
    void Start()
    {
        GameObject temp = GameObject.Find("lifeSlider");

        if (temp != null)
        {
            lifeSlider = temp.GetComponent<Slider>();
        }

        if (lifeSlider != null)
        {
            lifeSlider.value = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeKeeper += Time.deltaTime;
        lifeSlider.value = (lifeTime - timeKeeper) / lifeTime;

        if (increase)
        {
            increaseSlider();
        }

        if (decrease)
        {
            decreaseSlider();
        }

        if (timeKeeper >= lifeTime)
        {
            Debug.Log("Game Over Sucka");
        }
    }

    public void decreaseSlider()
    {
        timeKeeper += 5;
        decrease = false;
    }

    public void increaseSlider()
    {
        timeKeeper -= 5;
        increase = false;
    }
}
