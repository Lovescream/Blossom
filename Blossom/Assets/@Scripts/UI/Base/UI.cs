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

        // RectTransform ã��: Popup, Panel �� Canvas ������ UI�� �ڽ� Rect�� ã��, �׷��� ������ ���� ã��. (������ ���, �ʿ��ϸ� ���� Ŭ�������� �����ؾ� �� ���� ����)
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