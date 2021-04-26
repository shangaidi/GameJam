using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemEnLarge : MonoBehaviour {

    Vector2 oldPos1;
    Vector2 oldPos2;
	void Start () {
		
	}
	
	void Update () {
        if (Input.touchCount == 2&& !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Vector2 tempPos1 = Input.GetTouch(0).position;
                Vector2 tempPos2 = Input.GetTouch(1).position;
                if (isEnLarge(oldPos1, oldPos2, tempPos1, tempPos2))
                {
                    float oldScalse = transform.localScale.x;
                    float newScalse = oldScalse * 1.025f;
                    transform.localScale = new Vector3(newScalse, newScalse, newScalse);
                }
                else
                {
                    float oldScalse = transform.localScale.x;
                    float newScalse = oldScalse / 1.025f;
                    transform.localScale = new Vector3(newScalse, newScalse, newScalse);
                }
                oldPos1 = tempPos1;
                oldPos2 = tempPos2;

            }
        }
    }

    //判断手势
    bool isEnLarge(Vector2 oP1,Vector2 oP2,Vector2 nP1,Vector2 nP2)
    {
        float length1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) 
            + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float length2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x)
            + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        if (length1<length2)//放大
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
