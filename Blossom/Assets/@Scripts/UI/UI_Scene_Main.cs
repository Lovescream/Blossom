using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Main : UI_Scene {

    #region Properties

    public MainScene Scene { get; private set; }

    #endregion

    #region Fields

    private UI_Text _txtTitle;
    private UI_Image _log;
    private UI_Text _txtLog;
    private UI_Button _btnLogIn;

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _txtTitle = this.gameObject.FindChild<UI_Text>("txtTitle");
        _log = this.gameObject.FindChild<UI_Image>("Log");
        _txtLog = this.gameObject.FindChild<UI_Text>("txtLog");
        _btnLogIn = this.gameObject.FindChild<UI_Button>("btnLogIn");
        _btnLogIn.SetEvent(OnBtnLogIn);

        return true;
    }

    public void SetInfo(MainScene scene) {
        this.Scene = scene;

        Scene.OnChangeLog -= OnChangedLog;
        Scene.OnChangeLog += OnChangedLog;
        OnChangedLog(Scene.Log);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_txtTitle.transform.DOScale(0, 0))
            .Join(_log.transform.DOScale(0, 0))
            .Join(_btnLogIn.transform.DOScale(0, 0))
            .Append(_txtTitle.transform.DOScale(1, 0.25f))
            .Append(_log.transform.DOScale(1, 0.25f))
            .Append(_btnLogIn.transform.DOScale(1, 0.25f))
            .OnComplete(Scene.Authenticate);
    }

    #endregion

    #region Events

    private void OnChangedLog(string log) {
        _txtLog.Text = log;
    }

    private void OnBtnLogIn() {
        Scene.ManuallyAuthenticate();
    }

    #endregion

}