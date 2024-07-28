using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GunBullets : MonoBehaviour, IPointerClickHandler
{
    public static GunBullets Instance {get; private set;}
    public event EventHandler OnBullets;

    [SerializeField] private TextMeshProUGUI bulletsText;

    private enum State
    {
        PowerUpOn,
        PowerUpOff
    }

    private State powerUpState;

    private int bullets = 6;

    private bool outOfBullets = false;


    private void Awake() 
    {
        Instance = this;
    }

    void Start()
    {   
        powerUpState = State.PowerUpOff;
        bulletsText.text = bullets.ToString();
        bulletsText.GetComponent<TextMeshProUGUI>().color = Color.grey;
    }
  
    void Update()
    {
        switch(powerUpState)
        {
            case State.PowerUpOn:
                if(Input.GetMouseButtonDown(0) && !GameManager.Instance.GetGameOverActive())
                {
                    //infinite bullets
                    
                }
                break;
            case State.PowerUpOff:
                if(Input.GetMouseButtonDown(0) && !GameManager.Instance.GetGameOverActive())
                {
                    Shoot();
                }
                break;
        }
        
    }

    private void Shoot()
    {
        if(bullets <= 1)
        {
            //out of bullets
            outOfBullets = true;
            
            bulletsText.text = "Reload";
            bulletsText.GetComponent<TextMeshProUGUI>().color = Color.red;
            bullets --;
            OnBullets?.Invoke(this, EventArgs.Empty);
            if(bullets <= 0)
            {
                AudioManager.instance.OutOfBulletSound();
            }
        }
        else
        {
            bullets --;
            bulletsText.text = bullets.ToString();
            OnBullets?.Invoke(this, EventArgs.Empty);
            AudioManager.instance.ShootSound();
        }
    }

    public void Reload()
    {
        bullets = 6;
        bulletsText.text = bullets.ToString();
        outOfBullets = false;
        bulletsText.GetComponent<TextMeshProUGUI>().color = Color.grey;
        OnBullets?.Invoke(this, EventArgs.Empty);
        if(!GameManager.Instance.GetGameOverActive())
        {
            AudioManager.instance.ReloadSound();
        }

    }

    public void InfiniteBulletsPowerUp()
    {
        powerUpState = State.PowerUpOn;
        bulletsText.color = Color.blue;
        bulletsText.text = "Infinity";

        bullets = 6;
        OnBullets?.Invoke(this, EventArgs.Empty);
        
    }

    public void StopInfiniteBulletsPowerUp()
    {
        powerUpState = State.PowerUpOff;
        bulletsText.color = Color.grey;
        bulletsText.text = bullets.ToString();;
    }

    public bool GetOutOfBullets()
    {
        return outOfBullets;
    }

    public int GetBullets()
    {
        return bullets;
    }

    public TextMeshProUGUI BulletText()
    {
        return bulletsText;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right && powerUpState == State.PowerUpOff)
        {
            Reload();
        }
    }


    
}