using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lab1
{
    public class Dog : MonoBehaviour
    {
        [SerializeField]
        int ID;

        Rigidbody rigidbody;
        bool isRunning;
        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            isRunning = false;
        }
        
        public void Run()
        {
            float duration = Random.Range(0.03f, 0.04f);
            Debug.Log("duration" + (duration));

            isRunning = true;
            StartCoroutine(Running(duration));

        }

        public void Stop()
        {
            isRunning = false;
        }

        IEnumerator Running(float duration)
        {
            while(isRunning)
            {
                int velocity = Random.Range(19, 21);
                Debug.Log("velocity" + (velocity));

                rigidbody.AddForce(new Vector3(velocity, 0, 0));
                yield return new WaitForSeconds(duration);
            }
        }
    }
}


