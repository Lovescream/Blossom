using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : CoreManager {

    public GameData Current {
        get => _current;
        private set => _current = value;
    }

    private Dictionary<Type, Dictionary<string, Data>> _data = new();
    private GameData _current;

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        // #1. GameData 로드 / 생성
        if (!Load()) NewGame();

        // #2. Resources/Data 폴더 내 모든 파일 로드.
        TextAsset[] dataFiles = Resources.LoadAll<TextAsset>("Data");

        // #3. Data 불러오기.
        foreach (TextAsset textAsset in dataFiles) {
            // #3-1. 타입 찾기.
            Type type = Type.GetType(textAsset.name);
            if (type == null) {
                Debug.LogError($"[DataManager] Initialize(): The type({textAsset}) was not found.");
                continue;
            }

            // #3-2. 컬렉션에 추가.
            _data[type] = (typeof(JsonConvert)
                .GetMethods()
                .Where(m => m.Name == "DeserializeObject" && m.IsGenericMethod && m.GetParameters().Length == 1)
                .FirstOrDefault()
                .MakeGenericMethod(typeof(List<>)
                .MakeGenericType(type))
                .Invoke(null, new object[] { textAsset.text })
                as IEnumerable<Data>).ToDictionary(x => x.Key);
        }

        return true;
    }

    public T Get<T>(string key) where T : Data {
        if (!_data.TryGetValue(typeof(T), out Dictionary<string, Data> dictionary)) {
            Debug.LogError($"[DataManager] Get<{typeof(T)}>({key}): Failed to get data. Not found the type.");
            return null;
        }
        if (!dictionary.TryGetValue(key, out Data data)) {
            Debug.LogError($"[DataManager] Get<{typeof(T)}>({key}): Failed to get data. Not found the key.");
            return null;
        }
        return data as T;
    }
    public List<T> GetAll<T>() where T : Data {
        if (!_data.TryGetValue(typeof(T), out Dictionary<string, Data> dictionary)) {
            Debug.LogError($"[DataManager] GetAll<{typeof(T)}>(): Failed to get data. Not found the type.");
            return null;
        }
        return dictionary.Values.Select(x => x as T).ToList();
    }

    #region GameData

    public bool Exists(string path) => File.Exists(path);

    public void Save() {
        File.WriteAllText($"{Path.DataPath}/{Path.DATA_GAME}", JsonConvert.SerializeObject(_current));
    }

    public bool Load() {
        string path = System.IO.Path.Combine(Path.DataPath, Path.DATA_GAME);
        if (!Exists(path)) return false;
        GameData gameData = JsonConvert.DeserializeObject<GameData>(File.ReadAllText(path));
        if (gameData == null) return false;
        Current = gameData;
        return true;
    }

    private void NewGame() {
        Current = new();
        // ============================== 새 게임 생성 시 GameData 초기화 ====================================





        // ===================================================================================================

        Save();
    }

    #endregion
}

public class Data {
    public string Key { get; set; }
}

public class GameData {

}
