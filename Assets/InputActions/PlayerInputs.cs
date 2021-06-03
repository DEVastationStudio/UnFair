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
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Mouse Left Action"",
                    ""type"": ""Button"",
                    ""id"": ""80a6d4dc-7a4a-4b9a-9ddd-072bec23f1cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
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
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""176a288d-6e1a-447f-883c-92b7019f1454"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftStick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c30d6243-c6a2-4cfa-aa79-231016298b96"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9ff3dc93-2712-4a23-91ed-ec4793265929"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""Esc Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66308019-f0db-44d7-a87f-37ecd72bdcef"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""Space Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29f75143-e752-43c7-888e-3cc87c0ee8a9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""Space Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74f81612-a5ec-4e81-beb8-388d95c7e89e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""Mouse Left Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35a2affc-fe3a-4f7f-af12-ca59b142dc9f"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""Mouse Right Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e746f2d3-edcd-4305-9fdf-943ac9f6bcae"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8865be24-b19c-4a5f-8c12-db8bdfea4308"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b3f5740-3d59-4b8b-b04f-1ae50cd82a0c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""A Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85dc5c11-9ef4-498f-80b9-60ec2b279d3a"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""A Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbcbf9dd-ab28-4afa-9346-f53060d163da"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""W Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fda9b93-83c7-43f4-9c4e-2f4652ee6e0a"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""W Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d9a38f3-5f1e-4abd-b520-7cfa76377fef"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""S Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3272ac7b-90d9-4484-905f-234064f4f19b"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""S Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a04c66ca-5a60-4dfb-bd3d-de14489e81f2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
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
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""D Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14701437-0a36-488f-82c0-940ca45de308"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""D Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90f4819f-3f8a-4dc0-ba85-bb4d2b4adb47"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""D Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e757850-6293-43ae-bca3-275307aa3de9"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""Space Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05d9f662-7524-4ab6-8480-717b1d1eb49d"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2f71391-efee-4051-abd3-2720aed2df0d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c42d886-ebe4-4df1-8d90-1cf1d7610386"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""Space Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""D-Pad"",
                    ""id"": ""2d4eacd5-2ce6-41a1-a64a-e67b7309b1fd"",
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
                    ""id"": ""8785d2a1-71c5-4717-bb0b-06833afb2f0e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8d472014-5c27-487f-bb76-d05fad830d1e"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""819d8713-18d6-417a-a17b-924a5d15d78a"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""02c9dfdb-4851-41de-9049-fa6d8f709cf9"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e53d2810-9890-4ba3-93e6-49f31aafe3b7"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UIMap"",
            ""id"": ""80da7698-ee51-418b-a171-0f58513e437e"",
            ""actions"": [
                {
                    ""name"": ""MovementUI"",
                    ""type"": ""Value"",
                    ""id"": ""82b658c4-9105-4c7b-9d3e-b2a63087b794"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MouseRightActionUI"",
                    ""type"": ""Button"",
                    ""id"": ""33e9da74-60d5-4e68-9cf5-d9e92bfa4493"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLeftActionUI"",
                    ""type"": ""Button"",
                    ""id"": ""8d7553af-cb9b-4a97-8acf-bc45cf3f988a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""AcceptUI"",
                    ""type"": ""Button"",
                    ""id"": ""5de2e36b-0a6d-4e9f-82a4-8cb4595d8bed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PointUI"",
                    ""type"": ""Value"",
                    ""id"": ""0c26a9db-8a9c-4d8e-b1fc-7dfe93b8179b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Esc Action"",
                    ""type"": ""Button"",
                    ""id"": ""cbd1af9c-1dac-4b6d-bb31-1d3ee64e585e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrows"",
                    ""id"": ""cfc45ab9-0f0d-4c87-963d-c7c5dfec2512"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementUI"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ae801b79-d75f-4e2f-97e8-695c01cb5d64"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d54d83de-069f-4d9a-b5ee-b202b53aaf84"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8516239e-45af-439d-920c-610542333c50"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d4352d00-0988-43ce-ba4e-3c8a9624b6a6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""D-Pad"",
                    ""id"": ""a5887124-167a-4a07-a307-167dfc2d869e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementUI"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9e5e1b32-cad9-4e95-8ba4-702c71a2c76e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0bea481a-1892-4e9a-b94b-eaeb9868b67b"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bd571ab2-41b9-47c1-a86b-bb26ecdfbea5"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c32011a6-6dfa-4e43-96ce-2b4e6288a333"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""dc890a50-ec8a-466c-9f7f-452cb0b5654e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d212670-4a26-4564-8b1b-bae8aceafa20"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""E Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa6e54e9-d5d2-4fa6-85fe-3ac7ec024828"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""E Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f4dcba7-9471-45ca-a481-e27c2b4a9ce2"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""AcceptUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f271706-f373-43ae-8441-455a1f953b3b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""AcceptUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc1563b0-faba-45d6-b6e4-2930203a5abb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""AcceptUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0358226f-4181-4165-95e3-594b97567491"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""AcceptUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""869bc1e7-93c1-431a-a3f5-7f896bcacb26"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""AcceptUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7334769-18d7-48b5-85dd-668c4725be0a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""PointUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""662cb897-3d8d-4957-a827-f0423dfbae08"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""Esc Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61996363-c157-470e-beb9-1238259bed61"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""Esc Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""9612da76-e81e-492b-b4a7-32c29d88013d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementUI"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a391fb45-ab0f-465a-aad5-a287a91be67a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""efeccebf-993e-4ae1-b2b5-7af0983b1070"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""533ef685-e7e8-47b2-8c0e-10d4939374b6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""efe7b68d-c692-4527-99de-11526ed73f87"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""MovementUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""943e88ce-31d7-451a-a3d6-84e63a408564"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""MouseLeftActionUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5baed693-11b6-489e-94e1-80c97ce536b9"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""MouseLeftActionUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""674d3159-d42a-49e5-be1c-d9b1b940a235"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouseScheme"",
                    ""action"": ""MouseRightActionUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96693e5d-7404-4da7-87ee-da2c53233bf0"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadScheme"",
                    ""action"": ""MouseRightActionUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""NoMap"",
            ""id"": ""73265a04-b4f9-4a97-a827-e4d9a9c41953"",
            ""actions"": [],
            ""bindings"": []
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
        },
        {
            ""name"": ""GamepadScheme"",
            ""bindingGroup"": ""GamepadScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // ActionMap
        m_ActionMap = asset.FindActionMap("ActionMap", throwIfNotFound: true);
        m_ActionMap_EscAction = m_ActionMap.FindAction("Esc Action", throwIfNotFound: true);
        m_ActionMap_SpaceAction = m_ActionMap.FindAction("Space Action", throwIfNotFound: true);
        m_ActionMap_MouseLeftAction = m_ActionMap.FindAction("Mouse Left Action", throwIfNotFound: true);
        m_ActionMap_MouseRightAction = m_ActionMap.FindAction("Mouse Right Action", throwIfNotFound: true);
        m_ActionMap_Movement = m_ActionMap.FindAction("Movement", throwIfNotFound: true);
        m_ActionMap_WAction = m_ActionMap.FindAction("W Action", throwIfNotFound: true);
        m_ActionMap_AAction = m_ActionMap.FindAction("A Action", throwIfNotFound: true);
        m_ActionMap_SAction = m_ActionMap.FindAction("S Action", throwIfNotFound: true);
        m_ActionMap_DAction = m_ActionMap.FindAction("D Action", throwIfNotFound: true);
        m_ActionMap_Look = m_ActionMap.FindAction("Look", throwIfNotFound: true);
        m_ActionMap_LeftStick = m_ActionMap.FindAction("LeftStick", throwIfNotFound: true);
        // UIMap
        m_UIMap = asset.FindActionMap("UIMap", throwIfNotFound: true);
        m_UIMap_MovementUI = m_UIMap.FindAction("MovementUI", throwIfNotFound: true);
        m_UIMap_MouseRightActionUI = m_UIMap.FindAction("MouseRightActionUI", throwIfNotFound: true);
        m_UIMap_MouseLeftActionUI = m_UIMap.FindAction("MouseLeftActionUI", throwIfNotFound: true);
        m_UIMap_AcceptUI = m_UIMap.FindAction("AcceptUI", throwIfNotFound: true);
        m_UIMap_PointUI = m_UIMap.FindAction("PointUI", throwIfNotFound: true);
        m_UIMap_EscAction = m_UIMap.FindAction("Esc Action", throwIfNotFound: true);
        // NoMap
        m_NoMap = asset.FindActionMap("NoMap", throwIfNotFound: true);
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
    private readonly InputAction m_ActionMap_EscAction;
    private readonly InputAction m_ActionMap_SpaceAction;
    private readonly InputAction m_ActionMap_MouseLeftAction;
    private readonly InputAction m_ActionMap_MouseRightAction;
    private readonly InputAction m_ActionMap_Movement;
    private readonly InputAction m_ActionMap_WAction;
    private readonly InputAction m_ActionMap_AAction;
    private readonly InputAction m_ActionMap_SAction;
    private readonly InputAction m_ActionMap_DAction;
    private readonly InputAction m_ActionMap_Look;
    private readonly InputAction m_ActionMap_LeftStick;
    public struct ActionMapActions
    {
        private @PlayerInputs m_Wrapper;
        public ActionMapActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @EscAction => m_Wrapper.m_ActionMap_EscAction;
        public InputAction @SpaceAction => m_Wrapper.m_ActionMap_SpaceAction;
        public InputAction @MouseLeftAction => m_Wrapper.m_ActionMap_MouseLeftAction;
        public InputAction @MouseRightAction => m_Wrapper.m_ActionMap_MouseRightAction;
        public InputAction @Movement => m_Wrapper.m_ActionMap_Movement;
        public InputAction @WAction => m_Wrapper.m_ActionMap_WAction;
        public InputAction @AAction => m_Wrapper.m_ActionMap_AAction;
        public InputAction @SAction => m_Wrapper.m_ActionMap_SAction;
        public InputAction @DAction => m_Wrapper.m_ActionMap_DAction;
        public InputAction @Look => m_Wrapper.m_ActionMap_Look;
        public InputAction @LeftStick => m_Wrapper.m_ActionMap_LeftStick;
        public InputActionMap Get() { return m_Wrapper.m_ActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IActionMapActions instance)
        {
            if (m_Wrapper.m_ActionMapActionsCallbackInterface != null)
            {
                @EscAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEscAction;
                @EscAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEscAction;
                @EscAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnEscAction;
                @SpaceAction.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnSpaceAction;
                @SpaceAction.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnSpaceAction;
                @SpaceAction.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnSpaceAction;
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
                @Look.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLook;
                @LeftStick.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftStick;
                @LeftStick.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftStick;
                @LeftStick.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftStick;
            }
            m_Wrapper.m_ActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @EscAction.started += instance.OnEscAction;
                @EscAction.performed += instance.OnEscAction;
                @EscAction.canceled += instance.OnEscAction;
                @SpaceAction.started += instance.OnSpaceAction;
                @SpaceAction.performed += instance.OnSpaceAction;
                @SpaceAction.canceled += instance.OnSpaceAction;
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
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @LeftStick.started += instance.OnLeftStick;
                @LeftStick.performed += instance.OnLeftStick;
                @LeftStick.canceled += instance.OnLeftStick;
            }
        }
    }
    public ActionMapActions @ActionMap => new ActionMapActions(this);

    // UIMap
    private readonly InputActionMap m_UIMap;
    private IUIMapActions m_UIMapActionsCallbackInterface;
    private readonly InputAction m_UIMap_MovementUI;
    private readonly InputAction m_UIMap_MouseRightActionUI;
    private readonly InputAction m_UIMap_MouseLeftActionUI;
    private readonly InputAction m_UIMap_AcceptUI;
    private readonly InputAction m_UIMap_PointUI;
    private readonly InputAction m_UIMap_EscAction;
    public struct UIMapActions
    {
        private @PlayerInputs m_Wrapper;
        public UIMapActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementUI => m_Wrapper.m_UIMap_MovementUI;
        public InputAction @MouseRightActionUI => m_Wrapper.m_UIMap_MouseRightActionUI;
        public InputAction @MouseLeftActionUI => m_Wrapper.m_UIMap_MouseLeftActionUI;
        public InputAction @AcceptUI => m_Wrapper.m_UIMap_AcceptUI;
        public InputAction @PointUI => m_Wrapper.m_UIMap_PointUI;
        public InputAction @EscAction => m_Wrapper.m_UIMap_EscAction;
        public InputActionMap Get() { return m_Wrapper.m_UIMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIMapActions set) { return set.Get(); }
        public void SetCallbacks(IUIMapActions instance)
        {
            if (m_Wrapper.m_UIMapActionsCallbackInterface != null)
            {
                @MovementUI.started -= m_Wrapper.m_UIMapActionsCallbackInterface.OnMovementUI;
                @MovementUI.performed -= m_Wrapper.m_UIMapActionsCallbackInterface.OnMovementUI;
                @MovementUI.canceled -= m_Wrapper.m_UIMapActionsCallbackInterface.OnMovementUI;
                @MouseRightActionUI.started -= m_Wrapper.m_UIMapActionsCallbackInterface.OnMouseRightActionUI;
                @MouseRightActionUI.performed -= m_Wrapper.m_UIMapActionsCallbackInterface.OnMouseRightActionUI;
                @MouseRightActionUI.canceled -= m_Wrapper.m_UIMapActionsCallbackInterface.OnMouseRightActionUI;
                @MouseLeftActionUI.started -= m_Wrapper.m_UIMapActionsCallbackInterface.OnMouseLeftActionUI;
                @MouseLeftActionUI.performed -= m_Wrapper.m_UIMapActionsCallbackInterface.OnMouseLeftActionUI;
                @MouseLeftActionUI.canceled -= m_Wrapper.m_UIMapActionsCallbackInterface.OnMouseLeftActionUI;
                @AcceptUI.started -= m_Wrapper.m_UIMapActionsCallbackInterface.OnAcceptUI;
                @AcceptUI.performed -= m_Wrapper.m_UIMapActionsCallbackInterface.OnAcceptUI;
                @AcceptUI.canceled -= m_Wrapper.m_UIMapActionsCallbackInterface.OnAcceptUI;
                @PointUI.started -= m_Wrapper.m_UIMapActionsCallbackInterface.OnPointUI;
                @PointUI.performed -= m_Wrapper.m_UIMapActionsCallbackInterface.OnPointUI;
                @PointUI.canceled -= m_Wrapper.m_UIMapActionsCallbackInterface.OnPointUI;
                @EscAction.started -= m_Wrapper.m_UIMapActionsCallbackInterface.OnEscAction;
                @EscAction.performed -= m_Wrapper.m_UIMapActionsCallbackInterface.OnEscAction;
                @EscAction.canceled -= m_Wrapper.m_UIMapActionsCallbackInterface.OnEscAction;
            }
            m_Wrapper.m_UIMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovementUI.started += instance.OnMovementUI;
                @MovementUI.performed += instance.OnMovementUI;
                @MovementUI.canceled += instance.OnMovementUI;
                @MouseRightActionUI.started += instance.OnMouseRightActionUI;
                @MouseRightActionUI.performed += instance.OnMouseRightActionUI;
                @MouseRightActionUI.canceled += instance.OnMouseRightActionUI;
                @MouseLeftActionUI.started += instance.OnMouseLeftActionUI;
                @MouseLeftActionUI.performed += instance.OnMouseLeftActionUI;
                @MouseLeftActionUI.canceled += instance.OnMouseLeftActionUI;
                @AcceptUI.started += instance.OnAcceptUI;
                @AcceptUI.performed += instance.OnAcceptUI;
                @AcceptUI.canceled += instance.OnAcceptUI;
                @PointUI.started += instance.OnPointUI;
                @PointUI.performed += instance.OnPointUI;
                @PointUI.canceled += instance.OnPointUI;
                @EscAction.started += instance.OnEscAction;
                @EscAction.performed += instance.OnEscAction;
                @EscAction.canceled += instance.OnEscAction;
            }
        }
    }
    public UIMapActions @UIMap => new UIMapActions(this);

    // NoMap
    private readonly InputActionMap m_NoMap;
    private INoMapActions m_NoMapActionsCallbackInterface;
    public struct NoMapActions
    {
        private @PlayerInputs m_Wrapper;
        public NoMapActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_NoMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NoMapActions set) { return set.Get(); }
        public void SetCallbacks(INoMapActions instance)
        {
            if (m_Wrapper.m_NoMapActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_NoMapActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public NoMapActions @NoMap => new NoMapActions(this);
    private int m_KeyboardMouseSchemeSchemeIndex = -1;
    public InputControlScheme KeyboardMouseSchemeScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeSchemeIndex == -1) m_KeyboardMouseSchemeSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouseScheme");
            return asset.controlSchemes[m_KeyboardMouseSchemeSchemeIndex];
        }
    }
    private int m_GamepadSchemeSchemeIndex = -1;
    public InputControlScheme GamepadSchemeScheme
    {
        get
        {
            if (m_GamepadSchemeSchemeIndex == -1) m_GamepadSchemeSchemeIndex = asset.FindControlSchemeIndex("GamepadScheme");
            return asset.controlSchemes[m_GamepadSchemeSchemeIndex];
        }
    }
    public interface IActionMapActions
    {
        void OnEscAction(InputAction.CallbackContext context);
        void OnSpaceAction(InputAction.CallbackContext context);
        void OnMouseLeftAction(InputAction.CallbackContext context);
        void OnMouseRightAction(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnWAction(InputAction.CallbackContext context);
        void OnAAction(InputAction.CallbackContext context);
        void OnSAction(InputAction.CallbackContext context);
        void OnDAction(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnLeftStick(InputAction.CallbackContext context);
    }
    public interface IUIMapActions
    {
        void OnMovementUI(InputAction.CallbackContext context);
        void OnMouseRightActionUI(InputAction.CallbackContext context);
        void OnMouseLeftActionUI(InputAction.CallbackContext context);
        void OnAcceptUI(InputAction.CallbackContext context);
        void OnPointUI(InputAction.CallbackContext context);
        void OnEscAction(InputAction.CallbackContext context);
    }
    public interface INoMapActions
    {
    }
}
