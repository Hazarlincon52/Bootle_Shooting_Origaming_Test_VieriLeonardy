using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public static PowerUps Instance;

    [SerializeField] private Transform[] powerUps;
    [SerializeField] private GameObject[] powerUpsCoolDown;

    private int randomPowerUps;

    private float doubleScoreTimer = 5f;
    private float slowBottleTimer = 5f;
    private float infiniteBullletTimer = 5f;

    private bool slowBottleAcrive = false;

    private void Awake() 
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!powerUpsCoolDown[0].activeSelf)
        {
            infiniteBullletTimer -= Time.deltaTime;
            if(infiniteBullletTimer <= 0)
            {
                powerUpsCoolDown[0].SetActive(true);
                infiniteBullletTimer = 5f;
                InfiniteBullletStop();
            }
        }

        if(!powerUpsCoolDown[1].activeSelf)
        {
            doubleScoreTimer -= Time.deltaTime;
            if(doubleScoreTimer <= 0)
            {
                powerUpsCoolDown[1].SetActive(true);
                doubleScoreTimer = 5f;
                DoubleScoreStop();
            }
        }

        if(!powerUpsCoolDown[2].activeSelf)
        {
            slowBottleTimer -= Time.deltaTime;
            if(slowBottleTimer <= 0)
            {
                powerUpsCoolDown[2].SetActive(true);
                slowBottleTimer = 5f;
                slowBottleAcrive = false;
            }
        }
    }

    public void RandomPowerUpsCalculation()
    {
        randomPowerUps = Random.Range(0, powerUps.Length);

        if(!powerUpsCoolDown[randomPowerUps].activeSelf)
        {
            switch(randomPowerUps)
            {
                case 0:
                    infiniteBullletTimer = 5f;
                    break;
                case 1:
                    doubleScoreTimer = 5f;
                    break;
                case 2:
                    slowBottleTimer = 5f;
                    break;
            }

        }
        else
        {
            powerUpsCoolDown[randomPowerUps].SetActive(false);
        }
        

    }

    //PowerUps Effect InfiniteBullets
    public void InfiniteBullletStart()
    {
        GunBullets.Instance.InfiniteBulletsPowerUp();
    }

    public void InfiniteBullletStop()
    {
        GunBullets.Instance.StopInfiniteBulletsPowerUp();
    }

    //PowerUps Effect DoublePoint
    public void DoubleScoreStart()
    {
        GameManager.Instance.ScorePowerUp();
    }

    public void DoubleScoreStop()
    {
        GameManager.Instance.StopScorePowerUp();
    }

    //PowerUps Effect SlowBottle
    public void SlowBottleStart()
    {
        slowBottleAcrive = true;
    }

    public bool GetSlowBottle()
    {
        return slowBottleAcrive;// activation slowpowerup in every bootles about to spawn
    }


    public void StopAllPowerUps()
    {
        infiniteBullletTimer = doubleScoreTimer = slowBottleTimer = 0;
    }

    public void RestartAllPowerUps()
    {
        infiniteBullletTimer = doubleScoreTimer = slowBottleTimer = 5f;
    }

    public Transform GetPrefabPowerUps()
    {
        return powerUps[randomPowerUps];
    }

    public int GetRandomPowerUps()
    {
        return randomPowerUps;
    }
}
