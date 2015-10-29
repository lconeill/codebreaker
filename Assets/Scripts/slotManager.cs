using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class slotManager : MonoBehaviour
{
    public GameObject slotPanel;
    public SlotGame[] slotReels;
    public bool inMiniGame = false;

    private string[] slotResults = new string[3]{"","",""};
    private int count = 0;
    private bool threeMatch, noMatch;
    public RewardManager sendIconResult;

    private Animator slotPanelAnimation;
    
	private GameObject SlotGameTheme_01_ref;
	
	private AudioSource SlotGameTheme_01;
	
	private GameObject MainTheme_01_ref;
	
	private AudioSource MainTheme_01;
	
	private GameObject SlotGameWin_01_ref;
	
	private AudioSource SlotGameWin_01;
	
	private GameObject SlotGameLose_01_ref;
	
	private AudioSource SlotGameLose_01;

    // Deactivate the slot game by default
    void Start()
    {
        activateSlotGame(false);
        
		SlotGameTheme_01_ref = GameObject.Find("SlotGameTheme_01");
		SlotGameTheme_01 = SlotGameTheme_01_ref.GetComponent<AudioSource>();
		
		MainTheme_01_ref = GameObject.Find("MainTheme_01");
		MainTheme_01 = MainTheme_01_ref.GetComponent<AudioSource>();
		
		SlotGameWin_01_ref = GameObject.Find("SlotGameWin_01");
		SlotGameWin_01 = SlotGameWin_01_ref.GetComponent<AudioSource>();
		
		SlotGameLose_01_ref = GameObject.Find("SlotGameLose_01");
		SlotGameLose_01 = SlotGameLose_01_ref.GetComponent<AudioSource>();

    }

    // Checks if all three reels have been pressed and determines the reward
    void Update()
    {
        if (count == 3)
        {
            getReward();
            count = 0;
        }
    }

    public void activateSlotGame(bool activate)
    {

        // This needs to be at the top so that reference to the animator
        // can be reached when activate is true
        slotPanel.SetActive(activate);

        if (activate)
        {
			MainTheme_01.Stop();
			SlotGameTheme_01.Play();
			
            inMiniGame = true;

            GameObject temp = GameObject.Find("slotGamePanel");
            slotPanelAnimation = temp.GetComponent<Animator>();
        }

        else
        {   
            inMiniGame = false;
        }

        for (int i = 0; i <= 2; i++)
        {
            slotReels[i].enabled = activate;
            slotReels[i].GetComponent<Renderer>().enabled = activate;
            slotReels[i].slotButton.GetComponent<Button>().interactable = false;
        }
    }

    // Gets the results from SlotGame objects 
    public void getResults(string individualResult)
    {
        slotResults[count] = individualResult;
        count++;
        //Debug.Log(count);
    }

    // Play the scale out animation then deactive the slot game when finished
    IEnumerator playScaleOut()
    {
        yield return new WaitForSeconds(2);
        slotPanelAnimation.SetBool("scale", false);
        yield return new WaitForSeconds(2);
        slotReels[0].resetReel();
        slotReels[1].resetReel();
        slotReels[2].resetReel();
        activateSlotGame(false);
        MainTheme_01.Play();
    }

    public void getReward()
    {
        threeMatch = slotResults[0].Equals(slotResults[1]) && slotResults[1].Equals(slotResults[2]);
        noMatch = !(slotResults[0].Equals(slotResults[1]) || slotResults[1].Equals(slotResults[2]) || slotResults[0].Equals(slotResults[2]));

        if (threeMatch)
        {
			SlotGameTheme_01.Stop();
			SlotGameWin_01.Play();
			//MainTheme_01.PlayDelayed(1.5f);
            sendIconResult.returnReward(slotResults[0]);
        }

        else if (noMatch)
        {
			SlotGameTheme_01.Stop();
			SlotGameLose_01.Play();
			//MainTheme_01.PlayDelayed(1.5f);
            sendIconResult.returnReward("noMatch");
        }

        else
        {
			SlotGameTheme_01.Stop();
			SlotGameLose_01.Play();
			//MainTheme_01.PlayDelayed(1.5f);
            sendIconResult.returnReward("twoMatch");
        }

        //Using coroutine to play the scale out animation
        StartCoroutine(playScaleOut());
        //activateSlotGame(false);
    }
}
