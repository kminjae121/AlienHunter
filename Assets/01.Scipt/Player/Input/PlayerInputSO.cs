using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "Scriptable Objects/PlayerInputSO")]
public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
{
    [SerializeField] private LayerMask whatIsGround;

    public event Action<bool> OnAttackPressd;

    public event Action OnFirstSelect;
    public event Action OnSecondSelect;
    public event Action OnThridSelect;
    

    private Controls _control;
    private Vector2 _screenPos;
    private Vector3 _worldPos;
    public Action OnRollPressed;

    public Vector2 MovementKey { get; set; }

    private void OnEnable()
    {
        if (_control == null)
        {
            _control = new Controls();
            _control.Player.SetCallbacks(this);
        }

        _control.Player.Enable();
    }

    private void OnDisable()
    {
        _control.Player.Disable();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnAttackPressd?.Invoke(true);
        else if(context.canceled)
                OnAttackPressd?.Invoke(false);
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        var movementKey = context.ReadValue<Vector2>();
        MovementKey = movementKey;
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
        _screenPos = context.ReadValue<Vector2>();
    }

    public void OnSelectFirst(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnFirstSelect?.Invoke();
    }

    public void OnSelectSecond(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnSecondSelect?.Invoke();
    }

    public void OnSelectThird(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnThridSelect?.Invoke();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnRollPressed?.Invoke();
    }


    public Vector3 GetWorldPosition(out RaycastHit hit)
    {
        var main = Camera.main;
        Debug.Assert(main != null, "No main camera in this scene");

        var cameraRay = main.ScreenPointToRay(_screenPos);
        if (Physics.Raycast(cameraRay, out hit, main.farClipPlane, whatIsGround)) _worldPos = hit.point;
        return _worldPos;
    }
}