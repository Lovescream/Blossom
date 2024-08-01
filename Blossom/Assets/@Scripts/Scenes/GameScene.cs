using GooglePlayGames;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameScene : Scene {

    #region Properties

    public UI_Scene_Game SceneUI { get; private set; }

    public string Log {
        get => _log;
        set {
            _log = value;
            Debug.Log(value);
            OnChangedLog?.Invoke(value);
        }
    }

    public UI_Panel_Achievements AchievementUI { get; private set; }

    #endregion

    #region Fields

    private string _log;
    private IAchievement[] _achievements = null;
    private IAchievementDescription[] _descriptions = null;

    public event Action<string> OnChangedLog;

    #endregion

    #region Initialize / Set

    protected override bool Initialize() {
        if (!base.Initialize()) return false;

        SceneUI = Main.UI.OpenScene<UI_Scene_Game>();
        SceneUI.SetInfo(this);

        return true;
    }

    #endregion

    public void LoadAchievements() {
        Log = "";

        if (AchievementUI != null) {
            AchievementUI.DeactiveButton();
        }

        PlayGamesPlatform.Instance.LoadAchievements(OnLoadAchievements);
        PlayGamesPlatform.Instance.LoadAchievementDescriptions(OnLoadAchievementDescription);
    }

    #region Events

    private void OnLoadAchievements(IAchievement[] achievements) {
        _achievements = achievements;
        Log += $"Loaded {achievements.Length} Achievements.\n";
        OnLoadAchievementsCompleted();
    }

    private void OnLoadAchievementDescription(IAchievementDescription[] descriptions) {
        _descriptions = descriptions;
        Log += $"Loaded {descriptions.Length} Achievement Descriptions.\n";
        OnLoadAchievementsCompleted();
    }

    private void OnLoadAchievementsCompleted() {
        if (_achievements == null || _descriptions == null) return;
        Log += $"Achievement Load Completed.\n";

        if (AchievementUI == null) {
            AchievementUI = Main.UI.OpenPanel<UI_Panel_Achievements>();
            AchievementUI.OnClosed += () => AchievementUI = null;
        }
        AchievementUI.SetInfo(this, _achievements, _descriptions);
        _achievements = null;
        _descriptions = null;
    }

    #endregion

    #region UI

    public void SetUIActive(bool active) => SceneUI.gameObject.SetActive(active);

    #endregion

}