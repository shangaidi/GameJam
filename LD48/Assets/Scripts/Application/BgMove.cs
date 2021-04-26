/*
 *FileName:      BgMove.cs
 *Author:        MC
 *Date:          2021/04/24 13:56:53
 *UnityVersion:  2020.3.0f1c1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Bg,
    Item
}
public class BgMove : MonoBehaviour
{
    public Type type;
    //public float speed;
    // Start is called before the first frame update
    void Start()
    {
        if (type == Type.Item)
        {
            Destroy(this.gameObject, 7f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (type == Type.Bg)
        {
            this.transform.Translate(3*Vector3.up * Time.deltaTime * GameManager.Instance.GameSpeed);
            if (this.transform.localPosition.y > 15)
            {
                this.transform.localPosition = new Vector2(-0.1f, -35f);
            }
        }
        else if (type == Type.Item)
        {
            this.transform.Translate(3*Vector3.up * Time.deltaTime * GameManager.Instance.GameSpeed);
        }
    }
}
