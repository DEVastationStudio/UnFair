// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""ActionMap"",
            ""id"": ""232b1c40-61a5-4b70-82c4-b9e680054f9d"",
            ""actions"": [
                {
                    ""name"": ""E Action"",
                    ""type"": ""Button"",
                    ""id"": ""a86306c8-53f7-4c91-89f8-47315e760279"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Esc Action"",
                    ""type"": ""Button"",
                    ""id"": ""877e78e4-a951-413c-b2b8-cae01e93f0ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Space Action"",
                    ""type"": ""Button"",
                    ""id"": ""43a15b34-3642-4298-a093-8e8713900381"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Enter Action"",
                    ""type"": ""Button"",
                    ""id"": ""62cba363-2fa7-4ce0-aba9-d4d954b9f84e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Left Action"",
                    ""type"": ""Button"",
                    ""id"": ""80a6d4dc-7a4a-4b9a-9ddd-072bec23f1cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Right Action"",
                    ""type"": ""Button"",
                    ""id"": ""3ce2b14c-f3bd-4b46-b36a-2a529590803c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""08a604c3-cce7-4a7a-aa9d-947004da7b1a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""W Action"",
                    ""type"": ""Button"",
                    ""id"": ""4168d36a-756e-4898-b37d-83e8ed2fc1fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""A Action"",
                    ""type"": ""Button"",
                    ""id"": ""801e300d-bb06-4aef-abb9-f903b283a88e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""S Action"",
                    ""type"": ""Button"",
                    ""id"": ""f67cc771-8204-47fa-8e51-3ca0acb1570a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""D Action"",
                    ""type"": ""Button"",
                    ""id"": ""aab910ec-2c39-4f46-b466-f6f19104a363"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6aae309b-8dc6-43b5-8633-a0fbc20f50fd"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""E Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ff3dc93-2712-4a23-91ed-ec4793265929"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Esc Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3073530-c139-4f05-aaf1-15eb63872eed"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e757850-6293-43ae-bca3-275307aa3de9"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74f81612-a5ec-4e81-beb8-388d95c7e89e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Left Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a9fd79f-5892-44b5-b373-68e70389d062"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Right Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""1e404e11-44d7-4127-b18d-4e94b17bc281"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""71bd4c83-6a02-4813-ab7c-1fe9904d78ae"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""85d08449-01a5-4cb0-8bd4-37a434db0fc7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""19454b68-b0cd-4d72-bc91-8c32493bfdfd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8bacdbc3-9fbe-4175-bb38-0dd850c446d5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""01058ec1-871b-4a83-98f6-4dfc96472915"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""eaf75d1d-cb2b-4d2c-b920-26cb482a2594"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c576e641-aace-4e2a-b066-e7ea5011f63f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""296a9ab0-f6a7-4b0c-bc28-ad2ccb6076ed"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a3183c38-fe6b-4dba-bf71-57b6bb879347"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0b3f5740-3d59-4b8b-b04f-1ae50cd82a0c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84c184df-c6bb-4ebd-a6d5-c52f979dde47"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""W Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1af5b1a9-98f1-4d57-aa66-21434c379af5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""S Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fe6ef79-cd32-423c-b664-0853309d192d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouseScheme"",
            ""bindingGroup"": ""KeyboardMouseScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // ActionMap
        m_ActionMap = asset.FindActionMap("ActionMap", throwIfNotFound: true);
        m_ActionMap_EAction = m_ActionMap.FindAction("E Action", throwIfNotFound: true);
        m_ActionMap_EscAction = m_ActionMap.FindAction("Esc Action", throwIfNotFound: true);
        m_ActionMap_SpaceAction = m_ActionMap.FindAction("Space Action", throwIfNotFound: true);
        m_ActionMap_EnterAction = m_ActionMap.FindAction("Enter Action", throwIfNotFound: true);
        m_ActionMap_MouseLeftAction = m_ActionMap.FindAction("Mouse Left Action", throwIfNotFound: true);
        m_ActionMap_MouseRightAction = m_ActionMap.FindAction("Mouse Right Action", throwIfNotFound: true);
        m_ActionMap_Movement = m_ActionMap.FindAction("Movement", throwIfNotFound: true);
        m_ActionMap_WAction = m_ActionMap.FindAction("W Action", throwIfNotFound: true);
        m_ActionMap_AAction = m_ActionMap.FindAction("A Action", throwIfNotFound: true);
        m_ActionMap_SAction = m_ActionMap.FindAction("S Action", throwIfNotFound: true);
        m_ActionMap_DAction = m_ActionMap.FindAction("D Action", throwIfNotFound: true);
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

    // ActionMap
    private readonly InputActionMap m_ActionMap;
    private IActionMapActions m_ActionMapActionsCallbackInterface;
    private readonly InputAction m_ActionMap_EAction;
    private readonly InputAction m_ActionMap_EscAction;
    private readonly InputAction m_ActionMap_SpaceAction;
    private readonly InputAction m_ActionMap_EnterAction;
    private readonly InputAction m_ActionMap_MouseLeftAction;
    private readonly InputAction m_ActionMap_MouseRightAction;
    private readonly InputAction m_ActionMap_Movement;
    private readonly InputAction m_ActionMap_WAction;
    private readonly InputAction m_ActionMap_AAction;
    private readonly InputAction m_ActionMap_SAction;
    private readonly InputAction m_ActionMap_DAction;
    public struct ActionMapActions
    {
        private @PlayerInputs m_Wrapper;
        public ActionMapActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @EAction => m_Wrapper.m_ActionMap_EAction;
        public InputAction @EscAction => m_Wrapper.m_ActionMap_EscAction;
        public InputAction @SpaceAction => m_Wrapper.m_ActionMap_SpaceAction;
        public InputAction @EnterAction => m_Wrapper.m_ActionMap_EnterAction;
        public InputAction @MouseLeftAction => m_Wrapper.m_ActionMap_MouseLeftAction;
        public InputAction @MouseRightAction => m_Wrapper.m_ActionMap_MouseRightAction;
        public InputAction @Movement => m_Wrapper.m_ActionMap_Movement;
        public InputAction @WAction => m_Wrapper.m_ActionMap_WAction;
        public InputAction @AAction => m_Wrapper.m_ActionMap_AAction;
        public InputAction @SAction => m_Wrapper.m_ActionMap_SAction;
        public InputAction @DAction => m_Wrapper.m_ActionMap_DAction;
        public InputActionMap Get() { return m_Wrapper.m_ActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IActionMapActions instance)
        {
            if (m_Wrapper.m_ActionMapActionsCallbackInterface != null)
            {
                @EAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEAction;
                @EAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEAction;
                @EAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEAction;
                @EscAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEscAction;
                @EscAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEscAction;
                @EscAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEscAction;
                @SpaceAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnSpaceAction;
                @SpaceAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnSpaceAction;
                @SpaceAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnSpaceAction;
                @EnterAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEnterAction;
                @EnterAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEnterAction;
                @EnterAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEnterAction;
                @MouseLeftAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMouseLeftAction;
                @MouseLeftAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMouseLeftAction;
                @MouseLeftAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMouseLeftAction;
                @MouseRightAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMouseRightAction;
                @MouseRightAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMouseRightAction;
                @MouseRightAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMouseRightAction;
                @Movement.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMovement;
                @WAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnWAction;
                @WAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnWAction;
                @WAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnWAction;
                @AAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnAAction;
                @AAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnAAction;
                @AAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnAAction;
                @SAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnSAction;
                @SAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnSAction;
                @SAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnSAction;
                @DAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnDAction;
                @DAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnDAction;
                @DAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnDAction;
            }
            m_Wrapper.m_ActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @EAction.started += instance.OnEAction;
                @EAction.performed += instance.OnEAction;
                @EAction.canceled += instance.OnEAction;
                @EscAction.started += instance.OnEscAction;
                @EscAction.performed += instance.OnEscAction;
                @EscAction.canceled += instance.OnEscAction;
                @SpaceAction.started += instance.OnSpaceAction;
                @SpaceAction.performed += instance.OnSpaceAction;
                @SpaceAction.canceled += instance.OnSpaceAction;
                @EnterAction.started += instance.OnEnterAction;
                @EnterAction.performed += instance.OnEnterAction;
                @EnterAction.canceled += instance.OnEnterAction;
                @MouseLeftAction.started += instance.OnMouseLeftAction;
                @MouseLeftAction.performed += instance.OnMouseLeftAction;
                @MouseLeftAction.canceled += instance.OnMouseLeftAction;
                @MouseRightAction.started += instance.OnMouseRightAction;
                @MouseRightAction.performed += instance.OnMouseRightAction;
                @MouseRightAction.canceled += instance.OnMouseRightAction;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @WAction.started += instance.OnWAction;
                @WAction.performed += instance.OnWAction;
                @WAction.canceled += instance.OnWAction;
                @AAction.started += instance.OnAAction;
                @AAction.performed += instance.OnAAction;
                @AAction.canceled += instance.OnAAction;
                @SAction.started += instance.OnSAction;
                @SAction.performed += instance.OnSAction;
                @SAction.canceled += instance.OnSAction;
                @DAction.started += instance.OnDAction;
                @DAction.performed += instance.OnDAction;
                @DAction.canceled += instance.OnDAction;
            }
        }
    }
    public ActionMapActions @ActionMap => new ActionMapActions(this);
    private int m_KeyboardMouseSchemeSchemeIndex = -1;
    public InputControlScheme KeyboardMouseSchemeScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeSchemeIndex == -1) m_KeyboardMouseSchemeSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouseScheme");
            return asset.controlSchemes[m_KeyboardMouseSchemeSchemeIndex];
        }
    }
    public interface IActionMapActions
    {
        void OnEAction(InputAction.CallbackContext context);
        void OnEscAction(InputAction.CallbackContext context);
        void OnSpaceAction(InputAction.CallbackContext context);
        void OnEnterAction(InputAction.CallbackContext context);
        void OnMouseLeftAction(InputAction.CallbackContext context);
        void OnMouseRightAction(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnWAction(InputAction.CallbackContext context);
        void OnAAction(InputAction.CallbackContext context);
        void OnSAction(InputAction.CallbackContext context);
        void OnDAction(InputAction.CallbackContext context);
    }
}
