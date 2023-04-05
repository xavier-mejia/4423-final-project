using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    private int _currentHealth;
    public UIBar healthBar;

    [SerializeField] private int maxMana = 5;
    private int _currentMana;
    public UIBar manaBar;

    // Animations should be moved to a different player script. Works for now.
    private Animator _animator;
    private PlayerController _player;
    private void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetValue(maxHealth);

        _currentMana = maxMana;
        manaBar.SetValue(maxMana);

        _animator = GetComponent<Animator>();
        _player = GetComponent<PlayerController>();
    }
    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        healthBar.SetValue(_currentHealth);

        if (_currentHealth <= 0) Die();
    }
    
    private void Die()
    {
        Debug.Log("Player is dead!");
        _animator.SetTrigger("die");
    }
    
    private void SetHealth(int health)
    {
        _currentHealth = health;
        healthBar.SetValue(health);
    }

    private void SetMana(int mana)
    {
        _currentMana = mana;
        manaBar.SetValue(mana);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
