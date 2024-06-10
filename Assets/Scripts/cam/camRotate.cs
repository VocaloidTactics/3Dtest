using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camRotate: MonoBehaviour
{
    // 灵敏度
    public float mouseSensitivity = 10f;
    public float scrollSensitivity = 50f;
    
    // 最大和最小的垂直旋转角度
    private float maxVerticalRotation = 80f;
    private float minVerticalRotation = 40f;
    
    //外部对象
    public GameObject go;
    
    // FOV
    private float targetFov = 60f;
    private float currentFov;
    
    // 最大和最小的FOV
    private float minFov = 40f;
    private float maxFov = 80f;
    
    // Rotation
    private float targetRotation;
    private float currentRotation = 45f;
    
    // 初始视角
    private Quaternion initialRotation;
    
    // 相机对象
    private CinemachineVirtualCamera vcam;
    
    // 鼠标按下标志
    private bool isRightMouseButtonDown = false;
    
    // 鼠标右键按下时的时间
    private float releaseTime = -1;
    
    // 重置视角延迟
    private float resetDelay = 1f; 
    
    // 平滑时间
    private float smoothTime = 0.1f;
    

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
            /* 调整fov */
            /***************/
            targetFov += scrollDelta * scrollSensitivity;
            targetFov = Mathf.Clamp(targetFov, minFov, maxFov);
            // 使用Lerp进行平滑插值
            currentFov = Mathf.Lerp(currentFov, targetFov, smoothTime);
            vcam.m_Lens.FieldOfView = currentFov;
            /***************/
            
            /* 调整垂直角度 */
            // 根据鼠标滚轮的滚动方向和角度,调整相机的垂直角度
            float verticalRotationDelta = scrollDelta * scrollSensitivity;
            targetRotation = Mathf.Clamp(targetRotation + verticalRotationDelta, minVerticalRotation, maxVerticalRotation);
            // 使用Lerp进行平滑插值
           
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, smoothTime);
            transform.localRotation = Quaternion.Euler(currentRotation, transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
            /***************/
        }
    }
    
    // 重置相机视角
    private void ResetCameraRotation()
    {
        transform.rotation = initialRotation;
    }
}
