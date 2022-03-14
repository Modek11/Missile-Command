using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilesSpawn : MonoBehaviour
{
    #region Variables
    public GameObject Missile;
    public static Vector3 playerPosition;
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameHandler.missilesLeft > 0)
        {
            SpawnMissile();
        }
    }

    void SpawnMissile()
    {
        playerPosition = GameObject.Find("Player").transform.position;
        var newMissile = GameObject.Instantiate(Missile);
        newMissile.transform.parent = gameObject.transform;
        newMissile.transform.up = playerPosition - newMissile.transform.position;
        GameHandler.missilesLeft--;
    }
}
