using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    #region Singleton
    
    private static BricksManager _instance;
    public static BricksManager Instance => _instance;

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
    
    public List<Brick> remainingBriks;
    
    
    // Start is called before the first frame update
    void Start()
    {
        remainingBriks = new List<Brick>(gameObject.GetComponentsInChildren<Brick>());
        Brick.OnBrickDestruction += OnBrickDestruction;
    }

    private void OnBrickDestruction(Brick brick)
    {
        remainingBriks.Remove(brick);
        if (remainingBriks.Count <= 0)
        {
            GameManager.Instance.Victory();
        }
    }

    private void OnDisable()
    {
        Brick.OnBrickDestruction -= OnBrickDestruction;
    }
}
