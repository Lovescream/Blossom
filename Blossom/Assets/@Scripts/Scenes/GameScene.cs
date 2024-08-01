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

        Log = $"Blossom Test App Initialized.\n";
        SceneUI = Main.UI.OpenScene<UI_Scene_Game>();
        SceneUI.SetInfo(this);

        return true;
    }

    #endregion

    public void LoadAchievements() {
        Log = "";

        PlayGamesPlatform.Instance.LoadAchievements(OnLoadAchievements);
        PlayGamesPlatform.Instance.LoadAchievementDescriptions(OnLoadAchievementDescription);
    }

    #region Events

    private void OnLoadAchievements(IAchievement[] achievements) {
        _achievements = achievements;
        Log += $"Loaded {achievements.Length} Achievements.";
        OnLoadAchievementsCompleted();
    }

    private void OnLoadAchievementDescription(IAchievementDescription[] descriptions) {
        _descriptions = descriptions;
        Log += $"Loaded {descriptions.Length} Achievement Descriptions.";
        OnLoadAchievementsCompleted();
    }

    private void OnLoadAchievementsCompleted() {
        if (_achievements == null || _descriptions == null) return;
        Log += $"Achievement Load Completed.";

        Main.UI.OpenPanel<UI_Panel_Achievements>().SetInfo(this, _achievements, _descriptions);
        _achievements = null;
        _descriptions = null;
    }

    #endregion

    #region UI

    public void SetUIActive(bool active) => SceneUI.gameObject.SetActive(active);

    #endregion

}