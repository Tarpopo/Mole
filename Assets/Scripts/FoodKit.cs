using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodKit : MonoBehaviour
{
   private List<Food> _food = new List<Food>();
   [SerializeField] private Text _text;
   [SerializeField] private PanelComponent _losePanel;
   [SerializeField] private GameTimer _gameTimer;
   private EnemyKit _enemyKit;
   [SerializeField] private GameObject _slider;
   private void Start()
   {
      foreach (var food in FindObjectsOfType<Food>())
      {
         _food.Add(food);
      }

      _enemyKit = FindObjectOfType<EnemyKit>();
      _text.text = _food.Count.ToString();
   }

   public Food TryGetFood(Vector3 position, float minDistance)
   {
      if (_food.Count <= 0) return null;
      foreach (var food in _food.Where(food => Vector3.Distance(food.transform.position, position)<=minDistance))
      {
         _food.Remove(food);
         return food;
      }
      return null;
   }

   public IEnumerable<IDamagable> GetFoodInDistance(Vector3 position, float minDistance)
   {
      if (_food.Count <= 0) return null;
      var foodMass = _food.Where(food => Vector3.Distance(food.transform.position, position) <= minDistance);
      var foodInDistance = foodMass as Food[] ?? foodMass.ToArray();
      return foodInDistance;
   }

   public void ReturnFood(Food food)
   {
      _food.Add(food);
   }

   public void RemoveFood(Food food)
   {
      _food.Remove(food);
      UpdateCount();
   }

   public void UpdateCount()
   {
      var count = _food.Count;
      if (count == 0)
      {
         if (_gameTimer.GetWinPanelStatus()) return;
         _slider.SetActive(false);
         _enemyKit.SetActiveEnemies(false);
         _losePanel.SetActive();
      }
      _text.text = count.ToString();
   }

}
