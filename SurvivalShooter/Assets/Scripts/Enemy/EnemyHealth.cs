using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public HitStop stopHit;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    Rigidbody body;
    bool isDead;
    bool isSinking;
    


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        body = GetComponent<Rigidbody>();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            Vector3 knockbackHit = body.transform.position - hitParticles.transform.position;
            transform.Translate (knockbackHit * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();
        
        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        TurnMeshOff();
        Invoke("TurnMeshOn", 0.1f);
        Invoke("TurnMeshOff", 0.2f);
        Invoke("TurnMeshOn", 0.3f);

        if (currentHealth <= 0)
        {
            StartCoroutine(stopHit.HitFreeze(0.1f));
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }

    void TurnMeshOff()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
    }
    void TurnMeshOn()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    }
}
