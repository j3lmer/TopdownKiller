using System;
using UnityEngine;

namespace GameScene.Player
{
   [RequireComponent(typeof(Death)), RequireComponent(typeof(Score))]
   public class Player : MonoBehaviour
   {
      private Health _health;
      private Shooting _shooter;
      private Score _score;
      private float _countingTime;
      private float _timeUntilShoot = 0.2f;

      private void Awake()
      {
         _health = GetComponent<Health>();
         _shooter = GetComponent<Shooting>();

         _health.SetHp(100) ;
         _health.SetLives(3);
      }

      private void Update()
      {
         _countingTime += Time.deltaTime;
         if (Input.GetButton("Fire1") && _timeUntilShoot < _countingTime)
         {
            _countingTime = 0f;
            _shooter.Shoot(Color.white);
         }
      }
   }
}
