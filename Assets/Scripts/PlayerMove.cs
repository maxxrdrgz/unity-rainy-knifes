using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Text timer_Text;

    private Animator anim;
    private SpriteRenderer sr;
    private float speed = 3f;
    private float min_X = -2.7f;
    private float max_X = 2.7f;
    private int timer;

    /**
        Initializes components from the gameObject that will be manipulated

        @returns {void}
     */
    void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    /**
        starts the timers that is displayed on the main UI
        
        @returns {void}
     */
    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(CountTime());
        timer = 0;
    }

    /**
        checks the player bounds to ensure he stays within the screen view and 
        updates player movement.
        
        @returns {void}
     */
    void Update()
    {
        Move();
        PlayerBounds();
    }

    /**
        Gets the gameObjects transform position and checks if its between min 
        and max X bounds

        @returns {void}
     */
    void PlayerBounds()
    {
        Vector3 temp = transform.position;

        if(temp.x > max_X)
        {
            temp.x = max_X;
        }else if(temp.x < min_X)
        {
            temp.x = min_X;
        }
        transform.position = temp;
    }

    /**
        Gets horizontal input from user and changes the X axis of the 
        gameObjects transform.position

        @returns {void}
     */
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 temp = transform.position;
        if (h > 0)
        {
            temp.x += speed * Time.deltaTime;
            sr.flipX = false;
            anim.SetBool("Walk", true);
        }
        else if(h < 0)
        {
            temp.x -= speed * Time.deltaTime;
            sr.flipX = true;
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        transform.position = temp;
    }

    /**
        Coroutine for restarting the game. When called, this function will wait 
        for a specific time frame and then reaload the current scene.

        @returns {IEnumarator} returns a time delay before continuing execution
     */
    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    /**
        Starts a count and acts as the timer by calling itself recursively

        @returns {IEnumerator} returns a time delay before continuing execution
     */
    IEnumerator CountTime()
    {
        yield return new WaitForSeconds(1f);
        timer++;
        timer_Text.text = "Timer: " + timer;
        StartCoroutine(CountTime());
    }

    /**
        Detects collision with gameObjects with the tag of Knife and restarts the
        game. Game over.

        @returns {void}
     */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Knife")
        {
            Time.timeScale = 0f;
            StartCoroutine(RestartGame());
        }    
    }
} // class
