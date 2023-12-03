using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera cmVC;
    CinemachineBasicMultiChannelPerlin cbmcp;

    float shakeTimer;

    private void Start()
    {
        cmVC = GetComponent<CinemachineVirtualCamera>();        
        cbmcp = cmVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity, float time)
    {
        cbmcp.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0)
            {
                cbmcp.m_AmplitudeGain = 0F;
            }
        }
    }
}