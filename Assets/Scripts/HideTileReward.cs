using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HideTileReward : MonoBehaviour {

    private Animator vaultLeftAnimation;        // Contains the left vault door animation clips
    private Animator vaultRightAnimation;       // Contains the right vault door animation clips

    private bool hide = false;                  // Used to start reward time counter
    private float rewardTime = 0;               // Counter used to determine if the rewardEffectTime has been reached
    private float rewardEffectTime = 5.0f;      // How long the effect lasts for in seconds

    public GameObject vault;                    // Needs to be public to setActive on/off so that default animation plays when it turns on

    private slotManager slots;

	// Get reference to animator components
    void Start () 
    {
        GameObject temp = GameObject.Find("vault_image_left");
        if (temp != null) { vaultLeftAnimation = temp.GetComponent<Animator>(); }

        GameObject temp_1 = GameObject.Find("vault_image_right");
        if (temp_1 != null) { vaultRightAnimation = temp_1.GetComponent<Animator>(); }

        GameObject temp_2 = GameObject.Find("slotManager");
        if (temp_2 != null) { slots = temp_2.GetComponent<slotManager>(); }

        vault.SetActive(false);
    }
	
    // TODO: Figure out why this isn't working
    // Hide the tiles for effect duration then play open vault animation
    void Update () 
    {

        if (hide && !slots.inMiniGame)
        {
            rewardTime += Time.deltaTime;
            if (rewardTime >= rewardEffectTime)
            {
                hide = false;
                StartCoroutine(revealTileRoutine());
            }
        }
    }

    // The default animation is played everytime the vault GameObject is set to active
    public void hideTile()
    {
        vault.SetActive(true);
        hide = true;
    }

    // Play open vault animation and then reset all variables
    IEnumerator revealTileRoutine()
    {
        vaultLeftAnimation.SetBool("unHide", true);
        vaultRightAnimation.SetBool("unHide", true);

        yield return new WaitForSeconds(2);

        vaultLeftAnimation.SetBool("unHide", false);
        vaultRightAnimation.SetBool("unHide", false);

        vault.SetActive(false);
        rewardTime = 0;
    }
}