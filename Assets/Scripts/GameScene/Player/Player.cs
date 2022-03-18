using System;
using UnityEngine;

namespace GameScene.Player
{
   public class Player : MonoBehaviour
   {
      public int hp = 100;
      public int lives = 3;
      private Shooting _shooter;

      private void Awake()
      {
         _shooter = gameObject.GetComponent<Shooting>();
      }


      private void OnCollisionEnter2D(Collision2D col)
      {
         if (col.collider.gameObject.CompareTag("Bullet"))
         {
            hp -= 10;
         }
      }


      private void Update()
      {

         if (Input.GetButtonDown("Fire1"))
         {
            _shooter.Shoot();
         }
         
         
         if (hp <= 0)
         {
            lives -= 1;
         }

         if (lives == 0)
         {
            //gameover
         }
      }
   }
}
