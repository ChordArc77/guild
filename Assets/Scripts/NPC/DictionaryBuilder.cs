using System.Linq;
using UnityEditor;
using UnityEngine;

public class DictionaryBuilder : EditorWindow
{
    Object dictionary;

    [MenuItem("Tools/Rebuild Dictionary")]
    public static void ShowWindow()
    {
        GetWindow<DictionaryBuilder>("Dictionary Builder");
    }

    void OnGUI()
    {
        dictionary = EditorGUILayout.ObjectField("Target Dictionary", dictionary, typeof(RebuilableDictionary<>), false);

        if (dictionary == null)
        {
            EditorGUILayout.HelpBox("Assign a dictionary", MessageType.Warning);
        }

        if (GUILayout.Button("Build"))
        {
            Build();
        }
    }

    void Build()
    {
        if (dictionary == null) return;

        var guids = AssetDatabase.FindAssets("t:NPCName");
        var hits = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<NPCName>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(item => item != null)
            .ToArray();

        dictionary.Names = hits;

        EditorUtility.SetDirty(dictionary);
        AssetDatabase.SaveAssets();
    }
}
