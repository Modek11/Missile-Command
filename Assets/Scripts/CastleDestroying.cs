using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDestroying : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            GameHandler.castleCounter--;
            GameHandler.score -= 125;
            GameObject.Find("Castles(Clone)").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
