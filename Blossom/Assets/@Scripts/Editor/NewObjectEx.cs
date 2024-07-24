using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;

public class NewObjectEx {

    #region Properties, Const

    private const string PATH = "Assets/@Resources/Prefabs/Prefabs.asset";
    private static Prefabs Prefabs => AssetDatabase.LoadAssetAtPath<Prefabs>(PATH);

    #endregion

    [MenuItem("GameObject/¡ÚUI/Base/Text", false, 1)]
    private static void CreateNewText() => Instantiate(Prefabs => Prefabs.Text);
    [MenuItem("GameObject/¡ÚUI/Base/Image", false, 1)]
    private static void CreateNewImage() => Instantiate(Prefabs => Prefabs.Image);
    [MenuItem("GameObject/¡ÚUI/Base/Button", false, 1)]
    private static void CreateNewButton() => Instantiate(Prefabs => Prefabs.Button);
    [MenuItem("GameObject/¡ÚUI/Base/Slider", false, 1)]
    private static void CreateNewSlider() => Instantiate(Prefabs => Prefabs.Slider);
    [MenuItem("GameObject/¡ÚUI/Base/Toggle", false, 1)]
    private static void CreateNewToggle() => Instantiate(Prefabs => Prefabs.Toggle);

    [MenuItem("GameObject/¡ÚUI/Base/Text", true)]
    [MenuItem("GameObject/¡ÚUI/Base/Image", true)]
    [MenuItem("GameObject/¡ÚUI/Base/Button", true)]
    [MenuItem("GameObject/¡ÚUI/Base/Slider", true)]
    [MenuItem("GameObject/¡ÚUI/Base/Toggle", true)]
    private static bool CreateNewComponentValidate() => Selection.activeGameObject && Selection.activeGameObject.GetComponentInParent<Canvas>();

    #region Generals

    private static void Instantiate(Func<Prefabs, GameObject> selector) {
        if (Prefabs == null) {
            Debug.LogWarning($"Prefabs not found at path {PATH}");
            return;
        }

        Object instance = PrefabUtility.InstantiatePrefab(selector(Prefabs), Selection.activeTransform);

        Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
        Selection.activeObject = instance;
    }

    #endregion

}