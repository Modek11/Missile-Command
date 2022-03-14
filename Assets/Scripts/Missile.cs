using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    #region Variables
    public float missileSpeed;
    Vector3 playerPosition = MissilesSpawn.playerPosition;
    Vector3 missilePossition;
    public GameObject Explosion;
    #endregion

    void Update()
    {
        MoveMissile();

        if(missilePossition == playerPosition)
        {
            Destroy(gameObject);
            SpawnExplosion();
        }
        
    }

    void MoveMissile()
    {
        missilePossition = this.transform.position;
        Vector3 move = Vector3.MoveTowards(missilePossition, playerPosition, missileSpeed * Time.deltaTime);
        transform.position = move;
    }

    void SpawnExplosion()
    {
        var newExplosion = GameObject.Instantiate(Explosion, playerPosition, Quaternion.identity);
        newExplosion.transform.parent = GameObject.Find("Missiles").transform;
    }
}
