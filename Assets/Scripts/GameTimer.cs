using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
   [SerializeField] private float _time;
   [SerializeField] private Slider _slider;
   [SerializeField] private PanelComponent _winPanel;
   [SerializeField] private PanelComponent _losePanel;
   [SerializeField] private float _delay; 
   public bool IsTick => _currentTime > 0||_isEnemyWait;
   private bool _isEnemyWait;
   private EnemyKit _enemyKit;
   private float _currentTime;

   public void StartTimer()
   {
      _currentTime = _time;
      _slider.maxValue = _time;
      _enemyKit = FindObjectOfType<EnemyKit>();
   }
   public void StartGameTimer()
   {
      Invoke(nameof(StartTimer),_delay);
   }

   private void Update()
   {
      if (_currentTime <= 0) return;
      _currentTime -= Time.deltaTime;
      _slider.value = _slider.maxValue - _currentTime;
      if (_currentTime <= 0)
      {
         _slider.gameObject.SetActive(false);
         if (_losePanel.gameObject.activeSelf) return;
         StartCoroutine(CheckEnemies());
      }
   }

   private IEnumerator CheckEnemies()
   {
      _isEnemyWait = true;
      while (true)
      {
         if (_losePanel.gameObject.activeInHierarchy || _enemyKit.IsHaveOutSideEnemies() == false) break;
         yield return null;
      }
      _isEnemyWait = false;
      if (_losePanel.gameObject.activeSelf==false)_winPanel.SetActive();
   }
   public bool GetWinPanelStatus()
   {
      return _winPanel.gameObject.activeInHierarchy;
   }

}
