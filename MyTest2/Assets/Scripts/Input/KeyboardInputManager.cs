﻿using mytest2.Character;
using mytest2.Character.Abilities;
using System.Collections.Generic;
using UnityEngine;

namespace mytest2.UI.InputSystem
{
    public class KeyboardInputManager : BaseInputManager
    {
        //Abilities
        public System.Action<AbilityTypes> OnAbilityActivate;
        public System.Action<AbilityTypes> OnAbilitySelect;
        public System.Action<Vector2> OnAbilityMove;
        public System.Action<Vector2> OnAbilityEnd;
        //Dodge
        public System.Action<Vector2> OnDodgeStart;
        public System.Action<Vector2> OnDodgeDrag;
        public System.Action<Vector2> OnDodge;

        public AbilityKeyboardWrapper[] AbilityKeyboardWrappers;

        private Dictionary<AbilityTypes, AbilityKeyboardWrapper> m_KeyboardWrappers; //Словарь создан для более удобного доступа к иконкам способностей
       

        public AbilityKeyboardWrapper GetAbilityKeyboard(AbilityTypes type)
        {
            if (m_KeyboardWrappers.ContainsKey(type))
                return m_KeyboardWrappers[type];

            return null;
        }

        public override void UpdateInput()
        {
            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (OnMove != null)
				OnMove(dir.normalized);

            ProcessDodgeKey(KeyCode.Space);

            ProcessAbilityKey(KeyCode.Alpha1, AbilityTypes.Red);
            ProcessAbilityKey(KeyCode.Alpha2, AbilityTypes.Green);
            ProcessAbilityKey(KeyCode.Alpha3, AbilityTypes.Blue);
            ProcessAbilityKey(KeyCode.Alpha4, AbilityTypes.Yellow);
            ProcessAbilityKey(KeyCode.Alpha5, AbilityTypes.Violet);

			ProcessAbilityUse ();
        }

        protected override void Start()
        {
            base.Start();

            //Создать словать иконок способностей
            m_KeyboardWrappers = new Dictionary<AbilityTypes, AbilityKeyboardWrapper>();
            for (int i = 0; i < AbilityKeyboardWrappers.Length; i++)
            {
                if (!m_KeyboardWrappers.ContainsKey(AbilityKeyboardWrappers[i].AbilityType))
                    m_KeyboardWrappers.Add(AbilityKeyboardWrappers[i].AbilityType, AbilityKeyboardWrappers[i]);
            }
        }


        void ProcessDodgeKey(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                Vector2 dodgeDir = GetMouseScreenDir();
                if (OnDodgeStart != null)
                    OnDodgeStart(dodgeDir.normalized);
            }
            else if (Input.GetKey(key))
            {
                Vector2 dodgeDir = GetMouseScreenDir();
                if (OnDodgeDrag != null)
                    OnDodgeDrag(dodgeDir.normalized);
            }
            else if (Input.GetKeyUp(key))
            {
                Vector2 dodgeDir = GetMouseScreenDir();
                if (OnDodge != null)
                    OnDodge(dodgeDir.normalized);
            }
        }

        void ProcessAbilityKey(KeyCode key, AbilityTypes type)
        {
			if (Input.GetKeyUp (key)) 
			{
				if (OnAbilitySelect != null)
					OnAbilitySelect(type);
			}
        }

		void ProcessAbilityUse()
		{
			if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			{
				if (OnAbilityActivate != null)
					OnAbilityActivate(PlayerController.SelectedAbility);
			}

			if(Input.GetMouseButton(0))
			{
				Vector2 abilityDir = GetMouseScreenDir();
				if (OnAbilityMove != null)
					OnAbilityMove(abilityDir.normalized);
			}

			if (Input.GetMouseButtonUp(0))
			{
				Vector2 abilityDir = GetMouseScreenDir();
				if (OnAbilityEnd != null)
					OnAbilityEnd(abilityDir.normalized);
			}
		}

        Vector2 GetMouseScreenDir()
        {
            return Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, Input.mousePosition.z);
        }
    }
}
