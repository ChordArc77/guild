using System;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    static int slot;

    static string GetDir() => Application.persistentDataPath;
    static string GetPath(string filename) => Path.Combine(GetDir(), $"{slot}", filename);
    static string GetTempPath(string path) => path + ".tmp";
    static string GetBackupPath(string path) => path + ".bak";

    #region Save

    public static void Save<T>(string filename, T data)
    {
        var json = JsonUtility.ToJson(data);

        if (!Directory.Exists(GetDir())) Directory.CreateDirectory(GetDir());
        var path = GetPath(filename);
        var tmp = GetTempPath(path);
        var bak = GetBackupPath(path);

        File.WriteAllText(tmp, json);
        if (File.Exists(path))
        {
            File.Copy(path, bak, overwrite: true);
        }
        File.Move(tmp, path);
    }

    public static void SaveStatus(PlayerStatus status)
    {
        Save("PlayerStatus", status);
    }

    #endregion

    #region Load

    public static bool TryLoad<T>(string filename, out T data)
    {
        var path = GetPath(filename);
        var backup = GetBackupPath(path);

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

    public static bool TryLoadStatus(out PlayerStatus status)
    {
        return TryLoad("PlayerStatus", out status);
    }

    #endregion

    public static void SetSlot(int n)
    {
        slot = n;
    }
}
