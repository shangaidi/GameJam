/*
 *FileName:      FishMove.cs
 *Author:        MC
 *Date:          2021/04/24 18:07:34
 *UnityVersion:  2020.3.0f1c1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    int direction = 0;

    public int speed ;
    private void OnEnable()
    {
        Destroy(this.gameObject, GetSpeed());

        if (transform.position.x <= 0) //Left
        {
            direction = 1;
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (transform.position.x > 0) //Right
        {
            direction = -1;
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    private int GetSpeed()
    {
        if (speed==1)
        {
            return 11;
        }
        else
        {
            return 7;
        }
    }
    private void Update()
    {
        ChangeDirection();
    }
    //turn·¶Î§
    private void ChangeDirection()
    {
        if (transform.position.x < -6)//left
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            direction = 1;
            transform.Translate(new Vector3(direction, 1.5f * GameManager.Instance.GameSpeed, 0) * speed * Time.deltaTime);
        }
        else if (transform.position.x > 6)//right
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            direction = -1;
            transform.Translate(new Vector3(direction, 1.5f * GameManager.Instance.GameSpeed, 0) * speed * Time.deltaTime);
        }
        else//other
        {
            transform.Translate(new Vector3(direction, 1.5f * GameManager.Instance.GameSpeed, 0) * speed * Time.deltaTime);
        }
    }
}
