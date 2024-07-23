using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : ContentManager {

    #region Const

    private static readonly int INITIAL_ORDER_POPUP = 100;
    private static readonly int INITIAL_ORDER_PANEL = 10;
    private static readonly int INITIAL_ORDER_TOAST = 1000;

    #endregion

    #region Properties

    public Transform Root {
        get {
            if (_root == null) {
                GameObject obj = GameObject.Find("@UI_Root");
                if (obj == null) obj = new("@UI_Root");
                _root = obj.transform;
            }
            return _root;
        }
    }

    #endregion

    #region Fields

    private int _popupOrder = INITIAL_ORDER_POPUP;
    private int _panelOrder = INITIAL_ORDER_PANEL;
    private int _toastOrder = INITIAL_ORDER_TOAST;

    // Collections.
    private readonly List<UI_Popup> _popups = new();
    private readonly List<UI_Panel> _panels = new();

    // Components.
    private Transform _root;

    #endregion

    #region Generals

    public T Instantiate<T>(bool pooling = true) where T : UI {
        GameObject prefab = Main.Resource.Get<GameObject>(typeof(T).Name);
        if (prefab == null) {
            Debug.LogError($"[UIManager] Instantiate<{typeof(T).Name}>(): Failed to load prefab.");
            return null;
        }
        T ui;
        if (pooling) ui = Main.Pool.Pop(prefab).GetOrAddComponent<T>();
        else {
            GameObject obj = GameObject.Instantiate(prefab, Root);
            obj.name = prefab.name;
            ui = obj.GetOrAddComponent<T>();
        }
        return ui;
    }

    public void Destroy(UI obj) {
        if (obj == null) return;

        if (Main.Pool.Push(obj.gameObject)) return;

        Object.Destroy(obj.gameObject);
    }

    #endregion

    #region Scenes

    public T OpenScene<T>() where T : UI_Scene {
        UI_Scene scene = Instantiate<T>(false);
        if (scene == null) return null;
        Main.Scene.CurrentUI = scene;

        return scene as T;
    }

    #endregion

    #region Popups

    public T OpenPopup<T>() where T : UI_Popup {
        // #1. 중복 비허용 팝업이면 열려있는지 여부를 검사하고, 열려있다면 해당 팝업을 맨 앞으로 이동.
        for (int i = 0; i < _popups.Count; i++) {
            if (_popups[i] is not T openedPopup) continue;
            if (openedPopup.AllowDuplicate) break;
            openedPopup.SetPopupToFront();
            return openedPopup;
        }

        // #2. 새 팝업 생성.
        UI_Popup popup = Instantiate<T>();
        if (popup == null) return null;
        _popups.Add(popup);
        popup.SetOrder(_popupOrder++);

        return popup as T;
    }

    public void ClosePopup(UI_Popup popup) {
        if (_popups.Count == 0) return;

        bool isLatest = _popups[^1] == popup;

        _popups.Remove(popup);
        Destroy(popup);

        if (isLatest) _popupOrder--;
        else ReorderAllPopups();
    }

    public void ClosePopups<T>() where T : UI_Popup => _popups.Where(x => x is T).ToList().ForEach(ClosePopup);

    public void CloseAllPopups() {
        if (_popups.Count == 0) return;

        for (int i = _popups.Count - 1; i >= 0; i--) {
            if (_popups[i] == null) continue;
            Destroy(_popups[i]);
        }
        _popups.Clear();
        _popupOrder = INITIAL_ORDER_POPUP;
    }

    public int OrderUpPopup() => ++_popupOrder - 1;

    public void ReorderAllPopups() {
        _popupOrder = INITIAL_ORDER_POPUP;
        _popups.ForEach(x => x.SetOrder(_popupOrder++));
    }

    public void SetPopupToFront(UI_Popup popup) {
        if (!_popups.Remove(popup)) return;
        _popups.Add(popup);
        ReorderAllPopups();
    }

    #endregion

    #region Panels

    public T OpenPanel<T>() where T : UI_Panel {
        // #1. 중복 비허용 패널이면 열려있는지 여부를 검사하고, 열려있다면 해당 패널을 맨 앞으로 이동.
        for (int i = 0; i < _panels.Count; i++) {
            if (_panels[i] is not T openedPanel) continue;
            if (openedPanel.AllowDuplicate) break;
            openedPanel.SetPanelToFront();
            return openedPanel;
        }

        // #2. 새 패널 생성.
        UI_Panel panel = Instantiate<T>();
        if (panel == null) return null;
        _panels.Add(panel);
        panel.SetOrder(_panelOrder++);

        return panel as T;
    }

    public void ClosePanel(UI_Panel panel) {
        if (_panels.Count == 0) return;

        bool isLatest = _panels[^1] == panel;

        _panels.Remove(panel);
        Destroy(panel);

        if (isLatest) _panelOrder--;
        else ReorderAllPanels();
    }

    public void ClosePanels<T>() where T : UI_Panel => _panels.Where(x => x is T).ToList().ForEach(ClosePanel);

    public void CloseAllPanels() {
        if (_panels.Count == 0) return;

        for (int i = _panels.Count - 1; i >= 0; i--) {
            if (_panels[i] == null) continue;
            Destroy(_panels[i]);
        }
        _panels.Clear();
        _panelOrder = INITIAL_ORDER_PANEL;
    }

    public int OrderUpPanel() => ++_panelOrder - 1;

    public void ReorderAllPanels() {
        _panelOrder = INITIAL_ORDER_PANEL;
        _panels.ForEach(x => x.SetOrder(_panelOrder++));
    }

    public void SetPanelToFront(UI_Panel panel) {
        if (!_panels.Remove(panel)) return;
        _panels.Add(panel);
        ReorderAllPanels();
    }

    #endregion

    #region Components

    public T CreateComponent<T>(Transform parent = null, bool pooling = true) where T : UI {
        T item = Instantiate<T>(pooling: pooling);
        item.transform.SetParent(parent);

        return item;
    }

    #endregion
}