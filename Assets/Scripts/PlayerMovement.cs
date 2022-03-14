using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public float playerMovementSpeed;
    #endregion

    void Update()
    {
        playerMovementSpeed = GameHandler.playerMovementSpeed;
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");
        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalAxis, verticalAxis) * playerMovementSpeed;
    }
}
