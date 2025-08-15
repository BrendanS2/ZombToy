using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using UnityEngine.Rendering;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    bool alreadyAttacked;
    ParticleSystem p_Sys;
    Animator shootingAnim;
    public GameObject projectile;
    [SerializeField] bool ZombBear = false;
    float distance;
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (ZombBear)
        {
            shootingAnim = GetComponent<Animator>();

        }
        if (p_Sys == null && ZombBear)
        {
            p_Sys = GameObject.FindGameObjectWithTag("Blast").GetComponent<ParticleSystem>();
        }
        
        
    }


    public void Update()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (!ZombBear)
            {
                nav.SetDestination(player.position);
            }
            else if (ZombBear)
            {
                if (distance < 8)
                {
                    EnemyShoot();
                }
                else
                {
                    
                    shootingAnim.SetTrigger("Back");
                    nav.SetDestination(player.position);
                }
            }

        }
        else
        {
            nav.enabled = false;
        }
    }

    private void EnemyShoot()
    {
        nav.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            if (p_Sys != null)
            {
                p_Sys.Play();
            }
                

            if (ZombBear)
            {
                shootingAnim.SetTrigger("Shooting");
            }
            
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 7f, ForceMode.Impulse);
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), 1f);

        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    
}
