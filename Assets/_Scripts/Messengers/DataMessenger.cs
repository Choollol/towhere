using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows for communication of data between scripts
public class DataMessenger : MonoBehaviour
{
    private static Dictionary<Type, Dictionary<string, object>> data;

    private static readonly Dictionary<Type, object> DEFAULT_VALUES = new()
    {
        { typeof(bool), false },
        { typeof(float), 0f },
        { typeof(int), 0 },
        { typeof(string), string.Empty },
        { typeof(Vector3), Vector3.zero },
        { typeof(Quaternion), Quaternion.identity },
        { typeof(GameObject), null },
        { typeof(ScriptableObject), null },
    };

    private void Awake()
    {
        data = new()
        {
            { typeof(bool), new() },
            { typeof(float), new() },
            { typeof(int), new() },
            { typeof(List<int>), new() },
            { typeof(List<string>), new() },
            { typeof(string), new() },
            { typeof(Vector3), new() },
            { typeof(Quaternion), new() },
            { typeof(GameObject), new() },
            { typeof(ScriptableObject), new() },
        };
    }

    #region Bool
    public static bool GetBool(string key)
    {
        var bools = data[typeof(bool)];
        if (!bools.TryGetValue(key, out object v))
        {
            bools[key] = DEFAULT_VALUES[typeof(bool)];
            return (bool)bools[key];
        }
        return (bool)v;
    }
    public static bool GetBool(BoolKey key)
    {
        return GetBool(key.ToString());
    }
    public static void SetBool(string key, bool value)
    {
        data[typeof(bool)][key] = value;
    }
    public static void SetBool(BoolKey key, bool value)
    {
        SetBool(key.ToString(), value);
    }
    public static void ToggleBool(string key)
    {
        var bools = data[typeof(bool)];
        bools[key] = !(bool)bools[key];
    }
    public static void ToggleBool(BoolKey key)
    {
        ToggleBool(key.ToString());
    }
    public static IEnumerator WaitForBool(string key, bool doInvert = false)
    {
        while (GetBool(key) ^ doInvert) yield return null;
    }

    /// <summary>
    /// Wait for bool to become false. If doInvert, wait for bool to become true.
    /// </summary>
    public static IEnumerator WaitForBool(BoolKey key, bool doInvert = false)
    {
        yield return WaitForBool(key.ToString(), doInvert);
    }
    #endregion Bool

    #region Float
    public static float GetFloat(string key)
    {
        var floats = data[typeof(float)];
        if (!floats.TryGetValue(key, out object v))
        {
            floats[key] = DEFAULT_VALUES[typeof(float)];
            return (float)floats[key];
        }
        return (float)v;
    }
    public static float GetFloat(FloatKey key)
    {
        return GetFloat(key.ToString());
    }
    public static void SetFloat(string key, float value)
    {
        data[typeof(float)][key] = value;
    }
    public static void SetFloat(FloatKey key, float value)
    {
        SetFloat(key.ToString(), value);
    }
    /// <summary>
    /// Performs an operation on the float associated with the given key with the value given. The operator is + by default.
    /// </summary>
    public static float OperateFloat(string key, float value, char op = '+')
    {
        switch (op)
        {
            case '+':
                SetFloat(key, GetFloat(key) + value);
                break;
            case '*':
                SetFloat(key, GetFloat(key) * value);
                break;
            case '/':
                SetFloat(key, GetFloat(key) / value);
                break;
        }
        return (float)data[typeof(float)][key];
    }
    /// <summary>
    /// Performs an operation on the float associated with the given key with the value given. The operator is + by default.
    /// </summary>
    public static float OperateFloat(FloatKey key, float value, char op = '+')
    {
        return OperateFloat(key.ToString(), value, op);
    }
    #endregion Float

    #region GameObject
    public static GameObject GetGameObject(string key)
    {
        var gameObjects = data[typeof(GameObject)];
        if (!gameObjects.TryGetValue(key, out object v))
        {
            gameObjects[key] = DEFAULT_VALUES[typeof(GameObject)];
            return (GameObject)gameObjects[key];
        }
        return (GameObject)v;
    }
    public static GameObject GetGameObject(GameObjectKey key)
    {
        return GetGameObject(key.ToString());
    }
    public static void SetGameObject(string key, GameObject obj)
    {
        data[typeof(GameObject)][key] = obj;
    }
    public static void SetGameObject(GameObjectKey key, GameObject obj)
    {
        SetGameObject(key.ToString(), obj);
    }
    #endregion GameObject

    #region Int
    public static int GetInt(string key)
    {
        var ints = data[typeof(int)];
        if (!ints.TryGetValue(key, out object v))
        {
            ints[key] = DEFAULT_VALUES[typeof(int)];
            return (int)ints[key];
        }
        return (int)v;
    }
    public static int GetInt(IntKey key)
    {
        return GetInt(key.ToString());
    }
    public static void SetInt(string key, int value)
    {
        data[typeof(int)][key] = value;
    }
    public static void SetInt(IntKey key, int value)
    {
        SetInt(key.ToString(), value);
    }
    /// <summary>
    /// Performs an operation on the int associated with the given key with the int given. The operator is + by default.
    /// </summary>
    public static int OperateInt(string key, int value, char op = '+')
    {
        switch (op)
        {
            case '+':
                SetInt(key, GetInt(key) + value);
                break;
            case '*':
                SetInt(key, GetInt(key) * value);
                break;
            case '/':
                SetInt(key, GetInt(key) / value);
                break;
        }
        return (int)data[typeof(int)][key];
    }
    /// <summary>
    /// Performs an operation on the int associated with the given key with the int given. The operator is + by default.
    /// </summary>
    public static int OperateInt(IntKey key, int value, char op = '+')
    {
        return OperateInt(key.ToString(), value, op);
    }

    /// <summary>
    /// Performs an operation on the int associated with the given key with the float given. 
    /// </summary>
    /// <param name="op">The operator is + by default.</param>
    /// <param name="doRound">Truncates instead of rounds by default.</param>
    public static int OperateInt(string key, float value, char op = '+', bool doRound = false)
    {
        switch (op)
        {
            case '+':
                SetInt(key, (int)(GetInt(key) + value + (doRound ? 0.5f : 0)));
                break;
            case '*':
                SetInt(key, (int)(GetInt(key) * value + (doRound ? 0.5f : 0)));
                break;
            case '/':
                SetInt(key, (int)(GetInt(key) / value + (doRound ? 0.5f : 0)));
                break;
        }
        return (int)data[typeof(int)][key];
    }
    /// <summary>
    /// Performs an operation on the int associated with the given key with the float given. 
    /// </summary>
    /// <param name="op">The operator is + by default.</param>
    /// <param name="doRound">Truncates instead of rounds by default.</param>
    public static int OperateInt(IntKey key, float value, char op = '+', bool doRound = false)
    {
        return OperateInt(key.ToString(), value, op, doRound);
    }
    #endregion Int

    #region List
    /// <summary>
    /// T must be in: int, string
    /// </summary>
    private static List<T> GetList<T>(string key)
    {
        var lists = data[typeof(List<T>)];
        if (!lists.TryGetValue(key, out object v))
        {
            lists[key] = new List<T>();
            return (List<T>)lists[key];
        }
        return (List<T>)v;
    }
    public static void SetList<T>(string key, List<T> value)
    {
        data[typeof(List<T>)][key] = value;
    }
    public static void AddToList<T>(string key, T value)
    {
        var stringLists = data[typeof(List<T>)];
        if (!stringLists.TryGetValue(key, out _))
        {
            stringLists[key] = new List<T>();
        }
        ((List<T>)stringLists[key]).Add(value);
    }

    /// <returns>Whether the string was removed.</returns>
    public static bool RemoveFromList<T>(string key, T value)
    {
        var lists = data[typeof(List<T>)];
        if (!lists.TryGetValue(key, out object list))
        {
            return false;
        }
        ((List<T>)list).Remove(value);
        return true;
    }

    #endregion List

    #region IntList
    public static List<int> GetIntList(string key)
    {
        return GetList<int>(key);
    }
    public static List<int> GetIntList(IntListKey key)
    {
        return GetList<int>(key.ToString());
    }
    public static void SetIntList(string key, List<int> value)
    {
        SetList(key, value);
    }
    public static void SetIntList(IntListKey key, List<int> value)
    {
        SetList(key.ToString(), value);
    }
    public static void AddIntToList(string key, int value)
    {
        AddToList(key, value);
    }
    public static void AddIntToList(IntListKey key, int value)
    {
        AddToList(key.ToString(), value);
    }

    /// <returns>Whether the string was removed.</returns>
    public static bool RemoveIntFromList(string key, int value)
    {
        return RemoveFromList(key, value);
    }
    /// <returns>Whether the string was removed.</returns>
    public static bool RemoveIntFromList(StringKey key, int value)
    {
        return RemoveFromList(key.ToString(), value);
    }
    #endregion IntList

    #region StringList
    public static List<string> GetStringList(string key)
    {
        return GetList<string>(key);
    }
    public static List<string> GetStringList(StringListKey key)
    {
        return GetList<string>(key.ToString());
    }
    public static void SetStringList(string key, List<string> value)
    {
        SetList(key, value);
    }
    public static void SetStringList(StringListKey key, List<string> value)
    {
        SetList(key.ToString(), value);
    }
    public static void AddStringToList(string key, string value)
    {
        AddToList(key, value);
    }
    public static void AddStringToList(StringListKey key, string value)
    {
        AddToList(key.ToString(), value);
    }

    /// <returns>Whether the string was removed.</returns>
    public static bool RemoveStringFromList(string key, string value)
    {
        return RemoveFromList(key, value);
    }
    /// <returns>Whether the string was removed.</returns>
    public static bool RemoveStringFromList(StringKey key, string value)
    {
        return RemoveFromList(key.ToString(), value);
    }
    #endregion StringList

    #region Quaternion
    public static Quaternion GetQuaternion(string key)
    {
        var quaternions = data[typeof(Quaternion)];
        if (!quaternions.TryGetValue(key, out object v))
        {
            quaternions[key] = DEFAULT_VALUES[typeof(Quaternion)];
            return (Quaternion)quaternions[key];
        }
        return (Quaternion)v;
    }
    public static Quaternion GetQuaternion(QuaternionKey key)
    {
        return GetQuaternion(key.ToString());
    }
    public static void SetQuaternion(string key, Quaternion value)
    {
        data[typeof(Quaternion)][key] = value;
    }
    public static void SetQuaternion(QuaternionKey key, Quaternion value)
    {
        SetQuaternion(key.ToString(), value);
    }
    #endregion Quaternion

    #region ScriptableObject
    public static ScriptableObject GetScriptableObject(string key)
    {
        var scriptableObjects = data[typeof(ScriptableObject)];
        if (!scriptableObjects.TryGetValue(key, out object obj))
        {
            scriptableObjects[key] = default;
            return (ScriptableObject)scriptableObjects[key];
        }
        return (ScriptableObject)obj;
    }
    public static ScriptableObject GetScriptableObject(ScriptableObjectKey key)
    {
        return GetScriptableObject(key.ToString());
    }
    public static void SetScriptableObject(string key, ScriptableObject obj)
    {
        data[typeof(ScriptableObject)][key] = obj;
    }
    public static void SetScriptableObject(ScriptableObjectKey key, ScriptableObject obj)
    {
        SetScriptableObject(key.ToString(), obj);
    }
    #endregion ScriptableObject

    #region String

    public static string GetString(string key)
    {
        var strings = data[typeof(string)];
        if (!strings.TryGetValue(key, out object v))
        {
            strings[key] = DEFAULT_VALUES[typeof(string)];
            return (string)strings[key];
        }
        return (string)v;
    }
    public static string GetString(StringKey key)
    {
        return GetString(key.ToString());
    }
    public static void SetString(string key, string value)
    {
        data[typeof(string)][key] = value;
    }
    public static void SetString(StringKey key, string value)
    {
        SetString(key.ToString(), value);
    }
    #endregion String

    #region Vector3
    public static Vector3 GetVector3(string key)
    {
        var vector3s = data[typeof(Vector3)];
        if (!vector3s.TryGetValue(key, out object v))
        {
            vector3s[key] = DEFAULT_VALUES[typeof(Vector3)];
            return (Vector3)vector3s[key];
        }
        return (Vector3)v;
    }
    public static Vector3 GetVector3(Vector3Key key)
    {
        return GetVector3(key.ToString());
    }

    public static void SetVector3(string key, Vector3 value)
    {
        data[typeof(Vector3)][key] = value;
    }
    public static void SetVector3(Vector3Key key, Vector3 value)
    {
        SetVector3(key.ToString(), value);
    }
    #endregion Vector3
}

public enum BoolKey
{
    IsGameActive,
    IsDialogueUnrolling,
    IsScreenTransitioning,
    CanOpenMenu,
    IsMenuOpen,
    IsInteractIndicatorActive,
}
public enum FloatKey
{

}
public enum GameObjectKey
{

}
public enum IntKey
{
    CurrentPhase
}

public enum IntListKey
{
}

public enum StringListKey
{

}
public enum QuaternionKey
{

}
public enum ScriptableObjectKey
{
    DialogueData
}
public enum StringKey
{
    DialogueText,
    DialogueSpeakerName,
    NewUIName
}
public enum Vector3Key
{

}