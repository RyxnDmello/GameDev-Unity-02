using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggly : MonoBehaviour
{
    #region VARIABLES

    [Header("WIGGLE COMPONENTS")]
    public LineRenderer LineRend;
    public Transform TargetDir;
    public Transform WiggleDir;
    public Vector3[] SegementPos;
    public int Length;
    private Vector3[] SegementV;

    [Header("WIGGLY NATURE")]
    public float WiggleSpeed;
    public float WiggleMagnitude;

    [Header("TRAIL NATURE")]
    public Gradient TrailGradient;
    public Gradient BlackGradient;
    public float TargetDis;
    public float SmoothSpeed;
    public float TrailSpeed;
    public float StartTime;
    private bool Final;

    #endregion

    #region UNITY

    public void Start()
    {
        LineRend.positionCount = Length;
        LineRend.colorGradient = BlackGradient;
        Final = false;

        SegementPos = new Vector3[Length];
        SegementV = new Vector3[Length];
    }

    public void Update()
    {
        Wiggle();
    }

    #endregion

    #region BEHAVIOURS

    private void Wiggle()
    {
        if (StartTime <= 0)
        {
            LineRend.colorGradient = TrailGradient;
            Final = true;
        }
        else if(Final == false)
        {
            StartTime -= Time.deltaTime;
            LineRend.colorGradient = BlackGradient;
        }

        WiggleDir.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * WiggleSpeed) * WiggleMagnitude);

        SegementPos[0] = TargetDir.position;

        for (int i = 1; i < SegementPos.Length; i++)
        {
            SegementPos[i] = Vector3.SmoothDamp(SegementPos[i], SegementPos[i - 1] + TargetDir.up * TargetDis, ref SegementV[i], SmoothSpeed + i / TrailSpeed);
        }

        LineRend.SetPositions(SegementPos);
    }

    #endregion
}
