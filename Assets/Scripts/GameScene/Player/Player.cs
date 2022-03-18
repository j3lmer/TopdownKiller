using System;
using UnityEngine;

namespace GameScene.Player
{
   [RequireComponent(typeof(Death))]
   public class Player : MonoBehaviour
   {
      private Health _health;
      private Shooting _shooter;
      private float _countingTime;
      private float _timeUntilShoot = 0.2f;
      public int points             = 0;

      private void Awake()
      {
         _health = GetComponent<Health>();
         _shooter = gameObject.GetComponent<Shooting>();

         _health.hp = 100;
         _health.lives = 3;
      }

      private void Update()
      {
         _countingTime += Time.deltaTime;
         if (Input.GetButtonDown("Fire1") && _timeUntilShoot < _countingTime)
         {
            _countingTime = 0f;
            _shooter.Shoot(Color.white);
         }
      }
   }
}
