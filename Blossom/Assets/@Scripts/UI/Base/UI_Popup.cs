using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup : UI {

    #region Properties

    public virtual bool AllowDuplicate => true;
    public float Duration { get; protected set; } = 2f;

    #endregion

    #region Fields

    protected Timer _closeTimer;

    // Components.
    protected Canvas _canvas;
    protected CanvasScaler _scaler;

    #endregion

    #region MonoBehaviours

    protected override void OnEnable() {
        base.OnEnable();
        Clear();
    }

    protected override void Update() {
        base.Update();

        if (_closeTimer != null && !_closeTimer.Update(Time.deltaTime)) _closeTimer = null;
    }

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _rect = this.transform.Find("Popup").GetComponent<RectTransform>();
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

    protected virtual void SetOrder() => _canvas.sortingOrder = Main.UI.OrderUpPopup();
    public void SetOrder(int order) => _canvas.sortingOrder = order;

    #endregion

    #region Popup

    public virtual void SetDuration(float duration = 2f) {
        Duration = duration;
        _closeTimer = new(duration, false, Close);
    }

    public virtual void SetPosition(Vector2 position) {
        // #1. Pivot 설정.
        Vector2 pivot = new((position.x >= Screen.width / 2f) ? 1 : 0, (position.y >= Screen.height / 2f) ? 1 : 0);

        // #2. Popup이 화면 밖으로 벗어나지 않도록 위치 조정.
        Vector2 size = _rect.sizeDelta;
        if (pivot.x < 0.5f && position.x + size.x >= Screen.width)
            position.x = Screen.width - size.x;
        else if (pivot.x >= 0.5f && position.x - size.x <= 0)
            position.x = size.x;
        if (pivot.y < 0.5f && position.y + size.y >= Screen.height)
            position.y = Screen.height - size.y;
        else if (pivot.y >= 0.5f && position.y - size.y <= 0)
            position.y = size.y;

        // #3. 적용.
        _rect.pivot = pivot;
        _rect.position = position;
    }

    public virtual void Clear() { }

    public virtual void Close() => Main.UI.ClosePopup(this);

    #endregion

}
