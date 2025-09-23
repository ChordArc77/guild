using UnityEngine;

public abstract class RebuilableDictionary<T> : ScriptableObject
{
    public T[] Entries;
    public abstract void Rebuild();
}