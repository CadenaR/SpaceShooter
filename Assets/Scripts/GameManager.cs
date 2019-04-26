using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject[] meteorHolders = new GameObject[3];
    public GameObject[] enemyHolders = new GameObject[3];
    public GameObject meteor;
    public GameObject enemy;


    private void Start()
    {
        InvokeRepeating("MeteorSpan",0,3);
        InvokeRepeating("EnemySpan", 0, 5);
    }
    
        //MeteorSpan();
        //EnemySpan();
       

    public void MeteorSpan()
    {
        Debug.Log("entroMeteoro");
        //yield return new WaitForSeconds(3.0f);
        Instantiate(meteor, meteorHolders[UnityEngine.Random.Range(0, 3)].GetComponent<Transform>().position, Quaternion.identity);
        
    }

    public void EnemySpan()
    {
        Debug.Log("entroEnemigo");

        //yield return new WaitForSeconds(5.0f);
        Instantiate(enemy, enemyHolders[UnityEngine.Random.Range(0, 3)].GetComponent<Transform>().position, Quaternion.identity);
    }



    public void GameOver()
    {
        gameOver.SetActive(true);
    }
}