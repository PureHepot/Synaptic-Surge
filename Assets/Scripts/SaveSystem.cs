using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public bool isFirstPlay;
    public int level;
}

// 存档系统类
public class SaveSystem
{
    // 存档文件的路径
    private static string saveFilePath = Application.persistentDataPath + "/savefile.json";

    // 保存数据
    public static void SaveGame(GameData data)
    {
        try
        {
            // 将数据序列化为JSON格式
            string jsonData = JsonUtility.ToJson(data, true);
            // 将JSON数据写入文件
            File.WriteAllText(saveFilePath, jsonData);
        }
        catch (Exception ex)
        {
            Debug.LogError("保存游戏进度时出错: " + ex.Message);
        }
    }

    // 读取数据
    public static GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                // 从文件读取JSON数据
                string jsonData = File.ReadAllText(saveFilePath);
                // 将JSON数据反序列化为GameData对象
                GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);
                return loadedData;
            }
            catch (Exception ex)
            {
                Debug.LogError("读取游戏进度时出错: " + ex.Message);
                return null;
            }
        }
        else
        {
            Debug.LogWarning("存档文件不存在，无法读取进度");
            return new GameData();
        }
    }

    // 删除存档
    public static void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
        else
        {
            Debug.LogWarning("存档文件不存在，无法删除");
        }
    }

    // 检查是否存在存档
    public static bool SaveExists()
    {
        return File.Exists(saveFilePath);
    }
}
