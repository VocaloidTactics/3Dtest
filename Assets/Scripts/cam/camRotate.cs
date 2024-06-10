using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camRotate: MonoBehaviour
{
    // 灵敏度
    public float mouseSensitivity = 2f;
    public float scrollSensitivity = 50f;
    
    // 最大和最小的垂直旋转角度
    private float maxVerticalRotation = 80f;
    private float minVerticalRotation = 40f;
    
    //外部对象
    public GameObject go;
    
    // 初始FOV
    private float currentFov = 60f;
    
    // 最大和最小的FOV
    private float minFov = 40f;
    private float maxFov = 80f;
    
    // 初始视角
    private float xRotation = 45f;
    private Quaternion initialRotation;
    
    // 相机对象
    private CinemachineVirtualCamera vcam;
    
    // 鼠标按下标志
    private bool isRightMouseButtonDown = false;
    
    // 鼠标右键按下时的时间
    private float releaseTime = -1;
    
    // 1秒后重置视角
    private float resetDelay = 1f; 


    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        initialRotation = transform.rotation;
        currentFov = vcam.m_Lens.FieldOfView;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseButtonDown = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRightMouseButtonDown = false;
            releaseTime = Time.time;
        }
        
        // 鼠标右键旋转
        if (isRightMouseButtonDown)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            // transform.Rotate(Vector3.up * mouseX);
            
            // 绕go的y轴进行水平旋转
            transform.RotateAround(go.transform.position, Vector3.up, mouseX);
        }
        else if (releaseTime != -1 && Time.time - releaseTime >= resetDelay)
        {
            //ResetCameraRotation();
        }
        
        // 通过鼠标滚轮控制相机缩放
        wheelMethod();
    }
    
    // 鼠标滚轮操作
    private void wheelMethod()
    {
        
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0f)
        {
            //调整fov
            currentFov += scrollDelta * scrollSensitivity;
            currentFov = Mathf.Clamp(currentFov, minFov, maxFov);
            vcam.m_Lens.FieldOfView = currentFov;
            
            // 根据鼠标滚轮的滚动方向和角度,调整相机的垂直角度
            float verticalRotationDelta = scrollDelta * scrollSensitivity;
            xRotation = Mathf.Clamp(xRotation + verticalRotationDelta, minVerticalRotation, maxVerticalRotation);
            transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y, 0f);
        }
    }
    
    // 重置相机视角
    private void ResetCameraRotation()
    {
        transform.rotation = initialRotation;
    }
}
