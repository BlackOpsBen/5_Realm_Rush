﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWayPoint, endWayPoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    Queue<Waypoint> queue = new Queue<Waypoint>();

    bool isRunning = true;

    Waypoint searchCenter;

    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };


    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        ColorStartAndEnd();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        path.Add(endWayPoint);
        Waypoint previous = endWayPoint.exploredFrom;
        while (previous != startWayPoint)
        {
            // Add intermediate waypoints
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        // Add start waypoint
        path.Add(startWayPoint);
        // Reverse the list
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWayPoint);

        while (queue.Count > 0)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            if (searchCenter == endWayPoint)
            {
                isRunning = false;
                break; // TODO remove?
            }
            ExploreNeighbors();
        }
        // TODO work out path
    }

    private void ExploreNeighbors()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighborCoords))
            {
                QueueNewNeighbors(neighborCoords);
            }
        }
    }

    private void QueueNewNeighbors(Vector2Int neighborCoords)
    {
        Waypoint neighbor = grid[neighborCoords];
        if (neighbor.isExplored || queue.Contains(neighbor))
        {
            // do nothing
        }
        else
        {
            neighbor.exploredFrom = searchCenter;
            queue.Enqueue(neighbor);
        }
    }

    private void ColorStartAndEnd()
    {
        //TODO consider moving out 
        startWayPoint.SetTopColor(Color.yellow);
        endWayPoint.SetTopColor(Color.white);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if (isOverlapping)
            {
                Debug.LogWarning("Overlapping block " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    }
}
