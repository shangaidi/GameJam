using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewRotationZ : MonoBehaviour
{
    private Quaternion mainRot = Quaternion.identity;
    [HideInInspector]
    public float roll = 0;
    [HideInInspector]
    public float pitch = 0;
    [HideInInspector]
    public float yaw = 0;
    private GameObject Tip;
    private float timer = 1.0f;

    bool IsClickscreen = false;

    private void Start()
    {
        mainRot = this.transform.rotation;
    }
    void Update()
    {
        if (Input.touchCount <= 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (IsClickscreen == true)
                {
                    foreach (Transform child in this.gameObject.GetComponentsInChildren<Transform>())
                    {
                        if (child.GetComponent<Outline>() != null)
                        {
                            child.GetComponent<Outline>().enabled = false;
                        }
                    }
                }
                //等待之后执行的动作  
                //飞机的旋转增量
                Quaternion AddRot = Quaternion.identity;
                // 利用roll、yaw、pitch的输入计算旋转增量
                AddRot.eulerAngles = new Vector3(pitch, yaw, -roll);
                //利用旋转增量计算飞机的目标旋转值
                mainRot *= AddRot;

                //增加飞机roll和pitch的自动回正功能，用来简化玩家操作。saveQ是简化后的飞机旋转值
                Quaternion saveQ = mainRot;
                Vector3 fixedAngles = new Vector3(mainRot.eulerAngles.x, mainRot.eulerAngles.y, mainRot.eulerAngles.z);

                fixedAngles.x = 0;
                fixedAngles.y = 0;
                fixedAngles.z = 0;
                saveQ.eulerAngles = fixedAngles;

                //将目标旋转值线性逼近saveQ，线性逼近的目的是为了让数据平滑，让玩家体验更接近物理现实，更自然。
                mainRot = Quaternion.Lerp(mainRot, saveQ, Time.fixedDeltaTime * 2);
                //让飞机的旋转值逼近目标旋转值
                GetComponent<Rigidbody>().rotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, mainRot, Time.fixedDeltaTime * 2f);
                //让yaw自动回正
                yaw = Mathf.Lerp(yaw, 0, Time.deltaTime);
                pitch = Mathf.Lerp(pitch, 0, Time.deltaTime);
                roll = Mathf.Lerp(roll, 0, Time.deltaTime);
                //this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime);
            }
        }
        if (1 == Input.touchCount)
        {
            timer = 1.0f;
        }        
    }
}
