using UnityEngine.Events;
using UnityEngine.UI;

public class UI_Toggle : UI {

    public bool Value {
        get => _toggle.isOn;
        set {
            Initialize();
            _toggle.isOn = value;
        }
    }

    protected Toggle _toggle;

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _toggle = this.gameObject.GetOrAddComponent<Toggle>();

        return true;
    }

    public UI_Toggle SetEvent(UnityAction<bool> action) {
        Initialize();

        _toggle.onValueChanged.RemoveListener(action);
        _toggle.onValueChanged.AddListener(action);

        return this;
    }

}