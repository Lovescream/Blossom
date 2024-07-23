using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_Button : UI {

    #region Fields

    // Components.
    protected UI_Image _image;
    protected Button _button;
    protected UI_Text _text;

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _image = this.gameObject.GetOrAddComponent<UI_Image>();
        _button = this.gameObject.GetOrAddComponent<Button>();
        _text = this.gameObject.FindChild<UI_Text>();

        return true;
    }

    public UI_Button SetText(string text, int size = 36, Color color = default, TextAlignmentOptions alignment = TextAlignmentOptions.Center) {
        Initialize();

        // #1. 텍스트가 비어있으면 텍스트 오브젝트 제거.
        if (string.IsNullOrEmpty(text)) {
            Main.UI.Destroy(_text);
            _text = null;
            return this;
        }

        // #2. 텍스트 오브젝트가 없다면 생성.
        if (_text == null) {
            _text = Main.UI.CreateComponent<UI_Text>(this.transform, false);
            _text.name = "txtButton";
        }

        // #3. 텍스트 설정.
        _text.SetSize(size)
            .SetAlignment(alignment)
            .SetColor(color.Equals(Color.clear) ? Styles.TEXTCOLOR_BUTTON : color)
            .Text = text;

        return this;
    }

    public UI_Button SetImage(Sprite sprite) {
        Initialize();

        _image.Sprite = sprite;

        return this;
    }

    public UI_Button SetEvent(UnityAction action) {
        Initialize();

        _button.onClick.RemoveListener(action);
        _button.onClick.AddListener(action);

        return this;
    }

    public UI_Button SetActive(bool active) {
        if (active) {
            _button.interactable = true;
            _image.SetColor(Color.white);
            if (_text != null) _text.SetColor(Color.white);
        }
        else {
            _button.interactable = false;
            _image.SetColor(Styles.BUTTONCOLOR_DISABLE);
            if (_text != null) _text.SetColor(Styles.BUTTONCOLOR_DISABLE);
        }

        return this;
    }

    #endregion

}