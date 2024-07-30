using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPGSManager : MonoBehaviour {

    #region Properties

    #endregion

    #region Fields

    private UI_Text _txtLog;

    #endregion

    #region MonoBehaviours

    void Start() {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        
    }

    #endregion

    #region LogIn

    public void SignIn() {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    #endregion

    internal void ProcessAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            // Continue with Play Game Services
            // Perfectly login success

            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

            _txtLog.Text = $"Success {name}";
        }
        else {
            _txtLog.Text = $"Sign in Failed!";
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            // Login failed
        }
    }

}