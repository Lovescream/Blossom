using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine;

public class MainScene : Scene {

    #region Properties

    public UI_Scene_Main SceneUI { get; private set; }

    public string Log {
        get => _log;
        set {
            _log = value;
            Debug.Log(value);
            OnChangeLog?.Invoke(value);
        }
    }

    #endregion

    #region Fields

    private string _log;

    public event Action<string> OnChangeLog;
    public event Action OnAuthenticated;

    #endregion

    #region Initialize / Set

    protected override bool Initialize() {
        if (!base.Initialize()) return false;

        Log = $"Blossom Test App Initialized. Version: {Application.version}\n";
        SceneUI = Main.UI.OpenScene<UI_Scene_Main>();
        SceneUI.SetInfo(this);

        return true;
    }

    #endregion

    #region LogIn

    public void Authenticate() {
        Log += $"Try Authenticate.\n";
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void ManuallyAuthenticate() {
        Log += $"Try ManuallyAuthenticate.\n";
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    #endregion

    #region Events

    private void ProcessAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            string id = PlayGamesPlatform.Instance.GetUserId();
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            Log += $"Successfully Logged In. ID: {id}, Name: {name}\n";
            OnAuthenticated?.Invoke();
        }
        else {
            Log += $"Log In Failed. \n";
        }
    }

    #endregion

    public void EnterTheGame() {
        Main.Scene.Load("GameScene");
    }

}