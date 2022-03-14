using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    #region Variables
    bool highEnough = false;
    Vector3 explosionScale = GameHandler.explosionScale;
    float explosionTime = GameHandler.explosionTime;
    float currentExplosionScaleX;
    #endregion

    void FixedUpdate()
    {
        ScaleExplosion();
        Destroy(gameObject,explosionTime);
    }

    void ScaleExplosion()
    {
        currentExplosionScaleX = gameObject.transform.localScale.x;
        
        if (currentExplosionScaleX < 20 && highEnough == false)
        {
            gameObject.transform.localScale += explosionScale;
        }
        else if (currentExplosionScaleX > 10)
        {
            gameObject.transform.localScale -= explosionScale;
            highEnough = true;
        }
    }
}
