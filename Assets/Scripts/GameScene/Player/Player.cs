using System;
using UnityEngine;

namespace GameScene.Player
{
   public class Player : MonoBehaviour
   {
      public int hp = 100;
      public int lives = 3;
      private Shooting shooter;

      private void Awake()
      {
         shooter = gameObject.GetComponent<Shooting>();
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
            shooter.Shoot();
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
