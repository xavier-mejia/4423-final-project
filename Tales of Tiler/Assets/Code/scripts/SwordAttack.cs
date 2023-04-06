using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private int _swordDamage = 10;
    private Collider2D _swordCollider;
    public AudioClip hitSuccess;
    public AudioClip hitFail;
    private AudioSource _audioSource;
    void Start()
    {
        _swordCollider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit!");
            _audioSource.PlayOneShot(hitSuccess);
            col.GetComponent<Enemy>().TakeDamage(_swordDamage);
        }
        else
        {
            _audioSource.Play();
        }
    }
}
