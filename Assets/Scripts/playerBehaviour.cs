using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float height = 400f;
    [SerializeField] private GameObject[] bulletPrefabs; //prefaab of the bullet that will be instantiated //working
    [SerializeField] private float firerate = 0.5f;
    [SerializeField] private int lives = 3; //this is for lives of player
    [SerializeField] private Animator animator; //this is for the animation  
    [SerializeField] private float shootdelay = 0.3f;
    [SerializeField] private bool shieldactive = false;
    [SerializeField] private GameObject shield;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource shieldRecharge;
    [SerializeField] private AudioSource ShootingSound;
    [SerializeField] private AudioSource shieldSound;
    private int jumpcount = 0;
    private int maxjump = 2;
    private bool alive = true;
    private bool invincible;
    UIManager ui;
    vehicleSpawning Vspawner;

    void Start()
    {
       animator = GetComponent<Animator>();
       ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
       shield.gameObject.SetActive(false);
       jumpSound = GetComponent<AudioSource>(); //this will get the audio source
       Vspawner = GameObject.FindGameObjectWithTag("V Spawner").GetComponent<vehicleSpawning>();
       shieldactive = true;
       alive = true;
       if(bulletPrefabs==null)
        {
            Debug.LogError("Bullet prefab is missing in playerBehaviour script");
            return;
        } 
        if(body == null)
        {
            Debug.LogError("Rigidbody2D is missing in playerBehvaiour scipt");
            return;
        }
        if(animator == null)  
        {
            Debug.LogError("Animator is missing in playerBehaviour script");
            return;
        }
        if(ui == null)
        {
            Debug.LogError("UIManager script is missing from playerBehaviour script");
            return;
        }
        if(shield == null)
        {
            Debug.LogError("Shield is missing in playerBehaviour script");
            return;
        }
        if(shieldRecharge == null)
        {
            Debug.LogError("Shield recharge audio source is missing in playerBehaviour script");
            return;
        }
        if(ShootingSound == null)
        {
            Debug.LogError("Shooting sould or audio source is missing in playerBehavior script");
            return;
        }
        if(shieldSound == null)
        {
            Debug.LogError("Shield sound is missing in playerNehaviour script");
            return;
        }
        if(Vspawner == null)
        {
            Debug.LogError("The vehhicle spawner is not connected buddy , do something");
        }
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
        Shield();
        WinCheck();
    }

    public void Movement()
    {
        float horiInput = Input.GetAxis("Horizontal"); //key maps for fonrizontal inputs

        Vector3 direction = new Vector3(horiInput,0,0);
        if(alive) //moving right and left                                                     //working
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if(transform.position.x <= -3.5f) //to bound the character to a boundry
        {
            transform.position = new Vector3(-3.5f, transform.position.y, transform.position.z);
        }

        if(Input.GetKeyDown(KeyCode.Space) && jumpcount<maxjump && alive)  //player jump logic
        {
            body.linearVelocity = new Vector3(body.linearVelocityX,0,0);
            jumpSound.Play();
            body.AddForce(Vector3.up * height, ForceMode2D.Impulse);
            jumpcount++;
        }

        if(horiInput > 0.1f) //this is the animation for movement   
        {
            animator.ResetTrigger("reset");
            animator.ResetTrigger("left");
            animator.SetTrigger("right");
            Vspawner.Spawn(1.2f);
        }
        else if(horiInput < -0.1f)
        {
            animator.ResetTrigger("reset");
            animator.ResetTrigger("right");
            animator.SetTrigger("left");
            Vspawner.Spawn(2);
        }
        else
        {
            animator.ResetTrigger("right");
            animator.ResetTrigger("left");
            animator.SetTrigger("reset");
            Vspawner.Spawn(1.2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            jumpcount=0;
        }
    }

    public void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.E) && firerate <= 0 && alive)
        {
            // animator.SetTrigger("shoot");  //triggering the shooting animation   
            animator.SetTrigger("throw");
            StartCoroutine(ShootingDelay(shootdelay));  
            firerate = 0.5f;
        }
        else
        {
            firerate -= Time.deltaTime;
        }
    }

    public void Damage()
    {
        if(lives>0 && !invincible) //bug should be fixed here
        {
            lives--;
            ui.UpdateLive(lives);
        }
        if(lives==0)
        {
            Destroy(gameObject);
            ui.DeadScreen();
            alive = false;
        }
    }

    public void Shield()  
    {
        if(Input.GetKeyDown(KeyCode.Q) && shieldactive)
        {
            // shield.gameObject.SetActive(true);
            // shieldSound.Play();
            animator.SetTrigger("shield");
            invincible = true;
            StartCoroutine(ShieldOverload());
            StartCoroutine(ShieldCooldown());
        }
    }

    public void WinCheck()
    {
       if(transform.position.x >= 258.3f)
        {
            ui.WinScreen();
        }
    }

    IEnumerator ShootingDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootingSound.Play();
        Instantiate(bulletPrefabs[UnityEngine.Random.Range(0,2)], transform.position + new Vector3(0.7f,0.5f,0) , quaternion.identity);  
    }

    IEnumerator ShieldOverload()  
    {
        yield return new WaitForSeconds(4f);
        shield.gameObject.SetActive(false);
        shieldactive = false;
        invincible = false;
    }
    
    IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(10f);
        shieldactive = true;
        shieldRecharge.Play();
    }

}
