using GooglePlayGames;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class UI_Achievement : UI {

    #region Properties

    public GameScene Scene { get; private set; }
    public IAchievement Achievement { get; private set; }
    public IAchievementDescription Description { get; private set; }

    #endregion

    #region Fields

    private Sprite[] _sprites = new Sprite[2];

    private UI_AchievementInfo _info;
    private UI_Button _btnAction;
    private UI_Image _imgButton;

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _info = this.gameObject.FindChild<UI_AchievementInfo>();
        _btnAction = this.gameObject.FindChild<UI_Button>("btnAction");
        _imgButton = this.gameObject.FindChild<UI_Image>("imgButton");
        _btnAction.SetEvent(OnButtonAction);

        _sprites[0] = Main.Resource.Get<Sprite>("Icon_Star");
        _sprites[1] = Main.Resource.Get<Sprite>("Icon_StarGrey");

        return true;
    }

    public void SetInfo(GameScene scene, IAchievement achievement, IAchievementDescription description) {
        Initialize();

        this.Scene = scene;
        this.Achievement = achievement;
        this.Description = description;
        _info.SetInfo(achievement, description);
        _btnAction.SetActive(true);
        _imgButton.Sprite = _sprites[achievement.completed ? 0 : 1];
    }

    public void ActiveButton() => _btnAction.SetActive(true);
    public void DeactiveButton() => _btnAction.SetActive(false);

    #endregion

    #region Events

    private void OnButtonAction() {
        _btnAction.SetActive(false);
        if (Achievement.hidden) {
            PlayGamesPlatform.Instance.ReportProgress(Achievement.id, 0.0, b => {
                if (b) Scene.Log += $"Successfully revealed the Achievement({Description.title} : {Achievement.id}).\n";
                else Scene.Log += $"Failed to revealed the Achievement({Description.title} : {Achievement.id}).\n";
                Scene.LoadAchievements();
            });
        }
        else if (Description.title.Contains("!")) {
            PlayGamesPlatform.Instance.IncrementAchievement(Achievement.id, 1, b => {
                if (b) Scene.Log += $"Successfully increased the Achievement({Description.title} : {Achievement.id}) by 1.\n";
                else Scene.Log += $"Failed to increase the Achievement({Description.title} : {Achievement.id}) by 1.\n";
                Scene.LoadAchievements();
            });
        }
        else {
            PlayGamesPlatform.Instance.ReportProgress(Achievement.id, 100.0, b => {
                if (b) Scene.Log += $"Successfully achieved the Achievement({Description.title} : {Achievement.id}).\n";
                else Scene.Log += $"Failed to achieved the Achievement({Description.title} : {Achievement.id}).\n";
                Scene.LoadAchievements();
            });
        }
    }

    #endregion

}