using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.StickyNote;

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
    public BaseLaserInstrument hitObject;
    
}

public class LaserControl : MonoBehaviour
{
    public static Dictionary<LaserColor, Color> laserColors = new Dictionary<LaserColor, Color>
    {
        {LaserColor.White, new Color(1,1,1) },
        {LaserColor.Red, new Color(1,0,0) },
        {LaserColor.Green, new Color(0,1,0) },
        {LaserColor.Blue, new Color(0,0,1) },
        {LaserColor.Yellow, new Color(1,1,0) },
        {LaserColor.Cyan, new Color(0,1,1) },
        {LaserColor.Violet, new Color(1,0,1) }
    };

    //激光发射方向
    private Vector2 laserDirection;

    //激光获取的信息
    public List<HitInfo> hitInfoes = new List<HitInfo>();

    private int hitCount;
    public int HitCount
    {
        get { return hitCount; }
        set { hitCount = value; }
    }

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
    [SerializeField] private float thickness = 5;
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

    private bool isStop = false;
    public bool IsStop
    {
        get { return isStop; }
        set { isStop = value; }
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

    public void SetColor(LaserColor color)
    {
        lineRenderer.material.color = laserColors[color] * colorIntensity;

        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
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

        // ------------->||

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction.normalized);

        float length = maxLength;
        float laserEndRotation = 180;
        Vector2 endPosition = startPosition + length * direction;

        if (hit)//通过反射检测获取两点之间的距离
        {
            RoadDFS(direction, hit);
            hitCount++;
            
            LaserManager.Instance.ChangeHitState(this, true);

            //激光进行深搜
            hit.transform.GetComponent<BaseLaserInstrument>().OnLaserHit(this);
            laserEndRotation = Vector2.Angle(direction, hit.normal);//获取发射方向与法线的角度

            //获取撞击点后进行路线设置
            if(!isStop)
            {
                lineRenderer.positionCount = hitCount + 2;
                for (int i = 1; i <= hitCount; i++)
                {
                    lineRenderer.SetPosition(i, hitInfoes[i - 1].hitPoint);
                }
                endPosition = hitInfoes[hitCount - 1].hitPoint + hitInfoes[hitCount].launchDirection * length;

                lineRenderer.SetPosition(hitCount + 1, endPosition);
            }
            else
            {
                lineRenderer.positionCount = hitCount + 1;
                for (int i = 1; i <= hitCount; i++)
                {
                    lineRenderer.SetPosition(i, hitInfoes[i - 1].hitPoint);
                }
                endPosition = hitInfoes[hitCount - 1].hitPoint;
            }

        }
        else
        {
            lineRenderer.positionCount = 2;
            LaserManager.Instance.ChangeHitState(this, false);
            lineRenderer.SetPosition(1, startPosition + length * direction);
        }

        hitCount = 0;
        LaserManager.Instance.ChangeObjectState(this);
        startVFX.transform.position = startPosition;
        endVFX.transform.position = endPosition;
        endVFX.transform.rotation = Quaternion.Euler(0, 0, laserEndRotation);
    }

    public void RoadDFS(Vector2 direction, RaycastHit2D hit)
    {
        BaseLaserInstrument laserInstrument = null;
        if (hit.transform != null)
        {
            laserInstrument = hit.transform.GetComponent<BaseLaserInstrument>();
            laserInstrument.hitLaser = this;
        }

        if (hitInfoes.Count <= hitCount)
            hitInfoes.Add(new HitInfo()
            {
                hitPoint = hit.point - 0.1f * direction,
                hitSurfaceNormal = hit.normal,
                launchDirection = direction,
                hitObject = laserInstrument
            });
        else
        {
            hitInfoes[hitCount].hitPoint = hit.point - 0.1f * direction;
            hitInfoes[hitCount].hitSurfaceNormal = hit.normal;
            hitInfoes[hitCount].launchDirection = direction;
            hitInfoes[hitCount].hitObject = laserInstrument;
        }
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
