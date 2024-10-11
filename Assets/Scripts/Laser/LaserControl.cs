using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LaserColor
{
    White,
    Red,
    Green,
    Blue,
    Yellow,
    Cyan,
    Violet
}


public class HitInfo
{
    public Vector2 launchDirection;
    public Vector2 hitSurfaceNormal;
    public Vector2 hitPoint;
}

public class LaserControl : MonoBehaviour
{
    private static Dictionary<LaserColor, Color> laserColors = new Dictionary<LaserColor, Color>
    {
        {LaserColor.White, new Color(1,1,1) },
        {LaserColor.Red, new Color(1,0,0) },
        {LaserColor.Green, new Color(0,1,0) },
        {LaserColor.Blue, new Color(0,0,1) },
        {LaserColor.Yellow, new Color(1,1,0) },
        {LaserColor.Cyan, new Color(0,1,1) },
        {LaserColor.Violet, new Color(1,0,1) }
    };

    //当前发射的方向
    public HitInfo hitInfo = new HitInfo();

    [SerializeField] private LaserColor color;
    public LaserColor Color
    {
        get { return color; }
        set 
        { 
            if(value.GetType() == typeof(LaserColor))
                color = value; 
        }
    }
    [SerializeField] private float colorIntensity = 1.5f;
    private float beamColorEnhance = 1f;

    [SerializeField] private float maxLength = 100;
    [SerializeField] private float thickness = 8;
    [SerializeField] private float noiseScale = 3;
    [SerializeField] private GameObject startVFX;
    [SerializeField] private GameObject endVFX;


    private LineRenderer lineRenderer;
    public LineRenderer LineRenderer
    {
        get
        {
            return lineRenderer;
        }

        set
        {
            lineRenderer = value;
        }
    }

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();

        lineRenderer.material.color = laserColors[color] * colorIntensity;
        lineRenderer.material.SetFloat("_LaserThickness", thickness);
        lineRenderer.material.SetFloat("_LaserScale", noiseScale);

        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem particle in particles)
        {
            Renderer r = particle.GetComponent<Renderer>();
            r.material.SetColor("_EmissionColor", laserColors[color] * (colorIntensity + beamColorEnhance));
        }
    }

    private void Start()
    {
        UpdateEndPosition();
        UpdateLaserPosition();
    }

    private void Update()
    {
        UpdateEndPosition();
        UpdateLaserPosition();
    }

    public void UpdatePosition(Vector2 startPosition, Vector2 direction)
    {
        direction = direction.normalized;
        transform.position = startPosition;
        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0,0,rotationZ * Mathf.Rad2Deg);
    }

    public void UpdateLaserPosition()
    {
        lineRenderer.SetPosition(0,transform.position);
    }

    public void UpdateEndPosition()
    {
        Vector2 startPosition = transform.position;
        float rotationZ = transform.rotation.eulerAngles.z;
        rotationZ *= Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(rotationZ), Mathf.Sin(rotationZ));

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction.normalized);

        float length = maxLength;
        float laserEndRotation = 180;

        if (hit)//通过反射检测获取两点之间的距离
        {
            hitInfo.hitPoint = hit.point;
            hitInfo.hitSurfaceNormal = hit.normal;
            hitInfo.launchDirection = direction;

            BaseLaserInstrument laserInstrument = hit.transform.GetComponent<BaseLaserInstrument>();
            laserInstrument.hitLaser = this;
            laserInstrument.OnLaserHit();
            LaserManager.Instance.ChangeHitState(this, true);
            LaserManager.Instance.ChangeObjectState(this, laserInstrument);
            length = (hit.point - startPosition).magnitude;
            laserEndRotation = Vector2.Angle(direction, hit.normal);//获取发射方向与法线的角度

            lineRenderer.SetPosition(1, hit.point);//设置激光落点
        }
        else
        {
            LaserManager.Instance.ChangeHitState(this, false);
            LaserManager.Instance.ChangeObjectState(this, null);
            lineRenderer.SetPosition(1, startPosition + length * direction);
        }

        Vector2 endPosition = startPosition + length * direction;
        startVFX.transform.position = startPosition;
        endVFX.transform.position = endPosition;
        endVFX.transform.rotation = Quaternion.Euler(0, 0, laserEndRotation);
    }

    public void HideVFX(int i)
    {
        if(i == 0)
        {
            startVFX.gameObject.SetActive(false);
        }
        else if(i == 1)
        {
            endVFX.gameObject.SetActive(false);
        }
    }

    public void PresentVFX(int i)
    {
        if (i == 0)
        {
            startVFX.gameObject.SetActive(true);
        }
        else if (i == 1)
        {
            endVFX.gameObject.SetActive(true);
        }
    }

}
