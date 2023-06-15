using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState
{
    MAINMENU, SHOP, GAMEPLAY
}
public class CameraFollow : Singleton<CameraFollow>
{
    public Transform target;
    public Vector3 mainMenuOffset;
    public Vector3 gamePlayOffset;
    public Vector3 shopOffset;
    private Quaternion mainMenuquaternion = Quaternion.Euler(new Vector3(20f, 0, 0));
    private Quaternion gamePlayQuaternion = Quaternion.Euler(new Vector3(45f, 0, 0));
    private Quaternion quaternion;
    public Vector3 offset;
    public float speed = 20;
    public CameraState cameraState;
    public Camera Camera;
    // Start is called before the first frame update
    private void Awake()
    {
        Camera = Camera.main;
        target = FindObjectOfType<Player>().transform;
        quaternion = mainMenuquaternion;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if (target != null) { }
        // Thay đổi vị trí theo Player
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
        transform.rotation = quaternion;
    }

    // Thay đổi vị trí và góc quay của camera theo state
    public void ChangeCameraState(CameraState state)
    {
        cameraState = state;
        switch(cameraState)
        {
            case CameraState.MAINMENU:
                offset = mainMenuOffset;
                quaternion = mainMenuquaternion;
                break;
            case CameraState.GAMEPLAY:
                offset = gamePlayOffset;
                quaternion = gamePlayQuaternion;
                break;
            case CameraState.SHOP:
                offset = shopOffset;
                quaternion = mainMenuquaternion;
                break;
        }
    }
}
