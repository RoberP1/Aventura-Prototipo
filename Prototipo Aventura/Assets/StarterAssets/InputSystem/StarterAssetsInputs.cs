using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool interaction;
		public bool OpenInv;
		public bool CloseInv;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;


#endif
        private void Update()
        {
			if (OpenInv) Time.timeScale = 0;
			if (Input.GetKeyDown(KeyCode.Escape))
            {
				Time.timeScale = 1;
				OpenInv = false;
				Cursor.lockState = CursorLockMode.Locked;
				cursorLocked = true;
			}
			
		}
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
		public void OnInteraction(InputValue value)
		{
			InteractionInput(value.isPressed);
		}
		public void OnOpenInv(InputValue value)
		{
			OpenInvInput(!OpenInv);
			
		}
		public void OnCloseInv(InputValue value)
		{
			CloseInvInput(value.isPressed);
		}
		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);	
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void InteractionInput(bool newInteractionState)
		{
			interaction = newInteractionState;
		}
		public void OpenInvInput(bool newInteractionState)
		{
			OpenInv = newInteractionState;
			

		}
		public void CloseInvInput(bool newInteractionState)
		{
			CloseInv = newInteractionState;
		}

		public void SprintInput(bool newSprintState)
		{  
            sprint = newSprintState;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}