using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectableManager : MonoBehaviour
{
    #region Singleton
    
    private static CollectableManager _instance;
    public static CollectableManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    public List<Collectable> buff;
    public List<Collectable> debuff;

    [Range(0,100)]
    public float buffChance;
    [Range(0,100)]
    public float debuffChance;

    private void Start()
    {
        Brick.OnBrickDestruction += OnBrickDestruction;
    }

    private void OnBrickDestruction(Brick brick)
    {
        float randomBuff = UnityEngine.Random.Range(0, 100f);
        float randomDebuff = UnityEngine.Random.Range(0, 100f);
        bool alreadySpawn = false;
        if (randomBuff <= buffChance)
        {
            alreadySpawn = true;
            Collectable newBuff = SpawnCollectable(true, brick.transform.position);

        }
        if (randomDebuff <= debuffChance && !alreadySpawn)
        {
            alreadySpawn = true;
            Collectable newBuff = SpawnCollectable(brick, brick.transform.position);

        }
        
    }
    private Collectable SpawnCollectable(bool isBuff, Vector3 position)
    {
        List<Collectable> collectables = isBuff ? new List<Collectable>(buff) : new List<Collectable>(debuff);
        int randomIndex = UnityEngine.Random.Range(0, collectables.Count);
        Collectable prefab = collectables[randomIndex];
        Collectable newCollectable = Instantiate(prefab, position, Quaternion.identity);
        return newCollectable;
    }

    private void OnDisable()
    {
        Brick.OnBrickDestruction -= OnBrickDestruction;
    }
}
