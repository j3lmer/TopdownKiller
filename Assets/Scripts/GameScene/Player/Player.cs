using System;
using UnityEngine;

namespace GameScene.Player
{
   public class Player : MonoBehaviour
   {
      public int points = 0;
      private Shooting _shooter;
      private float _timeUntilShoot = 0.2f;
      private float _countingTime;
      private Health _health;
      private Death _death;

      private void Awake()
      {
         _health = gameObject.AddComponent<Health>();
         _death = gameObject.AddComponent<Death>();
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
