using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public delegate void OnPlayerActionEventHandler(object sender, OnPlayerActionEventArgs e);

public class OnPlayerActionEventArgs : EventArgs {

}

public class PlayerController : MonoBehaviour
{
    #region DATA
    public List<Duckling> ducklings;
    public Transform wingPoint;
    public SpriteRenderer wing;
        [SerializeField] private float speed;
    [SerializeField] private float topSpeed;
    [SerializeField] private float jump;
    [SerializeField] private float dash;
    [SerializeField] private float dashTimer;
    [SerializeField] private float drag;
    [SerializeField] private float throwPower;
    [SerializeField] private bool throwing;
    [SerializeField] private bool facingLeft = false;
    [SerializeField] private bool grounded = false;
    [SerializeField] private bool boostReady = true;
    [SerializeField] private bool checkGroundRequest = true;
    [SerializeField] private bool dead = false;
    [SerializeField] private int ammo = 0;
    [SerializeField] private int maxAmmo;
    private bool dashing = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator an;
    #endregion DATA

    #region MONOBEHAVIOUR
    protected virtual void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
        ducklings = new List<Duckling>();
    }
    protected virtual void FixedUpdate()
    {
        if (!dead)
        {
            if (grounded && !dashing)
            {
                PlayerJump();
                PlayerHorizontalMovement();
                //PlayerBoost();
            }
            GroundCheck();
            ThrowDuckling();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AudioManager.instance.PlaySound("duck");

            }
        }
    }
    #endregion MONOBEHAVIOUR

    #region BLACKBOX
    private void PlayerBoost()
    {
        if (boostReady)
        {
            if (Input.GetKey(KeyCode.S))
            {
                an.SetBool("Dashing", true);
                dashing = true;
                boostReady = false;
                Vector2 direction = facingLeft ? Vector2.left : Vector2.right;
                rb.velocity = direction * dash;
                StartCoroutine(ApplyDragDelayed());
                StartCoroutine(RefreshBoost());
            }
        }
    }
    private IEnumerator ApplyDragDelayed()
    {
        yield return new WaitForSeconds(0.33f);
        dashing = false;
        an.SetBool("Dashing", false);
    }
    private IEnumerator RefreshBoost()
    {
        yield return new WaitForSeconds(dashTimer);
        boostReady = true;
    }
    private void PlayerHorizontalMovement()
    {
        if(topSpeed > rb.velocity.sqrMagnitude)
        {
           
            if (Input.GetKey(KeyCode.A))
            {
                //user pressed left.
                an.SetBool("Walking", true);
                facingLeft = true;
                transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                rb.AddForce(Vector2.left * speed * 5f);

            }
            else if (Input.GetKey(KeyCode.D))
            {
                //user pressed right.
                an.SetBool("Walking", true);
                facingLeft = false;
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                rb.AddForce(Vector2.right * speed * 5f);

            }
            else if (!dashing)
            {
                //apply drag.
                an.SetBool("Walking", false);
                Vector2 velocity = rb.velocity;
                velocity.x *= 1.0f - drag;
                rb.velocity = velocity;
            }
        }
    }
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            int weight = (ducklings.Count > 0) ? ducklings.Count : 1;
            rb.AddForce(Vector2.up * jump/weight, ForceMode2D.Impulse);
            AudioManager.instance.PlaySound("duckling");
            grounded = false;
            wing.color = new Color(1, 1, 1, 0); 
            an.SetBool("Jumping", true);
            if (ducklings.Count > 0)
            {
                foreach (Duckling duckling in ducklings)
                {
                    duckling.Jumping();
                }
            }
            StartCoroutine(CheckGroundAfterDelay());
        }
    }
    private IEnumerator CheckGroundAfterDelay()
    {
        yield return new WaitForSeconds(.2f);
        checkGroundRequest = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Duckling"))
        {
            checkGroundRequest = false;
            grounded = true;
            an.SetBool("Jumping", false);
            if (ducklings.Count > 0)
            {
                foreach (Duckling d in ducklings)
                {
                    if (d != null)
                    {
                        d.NotJumping();
                    }
                }
            }
            Duckling duckling = collision.gameObject.GetComponent<Duckling>();
            PlaceDuckling(duckling);
        }
        else if (collision.gameObject.CompareTag("Platform"))
        {
            checkGroundRequest = false;
            grounded = true;
            an.SetBool("Jumping", false);
            if (ducklings.Count > 0)
            {
                foreach (Duckling duckling in ducklings)
                {
                    if (ducklings != null)
                    {
                        duckling.NotJumping();
                    }
                }
            }
        }
    }
    private void PlaceDuckling(Duckling duckling)
    {
        if (!duckling.isAmmo)
        {
            if (ammo < maxAmmo)
            {
                Debug.Log($"Gained Ammo {duckling.gameObject.name}");
                duckling.isAmmo = true;
                //ducklings[ammo] = duckling;
                ducklings.Add(duckling);
                ParentConstraint dpc = duckling.gameObject.GetComponent<ParentConstraint>();
                Rigidbody2D drc = duckling.gameObject.GetComponent<Rigidbody2D>();
                duckling.GetComponent<SpriteRenderer>().sortingOrder = 3 - ammo;
                dpc.constraintActive = true;
                drc.simulated = false;
                dpc.SetTranslationOffset(0, new Vector2(-0.75f, 0.50f + ammo));
                ammo++;
            }
        }
        else
        {
            return;
        }
    }
    private void ThrowDuckling()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 worldMousePosition = 
                Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 relativePosition = worldMousePosition - transform.position;

            if (ammo > 0)
            {
                --ammo;
                throwing = true;
                AudioManager.instance.PlaySound("duckling");
                Duckling duckling = ducklings[ammo];
                StartCoroutine(WingSpin());
                ducklings.Remove(duckling);
                duckling.motherless = true;
                duckling.an.SetBool("Thrown", true);
                duckling.NotJumping();
                ParentConstraint dpc = duckling.gameObject.GetComponent<ParentConstraint>();
                Rigidbody2D drc = duckling.gameObject.GetComponent<Rigidbody2D>();
                duckling.gameObject.transform.position = new Vector2(transform.position.x,transform.position.y + 2f);
                dpc.constraintActive = false;
                drc.AddForce(relativePosition.normalized * throwPower, ForceMode2D.Impulse);
                drc.simulated = true;
            }
        }
    }
    public IEnumerator WingSpin()
    {
        for(int i = 0; i < 361; i+=12)
        {
            yield return new WaitForEndOfFrame();
            wingPoint.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0-i));
        }
        throwing = false;
    }
    private void GroundCheck()
    {
        if (checkGroundRequest)
        {
            grounded = false;
            RaycastHit2D[] hits;
            
            hits = Physics2D.RaycastAll(this.transform.position, Vector2.down, 2.5f);
            foreach (var hit in hits)
            {
                GameObject goHit = hit.collider.gameObject;
                if (hit.collider.gameObject == gameObject)
                {
                    continue;
                }
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    grounded = true;
                    wing.color = new Color(1, 1, 1, 1);
                    an.SetBool("Jumping", false);
                    if (ducklings.Count > 0)
                    {
                        foreach (Duckling duckling in ducklings)
                        {
                            if(ducklings != null)
                            {
                                duckling.NotJumping();
                            }
                        }
                    }
                    checkGroundRequest = false;
                }
            }
        }
    }
    public void OnDeath()
    {
        dead = true;
        an.SetBool("Jumping", false);
        an.SetBool("Walking", false);
        an.SetBool("Dashing", false);
        AudioManager.instance.PlaySound("duckling");
        StopAllCoroutines();
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        GetComponent<BoxCollider2D>().isTrigger = true;
        rb.AddForce(Vector2.down * 2f, ForceMode2D.Impulse);
        PausedMenu.instance.GameOver();
    }
    #endregion BLACKBOX




}
