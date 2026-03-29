using System.Collections;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;
public class playerBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float height = 400f; //the height of jump
    [SerializeField] private float powerheight = 200; //the powerup height to be added to height
    [SerializeField] private GameObject[] bulletPrefabs; //prefaab of the bullet that will be instantiated //working
    [SerializeField] private float firerate = 0.5f;
    [SerializeField] private int lives = 3; //this is for lives of player
    [SerializeField] private Animator animator; //this is for the animation  
    [SerializeField] private float shootdelay = 0.3f;
    [SerializeField] private bool shieldactive = false;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource shieldRecharge;
    [SerializeField] private AudioSource ShootingSound;
    [SerializeField] private AudioSource shieldSound;
    private int jumpcount = 0;
    private float horiInput = 0f;
    private int maxjump = 2;
    private bool alive = true;
    private bool invincible;
    UIManager ui;

    void Start()
    {
       animator = GetComponent<Animator>();
       ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
       jumpSound = GetComponent<AudioSource>(); //this will get the audio source
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
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
        Shield();
        WinCheck();
    }

    public void Movement() //walking and jumpimg. plus animations
    {
        // horiInput = Input.GetAxis("Horizontal"); //key maps for fonrizontal inputs    //removing for working with phone

        Vector3 direction = new Vector3(horiInput,0,0);
        if(alive) //moving right and left                                                     
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if(transform.position.x <= -3.5f) //to bound the character to a boundry
        {
            transform.position = new Vector3(-3.5f, transform.position.y, transform.position.z);
        }

        if(transform.position.x >= 353)
        {
            transform.position = new Vector3(353, transform.position.y, transform.position.z); // to wrap the player at end
        }

        if(Input.GetKeyDown(KeyCode.Space) && jumpcount < maxjump && alive)  //player jump logic
        {
            body.linearVelocity = new Vector3(body.linearVelocityX,0,0);
            jumpSound.Play();
            body.AddForce(Vector3.up * height, ForceMode2D.Impulse);
            jumpcount++;
        }

        if(horiInput > 0.1f && alive) //this is the animation for movement   
        {
            animator.ResetTrigger("reset");
            animator.ResetTrigger("left");
            animator.SetTrigger("right");
        }
        else if(horiInput < -0.1f && alive)
        {
            animator.ResetTrigger("reset");
            animator.ResetTrigger("right");
            animator.SetTrigger("left");
        }
        else
        {
            animator.ResetTrigger("right");
            animator.ResetTrigger("left");
            animator.SetTrigger("reset");
        }
    }

    public void jump()  //jump function for moboile controls
    {
        if(jumpcount < maxjump && alive)
        {
            body.linearVelocity = new Vector3(body.linearVelocityX,0,0);
            jumpSound.Play();
            body.AddForce(Vector3.up * height, ForceMode2D.Impulse);
            jumpcount++;
        }
    }

    //mobile settings
    public void SetHorizontal(float value)
    {
        horiInput = value;
    }

    private void OnCollisionEnter2D(Collision2D collision) //double jump check 
    {
        if(collision.gameObject.tag == "Ground")
        {
            jumpcount=0;
        }
    }

    public void Shoot()  //shooting logic
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

    public void ShootMobile()  //shooting logic for mobile controls
    {
        if(firerate <= 0 && alive)
        {
            animator.SetTrigger("throw");
            StartCoroutine(ShootingDelay(shootdelay));
            firerate = 0.5f;
        }
        else
        {
            firerate -= Time.deltaTime;
        }
    }

    public void Damage()  //damage system
    {
        if(lives>0 && !invincible &&alive) //bug should be fixed here
        {
            lives--;
            ui.UpdateLive(lives);
        }
        if(lives==0)
        {
            ui.DeadScreen();
            alive = false;
            // Destroy(gameObject);
        }
    }

    public void Shield()  //shield
    {
        if(Input.GetKeyDown(KeyCode.Q) && shieldactive && alive)
        {
            animator.SetTrigger("shield"); 
            invincible = true;
            shieldSound.Play();
            StartCoroutine(ShieldOverload());
            StartCoroutine(ShieldCooldown());
        }
    }

    public void WinCheck()  //checks when the player won the game
    {
       if(transform.position.x >= 350)
        {
            alive = false;
            ui.WinScreen();
        }
    }

    public void powerup() //this is the logic of the powerup
    {
        if(lives==3)
        {
            height+=powerheight;
            StartCoroutine(PowerupCooldown());
        }
        else
        {
            lives++;
            ui.UpdateLive(lives);
        }
    }

    public void stopPlayer()
    {
        alive=false;
    }

    public void startPlayer()
    {
        alive = true;
    }

    IEnumerator ShootingDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootingSound.Play();
        Instantiate(bulletPrefabs[UnityEngine.Random.Range(0,2)], transform.position + new Vector3(0.7f,0.5f,0) , quaternion.identity);  
    }

    IEnumerator ShieldOverload()  
    {
        yield return new WaitForSeconds(1.9f);
        shieldactive = false;
        invincible = false;
    }
    
    IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(5f);
        shieldactive = true;
        shieldRecharge.Play();
    }

    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(5f);
        height -= powerheight;
    }

}
