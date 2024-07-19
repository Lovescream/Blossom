using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class DataParser : EditorWindow {

#if UNITY_EDITOR

    [MenuItem("Tools/ParseCSVtoJSON")]
    public static void ParseCSVtoJSON() {
        //ParseData<어쩌고Data>();
    }

    private static void ParseData<T>() where T : Data {
        // #1. 파싱 준비.
        Type type = typeof(T);
        List<T> list = new();

        // #2. 파일 읽기.
        string csvPath = $"{Application.dataPath}/{Path.DATA_CSV}/{type.Name}.csv";
        if (!File.Exists(csvPath)) {
            Debug.LogError($"[DataParser] ParseData<{type.Name}>(): The path was not found. ({csvPath})");
            return;
        }
        string[] lines = File.ReadAllText(csvPath).Split("\n");
        if (lines.Length <= 0) return;

        // #3. 프로퍼티 이름 캐싱.
        string[] propertyNames = lines[0].Replace("\r", "").Split(',');

        // #4. 데이터 파싱.
        for (int y = 1; y < lines.Length; y++) {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0 || string.IsNullOrEmpty(row[0])) continue;

            T data = Activator.CreateInstance<T>();

            for (int i = 0; i < row.Length; i++) {
                PropertyInfo property = type.GetProperty(propertyNames[i]);
                if (property == null) {
                    Debug.LogError($"[DataParser] ParseData<{type.Name}>(): Data parsing failed. Property '{propertyNames[i]}' not found.");
                    return;
                }
                property.SetValue(data, ConvertValue(property.PropertyType, row[i]));
            }

            list.Add(data);
        }

        // #5. Json으로 저장.
        string jsonPath = $"{Application.dataPath}/{Path.DATA_JSON}/{type.Name}.json";
        File.WriteAllText(jsonPath, JsonConvert.SerializeObject(list, Formatting.Indented));
        Debug.Log($"[DataParser] ParseData<{type.Name}>(): Parsed data: {jsonPath}");
        AssetDatabase.Refresh();
    }

    private static object ConvertValue(Type type, string value) {
        // #1. 기본 값 자료인 경우 변환.
        TypeConverter converter = TypeDescriptor.GetConverter(type);
        if (converter != null && converter.CanConvertFrom(typeof(string))) return string.IsNullOrEmpty(value) ? default : converter.ConvertFromString(value);

        // TODO:: 다른 타입 및 리스트 자료에 대한 변환 구현 필요.

        return Activator.CreateInstance(type, value);
    }

#endif

}