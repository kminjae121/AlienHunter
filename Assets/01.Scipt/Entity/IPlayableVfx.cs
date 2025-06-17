using UnityEngine;

public interface IPlayableVfx
{
    public string VfxName { get; }
    public void PlayVfx(Vector3 position, Quaternion rotation);
    public void StopVfx();
}
