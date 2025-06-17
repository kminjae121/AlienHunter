using System;
using Blade.Core.StatSystem;
using UnityEngine;
[CreateAssetMenu (menuName = "GunSO", fileName = "Gun")]
public class GunSO : ScriptableObject
{
    public string gunName;
    public float shootSpeed;
    public int maxAmmo;

    public Vector3 boomSize;
    public int Penetrationlevel;
    public float reloadTime;
}
