using UnityEngine;

public class JoystickForMovement : JoystickHandler
{
    public Vector2 ReturnVectorDirection()
    { 
        if (_inputVector.x != 0)
        {
            return new Vector2(_inputVector.x, _inputVector.y);
        }
        else
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}
