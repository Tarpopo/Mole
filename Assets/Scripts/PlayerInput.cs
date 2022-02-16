using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using Interfaces;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
   private IDamagable _enemy;
   private Camera _camera;
   private RaycastHit _collider;
   private GameTimer _gameTimer;
   private float _screenWidth;
   private Hummer _hummer;
 
   private void Start()
   {
      _gameTimer = FindObjectOfType<GameTimer>();
      _hummer = FindObjectOfType<Hummer>();
      _camera = Camera.main;
      _screenWidth = Screen.width/2;
   }

   private void Update()
   {
      if (_gameTimer.IsTick == false) return;
      if (Input.GetMouseButtonDown(0))
      {
         if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out _collider,100))
         {
            _hummer.transform.rotation = Input.mousePosition.x > _screenWidth
               ? Quaternion.Euler(0, 180, 0)
               : Quaternion.Euler(0, 0, 0); 
            if(_collider.transform.TryGetComponent(out _enemy))_hummer.StartHit(_collider);
            
         }
      }
   }
   
   private IEnumerator ImageToColorAnim(Material material, Color target, int frames)
   {
      Color deltaColor = (target - material.color) / frames;

      Color tempColor = material.color;

      for (int i = 0; i < frames; i++)
      {
         tempColor += deltaColor;
         material.color = tempColor;

         yield return null;
      }
   }
}
