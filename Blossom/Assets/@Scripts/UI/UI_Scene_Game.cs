public class UI_Scene_Game : UI_Scene {

    #region Properties

    public GameScene Scene { get; private set; }

    #endregion

    #region Fields

    private UI_Text _txtLog;
    private UI_Button _btnAchievements;

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _txtLog = this.gameObject.FindChild<UI_Text>("txtLog");
        _btnAchievements = this.gameObject.FindChild<UI_Button>("btnAchievements");
        _btnAchievements.SetEvent(OnBtnAchievements);

        return true;
    }

    public void SetInfo(GameScene scene) {
        this.Scene = scene;

        Scene.OnChangedLog -= OnChangedLog;
        Scene.OnChangedLog += OnChangedLog;
        OnChangedLog(Scene.Log);
    }

    #endregion

    #region Events

    private void OnChangedLog(string log) {
        _txtLog.Text = log;
    }

    private void OnBtnAchievements() {
        Scene.LoadAchievements();
        Scene.SetUIActive(false);
    }

    #endregion

}