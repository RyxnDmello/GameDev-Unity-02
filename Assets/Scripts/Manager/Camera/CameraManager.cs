using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("CAMERA SHAKE")]
    [SerializeField] private AnimationCurve[] Curve;

    [Header("REQUIREMENTS")]
    private Vector3 StartPosition;
    private float ElapsedTime;
    private float Strength;

    [Header("SHAKEABLE")]
    private bool IsShakeable;

    private void Start()
    {
        IsShakeable = true;
    }

    public void CameraShakes(int ShakeType, float Duration)
    {
        if (IsShakeable)
        {
            IsShakeable = false;

            if (ShakeType == 0 && Duration == 0) return;

            if (Duration > 1.5f) Duration = 1.5f;
            else if (Duration <= 0.5f) Duration = 0.65f;

            if (ShakeType < 1) ShakeType = 1;
            else if (ShakeType > 4) ShakeType = 4;

            StartCoroutine(Shake(ShakeType - 1, Duration));
        }
    }

    private IEnumerator Shake(int ShakeType, float Duration)
    {
        StartPosition = transform.position;
        ElapsedTime = 0.0f;

        while (ElapsedTime <= Duration)
        {
            ElapsedTime += Time.deltaTime;
            Strength = Curve[ShakeType].Evaluate(ElapsedTime / Duration);
            transform.position = StartPosition + Random.insideUnitSphere * Strength;
            yield return null;
        }

        transform.position = StartPosition;
        IsShakeable = true;
    }



    #region VARIABLES

    [Header("ANIMATIONS")]
    Animator Anim;

    #endregion

    #region EFFECTS

    public void CameraShake(int Value)
    {
        if (Value == 1)
        {
            Anim.SetTrigger("CameraShakeOne");
        }
        else if (Value == 2)
        {
            Anim.SetTrigger("CameraShakeTwo");
        }
        else if (Value == 3)
        {
            Anim.SetTrigger("CameraShakeThree");
        }
    }

    #endregion
}
