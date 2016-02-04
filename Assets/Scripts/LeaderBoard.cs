using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

// Leaderboard ID: CgkIkNLrku8MEAIQAQ

public class LeaderBoard : MonoBehaviour {

    public string leaderboard;

    void Start ()
    {
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate ();
    }

    // Login In Into Your Google+ Account
    public void LogIn ()
    {
        Social.localUser.Authenticate ((bool success) =>
        {
            if (success) {
                Debug.Log ("Login Sucess");
            } else {
                Debug.Log ("Login failed");
            }
        });
    }


    // Shows All Available Leaderborad
    public void OnShowLeaderBoard ()
    {
        Social.ShowLeaderboardUI(); // Show all leaderboard
        //((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (leaderboard); // Show current (Active) leaderboard
    }


    // Adds Score To leader board
    public void OnAddScoreToLeaderBoard ()
    {
        if (Social.localUser.authenticated) {
            Social.ReportScore (ScoreLogic.the_score, leaderboard, (bool success) =>
            {
                if (success) {
                    Debug.Log ("Update Score Success");
                    
                } else {
                    Debug.Log ("Update Score Fail");
                }
            });
        }
    }


    // On Logout of your Google+ Account
    public void OnLogOut ()
    {
        ((PlayGamesPlatform)Social.Active).SignOut ();
    }
}