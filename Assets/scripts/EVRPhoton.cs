using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        public bool OnConnectedToMasterISOK = false;
        private string mapNameToLoad;
        void Start()
        {
            api = FindObjectOfType<API>();
        }
        public void CreateRoom(string name, string MapName = null)
        {
            PhotonNetwork.CreateRoom(name);
            if(MapName != null)
            {
                LoadScene = true;
                mapNameToLoad = MapName;
            }
            Debug.Log("created");
        }
        public void JoinRoom(string name, string MapName = null)
        {
            PhotonNetwork.JoinRoom(name);
            if(MapName != null)
            {
                LoadScene = true;
                mapNameToLoad = MapName;
            }
            Debug.Log("joined");
        }
        public void LeaveRoom(string MapName = null)
        {
            PhotonNetwork.LeaveRoom();
            if(MapName != null)
            {
                api.LoadScene(MapName, true);
            }
            Debug.Log("left");
        }
        public void DisconnectFromMaster()
        {
            PhotonNetwork.Disconnect();
            Debug.Log("disconnected");
        }
        public void Instantiate(string name, Vector3 vector, Quaternion quaternion)
        {
            PhotonNetwork.Instantiate(name, vector, quaternion);
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("ConnectedToMaster");
            OnConnectedToMasterISOK = true;
            // CreateRoom("test");
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
    }
}