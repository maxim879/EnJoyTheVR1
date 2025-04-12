using System;
using UnityEngine;
using Photon.Pun;
using System.Reflection;

namespace EVR
{
    public class EVRPhotonObjSync : MonoBehaviour
    {
        public bool CheckIsMine = true;

        public void SyncEvent(GameObject targetObject, string scriptName, string methodName, string parameter = null)
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

            // Just send the string directly instead of serializing object[]
            photonView.RPC("ExecuteEvent", RpcTarget.All, scriptName, methodName, parameter);
        }

        [PunRPC]
        private void ExecuteEvent(string scriptName, string methodName, string parameter)
        {
            Type scriptType = Type.GetType(scriptName);
            if (scriptType == null)
            {
                Debug.LogError($"Script {scriptName} not found.");
                return;
            }

            object targetScript = gameObject.GetComponent(scriptType);
            if (targetScript == null)
            {
                Debug.LogError($"Script {scriptName} not found on object {gameObject.name}");
                return;
            }

            MethodInfo method = scriptType.GetMethod(methodName);
            if (method == null)
            {
                Debug.LogError($"Method {methodName} not found in script {scriptName} on object {gameObject.name}");
                return;
            }

            if (!CheckIsMine || !GetComponent<PhotonView>().IsMine)
            {
                method.Invoke(targetScript, new object[] { parameter });
            }
        }
    }
}
