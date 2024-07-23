using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene : UI {

    #region Fields

    protected Canvas _canvas;
    protected CanvasScaler _scaler;

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _canvas = this.gameObject.GetOrAddComponent<Canvas>();
        _scaler = this.gameObject.GetOrAddComponent<CanvasScaler>();
        SetCanvas();

        return true;
    }

    protected virtual void SetCanvas() {
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _canvas.overrideSorting = true;

        _scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        _scaler.referenceResolution = Main.Screen.Resolution;
    }

    protected virtual void SetOrder() => _canvas.sortingOrder = 0;

    #endregion

}