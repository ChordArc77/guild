#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class GenericDictionary<TKey, TValue, TEntry> : RebuildableDictionaryBase where TEntry : ScriptableObject
{
    public TEntry[] Entries;
    public Dictionary<TKey, TValue> Dictionary = new();

    protected abstract TKey GetKeyFromEntry(TEntry entry);
    protected abstract TValue GetValueFromEntry(TEntry entry);

    void OnValidate() => Rebuild();
    void OnEnable() => Rebuild();

    public override void Rebuild()
    {
        Dictionary.Clear();

        var guids = AssetDatabase.FindAssets($"t:{typeof(TEntry).Name}");
        var entries = new List<TEntry>();

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var entry = AssetDatabase.LoadAssetAtPath<TEntry>(path);
            if (entry == null) continue;

            entries.Add(entry);
        }

        Entries = entries.ToArray();

        foreach (var entry in Entries)
        {
            Dictionary.TryAdd(GetKeyFromEntry(entry), GetValueFromEntry(entry));
        }

        EditorUtility.SetDirty(this);

        Debug.Log($"{name} rebuild succeed with {Dictionary.Count} in dictionary");
    }
}

public interface IRebuildableDictionary
{
    void Rebuild();
}

public abstract class RebuildableDictionaryBase : ScriptableObject, IRebuildableDictionary
{
    public abstract void Rebuild();
}

[CustomEditor(typeof(RebuildableDictionaryBase), true)]
public class DictionaryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Rebuild"))
        {
            ((IRebuildableDictionary)target).Rebuild();
            AssetDatabase.SaveAssets();
        }
    }
}
#endif
