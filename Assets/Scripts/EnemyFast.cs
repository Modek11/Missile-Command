using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFast : MonoBehaviour
{
    #region Variables
    Vector2 endPoint = new Vector2();
    public static Vector2 enemyPosition;
    float enemyFastSpeed = GameHandler.enemyFastSpeed;
    float castleX = EnemyFastSpawn.castleX;
    #endregion

    void Start()
    {
        endPoint = new Vector2(castleX, -350);
    }

    void Update()
    {
        MoveFastEnemy();
        if (enemyPosition == endPoint)
        {
            Destroy(gameObject);
        }
    }

    void MoveFastEnemy()
    {
        enemyPosition = this.transform.position;
        Vector2 move = Vector2.MoveTowards(enemyPosition, endPoint, enemyFastSpeed * Time.deltaTime);
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
