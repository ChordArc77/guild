using System.Linq;
using UnityEditor;
using UnityEngine;


public class ItemDictionaryBuilder : EditorWindow
{
    ItemDictionary dictionary;

    [MenuItem("Tools/Rebuild/Item Dictionary")]
    public static void ShowWindow()
    {
        GetWindow<ItemDictionaryBuilder>("Item Dictionary Builder");
    }

    void OnGUI()
    {
        dictionary = (ItemDictionary)EditorGUILayout.ObjectField("Target Dictionary", dictionary, typeof(ItemDictionary), false);

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

        var guids = AssetDatabase.FindAssets("t:Item");
        var items = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<Item>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(item => item != null)
            .ToArray();

        dictionary.Items = items;

        EditorUtility.SetDirty(dictionary);
        AssetDatabase.SaveAssets();
    }
}
