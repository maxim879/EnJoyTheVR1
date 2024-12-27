using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Photon.Realtime.Demo
{
    public class PhotonSetConnection : MonoBehaviour
    {

        [SerializeField]
        private AppSettings appSettings = new AppSettings();
        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings(appSettings);
        }
    }
}