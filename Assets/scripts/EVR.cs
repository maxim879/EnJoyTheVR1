// //The behavior of some methods of this script differ slightly from the script in the game, please be careful


using UnityEngine;
using XLua;
using System;
using UnityEngine.SceneManagement;

namespace EVR
{
    [LuaCallCSharp]
    public class API : MonoBehaviour
    {
        public GameObject Player
        {
            get
            {
                return GameObject.Find("PlayerController");
            }
        }

        private string GetAssetBundlePath()
        {
            return PlayerPrefs.GetString("LastLoadedAssetBundle");
        }

         public void Save(string key, object value)
        {
            if (value is int)
            {
                PlayerPrefs.SetInt(key, (int)value);
            }
            else if (value is long)
            {
                PlayerPrefs.SetFloat(key, (int)(long)value);
            }
            else if (value is float)
            {
                PlayerPrefs.SetFloat(key, (float)value);
            }
            else if (value is string)
            {
                PlayerPrefs.SetString(key, (string)value);
            }
            else if (value is bool)
            {
                PlayerPrefs.SetInt(key, (bool)value ? 1 : 0);
            }
            else
            {
                Debug.LogError("Тип переменной не поддерживается для сохранения.");
            }

            PlayerPrefs.Save();
        }

        public object Load(string key, string type)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                Debug.LogWarning("Ключ не найден: " + key);
                return null;
            }

            if (type == "string")
            {
                string value = PlayerPrefs.GetString(key);
                if (string.IsNullOrEmpty(value))
                {
                    Debug.LogWarning("Загруженная строка пуста: " + key);
                }
                return value;
            }
            else if (type == "int")
            {
                return Convert.ToInt32(PlayerPrefs.GetFloat(key));
            }
            else if (type == "long")
            {
                return PlayerPrefs.GetInt(key);
            }
            else if (type == "float")
            {
                return PlayerPrefs.GetFloat(key);
            }
            else if (type == "bool")
            {
                return PlayerPrefs.GetInt(key) == 1;
            }
            else
            {
                Debug.LogError("Тип переменной не поддерживается для загрузки: " + type);
                return null;
            }
        }

        public void SetTime(float scale)
        {
            Time.timeScale = scale;
        }

        public void CloseApp()
        {
            Debug.Log("YourAppWasClosed");
        }

        public void LoadScene(string scene, bool deletePrev)
        {
            SceneManager.LoadScene(scene);
        }
        public void SetNewHandL(GameObject handObject = null)
        {
            Debug.Log("LeftHandChanged");
        }

        public void SetNewHandR(GameObject handObject = null)
        {
            Debug.Log("RightHandChanged");
        }
        public void BlockInput()
        {
            Debug.Log("StandartInputWasTurnedOFF");
        }

        public void UnblockInput()
        {
            Debug.Log("StandartInputWasTurnedON");
        }
    }
}