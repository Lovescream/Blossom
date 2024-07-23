using UnityEngine;
using UnityEngine.UI;

public class UI_Image : UI {

    public Sprite Sprite {
        get => _image.sprite;
        set {
            Initialize();
            _image.sprite = value;
            if (value == null) _image.color = Color.clear;
            else _image.color = Color.white;
        }
    }

    protected Image _image;

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _image = this.gameObject.GetOrAddComponent<Image>();

        return true;
    }

    public void SetColor(Color color) {
        _image.color = color;
    }
}