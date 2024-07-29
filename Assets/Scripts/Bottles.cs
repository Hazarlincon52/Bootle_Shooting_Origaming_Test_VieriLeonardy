using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottles : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bottle;
    [SerializeField] private SpriteRenderer bottleBreak;

    private Rigidbody2D rb;

    [SerializeField] private int point;
    private int rotatingDir;

    [SerializeField] private float minRotation;
    [SerializeField] private float maxRotation;
    private float rotationSpeed;
    [SerializeField]private float timeDes = 3f;//time wait for animation then destroy
    [SerializeField]private float maxSpeedPowerUp = 4.5f;
    
    private bool rotateActive = true;
    private bool hitGround = false;
    private bool slowPowerUp = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotationSpeed = Random.Range(minRotation, maxRotation);
        rotatingDir = Random.Range(1, 10);
        slowPowerUp = PowerUps.Instance.GetSlowBottle();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!rotateActive)
        {
            //stop rotating and destroy in 3 second;
            
            timeDes -= Time.deltaTime;
            if(timeDes <= 0)
            {
                //Debug.Log("destroy");
                Destroy(gameObject);
            }
        }
        else
        {
            if(rotatingDir >= 5)//if rotatingDir 5 or more rotate right
            {
                transform.Rotate(0,0,rotationSpeed);
            }
            else//else rotate left
            {
                transform.Rotate(0,0,-rotationSpeed);
            }
            
        }

        if(rb.velocity.magnitude > maxSpeedPowerUp && slowPowerUp)// slow velocity if powerups active
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedPowerUp);
        }
        
    }

    public void Hit() 
    {
        if(!hitGround && !GunBullets.Instance.GetOutOfBullets())// every time bottle get hit
        {
            GameManager.Instance.CalculateScore(point);
            ShowBreak();
            rotateActive = false;
            RandomPowerUps();
            StartCoroutine(FadeOut());
        }
        
    }

    private void ShowBreak()
    {
        bottle.enabled = false;
        bottleBreak.enabled = true;
        rb.simulated = false;
        AudioManager.instance.GlassShootSound();
    }

    private void RandomPowerUps()//activate random Powerups with 10% changes
    {
        int randomChange = Random.Range(1,100);
        
        if(randomChange <= 10)
        {
            PowerUps.Instance.RandomPowerUpsCalculation();

            if(PowerUps.Instance.GetRandomPowerUps() == 0)
            {
                PowerUps.Instance.InfiniteBullletStart();
            }
            else if(PowerUps.Instance.GetRandomPowerUps() == 1)
            {
                PowerUps.Instance.DoubleScoreStart();
            }
            else if(PowerUps.Instance.GetRandomPowerUps() == 2)
            {
                PowerUps.Instance.SlowBottleStart();
                rb.gravityScale = 0.25f;
            }
            
            Instantiate(PowerUps.Instance.GetPrefabPowerUps(), transform.position, Quaternion.identity);

            AudioManager.instance.PowerUpsSound();//SFX PowerUps
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)// if hit ground destroy
    {
        if(other.gameObject.tag == "Ground")
        {
            ShowBreak();
            rotateActive = false;
            StartCoroutine(FadeOut());
            AudioManager.instance.GlassDropSound();
        }    

    }

    IEnumerator FadeOut()//if bottle about to be destroy add fadeout effect
    {
        yield return new WaitForSeconds(0.8f);

        Color c;
        for (float f = 1f; f >= -0.5f; f -= 0.1f)
        {
            c = bottleBreak.color;
            c.a = f;
            bottleBreak.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
