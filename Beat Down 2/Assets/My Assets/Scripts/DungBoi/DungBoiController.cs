using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungBoiController : MonoBehaviour
{

    [SerializeField] LegStepper L1LegStepper;
    [SerializeField] LegStepper L2LegStepper;
    [SerializeField] LegStepper L3LegStepper;
    [SerializeField] LegStepper R1LegStepper;
    [SerializeField] LegStepper R2LegStepper;
    [SerializeField] LegStepper R3LegStepper;
    
    void Awake()
    {
        StartCoroutine(LegUpdateCoroutine());
    }

    // Only allow diagonal leg pairs to step together
    IEnumerator LegUpdateCoroutine()
    {
        // Run forever
        while (true)
        {
            // Try moving one diagonal pair of legs
            do
            {
                L1LegStepper.TryMove();
                L3LegStepper.TryMove();
                R2LegStepper.TryMove();
                // Wait a frame
                yield return null;
            
            // Stay in this loop while either leg is moving.
            // If only one leg in the pair is moving, the calls to TryMove() will let
            // the other leg move if it wants to.
            } while (L1LegStepper.Moving || L3LegStepper.Moving || R2LegStepper.Moving);

            // Do the same thing for the other pair
            do
            {
                L2LegStepper.TryMove();
                R1LegStepper.TryMove();
                R3LegStepper.TryMove();
                yield return null;
            } while (L2LegStepper.Moving || R1LegStepper.Moving || R3LegStepper.Moving);
        }
    }
}
