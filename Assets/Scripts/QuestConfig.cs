using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Config", menuName = "Quest/Config")]
public class QuestConfig : ScriptableObject
{
    [SerializeField] Weight<Rank>[] rankWeight;
    [SerializeField] Weight<QuestType>[] typeWeight;
    public List<Quest> QuestPool;

    // cumulative distribution
    public float[] RankCdf;
    public float[] TypeCdf;

    void OnValidate() => RebuildCdf();
    void OnEnable() => RebuildCdf();

    void RebuildCdf()
    {
        RankCdf = RebuildCdf(ref rankWeight);
        TypeCdf = RebuildCdf(ref typeWeight);
    }

    float[] RebuildCdf<T>(ref Weight<T>[] weights)
    {
        var all = (T[])Enum.GetValues(typeof(T));

        if (weights.Length != all.Length)
        {
            Debug.LogWarning($"{name} missing weight for type: {typeof(T)}");
            return null;
        }

        var ordered = weights.OrderBy(w => w.Key).ToArray();
        var total = ordered.Sum(w => w.Value);
        var cdf = new float[ordered.Length];
        var run = 0f;

        for (var i = 0; i < ordered.Length; i++)
        {
            run += Mathf.Max(0f, ordered[i].Value) / total; // normalize
            cdf[i] = run;
        }

        // Clamp last to exactly 1 to avoid float drift
        cdf[^1] = 1f;

        // Write back the ordered array to keep inspector consistent
        weights = ordered;

        return cdf;
    }
}

[Serializable]
public struct Weight<T>
{
    public T Key;
    [Min(0f)] public float Value;
}
