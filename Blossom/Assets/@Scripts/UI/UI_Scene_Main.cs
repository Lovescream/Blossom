using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Main : UI_Scene {

    #region Properties

    public MainScene Scene { get; private set; }

    #endregion

    #region Fields

    private bool _isAuthenticated;

    private UI_Text _txtTitle;
    private UI_Image _log;
    private UI_Text _txtLog;
    private UI_Button _btnEnter;

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _isAuthenticated = false;

        _txtTitle = this.gameObject.FindChild<UI_Text>("txtTitle");
        _log = this.gameObject.FindChild<UI_Image>("Log");
        _txtLog = this.gameObject.FindChild<UI_Text>("txtLog");
        _btnEnter = this.gameObject.FindChild<UI_Button>("btnEnter");
        _btnEnter.SetEvent(OnBtnEnter);

        return true;
    }

    public void SetInfo(MainScene scene) {
        this.Scene = scene;

        Scene.OnChangeLog -= OnChangedLog;
        Scene.OnChangeLog += OnChangedLog;
        Scene.OnAuthenticated -= OnAuthenticated;
        Scene.OnAuthenticated += OnAuthenticated;
        OnChangedLog(Scene.Log);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_txtTitle.transform.DOScale(0, 0))
            .Join(_log.transform.DOScale(0, 0))
            .Join(_btnEnter.transform.DOScale(0, 0))
            .Append(_txtTitle.transform.DOScale(1, 0.25f))
            .Append(_log.transform.DOScale(1, 0.25f))
            .Append(_btnEnter.transform.DOScale(1, 0.25f))
            .OnComplete(Scene.Authenticate);
    }

    #endregion

    #region Events

    private void OnAuthenticated() {
        _isAuthenticated = true;
        _btnEnter.SetText("Enter the Game");
    }

    private void OnChangedLog(string log) {
        _txtLog.Text = log;
    }
    
    private void OnBtnEnter() {
        if (!_isAuthenticated) Scene.ManuallyAuthenticate();
        else Scene.EnterTheGame();
    }

    #endregion

}