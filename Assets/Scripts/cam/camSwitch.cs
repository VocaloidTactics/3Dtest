using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras;
    private int currentCameraIndex = 0;

    void Start()
    {
        // 初始化相机状态
        SetActiveCamera(currentCameraIndex, true);
    }

    void Update()
    {
        // 在Update中检测按键 V，按下时切换到下一个相机
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchToNextCamera();
        }
    }

    void SwitchToNextCamera()
    {
        // 禁用当前相机
        SetActiveCamera(currentCameraIndex, false);

        // 切换到下一个相机
        currentCameraIndex = (currentCameraIndex + 1) % virtualCameras.Length;

        // 启用新的相机
        SetActiveCamera(currentCameraIndex, true);
    }

    void SetActiveCamera(int index, bool isActive)
    {
        // 设置相机的启用状态
        if (index >= 0 && index < virtualCameras.Length)
        {
            virtualCameras[index].gameObject.SetActive(isActive);
        }
    }
}