using TMPro;
using UnityEngine;

public class UI_Text : UI {

    public string Text {
        get => _txt.text;
        set {
            Initialize();
            _txt.text = value;
        }
    }

    protected TextMeshProUGUI _txt;

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _txt = this.gameObject.GetOrAddComponent<TextMeshProUGUI>();

        return true;

    }

    public UI_Text SetSize(int size) {
        Initialize();

        if (size < 0) _txt.enableAutoSizing = true;
        else _txt.fontSize = size;

        return this;
    }
    public UI_Text SetColor(Color color) {
        Initialize();

        _txt.color = color;
        return this;
    }
    public UI_Text SetAlignment(TextAlignmentOptions alignment) {
        Initialize();

        _txt.alignment = alignment;
        return this;
    }

}