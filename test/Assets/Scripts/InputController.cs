using UnityEngine;
using System.Collections;
using System;

// TO-DO:
// - support for GetKeyDown when using VERSION B
// - support GetKeyDown for Joystick Move, Aim and DPad
// - support for adaptative input: right now there is independence between keyboard and joystick but 
//   there is no support for keyboard AND joystick controlling different players if they are assigned
//   to the same player.
// - support CustomKey definition: right now you can just declare them over the existing Keys
//   


public static class InputController {
	public const int MAX_PLAYERS = 4;
	// if true then vibration of a player just happens if it's using joystick
	public static bool intelligentVibration = false;
	
	// TO-DO Add here your devices
	public enum InputDevice {Keyboard, Joystick, GreenThrottle, Move, Custom};
	
	public enum Key { A, B, X, Y, LB, RB, Back, Start, L3, R3, LT, RT, 
		Left, Right, Up, Down, AimLeft, AimRight, AimUp, AimDown, DPadLeft, DpadRight, DPadUp, DPadDown};
	
	// TO-DO Customize with your own maping 
	private static KeyCode[][] ToKeyboardKeyCode = new KeyCode[24][] {
		/* A */		new KeyCode[]{ KeyCode.B, KeyCode.Space, KeyCode.Space, KeyCode.Space, KeyCode.Space}, 
		/* B */		new KeyCode[]{}, 
		/* X */		new KeyCode[]{ }, 
		/* Y */		new KeyCode[]{KeyCode.B, KeyCode.B, KeyCode.B, KeyCode.B, KeyCode.B}, 
		/* LB */	new KeyCode[]{}, 
		/* RB */	new KeyCode[]{},
		/* Back */	new KeyCode[]{}, 
		/* Start */	new KeyCode[]{}, 
		/* L3 */	new KeyCode[]{}, 
		/* R3 */	new KeyCode[]{}, 
		/* LT */	new KeyCode[]{}, 
		/* RT */	new KeyCode[]{},
		/* Left */	new KeyCode[]{	KeyCode.JoystickButton8,KeyCode.A,KeyCode.LeftArrow,KeyCode.Joystick3Button8,KeyCode.Joystick4Button8},
		/* Right */	new KeyCode[]{	KeyCode.JoystickButton10,KeyCode.D,KeyCode.RightArrow,KeyCode.Joystick3Button10,KeyCode.Joystick4Button10},
		/* Up */	new KeyCode[]{	KeyCode.JoystickButton9,KeyCode.W,KeyCode.UpArrow,KeyCode.Joystick3Button9,KeyCode.Joystick4Button9},
		/* Down */	new KeyCode[]{	KeyCode.JoystickButton11,KeyCode.S,KeyCode.DownArrow,KeyCode.Joystick3Button11,KeyCode.Joystick4Button11},
		/* AimLeft */new KeyCode[]{	KeyCode.JoystickButton8,KeyCode.J,KeyCode.Keypad4,KeyCode.Joystick3Button8,KeyCode.Joystick4Button8},
		/* AimRight */new KeyCode[]{KeyCode.JoystickButton10,KeyCode.L,KeyCode.Keypad6,KeyCode.Joystick3Button10,KeyCode.Joystick4Button10},
		/* AimUp*/	new KeyCode[]{	KeyCode.JoystickButton9,KeyCode.I,KeyCode.Keypad8,KeyCode.Joystick3Button9,KeyCode.Joystick4Button9},
		/* AimDown*/new KeyCode[]{	KeyCode.JoystickButton11,KeyCode.K,KeyCode.Keypad2,KeyCode.Joystick3Button11,KeyCode.Joystick4Button11},
		/* DPadLeft */new KeyCode[]{KeyCode.JoystickButton8,KeyCode.A,KeyCode.LeftArrow,KeyCode.Joystick3Button8,KeyCode.Joystick4Button8},
		/* DPadRight */new KeyCode[]{KeyCode.JoystickButton10,KeyCode.D,KeyCode.RightArrow,KeyCode.Joystick3Button10,KeyCode.Joystick4Button10},
		/* DPadUp */new KeyCode[]{	KeyCode.JoystickButton9,KeyCode.W,KeyCode.UpArrow,KeyCode.Joystick3Button9,KeyCode.Joystick4Button9},
		/* DPadDown */new KeyCode[]{KeyCode.JoystickButton11,KeyCode.S,KeyCode.DownArrow,KeyCode.Joystick3Button11,KeyCode.Joystick4Button11},
	};
	
	// TO-DO add your custom2D 
	private static Vector2 CustomProcess2DKey(int player, int key2DType, out InputDevice device) {
		Vector2 res = Vector2.zero;
		device = InputDevice.Custom;

		return res;
	}
	
	private static float CustomGetTrigger(int player, out InputDevice device) {
		float res = 0.0f;
		device = InputDevice.Custom;
		
		return res;
	}
	
	
	
	/*private static int[] _moveKeyMap = new int[] {
		PSMoveButton.Cross, PSMoveButton.Circle, PSMoveButton.Square, PSMoveButton.Triangle, 
	};
		
		
	}*/
	
	// TO-DO implement your weird extra mapings here
	private static bool CustomGetKey(Key key, int player, GetKeyFunction getKeyFunction, out InputDevice device) {
		bool result = false;
		device = InputDevice.Custom;
		
		
		return result;
	}
	
	public static Vector2 GetMovement(int player = 1) {
		return Process2DKey(player, Key.Left, Input.GetKey);
	}
	
	public static Vector2 GetMovementDown(int player = 1) {
		return Process2DKey(player, Key.Left, Input.GetKeyDown);	
	}
	
	public static Vector2 GetAim(int player) {
		return Process2DKey(player, Key.AimLeft, Input.GetKey);
	}
	
	public static Vector2 GetAimDown(int player) {
		return Process2DKey(player, Key.AimLeft, Input.GetKeyDown);
	}
	
	public static Vector2 GetDPad(int player) {
		return Process2DKey(player, Key.DPadLeft, Input.GetKey);
	}
	
	public static Vector2 GetDPadDown(int player) {
		return Process2DKey(player, Key.AimLeft, Input.GetKeyDown);
	}
	
	public static Vector2 GetTrigger(int player) {
		return Process2DKey(player, Key.LT, Input.GetKey);
	}
	
	public static Vector2 GetTriggerDown(int player) {
		return Process2DKey(player, Key.LT, Input.GetKeyDown);
	}
	
	public static bool GetKeyDown(Key key, int player = 0) {
		return EvaluateGetKey(key, player, Input.GetKeyDown);	
	}
	
	public static bool GetKey(Key key, int player = 0) {
		return EvaluateGetKey(key, player, Input.GetKey);	
	}
	
	// Rumbling 
	public static void Vibrate(float big = 0.5f, float small = 0.5f, float time = 0.0f, int playerIndex = 1) {
		// right now just support in windows
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		if ((intelligentVibration && _deviceUsed[playerIndex] == InputDevice.Joystick) 
															|| !intelligentVibration) {
			Rumble.instance.Vibrate(big, small, time, playerIndex);
		}
		#endif
	}
	
	public static InputDevice GetUsedInputDevice(int player = 0) {
		return _deviceUsed[player];	
	}
	
	private static bool EvaluateGetKey(Key key, int player, GetKeyFunction getKeyFunction) {
		bool result = false;
		int size = MAX_PLAYERS;
		int initialPlayer = 1;
		if (player != 0) { 
			size = 1; 
			initialPlayer = player;
		}
		// VERSION A: user defines in this cs the keycodes
		
		// TO-DO (?) make that you can set several keycodes associated to each key? -> ToKeyboardKeyCode
		// and also joining keyboard and joystick map in one using the fact to use several keys
		
		
		for (int i = 0; i < size; ++i) {
			// Keyboard
			result = result || getKeyFunction(ToKeyboardKeyCode[(int)key][initialPlayer+i]);
			if (result) {_deviceUsed[initialPlayer+i] = InputDevice.Keyboard; _deviceUsed[0] = InputDevice.Keyboard;return result;}
			
			// Custom calls
			InputDevice device;
			result = result || CustomGetKey(key, initialPlayer+i, getKeyFunction, out device);
			if (result) {_deviceUsed[player] = device;_deviceUsed[0] = device;return result;}
			
		}
		
		// Joystick
		if (Input.GetJoystickNames().Length == 0) return result;
		
		int axesDirections = 4;
		int index0key = (int)key -(int)Key.Left;
		int keyNormalized = index0key%axesDirections;
		int isNegativeDirection = ( keyNormalized == 0 || keyNormalized == 3? -1 : 1);
		string axis = (index0key/2)+" axis_P";
		
		if (key >= Key.Left && key <= Key.DPadDown) { // axis kind key
			result = result || isNegativeDirection*Input.GetAxis(axis+player) > 0;
		} else result = result || getKeyFunction(ToJoystickKeyCode[(int)key, player]);
		if (result) {_deviceUsed[player] = InputDevice.Joystick;_deviceUsed[0] = InputDevice.Joystick;return result;}
		
		
		/*
		// VERSION B: user defines in Edit > Project Settings > Input the keycodes
		for (int i = 0; i < size; ++i) {
			result = result || Input.GetAxis (key+"_P"+(initialPlayer+i))> 0.0f;
			if (result) {_deviceUsed[player] = InputDevice.Joystick;return result;}
			
			// Custom calls
			InputDevice device;
			result = result || CustomGetKey(key, initialPlayer+i, getKeyFunction, out device);
			if (result) {_deviceUsed[player] = device;return result;}
		}*/
		
		
		return result;
	}
	
	private static Vector2 Process2DKey(int player, Key key, GetKeyFunction getKeyFunction) {
		// Keyboard
		Vector2 res = KeyboardToVector2(key, player, getKeyFunction);
		if (UpdateDeviceUsed(player, res.magnitude != 0, InputDevice.Keyboard)) return res;
			
		// Joystick
		int index0key = (int)key - (int)Key.Left;
		res = new Vector2(Input.GetAxis(index0key + " axis_P" + player), Input.GetAxis((index0key+1) + " axis_P"+player));
		if (UpdateDeviceUsed(player, res.magnitude != 0, InputDevice.Keyboard)) return res;
		
		// Custom
		InputDevice device;
		res = CustomProcess2DKey(player, index0key/2, out device);
		if (UpdateDeviceUsed(player, res.magnitude != 0, device)) return res;
		
		return res;
	}
	
	private static bool UpdateDeviceUsed(int player, bool update, InputDevice device) {
		if (update){
			_deviceUsed[player] = device;
			_deviceUsed[0] = device;
		}
		return update;
	}
	
	private delegate bool GetKeyFunction(KeyCode key);
	private delegate Vector2 Get2DKey(int player);
		
	private static bool CheckPlayer(int player) {
		if (player > MAX_PLAYERS || player < 0) throw new Exception("InputController :: Invalid identifier of player");	
		return true;
	}
	
	private static InputDevice[] _deviceUsed = new InputDevice[MAX_PLAYERS+1];
	
	private static Vector2 KeyboardToVector2(Key initialKey, int player, GetKeyFunction getKeyFunction) {
		if (ToKeyboardKeyCode.Length < (int)initialKey || ToKeyboardKeyCode[(int)initialKey].Length < player) return Vector2.zero;
		return new Vector2(
				(getKeyFunction (ToKeyboardKeyCode[(int)initialKey][player])? -1 : 0) +
				(getKeyFunction (ToKeyboardKeyCode[(int)initialKey+1][player])? 1 : 0),
				(getKeyFunction (ToKeyboardKeyCode[(int)initialKey+2][player])? 1 : 0) +
				(getKeyFunction (ToKeyboardKeyCode[(int)initialKey+3][player])? -1 : 0)
		);
	}

	
	// TO-DO (?) use the order of the enum to avoid this multiarray? Problems if they change it
	private static KeyCode[,] ToJoystickKeyCode = new KeyCode[,] {
		{KeyCode.JoystickButton0,KeyCode.Joystick1Button0,KeyCode.Joystick2Button0,KeyCode.Joystick3Button0,KeyCode.Joystick4Button0},
		{KeyCode.JoystickButton1,KeyCode.Joystick1Button1,KeyCode.Joystick2Button1,KeyCode.Joystick3Button1,KeyCode.Joystick4Button1},
		{KeyCode.JoystickButton2,KeyCode.Joystick1Button2,KeyCode.Joystick2Button2,KeyCode.Joystick3Button2,KeyCode.Joystick4Button2},
		{KeyCode.JoystickButton3,KeyCode.Joystick1Button3,KeyCode.Joystick2Button3,KeyCode.Joystick3Button3,KeyCode.Joystick4Button3},
		{KeyCode.JoystickButton4,KeyCode.Joystick1Button4,KeyCode.Joystick2Button4,KeyCode.Joystick3Button4,KeyCode.Joystick4Button4},
		{KeyCode.JoystickButton5,KeyCode.Joystick1Button5,KeyCode.Joystick2Button5,KeyCode.Joystick3Button5,KeyCode.Joystick4Button5},
		{KeyCode.JoystickButton6,KeyCode.Joystick1Button6,KeyCode.Joystick2Button6,KeyCode.Joystick3Button6,KeyCode.Joystick4Button6},
		{KeyCode.JoystickButton7,KeyCode.Joystick1Button7,KeyCode.Joystick2Button7,KeyCode.Joystick3Button7,KeyCode.Joystick4Button7},
		{KeyCode.JoystickButton8,KeyCode.Joystick1Button8,KeyCode.Joystick2Button8,KeyCode.Joystick3Button8,KeyCode.Joystick4Button8},
		{KeyCode.JoystickButton9,KeyCode.Joystick1Button9,KeyCode.Joystick2Button9,KeyCode.Joystick3Button9,KeyCode.Joystick4Button9},
		{KeyCode.JoystickButton10,KeyCode.Joystick1Button10,KeyCode.Joystick2Button10,KeyCode.Joystick3Button10,KeyCode.Joystick4Button10},
		{KeyCode.JoystickButton11,KeyCode.Joystick1Button11,KeyCode.Joystick2Button11,KeyCode.Joystick3Button11,KeyCode.Joystick4Button11}
	};
}