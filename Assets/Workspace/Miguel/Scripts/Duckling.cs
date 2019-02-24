using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duckling : MonoBehaviour
{
    public bool isMomLeft;
    public bool isAmmo;
    public bool motherless;
    public bool hasHat;
    public Animator an;
    public GameObject hat;
    [SerializeField] private Transform mother;
    [SerializeField] private float offset;
    [SerializeField] private float speed;
    [SerializeField] private float topSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private ParticleSystem ps;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(WanderAimlessly());
    }
    protected virtual void FixedUpdate()
    {
        if (!isAmmo)
        {
            if (!motherless)
            {
                if (!WithinRange())
                {
                    an.SetBool("Walking", true);
                    ApproachMom();
                }
                else
                {
                    an.SetBool("Walking", false);
                }
            }
        }
    }
    private void ApproachMom()
    {
        if (topSpeed > rb.velocity.sqrMagnitude)
        {
            if (IsMomOnLeft())
            {
                isMomLeft = true;
                transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                rb.AddForce(Vector2.left * speed * 5f);
            }
            else
            {
                isMomLeft = false;
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                rb.AddForce(Vector2.right * speed * 5f);
            }
        }
    }
    private bool IsMomOnLeft()
    {
        return mother.position.x < transform.position.x;
    }
    private bool WithinRange()
    {
        Vector2 momPosition = mother.position;
        Vector2 ourPosition = transform.position;
        float distance = Mathf.Abs(Vector2.Distance(momPosition, ourPosition));
        return distance < offset;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            DespawnObject(collision.gameObject);
            EndGameResultsData.instance.enemiesKilled++;
        }
        else if(isAmmo)
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                isAmmo = false;
                an.SetBool("Thrown", false);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Found Mom!");
                motherless = false;
            }
        }
    }
    public void Jumping()
    {
        an.SetBool("IsParentJumping", true);
    }
    public void NotJumping()
    {
        an.SetBool("IsParentJumping", false);
    }
    private IEnumerator WanderAimlessly()
    {
        bool flip = true;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            if (motherless)
            {
                if (flip)
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    flip = false;
                } else
                {
                    flip = true;
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

                }
            }
        }
    }
    public void OnDeath()
    {
        AudioManager.instance.PlaySound("duckling");
        EndGameResultsData.instance.deadDucklings++;
        ps.Play();
        StopAllCoroutines();
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        GetComponent<BoxCollider2D>().isTrigger = true;
        rb.AddForce(Vector2.down * 2f, ForceMode2D.Impulse);
        StartCoroutine(DespawnObject(this.gameObject));
    }

    public void SubmitDuckling()
    {
        AudioManager.instance.PlaySound("duckling");
        EndGameResultsData.instance.savedDucklings++;
        sr.color = new Color(1, 1, 1, 0);
        ps.Play();
        StopAllCoroutines();
        StartCoroutine(DespawnObject(this.gameObject));
    }
    private IEnumerator DespawnObjectQuickly(GameObject go)
    {
        yield return new WaitForSeconds(.2f);
        go.SetActive(false);
    }
    private IEnumerator DespawnObject(GameObject go)
    {
        yield return new WaitForSeconds(2f);
        go.SetActive(false);
    }
}
