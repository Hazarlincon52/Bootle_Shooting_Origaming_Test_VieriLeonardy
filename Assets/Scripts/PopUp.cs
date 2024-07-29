using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    private float timeAppear = 1f;
    private float timer;

    private SpriteRenderer power;
    private float floatingSpeed = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        power = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 1f ,0) * floatingSpeed * Time.deltaTime;//Movement up a bit

        timer += Time.deltaTime;
        float fraction = timeAppear / 2f;
        if (timer > timeAppear) 
        {
            Destroy(gameObject); 
        }

        else if (timer > fraction)//fade out effect
        {       
            power.color = Color.Lerp(power.color,Color.clear,(timer - fraction)/(timeAppear - fraction));
        }

    }
}
