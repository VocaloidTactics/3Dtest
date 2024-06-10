using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
//实现让被挂在的物体往前移动
//按下W键往前移动，按下S键往后移动
public class RoleMove : MonoBehaviour
{
    public  float myspeed = 0.1f;
   
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("检测到你按下了W键，我开始移动啦");
            this.transform.Translate( 0, 0, 1 * myspeed,Space.Self);
            Debug.Log("开始移动啦");
        }
        //--往后移动
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("检测到你按下了S键，我开始移动啦");
            this.transform.Translate(0, 0, -1 * myspeed, Space.Self);
            Debug.Log("开始移动啦");
        }
        //--往左移动
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("检测到你按下了A键，我开始移动啦");
            this.transform.Translate(-1 * myspeed, 0, 0, Space.Self);
            Debug.Log("开始移动啦");
        }
        //--往YOU移动
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("检测到你按下了D键，我开始移动啦");
            this.transform.Translate(1 * myspeed, 0, 0, Space.Self);
            Debug.Log("开始移动啦");
        }
 
    }
 
}