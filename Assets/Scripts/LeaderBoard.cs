using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

// Leaderboard ID: CgkIkNLrku8MEAIQAQ

public class LeaderBoard : MonoBehaviour {

    public string leaderboard;
    public bool showLogIn = true;
    public GameObject login_game_object;
    public GameObject logout_game_object;

    void Start ()
    {
        // moved to achievements class script
        // recommended for debugging:
        //PlayGamesPlatform.DebugLogEnabled = true;
        
        // Activate the Google Play Games platform
        //PlayGamesPlatform.Activate ();

        // Check if the user is already logged in
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            showLogIn = false;
        }
    }

    // Login In Into Your Google+ Account
    public void LogIn ()
    {
        Social.localUser.Authenticate ((bool success) =>
        {
            if (success) 
            {
                Debug.Log ("Login Sucess");
                showLogIn = false;
                swapLogButtons();
            } 
            
            else 
            {
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
        showLogIn = true;
        swapLogButtons();
    }


    // On successful log-in swap button to log-out
    // On successful log-out swap button to log-in
    public void swapLogButtons()
    {
        if (showLogIn)
        {
            login_game_object.SetActive(true);
            logout_game_object.SetActive(false);
        }

        else
        {
            login_game_object.SetActive(false);
            logout_game_object.SetActive(true);
        }
    }
}