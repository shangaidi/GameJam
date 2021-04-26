using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemRotate : MonoBehaviour {
    public float rotateSpeed = 0.5f;
    public bool _is360=false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {        
        if (_is360==true)
        {
            if (1 == Input.touchCount && !EventSystem.current.IsPointerOverGameObject())
            {
                Touch touch = Input.GetTouch(0);
                Vector2 deltaPos = touch.deltaPosition;
                transform.Rotate(Vector3.down * deltaPos.x * rotateSpeed, Space.World);
                transform.Rotate(Vector3.right * deltaPos.y, Space.World);
            }
        }
        else
        {
            //单点触摸， 水平上下旋转  
            if (1 == Input.touchCount && !EventSystem.current.IsPointerOverGameObject())
            {
                Touch touch = Input.GetTouch(0);
                Vector2 deltaPos = touch.deltaPosition;
                transform.Rotate(Vector3.down * deltaPos.x * rotateSpeed, Space.World);
                //transform.Rotate(Vector3.right * deltaPos.y, Space.World);
            }
        }
    }
}
