// //The behavior of some methods of this script differ slightly from the script in main EnJoyTheVR project, please be careful


using UnityEngine;
using XLua;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
namespace EVR
{

    [LuaCallCSharp]
    public class API : MonoBehaviour
    {
        public bool JoyConModeEnabled = false;
        public GameObject Player
        {
            get
            {
                return GameObject.Find("PlayerController");
            }
        }
        public GameObject PlayerHead
        {
            get { return GameObject.Find("EVRHead"); }
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
        public void SetNewHead(GameObject head = null)
        {
            Debug.Log("HeadChanged");
        }
        public void SetStandartHead()
        {
            Debug.Log("HeadWasSetToStandart");
        }

        public void SetStadnartHandL()
        {
            Debug.Log("LeftChangedWasSetToStandart");
        }
        public void SetStadnartHandR()
        {
            Debug.Log("RightChangedWasSetToStandart");
        }

        public void SetNewHandR(GameObject handObject = null)
        {
            Debug.Log("RightHandChanged");
        }
        public void BlockInput()
        {
            Debug.Log("StandartInputWasTurnedOFF");
        }
        public void BlockStick()
        {
            Debug.Log("StandartMovementWasTurnedOFF");
        }
        public void UnblockInput()
        {
            Debug.Log("StandartInputWasTurnedON");
        }
        public void UnblockStick()
        {
            Debug.Log("StandartMovementWasTurnedON");
        }
        public void EnableKeyboard(TMP_InputField AttachedInputField)
        {
            Debug.Log("Keyboard is enabled");
        }
        public void DisableKeyboard()
        {
            Debug.Log("Keyboard is disabled");
        }

        public KeyCode AKeyCode()
        {
            return KeyCode.A;
        }

        public KeyCode BKeyCode()
        {
            return KeyCode.B;
        }

        public KeyCode XKeyCode()
        {
            return KeyCode.X;
        }

        public KeyCode YKeyCode()
        {
            return KeyCode.Y;
        }

        public KeyCode RStickButtonKeyCode()
        {
            return KeyCode.RightShift;
        }

        public KeyCode LStickButtonKeyCode()
        {
            return KeyCode.LeftShift;
        }

        public KeyCode RKeyCode()
        {
            return KeyCode.R;
        }

        public KeyCode LKeyCode()
        {
            return KeyCode.L;
        }

        public KeyCode ZRKeyCode()
        {
            return KeyCode.F;
        }

        public KeyCode ZLKeyCode()
        {
            return KeyCode.M;
        }

        public KeyCode UPKeyCode()
        {
            return KeyCode.UpArrow;
        }

        public KeyCode DownKeyCode()
        {
            return KeyCode.DownArrow;
        }

        public KeyCode LeftKeyCode()
        {
            return KeyCode.LeftArrow;
        }

        public KeyCode RightKeyCode()
        {
            return KeyCode.RightArrow;
        }
    }

    // public class Photon : MonoBehaviour
    // {
    //     public void CreateRoom(string name)
    //     {
    //         PhotonNetwork.CreateRoom(name);
    //     }
    //     public void JoinRoom(string name)
    //     {
    //         PhotonNetwork.JoinRoom(name);
    //     }
    //     public void Instantiate(string name, Vector3 vector, Quaternion quaternion)
    //     {
    //         PhotonNetwork.Instantiate(name, vector, quaternion);
    //     }
    // }
}