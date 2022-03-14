using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Variables
    Vector2 endPoint = new Vector2();
    public static Vector2 enemyPosition;
    float enemySpeed = GameHandler.enemySpeed;
    float castleX = EnemySpawn.castleX;
    #endregion

    void Start()
    {
        endPoint = new Vector2(castleX,-350);
    }

    void Update()
    {
        MoveEnemy();
        if(enemyPosition == endPoint)
        {
            Destroy(gameObject);
        }
    }

    void MoveEnemy()
    {
        enemyPosition = this.transform.position;
        Vector2 move = Vector2.MoveTowards(enemyPosition, endPoint, enemySpeed * Time.deltaTime);
        transform.position = move;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Destroy(gameObject);
            GameHandler.score += 25;
        }
    }
}
