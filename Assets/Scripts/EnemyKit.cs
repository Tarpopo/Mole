using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEditor;
using UnityEngine;

public class EnemyKit : MonoBehaviour
{
    [SerializeField] private int _activeEnemies;
    [SerializeField] private List<EnemyType> _enemieTypes;
    private List<GameObject> _allEnemies=new List<GameObject>();
    private Mine[] _mines;
    
    private void Awake()
    {
        _mines = FindObjectsOfType<Mine>();
        foreach (var mine in _mines)
        {
            mine.gameObject.SetActive(false);
        }

        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            var obj = enemy.gameObject;
            obj.SetActive(false);
            _allEnemies.Add(obj);
        }
        
        //_allEnemies = new GameObject[_activeEnemies];
        // for (int i = 0; i < _activeEnemies; i++)
        // { 
        //     _allEnemies[i] = Instantiate(_enemieTypes.RandomByChance().Enemy);
        //     _allEnemies[i].SetActive(false);
        // }
        //print(_enemieTypes.Count);
    }

    public bool IsHaveOutSideEnemies()
    {
        bool isHave = false;
        foreach (var enemy in _allEnemies)
        {
            if (enemy.GetComponent<IHoleEntity>().IsOutside)
            {
                isHave = true;
                continue;
            }
            enemy.SetActive(false);
        }
        
        return isHave;
    }
    public void SetActiveEnemies(bool active)
    {
        foreach (var enemy in _allEnemies)
        {
            enemy.SetActive(active);
        }
        foreach (var mine in _mines)
        {
            mine.gameObject.SetActive(active);
        }
    }
}
[System.Serializable]
public class EnemyType : IRandom
{
    public float ReturnChance => _chance;
    public GameObject Enemy => _enemy;
    [SerializeField]private GameObject _enemy;
    [SerializeField] private float _chance;
}
