using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    Queue<GameObject> TowerQueue = new Queue<GameObject>();

    [SerializeField] int towerLimit = 5;
    int towersOut = 0;

    GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        tower = Resources.Load<GameObject>("Tower");
    }

    public void AddTower(Waypoint baseWaypoint)
    {
        if (towersOut < towerLimit)
        {
            towersOut++;
            GameObject placedTower = Instantiate(tower, baseWaypoint.transform.position, Quaternion.identity);
            placedTower.transform.parent = GameObject.Find("Towers").transform;
            placedTower.GetComponent<Tower>().stationPoint = baseWaypoint;
            TowerQueue.Enqueue(placedTower);
            baseWaypoint.isOccupied = true;
        }
        else
        {
            MoveOldestTower(baseWaypoint);
        }
    }

    private void MoveOldestTower(Waypoint baseWaypoint)
    {
        GameObject movingTower = TowerQueue.Dequeue();
        movingTower.GetComponent<Tower>().stationPoint.isOccupied = false;
        Debug.LogWarning("Max towers placed!");
        movingTower.transform.position = baseWaypoint.transform.position;
        movingTower.GetComponent<Tower>().stationPoint = baseWaypoint;
        TowerQueue.Enqueue(movingTower);
    }
}
