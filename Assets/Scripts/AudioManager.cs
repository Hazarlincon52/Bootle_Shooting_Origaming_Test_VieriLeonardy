using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("SFX")]
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip powerUpsSound;
    [SerializeField] private AudioClip outOfBulletSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip[] glassShootSound;
    [SerializeField] private AudioClip[] glassDropSound;
    [SerializeField] private AudioClip[] shootpSound;
    
    
   
    private void Awake() 
    {
        instance = this;
    }   

    public void ButtonSound()
    {
        SFXSource.PlayOneShot(buttonSound);
    }

    public void PowerUpsSound()
    {
        SFXSource.PlayOneShot(powerUpsSound);
    }

    public void OutOfBulletSound()
    {
        SFXSource.PlayOneShot(outOfBulletSound);
    }

    public void ReloadSound()
    {
        SFXSource.PlayOneShot(reloadSound);
    }

    public void GlassShootSound()
    {
        SFXSource.PlayOneShot(glassShootSound[Random.Range(0, glassShootSound.Length)]);
    }

    public void GlassDropSound()
    {
        SFXSource.PlayOneShot(glassDropSound[Random.Range(0, glassDropSound.Length)]);
    }

    public void ShootSound()
    {
        SFXSource.PlayOneShot(shootpSound[Random.Range(0, shootpSound.Length)]);
    }

    

    


}
