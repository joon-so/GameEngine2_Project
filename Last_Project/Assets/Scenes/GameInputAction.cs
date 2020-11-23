// GENERATED AUTOMATICALLY FROM 'Assets/Scenes/Input System/Player/GameInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInputAction"",
    ""maps"": [
        {
            ""name"": ""Fps"",
            ""id"": ""ebe97488-164f-4dab-a0e7-3cd20939f409"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1fcaf0d4-0b5d-4ec8-bd94-3a8f138d48be"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""0b029756-1289-43dd-8288-f1585ba88ab9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d4398b0f-ae8c-44a9-9baf-5246b5c59228"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""427ccab8-7a34-44d5-97f3-6bb8c6fe05a1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c84e4731-17a8-409a-9b03-9c411db5772c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""780c235c-ca3a-4e28-9c93-e467ceecc415"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""95047db7-ff28-47c2-8e86-c88ed0f9b67e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1af63e94-6bec-42da-9a56-b4b6ba53b627"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""88c480c5-9c6f-4fb8-97f0-98e540ba2ca2"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c361ccbb-d9b8-4f9a-bf76-0f9e8f8609b9"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Fps
        m_Fps = asset.FindActionMap("Fps", throwIfNotFound: true);
        m_Fps_Move = m_Fps.FindAction("Move", throwIfNotFound: true);
        m_Fps_Shoot = m_Fps.FindAction("Shoot", throwIfNotFound: true);
        m_Fps_Jump = m_Fps.FindAction("Jump", throwIfNotFound: true);
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

    // Fps
    private readonly InputActionMap m_Fps;
    private IFpsActions m_FpsActionsCallbackInterface;
    private readonly InputAction m_Fps_Move;
    private readonly InputAction m_Fps_Shoot;
    private readonly InputAction m_Fps_Jump;
    public struct FpsActions
    {
        private @GameInputAction m_Wrapper;
        public FpsActions(@GameInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Fps_Move;
        public InputAction @Shoot => m_Wrapper.m_Fps_Shoot;
        public InputAction @Jump => m_Wrapper.m_Fps_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Fps; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FpsActions set) { return set.Get(); }
        public void SetCallbacks(IFpsActions instance)
        {
            if (m_Wrapper.m_FpsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_FpsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_FpsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_FpsActionsCallbackInterface.OnMove;
                @Shoot.started -= m_Wrapper.m_FpsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_FpsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_FpsActionsCallbackInterface.OnShoot;
                @Jump.started -= m_Wrapper.m_FpsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_FpsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_FpsActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_FpsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public FpsActions @Fps => new FpsActions(this);
    public interface IFpsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
