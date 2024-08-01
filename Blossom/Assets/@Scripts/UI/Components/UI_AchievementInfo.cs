using UnityEngine;
using UnityEngine.SocialPlatforms;

public class UI_AchievementInfo : UI {

    #region Properties

    public IAchievement Achievement { get; private set; }
    public IAchievementDescription Description { get; private set; }

    #endregion

    #region Fields

    private UI_Image _imgIcon;
    private UI_Image _imgCheck;
    private UI_Image _imgHidden;
    private UI_Text _txtTitle;
    private UI_Text _txtDescription;
    private UI_Slider _sliderPercent;
    private UI_Text _txtStep;
    private UI_Text _txtDate;
    private UI_Text _txtPoint;

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _imgIcon = this.gameObject.FindChild<UI_Image>("imgIcon");
        _imgCheck = this.gameObject.FindChild<UI_Image>("imgCheck");
        _imgHidden = this.gameObject.FindChild<UI_Image>("imgHidden");
        _txtTitle = this.gameObject.FindChild<UI_Text>("txtTitle");
        _txtDescription = this.gameObject.FindChild<UI_Text>("txtDescription");
        _sliderPercent = this.gameObject.FindChild<UI_Slider>("sliderPercent");
        _txtStep = this.gameObject.FindChild<UI_Text>("txtStep");
        _txtDate = this.gameObject.FindChild<UI_Text>("txtDate");
        _txtPoint = this.gameObject.FindChild<UI_Text>("txtPoint");

        return true;
    }

    public void SetInfo(IAchievement achievement, IAchievementDescription description) {
        Initialize();

        this.Achievement = achievement;
        this.Description = description;

        Texture2D texture = description.image;
        if (texture != null) _imgIcon.Sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(0.5f, 0.5f));
        _imgCheck.gameObject.SetActive(achievement.completed);
        _imgHidden.gameObject.SetActive(achievement.hidden);

        _txtTitle.Text = $"{description.title} ({description.id})";
        _txtDescription.Text = $"{(achievement.completed ? description.achievedDescription : description.unachievedDescription)}";
        _sliderPercent.Value = Mathf.Clamp01((float)achievement.percentCompleted / 100f);
        _txtStep.Text = $"{achievement.percentCompleted}%";
        _txtDate.Text = $"{achievement.lastReportedDate}";
        _txtPoint.Text = $"+{description.points}";
    }

    #endregion

}