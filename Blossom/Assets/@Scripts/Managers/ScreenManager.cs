using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ScreenManager : CoreManager {

    #region Properties

    public Vector2 Resolution { get; private set; }
    public float ScreenRatio { get; private set; }
    public float CameraSize { get; private set; }

    public Camera Cam { get; private set; }

    public Vector2 CamMin => (Vector2)Cam.transform.position - new Vector2(CameraSize * ScreenRatio, CameraSize);
    public Vector2 CamMax => (Vector2)Cam.transform.position + new Vector2(CameraSize * ScreenRatio, CameraSize);


    #endregion

    #region Initialize

    public override bool Initialize() {
        if (!base.Initialize()) return false;

#if UNITY_IOS || UNITY_ANDROID
        Application.targetFrameRate = 30;
#endif

        Reset();

        return true;
    }

    #endregion

    public void Reset() {
        Cam = Camera.main;

        this.Resolution = new(1080f, 1920f);
        this.ScreenRatio = Resolution.x / Resolution.y;
        this.CameraSize = Cam.orthographicSize;
    }

}