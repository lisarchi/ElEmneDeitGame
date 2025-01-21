using UnityEngine;

public class Flip : MonoBehaviour
{
    internal void FlipDirection(string direction = "right")
    {

        if (direction == "right")
        {
            transform.localScale = new Vector2(1, 1);
        }

        if (direction == "left")
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
