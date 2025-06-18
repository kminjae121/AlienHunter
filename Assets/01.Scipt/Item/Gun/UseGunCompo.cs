using System;
using System.Collections;
using UnityEngine;

namespace _01.Scipt.Item.Gun
{
    public class UseGunCompo : MonoBehaviour
    {
        private GunManageCompo _currentGunCompo;
        [SerializeField] private PlayerInputSO _inputReader;

        private Coroutine shootCoroutine;

        private void Awake()
        {
            _currentGunCompo = GetComponent<GunManageCompo>();
            _inputReader.OnAttackPressd += ShootGun;
        }

        private void OnDestroy()
        {
            _inputReader.OnAttackPressd -= ShootGun;
        }

        private void Update()
        {
            _currentGunCompo.AutoReload();
        }

        public void ShootGun(bool isPressed)
        {
            if (isPressed)
            {
                if (shootCoroutine == null)
                {
                    shootCoroutine = StartCoroutine(ShootLoop());
                }
            }
            else
            {
                if (shootCoroutine != null)
                {
                    StopCoroutine(shootCoroutine);
                    shootCoroutine = null;
                }
            }
        }

        private IEnumerator ShootLoop()
        {
            while (true)
            {
                if (_currentGunCompo.currentAmmo > 0)
                {
                    _currentGunCompo.currentAmmo -= 1;
                    Debug.Log("빵"); 
                }
                else
                {
                    Debug.Log("탄약 없음");
                    break; 
                }

                yield return new WaitForSeconds(_currentGunCompo.shootSpeed);
            }

            shootCoroutine = null; 
        }
    }
}
