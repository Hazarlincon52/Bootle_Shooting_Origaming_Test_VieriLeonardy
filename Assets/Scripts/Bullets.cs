using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
  
    [SerializeField] private Transform containerBullets;
    [SerializeField] private Transform bullets;
    
    private void Start()
    {
        GunBullets.Instance.OnBullets += GunBullets_OnBullets;
        BulletsVisual();
    }
    
    private void BulletsVisual()// destroy child then add how many bullets 
    {
        foreach(Transform child in containerBullets)
        {
            if(child == bullets) continue;
            Destroy(child.gameObject);
        }

        ReloadBulletsVisual();
        
    }

    private void ReloadBulletsVisual()//add bullets
    {
        for(int i = 1; i<=GunBullets.Instance.GetBullets(); i++)
        {
            Transform bulletsVisual = Instantiate(bullets, containerBullets);
            bulletsVisual.gameObject.SetActive(true);
        }
    }

    private void GunBullets_OnBullets(object sender, EventArgs e)
    {
        BulletsVisual();
    }

    private void OnDisable() 
    {
        GunBullets.Instance.OnBullets -= GunBullets_OnBullets;
    }

}
