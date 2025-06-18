using System;
using System.Collections;
using UnityEngine;

namespace _01.Scipt.Item.Gun
{
    public class UseGunCompo : MonoBehaviour
    {
        private GunManageCompo _currentGunCompo;
        
        private void Update()
        {
            _currentGunCompo.AutoReload();
        }

        public void ShootGun(bool istrue)
        {
            while (istrue)
            {
                Shoot();
            }
        }


        private IEnumerator Shoot()
        {
            _currentGunCompo.currentAmmo -= 1;
            
            yield return new WaitForSeconds(_currentGunCompo.shootSpeed);
            
        }
    }
}
