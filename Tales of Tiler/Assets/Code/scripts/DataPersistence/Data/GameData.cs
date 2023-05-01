using System.Collections.Generic;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

[System.Serializable]
public class GameData
{
    public int maxHealth = 5;
    public int maxMana = 5;
    public int currentHealth;
    public int currentMana;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> StrawberriesCollected;

    // Note: health/mana information needs to be the same as maxHealth & maxMana in PlayerUIController
    public GameData()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        playerPosition = Vector3.zero;
        StrawberriesCollected = new SerializableDictionary<string, bool>();
    }
}
