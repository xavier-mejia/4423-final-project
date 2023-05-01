using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour, IDataPersistence
{
    private int _maxHealth;
    private int _currentHealth;
    public UIBar healthBar;

    private int _maxMana;
    private int _currentMana;
    public UIBar manaBar;

    [SerializeField] private PauseMenu pauseMenu;
    
    // Animations should be moved to a different player script. Works for now.
    private Animator _animator;
    private PlayerController _player;
    private void Start()
    {
        _currentHealth = _maxHealth;
        healthBar.SetValue(_maxHealth);

        _currentMana = _maxMana;
        manaBar.SetValue(_maxMana);

        _animator = GetComponent<Animator>();
        _player = GetComponent<PlayerController>();
    }
    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        healthBar.SetValue(_currentHealth);

        if (_currentHealth <= 0) Die();
    }

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        healthBar.SetValue(_currentHealth);
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

    public void PauseGame()
    {
        pauseMenu.Pause();
    }

    public void ResumeGame()
    {
        pauseMenu.Resume();
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu");    
    }
    
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadData(GameData data)
    {
        _maxHealth = data.maxHealth;
        _maxMana = data.maxMana;
        _currentHealth = data.currentHealth;
        _currentMana = data.currentMana;
        
        healthBar.SetValue(_currentHealth);
        manaBar.SetValue(_currentMana);
    }

    public void SaveData(ref GameData data)
    {
        data.maxHealth = _maxHealth;
        data.maxMana = _maxMana;
        data.currentHealth = _currentHealth;
        data.currentMana = _currentMana;
    }
}
