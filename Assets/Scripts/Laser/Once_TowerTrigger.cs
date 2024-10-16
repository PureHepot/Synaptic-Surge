using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Once_TowerTrigger : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<LaserTower>().enabled = true;
        this.enabled = false;
    }
}
