using System.Collections.Generic;
using SecPlayerPrefs;

public class SaveData
{
    /** A local data saver using Unity player pref
    * It use secure player pref to encrypt data
    * https://forum.unity.com/threads/released-secureplayerprefs.320354
    - SetTmpItem ==> it store data in heap
    - GetTmpItem ==> it fetch stored data from heap
    - SetItem ==> it store [int,string] securely using secure player pref
    - GetItemString ==> it fetch string from secure player pref
    - GetItemInt ==> it fetch int from secure player pref
    - CheckItemExist ==> it check if an item exit in player pref
    - UnsetItem ==> it remove store item from player pref
    */
    private static Dictionary<string, object> tmpData = new Dictionary<string, object>();

    public static void SetTmpItem(string itemName, int itemValue)
    { // it set temporary data to memory
        if (tmpData.ContainsKey(itemName))
            tmpData[itemName] = itemValue;
        else
            tmpData.Add(itemName, itemValue);
    }
    public static object GetTmpItem(string itemName)
    { // it get temporary data from memory
        if (tmpData.ContainsKey(itemName))
            return tmpData[itemName];
        return null;
    }

    public static void SetItemInt(string itemName, int itemValue)
    {
        SecurePlayerPrefs.SetInt(itemName, itemValue);
    }
    public static void SetItem(string itemName, string itemValue)
    {
        SecurePlayerPrefs.SetString(itemName, itemValue);
    }

    public static string GetItemString(string itemName)
    {
        return CheckItemExist(itemName) ? SecurePlayerPrefs.GetString(itemName) : string.Empty;
    }
    public static int GetItemInt(string itemName)
    {
        return CheckItemExist(itemName) ? SecurePlayerPrefs.GetInt(itemName) : 0;
    }

    public static void SetItemBool(string itemName, bool value)
    {
        SecurePlayerPrefs.SetBool(itemName, value);
    }
    public static bool GetItemBool(string itemName)
    {
        return SecurePlayerPrefs.GetBool(itemName);
    }


    public static bool CheckItemExist(string itemName)
    {
        return SecurePlayerPrefs.HasKey(itemName) ? true : false;
    }

    public static void UnsetItem(string itemName)
    {
        if (CheckItemExist(itemName))
            SecurePlayerPrefs.DeleteKey(itemName);
    }

}
