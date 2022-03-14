using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    #region Variables
    public GameObject Enemy;
    public static Vector3 spawnPoint;
    int whichCastleToAttack;
    public static float castleX;
    public float spawnRatio;
    float timeToSpawnEnemy;
    #endregion

    void Start()
    {
        timeToSpawnEnemy = spawnRatio;
    }
    void Update()
    {
        if (timeToSpawnEnemy<=0 && GameHandler.howMuchEnemies>0)
        {
            SpawnEnemy();
        }
        else
        {
            timeToSpawnEnemy -= Time.deltaTime;
        }
        
    }
    void SpawnEnemy()
    {
        WhichCastleToAttack();
        var newEnemy = GameObject.Instantiate(Enemy, spawnPoint, Quaternion.identity);
        newEnemy.transform.parent = gameObject.transform;
        newEnemy.transform.up = -(new Vector3(castleX, -350) - newEnemy.transform.position);
        timeToSpawnEnemy = spawnRatio;
        GameHandler.howMuchEnemies--;
    }

    void WhichCastleToAttack()
    {
        CastleDontExists:
        whichCastleToAttack = Mathf.RoundToInt(Random.Range(0, 6));
        var chosenCastle = GameObject.Find($"Castle{whichCastleToAttack}");

        if (chosenCastle != null)
        {
            castleX = chosenCastle.transform.position.x;
        }
        else if (GameHandler.castleCounter != 0)
        {
            goto CastleDontExists;
        }

        spawnPoint = new Vector3(Random.Range(castleX - 400, castleX + 400), 400);

        //Enemies spawns only in gamemap
        if (spawnPoint.x < -900)
        {
            spawnPoint.x = -900;
        }
        else if (spawnPoint.x > 900)
        {
            spawnPoint.x = 900;
        }
    }
}
