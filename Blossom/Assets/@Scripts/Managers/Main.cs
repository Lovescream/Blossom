using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Main : MonoBehaviour
{

    #region Singleton

    private static Main _instance;
    public static Main Instance {
        get {
            if (_instance == null) Initialize();
            return _instance;
        }
    }

    #endregion

    #region Properties

    // Core.
    public static PoolManager Pool => Instance?._pool;
    public static ResourceManager Resource => Instance?._resource;
    public static DataManager Data => Instance?._data;
    public static ScreenManager Screen => Instance?._screen;
    public static SceneManagerEx Scene => Instance?._scene;

    #endregion

    #region Fields

    // Core.
    private readonly PoolManager _pool = new();
    private readonly ResourceManager _resource = new();
    private readonly DataManager _data = new();
    private readonly ScreenManager _screen = new();
    private readonly SceneManagerEx _scene = new();

    private static bool _isInitialized;

    #endregion

    #region Initialize

    private static void Initialize() {
        // #0. 중복 초기화 방지.
        if (_instance != null || _isInitialized) return;
        _isInitialized = true;

        // #1. 새 오브젝트 및 Main 인스턴스 생성, 등록.
        GameObject obj = GameObject.Find("@Main");
        if (obj == null) {
            obj = new("@Main");
            obj.AddComponent<Main>();
        }
        DontDestroyOnLoad(obj);
        _instance = obj.GetComponent<Main>();

        // #2. 매니저 초기화.
        foreach (FieldInfo fieldInfo in typeof(Main).GetFields(BindingFlags.Instance | BindingFlags.NonPublic)) {
            CoreManager manager = fieldInfo.GetValue(_instance) as CoreManager;
            manager?.Initialize();
        }
    }

    #endregion

}

public abstract class CoreManager {

    private bool _initialized;

    public virtual bool Initialize() {
        if (_initialized) return false;
        _initialized = true;

        return true;
    }

}

public static class Path {
    public static readonly string RESOURCE_SPRITES = "Sprites";
    public static readonly string RESOURCE_PREFABS = "Prefabs";

    public static readonly string DATA_CSV = "@Resources/Data";
    public static readonly string DATA_JSON = "Resources/Data";
    public static readonly string DATA_GAME = "SaveData.json";

    public static string AssetPath = Application.dataPath;
    public static string DataPath = Application.persistentDataPath;
}