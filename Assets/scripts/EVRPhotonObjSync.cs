using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Reflection;

namespace EVR
{
    public class EVRPhotonObjSync : MonoBehaviour
    {
        public bool CheckIsMine = true;
        public void SyncEvent(GameObject targetObject, string scriptName, string methodName, object[] parameters = null)
        {
            if (targetObject == null)
            {
                Debug.LogError("Target object is null");
                return;
            }

            PhotonView photonView = targetObject.GetComponent<PhotonView>();
            if (photonView == null)
            {
                Debug.LogError($"PhotonView not found on object {targetObject.name}");
                return;
            }

            // Сериализация параметров в строку JSON
            string serializedParameters = SerializeParameters(parameters);
            photonView.RPC("ExecuteEvent", RpcTarget.All, scriptName, methodName, serializedParameters);
        }

        // RPC метод для выполнения синхронизированного события
        [PunRPC]
        private void ExecuteEvent(string scriptName, string methodName, string serializedParams)
        {
            // Десериализация строки JSON обратно в параметры
            object[] parameters = DeserializeParameters(serializedParams);

            // Находим компонент по имени
            Type scriptType = Type.GetType(scriptName);
            if (scriptType == null)
            {
                Debug.LogError($"Script {scriptName} not found.");
                return;
            }

            // Ищем экземпляр класса в объекте
            object targetScript = gameObject.GetComponent(scriptType);
            if (targetScript == null)
            {
                Debug.LogError($"Script {scriptName} not found on object {gameObject.name}");
                return;
            }

            // Находим метод в компоненте
            MethodInfo method = scriptType.GetMethod(methodName);
            if (method == null)
            {
                Debug.LogError($"Method {methodName} not found in script {scriptName} on object {gameObject.name}");
                return;
            }

            // Вызываем метод
            if (this.GetComponent<PhotonView>().IsMine == false)
            {
                method.Invoke(targetScript, parameters);
            }
            if (CheckIsMine == false)
            {
                method.Invoke(targetScript, parameters);
            }
            
        }

        // Сериализация параметров в строку JSON
        private string SerializeParameters(object[] parameters)
        {
            return JsonUtility.ToJson(new ParameterWrapper(parameters));
        }

        // Десериализация строки JSON обратно в параметры
        private object[] DeserializeParameters(string serializedParameters)
        {
            ParameterWrapper wrapper = JsonUtility.FromJson<ParameterWrapper>(serializedParameters);
            return wrapper.Parameters;
        }

        // Класс-обертка для сериализации параметров
        [System.Serializable]
        private class ParameterWrapper
        {
            public object[] Parameters;

            public ParameterWrapper(object[] parameters)
            {
                Parameters = parameters;
            }
        }
    }
}