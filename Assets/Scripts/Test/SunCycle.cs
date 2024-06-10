using UnityEngine;

public class SunCycle : MonoBehaviour
{
    public float speed = 1.0f;  // 太阳运动的速度

    void Update()
    {
        // 每帧旋转光源，模拟太阳的运动
        // 绕X轴旋转，模拟日出和日落
        transform.Rotate(speed * Time.deltaTime, 0, 0);

        // 可选：调整光照强度，模拟日出和日落时的光线变化
        float angle = transform.eulerAngles.x;
        float intensity = Mathf.Clamp01((Mathf.Cos(angle * Mathf.Deg2Rad) + 1) / 2);
        GetComponent<Light>().intensity = intensity;
    }
}