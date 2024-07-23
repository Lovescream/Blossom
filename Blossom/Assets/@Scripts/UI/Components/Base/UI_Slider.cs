using UnityEngine.Events;
using UnityEngine.UI;

public class UI_Slider : UI {

    public float Value {
        get => _slider.value;
        set {
            Initialize();
            _slider.value = value;
        }
    }

    protected Slider _slider;

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _slider = this.gameObject.GetOrAddComponent<Slider>();

        return true;
    }

    public UI_Slider SetEvent(UnityAction<float> action) {
        Initialize();

        _slider.onValueChanged.RemoveListener(action);
        _slider.onValueChanged.AddListener(action);

        return this;
    }

}