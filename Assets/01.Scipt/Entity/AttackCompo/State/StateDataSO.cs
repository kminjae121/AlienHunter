using UnityEngine;

[CreateAssetMenu(fileName = "StateData", menuName = "SO/Fsm/StateData", order = 0)]
public class StateDataSO : ScriptableObject
{
    public string stateName;
    public string className;
    public string animParamName;

    public int animationHash;

    private void OnValidate()
    {
        animationHash = Animator.StringToHash(animParamName);
    }
}
