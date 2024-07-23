using UnityEngine;
using UnityEngine.EventSystems;

public class SceneManagerEx : CoreManager {

    public Scene Current { get; set; }
    public UI_Scene CurrentUI { get; set; }

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        return true;
    }

}

public abstract class Scene : MonoBehaviour {

    private bool _initialized;

    protected virtual void Start() {
        Initialize();
    }
    protected virtual void Update() {

    }

    protected virtual bool Initialize() {
        if (_initialized) return false;
        _initialized = true;

        Main.Scene.Current = this;

        if (FindObjectOfType<EventSystem>() == null) {
            GameObject prefab = Main.Resource.Get<GameObject>("EventSystem");
            if (prefab == null) {
                Debug.LogError($"[Scene] Initialize(): Failed to load EventSystem prefab.");
                return false;
            }
            Instantiate(prefab).name = "EventSystem";
        }

        return true;
    }

}