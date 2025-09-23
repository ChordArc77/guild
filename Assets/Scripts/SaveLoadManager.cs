using System;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    static int slot;

    static string GetDir() => Application.persistentDataPath;
    static string GetPath() => Path.Combine(GetDir(), slot + ".json");
    static string GetTempPath() => GetPath() + ".tmp";
    static string GetBackupPath() => GetPath() + ".bak";

    public static void Save<T>(T data)
    {
        var json = JsonUtility.ToJson(data);

        if (!Directory.Exists(GetDir())) Directory.CreateDirectory(GetDir());
        var path = GetPath();
        var tmp = GetTempPath();
        var bak = GetBackupPath();

        File.WriteAllText(tmp, json);
        if (File.Exists(path))
        {
            File.Copy(path, bak, overwrite: true);
        }
        File.Move(tmp, path);
    }

    public static bool TryLoad<T>(out T data)
    {
        var path = GetPath();
        var backup = GetBackupPath();

        if (TryRead(path, out data)) return true;
        if (TryRead(backup, out data)) return true;

        return false;

        bool TryRead(string path, out T result)
        {
            result = default;
            if (!File.Exists(path)) return false;
            try
            {
                var json = File.ReadAllText(path);
                result = JsonUtility.FromJson<T>(json);
                return result != null;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"TryLoad: failed reading {path}\n{e}");
                return false;
            }
        }
    }

    public void SetSlot(int n)
    {
        slot = n;
    }
}
