using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Achievements : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    // Unlock the first achievement
    public static void unlockFirstAchievement()
    {
        string first_achievement_id = GPGS.achievement_upandcomer;
        Social.ReportProgress(first_achievement_id, 100.0f, (bool success) =>
        {
            if (success) 
            {
                Debug.Log("Up-and-comer achievement unlocked!");
            } 
            
            else 
            {
                Debug.Log("Failed to unlock Up-and-comer achievement.");
            }
        });
    }


    // Unlock the bronze achievement
    public static void bronzeAchievement()
    {
        string bronze_achievement_id = GPGS.achievement_moving_on_up;
        Social.ReportProgress(bronze_achievement_id, 100.0f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Bronze achievement unlocked!");
            }

            else
            {
                Debug.Log("Failed to unlock bronze achievement.");
            }
        });
    }


    // Unlock the silver achievement
    public static void silverAchievement()
    {
        string silver_achievement_id = GPGS.achievement_professional_reflexes;
        Social.ReportProgress(silver_achievement_id, 100.0f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Silver achievement unlocked!");
            }

            else
            {
                Debug.Log("Failed to unlock silver achievement.");
            }
        });
    }


    // Unlock the silver achievement
    public static void goldAchievement()
    {
        string gold_achievement_id = GPGS.achievement_i_do_this_for_a_living;
        Social.ReportProgress(gold_achievement_id, 100.0f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Gold achievement unlocked!");
            }

            else
            {
                Debug.Log("Failed to unlock gold achievement.");
            }
        });
    }


    // Unlock the silver achievement
    public static void finalAchievement()
    {
        string final_achievement_id = GPGS.achievement_mayhem;
        Social.ReportProgress(final_achievement_id, 100.0f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Final achievement unlocked!");
            }

            else
            {
                Debug.Log("Failed to unlock final achievement.");
            }
        });
    }
}
