using UnityEngine;

namespace DitzelGames.FastIK
{
    public class SampleRotator : MonoBehaviour
    {
        
        void Update()
        {
            //just rotate the object
            transform.Rotate(0, Time.deltaTime * 20, 0);
        }
    }
}
