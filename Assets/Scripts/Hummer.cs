using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Hummer : MonoBehaviour
{
      private Coroutine _hit;
      private Animator _hummerAnimator;
      private IDamagable _enemy;

      [SerializeField]
      private Material[] _hammerMaterials;

      private void Start()
      {
            _hummerAnimator = GetComponent<Animator>();
      }

      public void OnRotationEnd()
      {
          _hit = null;

        for (int i = 0; i < _hammerMaterials.Length; i++)
        {
            Color targetColor = _hammerMaterials[i].color;
            targetColor.a = 0;

            StartCoroutine(MaterialToColor(_hammerMaterials[i], targetColor, 10));
        }

        //  gameObject.SetActive(false);
    }

      public void StartHit(RaycastHit enemy)
      {
            if (_hit != null) return;
            StopAllCoroutines();
        // gameObject.SetActive(true);
             ReturnColors();
            //_hummerAnimator.Play("Idle");
            _enemy = enemy.collider.GetComponent<IDamagable>(); 
            _hit=StartCoroutine(Hit(transform,enemy.transform.position,15));
            _hummerAnimator.Play("rotation");
      }

      private IEnumerator Hit(Transform from, Vector3 target, int frames)
      {
            from.position = new Vector3(target.x + 2f, 1, target.z);
            yield return new WaitForSeconds(0.13f);
            _enemy.TakeDamage();

            // var delta = (new Vector3(target.x, 10, target.z) - from.position) / frames;
            // for (int i = 0; i < frames; i++)
            // {
            //       from.position += delta;
            //       yield return new WaitForFixedUpdate();
            // }
            _hit = null;
      }

      private void ReturnColors()
    {
        for (int i = 0; i < _hammerMaterials.Length; i++)
        {
            Color target = _hammerMaterials[i].color;
            target.a = 1;
            _hammerMaterials[i].color = target;
        }
    }

      private IEnumerator MaterialToColor(Material mat, Color target, int frames)
      {
        Color delta = (target - mat.color) / frames;

        for (int i = 0; i < frames; i++)
        {
            mat.color += delta;

            yield return null;
        }
      }
}
