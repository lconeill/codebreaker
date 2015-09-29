﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class slotManager : MonoBehaviour
{
    public GameObject slotPanel;
    public SlotGame[] slotReels;
    public bool inMiniGame = true;

    private string[] slotResults = new string[3]{"","",""};
    private int count = 0;
    private bool threeMatch, noMatch;
    public RewardManager sendIconResult;

    // Deactivate the slot game by default
    void Start()
    {
        activateSlotGame(false);
    }

    // Checks if all three reels have been pressed and determines the reward
    void Update()
    {
        if (count == 3)
        {
            getReward();
            count++;
        }
    }

    public void activateSlotGame(bool activate)
    {
        inMiniGame = !inMiniGame;

        slotPanel.SetActive(activate);

        for (int i = 0; i <= 2; i++)
        {
            slotReels[i].enabled = activate;
            slotReels[i].GetComponent<Renderer>().enabled = activate;
        }

        //if (activate)
        //{
        //    inMiniGame = true;
        //}
        
    }

    // Gets the results from SlotGame objects 
    public void getResults(string individualResult)
    {
        slotResults[count] = individualResult;
        count++;
    }

    public void getReward()
    {
        threeMatch = slotResults[0].Equals(slotResults[1]) && slotResults[1].Equals(slotResults[2]);
        noMatch = !(slotResults[0].Equals(slotResults[1]) || slotResults[1].Equals(slotResults[2]) || slotResults[0].Equals(slotResults[2]));

        if (threeMatch)
        {
            sendIconResult.returnReward(slotResults[0]);
        }

        else if (noMatch)
        {
            sendIconResult.returnReward("noMatch");
        }

        else
        {
            sendIconResult.returnReward("twoMatch");
        }

        activateSlotGame(false);
    }
}
