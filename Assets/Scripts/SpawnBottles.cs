using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnBottles : MonoBehaviour
{
    public static SpawnBottles Instance {get; private set;}
    [SerializeField] private Transform[] bottlesPrefabs;
    [SerializeField] private Transform[] spawnPointsRight;
    [SerializeField] private Transform[] spawnPointsLeft;

    private float timerRight = 2.5f;
    private float timerLeft = 2.5f;

    private bool gameStart = false;
    //private bool bottleSlowPowerUp = false;

    private void Awake() 
    {
        Instance = this;
    }
    
    // Update is called once per frame
    private void Update()
    {
        if(gameStart)
        {
            
            timerRight -= Time.deltaTime;
            if(timerRight <= 0)
            {
                SpawnRight();
                TimeSetRight();
            }

            timerLeft -= Time.deltaTime;
            if(timerLeft <= 0)
            {
                SpawnLeft();
                TimeSetLeft();
            }
        }
        
    }

    private void SpawnRight()
    {
        int randBottle = Random.Range(0,bottlesPrefabs.Length);
        int randPoint = Random.Range(0,spawnPointsRight.Length);
        Transform bottle = Instantiate(bottlesPrefabs[randBottle], spawnPointsRight[randPoint].transform.position, spawnPointsRight[randPoint].transform.rotation);
        
        Vector3 randomizedRightAngle = transform.up + transform.right + new Vector3(Random.Range(0.2f, 0.5f), 0, 0).normalized;
            
        bottle.gameObject.GetComponent<Rigidbody2D>().AddForce(randomizedRightAngle * Random.Range(2, 4), ForceMode2D.Impulse);
    }

    private void SpawnLeft()
    {
        int randBottle = Random.Range(0,bottlesPrefabs.Length);
        int randPoint = Random.Range(0,spawnPointsLeft.Length);
        Transform bottle = Instantiate(bottlesPrefabs[randBottle], spawnPointsLeft[randPoint].transform.position, spawnPointsLeft[randPoint].transform.rotation);
        
        Vector3 randomizedLeftAngle = transform.up + -transform.right + new Vector3(Random.Range(-0.2f, -0.5f), 0, 0).normalized;
            
        bottle.gameObject.GetComponent<Rigidbody2D>().AddForce(randomizedLeftAngle * Random.Range(2, 4), ForceMode2D.Impulse);
        
    }

    private void TimeSetRight()
    {
        
        if(GameManager.Instance.GetTime() <= 15)// 15 seconds left speed up the bottle spawn
        {
            timerRight = Random.Range(0.8f, 1.5f);
        }
        else
        {
            timerRight = Random.Range(1f, 3f);
        }
        
    }

    private void TimeSetLeft()
    {
        if(GameManager.Instance.GetTime() <= 15)// 15 seconds left speed up the bottle spawn
        {
            timerLeft = Random.Range(0.8f, 1.5f);
        }
        else
        {
            timerLeft = Random.Range(1f, 3f);
        }
    }

    public void GetStartGame(bool gameStart)
    {
        this.gameStart = gameStart;
    }


}
