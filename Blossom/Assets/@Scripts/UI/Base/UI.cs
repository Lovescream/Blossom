using UnityEngine;

public abstract class UI : MonoBehaviour {

    #region Fields

    protected RectTransform _rect;

    private bool _isInitialized;

    #endregion

    #region MonoBehaviours

    protected virtual void OnEnable() {
        Initialize();
    }
    protected virtual void OnDisable() {

    }
    protected virtual void Update() {

    }

    #endregion

    #region Initialize

    public virtual bool Initialize() {
        if (_isInitialized) return false;

        // RectTransform 찾기: Popup, Panel 등 Canvas 단위의 UI는 자식 Rect를 찾고, 그렇지 않으면 직접 찾기. (전자의 경우, 필요하면 하위 클래스에서 직접해야 할 수도 있음)
        _rect = this.TryGetComponent<Canvas>(out _) ? this.transform.GetChild(0).GetComponent<RectTransform>() : this.GetComponent<RectTransform>();

        _isInitialized = true;
        return true;
    }

    #endregion

    #region Rect

    public UI SetRectAnchor(Vector2 anchorMin, Vector2 anchorMax) {
        Initialize();

        _rect.anchorMin = anchorMin;
        _rect.anchorMax = anchorMax;

        return this;
    }

    public UI SetRectPivot(Vector2 pivot) {
        Initialize();

        _rect.pivot = pivot;

        return this;
    }

    public UI SetRectAnchoredPosition(Vector2 position) {
        Initialize();

        _rect.anchoredPosition = position;

        return this;
    }

    public UI SetSize(Vector2 size) {
        Initialize();

        _rect.sizeDelta = size;

        return this;
    }

    public UI SetOffset(Vector2 offsetMin, Vector2 offsetMax) {
        Initialize();

        _rect.offsetMin = offsetMin;
        _rect.offsetMax = offsetMax;

        return this;
    }

    #endregion

}