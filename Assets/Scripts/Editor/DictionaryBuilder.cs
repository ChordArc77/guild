using UnityEditor;
using UnityEngine;

public class DictionaryBuilder : EditorWindow
{
    Object selected;

    [MenuItem("Tools/Rebuild Dictionary")]
    public static void ShowWindow()
    {
        GetWindow<DictionaryBuilder>("Dictionary Builder");
    }

    void OnGUI()
    {
        selected = EditorGUILayout.ObjectField("Target Dictionary", selected, typeof(ScriptableObject), false);

        using (new EditorGUI.DisabledScope(selected is not IRebuildableDictionary))
        {
            if (GUILayout.Button("Build"))
            {
                ((IRebuildableDictionary)selected).Rebuild();
                AssetDatabase.SaveAssets();
            }
        }
    }
}
