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

// �浵ϵͳ��
public class SaveSystem
{
    // �浵�ļ���·��
    private static string saveFilePath = Application.persistentDataPath + "/savefile.json";

    // ��������
    public static void SaveGame(GameData data)
    {
        try
        {
            // ���������л�ΪJSON��ʽ
            string jsonData = JsonUtility.ToJson(data, true);
            // ��JSON����д���ļ�
            File.WriteAllText(saveFilePath, jsonData);
        }
        catch (Exception ex)
        {
            Debug.LogError("������Ϸ����ʱ����: " + ex.Message);
        }
    }

    // ��ȡ����
    public static GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                // ���ļ���ȡJSON����
                string jsonData = File.ReadAllText(saveFilePath);
                // ��JSON���ݷ����л�ΪGameData����
                GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);
                return loadedData;
            }
            catch (Exception ex)
            {
                Debug.LogError("��ȡ��Ϸ����ʱ����: " + ex.Message);
                return null;
            }
        }
        else
        {
            Debug.LogWarning("�浵�ļ������ڣ��޷���ȡ����");
            return new GameData();
        }
    }

    // ɾ���浵
    public static void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
        else
        {
            Debug.LogWarning("�浵�ļ������ڣ��޷�ɾ��");
        }
    }

    // ����Ƿ���ڴ浵
    public static bool SaveExists()
    {
        return File.Exists(saveFilePath);
    }
}
