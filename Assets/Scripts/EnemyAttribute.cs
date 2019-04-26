using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttribute : CharacterAttribute
{
    [SerializeField]
    protected float minDistance;
    [SerializeField]
    protected float distance;

    protected GameObject player;
    protected float playerInX;
    protected float playerInY;
    protected float enemyInX;
    protected float enemyInY;
    protected float originX;
    protected float originY;
    protected int blockSize;
    protected bool isSeen;

    protected void Start()
    {
        minDistance = 2f;
        isSeen = false;
        player = GameObject.FindGameObjectWithTag("Player");
        //roomPoints = new RoomPoint[4];

    }


    protected void SetPlayer(GameObject player) => this.player = player;

}