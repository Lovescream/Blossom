using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ScreenManager : CoreManager {

    #region Properties

    public float CameraYSize => 2 * _camera.orthographicSize;
    public float CameraXSize => CameraYSize * _camera.aspect;
    public float UIRatio => CameraYSize / Screen.height;
    public Vector2 CamMin => (Vector2)_camera.transform.position - new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
    public Vector2 CamMax => (Vector2)_camera.transform.position + new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);

    #endregion

    #region Fields

    private Camera _camera;

    #endregion

    #region Initialize

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        Reset();

        return true;
    }

    #endregion

    public void Reset() {
        _camera = Camera.main;
    }

    public float CameraToWorld(float camLength) => camLength * UIRatio;

}