using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using Photon.Realtime;


namespace EVR
{
    public class EVRPhotonServerList : MonoBehaviourPunCallbacks
    {
        [Header("Настройки")]
        [SerializeField] private GameObject serverPanelPrefab;
        [SerializeField] private Transform contentParent;

        private List<RoomInfo> roomList = new List<RoomInfo>();

        public void UpdateServerList(List<RoomInfo> updatedRoomList)
        {
            foreach (Transform child in contentParent)
            {
                Destroy(child.gameObject);
            }

            roomList.Clear();
            roomList.AddRange(updatedRoomList);

            foreach (RoomInfo room in roomList)
            {
                GameObject serverPanel = Instantiate(serverPanelPrefab, contentParent);
                EVRPhotonServerPanel panelScript = serverPanel.GetComponent<EVRPhotonServerPanel>();

                if (panelScript != null)
                {
                    panelScript.SetRoomInfo(room.Name, room.PlayerCount, room.MaxPlayers);
                }
            }
        }
        public override void OnRoomListUpdate(List<RoomInfo> updatedRoomList)
        {
            UpdateServerList(updatedRoomList);
        }

    }
}