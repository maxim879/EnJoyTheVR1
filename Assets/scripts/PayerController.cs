using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerController : MonoBehaviour
{
    public float speed;
    //public float Mnozh = 1.0F;
    public Transform Player;
    public bool CharacterControllerInUse = false;
    public CharacterController character;
    public Transform cameraTransform;
    public float mouseSensitivity = 500f;

    public bool BlockedStick = false;
    private string Hor;
    private string Ver;
    private float xRotation = 0f;


    void Start()
    {
        if (PlayerPrefs.GetInt("UseSecStick") == 1)
        {
            Debug.Log("UseSecStick1");
            Hor = "HorizontalRight";
            Ver = "VerticalRight";
        }
        else
        {
            Debug.Log("UseSecStick0");
            Hor = "Horizontal";
            Ver = "Vertical";
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (CharacterControllerInUse == true && BlockedStick == false)
        {
            float x = Input.GetAxis(Hor);
            float z = Input.GetAxis(Ver);

            Vector3 forward = transform.right * x + transform.forward * z;
            character.SimpleMove(forward * speed);
            //Player.position += forward * speed;

        }
        else if (BlockedStick == false)
        {
            float x = Input.GetAxis(Hor);
            float z = Input.GetAxis(Ver);

            Vector3 forward = transform.right * x + transform.forward * z;
            //character.SimpleMove(forward * speed);
            Player.position += forward * speed;
        }
    }
    void Update()
    {
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // вращение тела игрока (влево/вправо)
        Player.Rotate(Vector3.up * mouseX);

        // вращение камеры (вверх/вниз)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ограничим поворот вверх/вниз

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
