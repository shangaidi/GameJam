/*
 *FileName:      Spawn.cs
 *Author:        MC
 *Date:          2021/04/24 14:18:03
 *UnityVersion:  2020.3.0f1c1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public enum ItemType
    {
        FISH,
        GRASS,
        ITEM
    }
    public ItemType _ItemType;

    public GameObject[] Spawns;

    private float nextSpawn;
    private float nextSpawnItem = 10;

    float _scale;
    void Update()
    {
        if (_ItemType == ItemType.FISH) 
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + GameManager.Instance.colddown;

                Vector3 spawnPos = transform.position;
                spawnPos.x += Random.Range(-6f, 6f);
                GameObject _item = Instantiate(Spawns[Random.Range(0, Spawns.Length)], spawnPos, Quaternion.identity);
            }
        }
        else if (_ItemType == ItemType.GRASS)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + GameManager.Instance.colddown;

                Vector3 spawnPos = transform.position;
                GameObject _item = Instantiate(Spawns[Random.Range(0, Spawns.Length)], spawnPos, Quaternion.identity);
                //Scale            
                if (TimeManager.Instance.timer<=10)
                {
                    _scale = Random.Range(0.3f, 0.3f);
                }
                else if (TimeManager.Instance.timer > 10&& TimeManager.Instance.timer<=20)
                {
                    _scale = Random.Range(0.4f, 0.6f);
                }
                else if (TimeManager.Instance.timer > 20)
                {
                    _scale = Random.Range(0.5f, 0.9f);
                }
                _item.transform.localScale = new Vector2(_scale, _scale);
                // rotate
                float _rotate = Random.Range(-30f, 30f);
                _item.transform.GetChild(0).localEulerAngles = new Vector3(0, 0, _rotate);
            }
        }
        else if (_ItemType == ItemType.ITEM)
        {
            if (Time.time > nextSpawnItem)
            {
                nextSpawnItem = Time.time + GameManager.Instance.O2colddown;

                Vector3 itemPos = transform.position;
                itemPos.x += Random.Range(-6f, 6f);
                GameObject _item = Instantiate(Spawns[Random.Range(0, Spawns.Length)], itemPos, Quaternion.identity);
            }
        }


    }
}
