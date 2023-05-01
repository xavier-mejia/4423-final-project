using System.ComponentModel;
using UnityEngine;

public class Strawberry : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    
    [SerializeField] private int healAmount;
    private Collider2D _collider;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    public bool isCollected = false;
    
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
        healAmount = 1;

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isCollected = true;
            _audioSource.Play();
            col.gameObject.GetComponent<PlayerCombat>().Heal(healAmount);

            transform.position = new Vector3(100000, 0, 0);
            Destroy(gameObject, 5);
        }
    }

    public void LoadData(GameData data)
    {
        data.StrawberriesCollected.TryGetValue(id, out isCollected);
        if (isCollected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.StrawberriesCollected.ContainsKey(id))
        {
            data.StrawberriesCollected.Remove(id);
        }
        
        data.StrawberriesCollected.Add(id, isCollected);
    }
}
