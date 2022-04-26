using UnityEngine;

public class BlinkStatus : MonoBehaviour
{
    public bool validOneEyedBlink;
    public bool validTwoEyedBlink;
    public bool oneEyedBlink;
    public bool twoEyedBlink;
    public bool left;
    public bool right;

    public BlinkStatus(bool validOneEyedBlink = false, bool validTwoEyedBlink = false, bool oneEyedBlink = false, bool twoEyedBlink = false, bool left = false, bool right = false)
    {
        this.validOneEyedBlink = validOneEyedBlink;
        this.oneEyedBlink = oneEyedBlink;
        this.twoEyedBlink = twoEyedBlink;
        this.left = left;
        this.right = right;
    }
}
