using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationControllerScript : MonoBehaviour
{
    private Animation animationclip;

    public void Start()
    {
        animationclip = GetComponent<Animation>();

    }

    public void InputUpdated(int i)
    {
        transform.position += new Vector3(0, 0, .2f);
    }
}
