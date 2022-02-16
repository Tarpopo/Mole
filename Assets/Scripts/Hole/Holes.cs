using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Holes : MonoBehaviour
{
   private List<Hole> _freeHoles=new List<Hole>();
   private List<Hole> _occupiedHoles=new List<Hole>();

   private void Awake()
   {
      _freeHoles.AddRange(FindObjectsOfType<Hole>());
   }

   public Hole GetFreeHole()
   {
      return _freeHoles.Substitute(_occupiedHoles, _freeHoles[Random.Range(0,_freeHoles.Count)]);
   }

   public void TryFreeHole(Hole occupiedHole)
   {
      _occupiedHoles.Substitute(_freeHoles, occupiedHole);
   }
   
}
