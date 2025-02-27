using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

//유져 정보 저장 관련
public class MyInfoManager
{
    const string FILE_NAME = "UserInfo";
    protected static MyInfoManager _instance;
    public static MyInfoManager Instance
    {
        set
        {
            _instance = value;
        }
        get
        {
            if (_instance == null)
            {
                string jsonData = SecurityPlayerPrefs.GetString(FILE_NAME, string.Empty);
                Debug.Log(jsonData);
                if (string.IsNullOrEmpty(jsonData) == false)
                {
                    _instance = XOR.XOREncryption.FromString<MyInfoManager>(jsonData);
                }

                if (_instance == null)
                {
                    _instance = new MyInfoManager();
                }
            }

            return _instance;
        }
    }
    //유닛정보
    public List<HeroSaveData> HeroSaveDatas = new List<HeroSaveData>();
    //보유자원 정보
    //




    public void SaveData()
    {
        //string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(instance);
        string jsonData = XOR.XOREncryption.GetString(this);
        SecurityPlayerPrefs.SetString(FILE_NAME, jsonData);
        PlayerPrefs.Save();
    }
}

namespace XOR
{
    public class XOREncryption
    {
        readonly static public byte[] encryptKey = Encoding.UTF8.GetBytes("eNPAC_Town_#");

        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            byte[] encryptedBytes = new byte[data.Length];
            for (int i = 0; i < data.Length; ++i)
            {
                encryptedBytes[i] = (byte)((data[i] ^ key[i % key.Length]) + key.Length);
            }

            return encryptedBytes;
        }

        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            byte[] decryptedBytes = new byte[data.Length];
            for (int i = 0; i < data.Length; ++i)
            {
                decryptedBytes[i] = (byte)((data[i] - key.Length) ^ key[i % key.Length]);
            }

            return decryptedBytes;
        }

        public static string GetString(object obj)
        {
            byte[] bytes = GetBytes(obj);
            string data = Convert.ToBase64String(bytes);
            return data;
        }

        public static byte[] GetBytes(object obj)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] bytes = Encoding.Unicode.GetBytes(json);
            return Decrypt(bytes, encryptKey);
        }



        public static T FromString<T>(string data)
        {
            byte[] btyes = Convert.FromBase64String(data);
            return FromBytes<T>(btyes);
        }

        public static T FromBytes<T>(byte[] bytes)
        {
            byte[] encrypts = Encrypt(bytes, encryptKey);

            string json = Encoding.Unicode.GetString(encrypts);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
    }
}