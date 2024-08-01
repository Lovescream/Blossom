using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class UI_Panel_Achievements : UI_Panel {

    #region Properties

    public GameScene Scene { get; private set; }

    #endregion

    #region Fields

    // Collections.
    private UI_Achievement[] _achievements;

    // Components.
    private UI_Button _btnBackground;
    private Transform _achievementParents;
    private UI_Text _txtLog;
    private UI_Button _btnShowUI;
    private UI_Button _btnClose;

    #endregion

    #region Initialized / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _btnBackground = this.gameObject.FindChild<UI_Button>("Background");
        _achievementParents = this.gameObject.FindChild<Transform>("Achievements");
        _txtLog = this.gameObject.FindChild<UI_Text>("txtLog");
        _btnShowUI = this.gameObject.FindChild<UI_Button>("btnShowUI");
        _btnClose = this.gameObject.FindChild<UI_Button>("btnClose");

        _btnBackground.SetEvent(OnBtnClose);
        _btnShowUI.SetEvent(OnBtnShowUI);
        _btnClose.SetEvent(OnBtnClose);

        _achievements = new UI_Achievement[6];
        for (int i = 0; i < 6; i++) {
            _achievements[i] = _achievementParents.GetChild(i).GetComponent<UI_Achievement>();
        }

        return true;
    }

    public void SetInfo(GameScene scene, IAchievement[] achievements, IAchievementDescription[] descriptions) {
        Initialize();

        this.Scene = scene;
        scene.OnChangedLog -= OnChangedLog;
        scene.OnChangedLog += OnChangedLog;
        OnChangedLog(scene.Log);

        for (int i = 0; i < 6; i++) _achievements[i].SetInfo(scene, achievements[i], descriptions[i]);
    }

    public void ActiveButton() {
        for (int i = 0; i < 6; i++) _achievements[i].ActiveButton();
    }

    public void DeactiveButton() {
        for (int i = 0; i < 6; i++) _achievements[i].DeactiveButton();
    }

    #endregion

    #region Events

    private void OnChangedLog(string log) {
        _txtLog.Text = log;
    }

    private void OnBtnShowUI() {
        PlayGamesPlatform.Instance.ShowAchievementsUI(status => {
            Scene.Log += $"Google Play Achievement UI Closed. ({status})\n";
        });
    }

    private void OnBtnClose() {
        Scene.SetUIActive(true);
        this.Close();
    }

    #endregion


}