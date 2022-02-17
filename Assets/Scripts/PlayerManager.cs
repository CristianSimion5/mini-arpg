using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("PlayerManager should be a singleton!");
            return;
        }

        instance = this;
    }

    #endregion

    public GameObject player;
    public PlayerStats playerStats;

    public Quest quest;
    public Camera cam;

    public List<Item> startingItems;

    private void Start()
    {
        foreach (var item in startingItems)
        {
            Inventory.instance.Add(item);
        }
    }

    public void DeathHandler()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
