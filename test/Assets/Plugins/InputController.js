#pragma strict

enum SBFKeys { Left, Right, Up, Down, Stats, Potions, Select}


static var rumble : Rumble;


static function GetMovement(player : int) : Vector2 {
	var res : Vector2;
	if (player == 1) {
		res = Vector2((Input.GetKey(KeyCode.A)? -1 : 0)+(Input.GetKey(KeyCode.D)? 1 : 0), 
			(Input.GetKey(KeyCode.W)? 1 : 0)+(Input.GetKey(KeyCode.S)? -1 : 0));
	} else {
		res = Vector2((Input.GetKey(KeyCode.LeftArrow)? -1 : 0)+(Input.GetKey(KeyCode.RightArrow)? 1 : 0), 
			(Input.GetKey(KeyCode.UpArrow)? 1 : 0)+(Input.GetKey(KeyCode.DownArrow)? -1 : 0));
	}
	if (res.magnitude != 0) return res;
	return Vector2(Input.GetAxis("HorizontalMovementP"+player), -Input.GetAxis("VerticalMovementP"+player));
}

static function GetTemplate(player : int) : Vector2 {
	var res : Vector2;
	if (player == 1) {
		res = Vector2((Input.GetKey(KeyCode.U)? -1 : 0)+(Input.GetKey(KeyCode.J)? 1 : 0),
		(Input.GetKey(KeyCode.H)? -1 : 0)+(Input.GetKey(KeyCode.K)? 1 : 0));
	} else {
		res = Vector2((Input.GetKey(KeyCode.Keypad8)? -1 : 0)+(Input.GetKey(KeyCode.Keypad5)? 1 : 0),
		(Input.GetKey(KeyCode.Keypad4)? -1 : 0)+(Input.GetKey(KeyCode.Keypad6)? 1 : 0));
	}	
	if (res.magnitude != 0) return res;

	return Vector2(Input.GetAxis("VerticalTemplateP"+player), Input.GetAxis("HorizontalTemplateP"+player));
}


static function GetAttack(player : int) : boolean {
	return  
		(player == 1 && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))? true : false) ||
		(player == 2 && (Input.GetKey(KeyCode.Keypad0)|| Input.GetKey(KeyCode.Joystick2Button0))? true : false);
}

static function GetButtonA(player : int) : boolean {
	return  
		(player == 1 && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))? true : false) ||
		(player == 2 && (Input.GetKey(KeyCode.Keypad5)|| Input.GetKey(KeyCode.Joystick2Button0))? true : false);
}

static function GetButtonX(player : int) : boolean {
	return  
		(player == 1 && (Input.GetKey(KeyCode.V) || Input.GetKey(KeyCode.Joystick1Button2))? true : false) ||
		(player == 2 && (Input.GetKey(KeyCode.Keypad4)|| Input.GetKey(KeyCode.Joystick2Button2))? true : false);
}

static function GetButtonB(player : int) : boolean {
	return  
		(player == 1 && (Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.Joystick1Button1))? true : false) ||
		(player == 2 && (Input.GetKey(KeyCode.Keypad6)|| Input.GetKey(KeyCode.Joystick2Button1))? true : false);
}


static function GetAttackDown(player : int) : boolean {
	return  
		(player == 1 && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))? true : false) ||
		(player == 2 && (Input.GetKeyDown(KeyCode.Keypad0)|| Input.GetKeyDown(KeyCode.Joystick2Button0))? true : false);
}



static function GetKeyDown(key : SBFKeys, player : int) {
	var result : boolean = false;
	switch (key) {
		case SBFKeys.Left:
			if (player == 1) {
				result =  result || Input.GetKeyDown(KeyCode.A);
			} else if (player == 2) {
				result =  result || Input.GetKeyDown(KeyCode.LeftArrow);
			}
			break;
		case SBFKeys.Right:
			if (player == 1) {
				result =  result || Input.GetKeyDown(KeyCode.D);
			}else if (player == 2) {
				result =  result || Input.GetKeyDown(KeyCode.RightArrow);
			}
			break;
		case SBFKeys.Up:
			if (player == 1) {
				result = result ||  Input.GetKeyDown(KeyCode.W);
			}else if (player == 2) {
				result =  result || Input.GetKeyDown(KeyCode.UpArrow);
			}
			break;
		case SBFKeys.Down:
			if (player == 1) {
				result = result || Input.GetKeyDown(KeyCode.S);
			}else if (player == 2) {
				result =  result || Input.GetKeyDown(KeyCode.DownArrow);
			}
			break;
		case SBFKeys.Select:
			if (player == 1) {
				result = result || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Joystick1Button6);
			}else if (player == 2) {
				result =  result || Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Joystick2Button6);
			}
			break;
	}
	
	return result;
}


static function GetKeyDown(key : SBFKeys) {
	return GetKeyDown(key, 1);
}


static function GetKey(key : SBFKeys, player : int) {
	var ret : boolean = false;
	
	var movArray : Vector2[];
	if (player == 0) {
		movArray = new Vector2[2];
		movArray[0] = GetMovement(1);
		movArray[1] = GetMovement(2);
	} else {
		movArray = new Vector2[1];
		movArray[0] = GetMovement(player);
	}
	
	switch (key) {
		case SBFKeys.Left:
			for (var mov : Vector2 in movArray)
				ret = ret || mov.y < -0.1; 
			break;
		case SBFKeys.Right:
			for (var mov : Vector2 in movArray)
				ret = ret || mov.y > 0.1; 
			break;
		case SBFKeys.Up:
			for (var mov : Vector2 in movArray)
				ret = ret || mov.x < -0.1; 
			break;
		case SBFKeys.Down:
			for (var mov : Vector2 in movArray)
				ret = ret || mov.x > 0.1; 
			break;
		case SBFKeys.Stats:
			if (player == 1) ret = ret || Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Z);
			else if (player == 2) ret = ret || Input.GetKey(KeyCode.Joystick2Button4) || Input.GetKey(KeyCode.Keypad1);
			break;
		case SBFKeys.Potions:
			if (player == 1) ret = ret || Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.X);
			else if (player == 2) ret = ret || Input.GetKey(KeyCode.Joystick2Button5)|| Input.GetKey(KeyCode.Keypad2);
			break;
	
	}
	
	return ret;
}

static function GetKey(key : SBFKeys) {
	return GetKey(key, 1);
}




static function PadVibration ( playerIndex : int, big : float, small : float ) {

    GamePad.SetVibration ( playerIndex, big, small );

}



static function Vibrate(playerIndex : int, val : float, time : float) {
	if (!rumble) {
		var go : GameObject= new GameObject();
		go.name = "RumbleManager";
		go.AddComponent(Rumble);
		rumble = go.GetComponent(Rumble);	
	}
	rumble.Vibrate(playerIndex, val, time);
}



static function StopPadVibration ( playerIndex : int ) {
	
    GamePad.SetVibration( playerIndex, 0, 0 );

}