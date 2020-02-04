using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
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
        }
        else
        {
            MoveOldestTower();
        }
    }

    private static void MoveOldestTower()
    {
        Debug.LogWarning("Max towers placed!");
    }
}
