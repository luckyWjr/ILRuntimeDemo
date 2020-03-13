// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputAction// : IInputActionCollection, IDisposable
{
//     public InputActionAsset asset { get; }
//     public @PlayerInputAction()
//     {
//         asset = InputActionAsset.FromJson(@"{
//     ""name"": ""PlayerInputAction"",
//     ""maps"": [
//         {
//             ""name"": ""Player"",
//             ""id"": ""d726d2b1-20de-43cb-a2b9-9f9f8957d2a1"",
//             ""actions"": [
//                 {
//                     ""name"": ""Move"",
//                     ""type"": ""Value"",
//                     ""id"": ""d7b37d90-6a6e-4b80-af00-90fd78eb47f5"",
//                     ""expectedControlType"": ""Vector2"",
//                     ""processors"": """",
//                     ""interactions"": """"
//                 },
//                 {
//                     ""name"": ""Skill1"",
//                     ""type"": ""Button"",
//                     ""id"": ""82254675-e746-4dc7-ab1c-bca360476bbe"",
//                     ""expectedControlType"": """",
//                     ""processors"": """",
//                     ""interactions"": """"
//                 },
//                 {
//                     ""name"": ""Skill2"",
//                     ""type"": ""Button"",
//                     ""id"": ""03a1d5a1-799b-4728-b12b-b9c96da84b05"",
//                     ""expectedControlType"": """",
//                     ""processors"": """",
//                     ""interactions"": """"
//                 }
//             ],
//             ""bindings"": [
//                 {
//                     ""name"": ""WASD"",
//                     ""id"": ""00ca640b-d935-4593-8157-c05846ea39b3"",
//                     ""path"": ""Dpad"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Move"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""up"",
//                     ""id"": ""e2062cb9-1b15-46a2-838c-2f8d72a0bdd9"",
//                     ""path"": ""<Keyboard>/w"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": ""Keyboard&Mouse"",
//                     ""action"": ""Move"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""down"",
//                     ""id"": ""320bffee-a40b-4347-ac70-c210eb8bc73a"",
//                     ""path"": ""<Keyboard>/s"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": "";Keyboard&Mouse"",
//                     ""action"": ""Move"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""left"",
//                     ""id"": ""d2581a9b-1d11-4566-b27d-b92aff5fabbc"",
//                     ""path"": ""<Keyboard>/a"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": "";Keyboard&Mouse"",
//                     ""action"": ""Move"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""right"",
//                     ""id"": ""fcfe95b8-67b9-4526-84b5-5d0bc98d6400"",
//                     ""path"": ""<Keyboard>/d"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": "";Keyboard&Mouse"",
//                     ""action"": ""Move"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""6c806ce1-5502-45c2-a8fd-a8353b556fe1"",
//                     ""path"": ""<Gamepad>/leftStick"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": ""Gamepad"",
//                     ""action"": ""Move"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""7aa03ef0-8587-46a3-8418-4b0dac8d5358"",
//                     ""path"": ""<Keyboard>/#(1)"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": ""Keyboard&Mouse"",
//                     ""action"": ""Skill1"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""48cf7c1e-7a63-47eb-8ab3-9af8ec3515e1"",
//                     ""path"": ""<Keyboard>/#(2)"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": ""Keyboard&Mouse"",
//                     ""action"": ""Skill2"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 }
//             ]
//         }
//     ],
//     ""controlSchemes"": [
//         {
//             ""name"": ""Keyboard&Mouse"",
//             ""bindingGroup"": ""Keyboard&Mouse"",
//             ""devices"": [
//                 {
//                     ""devicePath"": ""<Keyboard>"",
//                     ""isOptional"": false,
//                     ""isOR"": false
//                 },
//                 {
//                     ""devicePath"": ""<Mouse>"",
//                     ""isOptional"": false,
//                     ""isOR"": false
//                 }
//             ]
//         },
//         {
//             ""name"": ""Gamepad"",
//             ""bindingGroup"": ""Gamepad"",
//             ""devices"": [
//                 {
//                     ""devicePath"": ""<Gamepad>"",
//                     ""isOptional"": false,
//                     ""isOR"": false
//                 }
//             ]
//         },
//         {
//             ""name"": ""Touch"",
//             ""bindingGroup"": ""Touch"",
//             ""devices"": [
//                 {
//                     ""devicePath"": ""<Touchscreen>"",
//                     ""isOptional"": false,
//                     ""isOR"": false
//                 }
//             ]
//         },
//         {
//             ""name"": ""Joystick"",
//             ""bindingGroup"": ""Joystick"",
//             ""devices"": [
//                 {
//                     ""devicePath"": ""<Joystick>"",
//                     ""isOptional"": false,
//                     ""isOR"": false
//                 }
//             ]
//         },
//         {
//             ""name"": ""XR"",
//             ""bindingGroup"": ""XR"",
//             ""devices"": [
//                 {
//                     ""devicePath"": ""<XRController>"",
//                     ""isOptional"": false,
//                     ""isOR"": false
//                 }
//             ]
//         }
//     ]
// }");
//         // Player
//         m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
//         m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
//         m_Player_Skill1 = m_Player.FindAction("Skill1", throwIfNotFound: true);
//         m_Player_Skill2 = m_Player.FindAction("Skill2", throwIfNotFound: true);
//     }
//
//     public void Dispose()
//     {
//         UnityEngine.Object.Destroy(asset);
//     }
//
//     public InputBinding? bindingMask
//     {
//         get => asset.bindingMask;
//         set => asset.bindingMask = value;
//     }
//
//     public ReadOnlyArray<InputDevice>? devices
//     {
//         get => asset.devices;
//         set => asset.devices = value;
//     }
//
//     public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;
//
//     public bool Contains(InputAction action)
//     {
//         return asset.Contains(action);
//     }
//
//     public IEnumerator<InputAction> GetEnumerator()
//     {
//         return asset.GetEnumerator();
//     }
//
//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         return GetEnumerator();
//     }
//
//     public void Enable()
//     {
//         asset.Enable();
//     }
//
//     public void Disable()
//     {
//         asset.Disable();
//     }
//
//     // Player
//     private readonly InputActionMap m_Player;
//     private IPlayerActions m_PlayerActionsCallbackInterface;
//     private readonly InputAction m_Player_Move;
//     private readonly InputAction m_Player_Skill1;
//     private readonly InputAction m_Player_Skill2;
//     public struct PlayerActions
//     {
//         private @PlayerInputAction m_Wrapper;
//         public PlayerActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
//         public InputAction @Move => m_Wrapper.m_Player_Move;
//         public InputAction @Skill1 => m_Wrapper.m_Player_Skill1;
//         public InputAction @Skill2 => m_Wrapper.m_Player_Skill2;
//         public InputActionMap Get() { return m_Wrapper.m_Player; }
//         public void Enable() { Get().Enable(); }
//         public void Disable() { Get().Disable(); }
//         public bool enabled => Get().enabled;
//         public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
//         public void SetCallbacks(IPlayerActions instance)
//         {
//             if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
//             {
//                 @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
//                 @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
//                 @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
//                 @Skill1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1;
//                 @Skill1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1;
//                 @Skill1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill1;
//                 @Skill2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill2;
//                 @Skill2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill2;
//                 @Skill2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill2;
//             }
//             m_Wrapper.m_PlayerActionsCallbackInterface = instance;
//             if (instance != null)
//             {
//                 @Move.started += instance.OnMove;
//                 @Move.performed += instance.OnMove;
//                 @Move.canceled += instance.OnMove;
//                 @Skill1.started += instance.OnSkill1;
//                 @Skill1.performed += instance.OnSkill1;
//                 @Skill1.canceled += instance.OnSkill1;
//                 @Skill2.started += instance.OnSkill2;
//                 @Skill2.performed += instance.OnSkill2;
//                 @Skill2.canceled += instance.OnSkill2;
//             }
//         }
//     }
//     public PlayerActions @Player => new PlayerActions(this);
//     private int m_KeyboardMouseSchemeIndex = -1;
//     public InputControlScheme KeyboardMouseScheme
//     {
//         get
//         {
//             if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
//             return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
//         }
//     }
//     private int m_GamepadSchemeIndex = -1;
//     public InputControlScheme GamepadScheme
//     {
//         get
//         {
//             if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
//             return asset.controlSchemes[m_GamepadSchemeIndex];
//         }
//     }
//     private int m_TouchSchemeIndex = -1;
//     public InputControlScheme TouchScheme
//     {
//         get
//         {
//             if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
//             return asset.controlSchemes[m_TouchSchemeIndex];
//         }
//     }
//     private int m_JoystickSchemeIndex = -1;
//     public InputControlScheme JoystickScheme
//     {
//         get
//         {
//             if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
//             return asset.controlSchemes[m_JoystickSchemeIndex];
//         }
//     }
//     private int m_XRSchemeIndex = -1;
//     public InputControlScheme XRScheme
//     {
//         get
//         {
//             if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
//             return asset.controlSchemes[m_XRSchemeIndex];
//         }
//     }
//     public interface IPlayerActions
//     {
//         void OnMove(InputAction.CallbackContext context);
//         void OnSkill1(InputAction.CallbackContext context);
//         void OnSkill2(InputAction.CallbackContext context);
//     }
}
