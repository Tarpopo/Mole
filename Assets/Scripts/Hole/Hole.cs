using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
   private Transform _transform;
   [SerializeField] private ParticleSystem _particleSystem;
   [SerializeField] private float _scale=1.5f;
   [SerializeField] private int _frames=7;
   private void Awake()
   {
      _transform = transform;
   }

   public Vector3 GetHolePosition()
   {
      return _transform.position;
   }

   public void StartScale(bool particles=false)
   {
      if(particles)_particleSystem.Play();
      StartCoroutine(Scale(_transform,Vector3.one*_scale,_frames));
   }
   
   private IEnumerator Scale(Transform from,Vector3 target,int frames)
   {
      var startScale = from.localScale;
      var delta = (target - from.localScale) / frames;
      for (int i = 0; i < frames; i++)
      {
         from.localScale += delta;
         yield return new WaitForFixedUpdate();
      }

      delta = (startScale - from.localScale) / frames;
      for (int i = 0; i < frames; i++)
      {
         from.localScale += delta;
         yield return new WaitForFixedUpdate();
      }
   }
}
