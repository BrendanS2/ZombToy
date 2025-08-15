using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    public Slider enemySlider;
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    private Transform cam;
    private Image sliderFillImage;
    void Awake()
    {

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        if (enemySlider == null)
        {
            enemySlider = GetComponentInChildren<Slider>();
        }
        
        currentHealth = startingHealth;
        if (enemySlider != null)
        {

                sliderFillImage = enemySlider.fillRect.GetComponent<Image>();

            
        }
        
        enemySlider.maxValue = startingHealth;
    }


    void Update ()
    {
        if (enemySlider != null && enemySlider.gameObject != null)
    {
        enemySlider.value = currentHealth;
        enemySlider.transform.LookAt(cam);
    }

       
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

            StartCoroutine("Flash");
        
        
        
        enemyAudio.Play ();
        
        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }

    IEnumerator Flash()
    {
        if (sliderFillImage != null && sliderFillImage.gameObject != null)
        {
            Color ogColor = sliderFillImage.color;
            sliderFillImage.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            if (sliderFillImage != null && sliderFillImage.gameObject != null)
        {
            sliderFillImage.color = ogColor;
        }
        }
        
            

        
        
    }


    void Death ()
    {
        if (enemySlider != null)
        {
            Destroy(enemySlider.gameObject);
            enemySlider = null;
            sliderFillImage = null;
            
        }
        
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
