﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false;

    public bool isPlaceable = true;

    public bool isOccupied = false;

    public Waypoint exploredFrom;

    const int gridSize = 10;

    GameObject tower;

    private void Start()
    {
        tower = Resources.Load<GameObject>("Tower");
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    //public void SetTopColor(Color color)
    //{
    //    MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
    //    topMeshRenderer.material.color = color;
    //}

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Place"))
        {
            if (isPlaceable && !isOccupied)
            {
                GameObject placedTower = Instantiate(tower, transform.position, Quaternion.identity);
                placedTower.transform.parent = gameObject.transform;
                isOccupied = true;
            }
            else
            {
                print("Cannot place here");
            }
        }
    }
}
