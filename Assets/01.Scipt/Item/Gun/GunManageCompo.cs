using System;
using System.Collections;
using System.Collections.Generic;
using _01.Scipt.Player.Player;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using Unity.VisualScripting;
using UnityEngine;

public class GunManageCompo : MonoBehaviour, IEntityComponet
{
    [field: SerializeField] public GunSO currentGun;
    [field: SerializeField] public List<GunSO> invenGun;

    [field: SerializeField] public GameObject _bulletPrefab;
    
    [field: SerializeField] public float shootSpeed;
    [field: SerializeField] public int maxAmmo;
    
    [field: SerializeField] public float reloadTime;

    public int currentAmmo { get; set; }

    private Player _entity;
    public void Initialize(Entity entity)
    {
        _entity = entity as Player;
    }
    public bool canShoot { get; set; }

    private void Awake()
    {
        shootSpeed = currentGun.shootSpeed;
        maxAmmo = currentGun.maxAmmo;
        reloadTime = currentGun.reloadTime;
        currentAmmo = maxAmmo;
    }

    public void EquipNewWeapon(int selectGunIdx)
    {
        currentGun = invenGun[selectGunIdx];

        shootSpeed = currentGun.shootSpeed;
        maxAmmo = currentGun.maxAmmo;
        reloadTime = currentGun.reloadTime;
        currentAmmo = maxAmmo;
    }

    public void AutoReload()
    {
        if (currentAmmo <= 0)
        {
            canShoot = false;
            //_entity.ChangeState("RELOAD");
            StartCoroutine(ReloadAmmo());
        }
        else
            return;
    }

    public void SelfReload()
    {
        canShoot = false;
        _entity.ChangeState("RELOAD");
        StartCoroutine(ReloadAmmo());
    }

    private void Update()
    {
    }


    private IEnumerator ReloadAmmo()
    {
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        canShoot = true;
    }

}
