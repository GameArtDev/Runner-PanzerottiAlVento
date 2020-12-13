﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        foreach(GameObject obj in levels)
        {
            LoadChildObjects(obj);
        }
    }

    void LoadChildObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int neededChildren = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;

        for(int i = 0; i <= neededChildren; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x + objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.transform.localScale = new Vector3(1, 1, 1);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void RepositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;

            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;

            
            if (mainCamera.transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
            {
                //Check if we have to move the fist object at the end of the last object
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            } else if (mainCamera.transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
            {
                //Check if we have to move the last object before the first object
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2 , firstChild.transform.position.y, firstChild.transform.position.z);
            }

        }
    }

    void LateUpdate()
    {
        foreach(GameObject obj in levels)
        {
            RepositionChildObjects(obj);
        }
    }
}
