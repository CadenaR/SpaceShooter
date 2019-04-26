using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : CharacterAttribute
{
    public Text healtText;

    protected override void Die()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (horizontalMove != 0 || verticalMove != 0)
        {
            GetComponent<Rigidbody2D>().velocity = NormalizeSpeed(horizontalMove, verticalMove);
            
        }
        
        if (Time.time > shootSpeed) // Avoids running the animation when the shot is not valid
        {
            if (Input.GetKey(KeyCode.Space))
            {
                 Shoot(Vector2.right);
            }
            
        }



    }

    

   
   }

    