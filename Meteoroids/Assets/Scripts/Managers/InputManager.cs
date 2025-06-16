using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public event EventHandler<float> OnPlayerRotates;
    public event EventHandler<bool> OnPlayerThrusts;
    public event EventHandler OnPlayerShoots;
    public event EventHandler OnPlayerTeleports;
    public event EventHandler OnPlayerUsesPowerUp;

    [SerializeField] private InputActionReference _rotate;
    [SerializeField] private InputActionReference _thrust;
    [SerializeField] private InputActionReference _shoot;
    [SerializeField] private InputActionReference _teleport;
    [SerializeField] private InputActionReference _usePowerUp;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitActions();
        EnableInGameActions();
    }

    private void InitActions()
    {
        InitRotateAction();
        InitThrustAction();
        InitShootAction();
        InitTeleportAction();
        InitUsePowerUpAction();
    }
    private void EnableInGameActions()
    {
        _rotate.action.Enable();
        _thrust.action.Enable();
    }
    private void DisableInGameActions()
    {
        _rotate.action.Disable();
        _thrust.action.Disable();
    }

    #region RotateAction
    private void InitRotateAction()
    {
        _rotate.action.performed += RotatePerformed;
        _rotate.action.canceled += RotateCanceled;
    }

    private void RotatePerformed(InputAction.CallbackContext obj) => OnPlayerRotates?.Invoke(this, obj.ReadValue<float>());
    private void RotateCanceled(InputAction.CallbackContext obj) => OnPlayerRotates?.Invoke(this, 0.0f);

    #endregion

    #region ThrustAction
    private void InitThrustAction()
    {
        _thrust.action.performed += ThrustPerformed;
        _thrust.action.canceled += ThrustCanceled;
    }
    private void ThrustPerformed(InputAction.CallbackContext obj) => OnPlayerThrusts?.Invoke(this, true);
    private void ThrustCanceled(InputAction.CallbackContext obj) => OnPlayerThrusts?.Invoke(this, false);
    #endregion

    #region ShootAction
    private void InitShootAction() => _shoot.action.performed += ShootPerformed;
    private void ShootPerformed(InputAction.CallbackContext obj) => OnPlayerShoots?.Invoke(this, EventArgs.Empty);
    #endregion

    #region TeleportAction
    private void InitTeleportAction() => _shoot.action.performed += TeleportPerformed;
    private void TeleportPerformed(InputAction.CallbackContext obj) => OnPlayerTeleports?.Invoke(this, EventArgs.Empty);
    #endregion

    #region UsePowerUpAction
    private void InitUsePowerUpAction() => _shoot.action.performed += UsePowerUpPerformed;
    private void UsePowerUpPerformed(InputAction.CallbackContext obj) => OnPlayerUsesPowerUp?.Invoke(this, EventArgs.Empty);
    #endregion
}