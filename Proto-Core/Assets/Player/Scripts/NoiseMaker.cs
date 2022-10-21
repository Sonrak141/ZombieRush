using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    [SerializeField] float noiseRadius = 5f;
    [SerializeField] bool silent = false;
    [SerializeField] bool onlyWhenMoving = false;
    [SerializeField] float frequency = 5f;

    public interface INoiseListener { void OnHeard(NoiseMaker noiseMaker); }

    private void Start()
    {
        oldPosition = transform.position;
    }

    public void MakeNoise()
    {
        InternalMakeNoise();
    }

    Vector3 oldPosition;
    float timeSinceLastNoise;
    private void Update()
    {
        if (!silent)
        {
            timeSinceLastNoise += Time.deltaTime;

            if (timeSinceLastNoise > (1f / frequency))
            {
                timeSinceLastNoise -= (1f / frequency);

                bool makesSoundAllTheTime = !onlyWhenMoving;
                if (makesSoundAllTheTime || (oldPosition != transform.position))
                { InternalMakeNoise(); }

                oldPosition = transform.position;
            }
        }
    }

    void InternalMakeNoise()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, noiseRadius);
        foreach (Collider c in colliders)
        {
            INoiseListener listener = c.GetComponent<INoiseListener>();
            listener?.OnHeard(this);
        }
    }
}
