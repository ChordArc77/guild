using UnityEditor;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public abstract ItemType Type { get; }
}

public enum ItemType
{
    Plant,
    Sword,
    Shield,
    Spear,
    Hammer,
    Bow,
    Dagger,
    Staff,
    Potion,
    Food
}

[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.LabelField("Type", ((Item)target).Type.ToString());
    }
}