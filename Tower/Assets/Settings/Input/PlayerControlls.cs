// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/PlayerControlls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControlls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControlls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControlls"",
    ""maps"": [
        {
            ""name"": ""Controlls"",
            ""id"": ""8f05d640-9deb-4d0f-aa36-91fec24dfc8c"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7a737afe-4b2c-4f7e-9d09-f74556b4635a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TakeOff"",
                    ""type"": ""Button"",
                    ""id"": ""eccd11a1-0bb8-4535-999c-ba24a11d2f4e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""93b0a782-61c2-4f80-9fef-93cbd4a0c1f6"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4623c5f9-6dc4-4eb7-b65b-714b34836ccd"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1b8d847-c95d-4819-802b-5f6542cdc6f9"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TakeOff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b50828bc-2e8c-4175-af4b-7e518846a1ed"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controlls
        m_Controlls = asset.FindActionMap("Controlls", throwIfNotFound: true);
        m_Controlls_Jump = m_Controlls.FindAction("Jump", throwIfNotFound: true);
        m_Controlls_TakeOff = m_Controlls.FindAction("TakeOff", throwIfNotFound: true);
        m_Controlls_Move = m_Controlls.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Controlls
    private readonly InputActionMap m_Controlls;
    private IControllsActions m_ControllsActionsCallbackInterface;
    private readonly InputAction m_Controlls_Jump;
    private readonly InputAction m_Controlls_TakeOff;
    private readonly InputAction m_Controlls_Move;
    public struct ControllsActions
    {
        private @PlayerControlls m_Wrapper;
        public ControllsActions(@PlayerControlls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Controlls_Jump;
        public InputAction @TakeOff => m_Wrapper.m_Controlls_TakeOff;
        public InputAction @Move => m_Wrapper.m_Controlls_Move;
        public InputActionMap Get() { return m_Wrapper.m_Controlls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllsActions set) { return set.Get(); }
        public void SetCallbacks(IControllsActions instance)
        {
            if (m_Wrapper.m_ControllsActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_ControllsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ControllsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ControllsActionsCallbackInterface.OnJump;
                @TakeOff.started -= m_Wrapper.m_ControllsActionsCallbackInterface.OnTakeOff;
                @TakeOff.performed -= m_Wrapper.m_ControllsActionsCallbackInterface.OnTakeOff;
                @TakeOff.canceled -= m_Wrapper.m_ControllsActionsCallbackInterface.OnTakeOff;
                @Move.started -= m_Wrapper.m_ControllsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ControllsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ControllsActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_ControllsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @TakeOff.started += instance.OnTakeOff;
                @TakeOff.performed += instance.OnTakeOff;
                @TakeOff.canceled += instance.OnTakeOff;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public ControllsActions @Controlls => new ControllsActions(this);
    public interface IControllsActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnTakeOff(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
