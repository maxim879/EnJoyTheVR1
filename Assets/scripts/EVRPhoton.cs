using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using XLua;

namespace EVR
{
    public class EVRPhoton : MonoBehaviourPunCallbacks
    {
        [LuaCallCSharp]
        private API api;
        private bool LoadScene = false;
        public bool IsMaster = false;
        public bool OnConnectedToMasterISOK = false;
        private string mapNameToLoad;

        void Start()
        {
            api = FindObjectOfType<API>();
        }

        public void CreateRoom(string name, string MapName = null)
        {
            PhotonNetwork.CreateRoom(name);
            if (MapName != null)
            {
                LoadScene = true;
                mapNameToLoad = MapName;
            }
            Debug.Log("created");
        }

        public void JoinRoom(string name, string MapName = null)
        {
            PhotonNetwork.JoinRoom(name);
            if (MapName != null)
            {
                LoadScene = true;
                mapNameToLoad = MapName;
            }
            Debug.Log("joined");
        }

        public string NickName
        {
            get
            {
                return PhotonNetwork.NickName;
            }
        }

        public void SetNickName(string name)
        {
            PhotonNetwork.NickName = name;
        }

        public void LeaveRoom(string MapName = null)
        {
            PhotonNetwork.LeaveRoom();
            if (MapName != null)
            {
                api.LoadScene(MapName, true);
            }
            Debug.Log("left");
        }

        public void AutomaticallySyncSceneEnabled(bool enabled = false)
        {
            PhotonNetwork.AutomaticallySyncScene = enabled;
        }

        public void DisconnectFromMaster()
        {
            PhotonNetwork.Disconnect();
            Debug.Log("disconnected");
        }

        public void RegisterInPool(string name, bool needDestroy = false)
        {
            Debug.Log("objectRegistered");
            if (needDestroy == true)
            {
                Destroy(GameObject.Find(name));
                Debug.Log("objectRegistered and destroyed");
            }
        }

        public void Instantiate(string name, Vector3 vector, Quaternion quaternion)
        {
            PhotonNetwork.Instantiate(name, vector, quaternion);
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("ConnectedToMaster");
            PhotonNetwork.JoinLobby();
            OnConnectedToMasterISOK = true;
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("JoinedRoom");
            if (LoadScene)
            {
                api.LoadScene(mapNameToLoad, true);
                LoadScene = false;
            }
        }

        public void SetOwnership(GameObject targetObject)
        {
            PhotonView photonView = targetObject.GetComponent<PhotonView>();
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
            Debug.Log($"Ownership of object {targetObject.name} transferred to this player.");
        }

        public void Sync(GameObject targetObject, string scriptName, string methodName, string parameters = null)
        {
            targetObject.GetComponent<EVRPhotonObjSync>().SyncEvent(targetObject, scriptName, methodName, parameters);
        }

        public void CheckIfMaster()
        {
            IsMaster = PhotonNetwork.IsMasterClient;
        }
    }
}
