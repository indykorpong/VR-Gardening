using System.Collections;
using UnityEngine;
using Valve.VR;

public class PourDetector : MonoBehaviour
{
    public Vector2 pourThreshold = new Vector2(0f, 90f);
    public Transform origin = null;
    public GameObject streamPrefab = null;

    public GameObject waterColliderPrefab = null;
    public float waterSpawnCooldown = 1f;
    private float waterSpawnCurrentCooldown = 0f;

    private bool isPouring = false;
    private Stream currentStream = null;

    public SteamVR_Action_Boolean m_TriggerAction = null;
    private SteamVR_Behaviour_Pose m_pose = null;

    private void Start()
    {
        m_pose = GameObject.Find("RightHand").GetComponent<SteamVR_Behaviour_Pose>();
        Debug.Log(m_pose);
    }

    private void Update()
    {
        bool pourCheck = CheckPour();
        
        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
                waterSpawnCurrentCooldown = 0;
            }
            else
            {
                EndPour();
            }
        }
                
        if (isPouring)
        {
            waterSpawnCurrentCooldown -= Time.deltaTime;
            if (waterSpawnCurrentCooldown < 0)
            {
                CreateWaterCollider();
                waterSpawnCurrentCooldown = waterSpawnCooldown;
            }
        }
    }

    private void StartPour()
    {
        Debug.Log("Start");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        Debug.Log("End");
        currentStream.End();
        currentStream = null;
    }

    private bool CheckPour()
    {
        Debug.Log(m_TriggerAction.GetState(m_pose.inputSource));
        return m_TriggerAction.GetState(m_pose.inputSource);
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

    private void CreateWaterCollider()
    {
        Instantiate(waterColliderPrefab, origin.position, Quaternion.identity);
    }
}