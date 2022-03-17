using UnityEngine;

namespace GameScene.Player
{
   public class Player : MonoBehaviour
   {
      public int hp = 100;
      public int lives = 3;


      private void OnCollisionEnter2D(Collision2D col)
      {
         if (col.collider.gameObject.CompareTag("Bullet"))
         {
            hp -= 10;
         }
      }


      private void Update()
      {
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
