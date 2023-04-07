using UnityEngine;

public class Strawberry : MonoBehaviour
{
    [SerializeField] private int healAmount;
    private Collider2D _collider;
    private AudioSource _audioSource;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
        healAmount = 1;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _audioSource.Play();
            col.gameObject.GetComponent<PlayerCombat>().Heal(healAmount);

            transform.position = new Vector3(100000, 0, 0);
            Destroy(gameObject, 5);
        }
    }
}
