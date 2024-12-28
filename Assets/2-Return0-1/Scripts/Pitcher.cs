using System.Collections;
using UnityEngine;

namespace Return0
{
    public class Pitcher : MonoBehaviour
    {
        public GameObject ball;
        [SerializeField] Animator ballAnimator;
        [SerializeField] Animator pitcherAnimator;
        float throwTime;
        public float maxThrowTime;
        public bool ballThrow;
        float currentTime;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
            //if (this.gameObject.GetComponent<Animator>())
            //{
            //    pitcherAnimator = this.gameObject.GetComponent<Animator>();
            //    Debug.Log(pitcherAnimator.name);
            //}
            //else Debug.Log("<color=red>Animator for pitcher not found</color>");
            //if (this.gameObject.GetComponentInChildren<Animator>() && this.gameObject.GetComponentInChildren<Animator>() != pitcherAnimator)
            //{
            //    ballAnimator = this.gameObject.GetComponentInChildren<Animator>();
            //    Debug.Log(ballAnimator.name);
            //}
            //else Debug.Log("<color=red>Animator for ball not found</color>");


            throwTime = Random.Range(0.5f, maxThrowTime);
            ball.SetActive(false);
            ballThrow = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!ballThrow)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= throwTime)
                {
                    ballThrow = true;
                    ThrowBall();
                    // set current time to 0 so throw ball isn't called again
                    currentTime = 0;
                }
            }
        }

        void ThrowBall()
        {
            
            //read an index and set the animation controller to pitch a certain ball
            if (ballThrow)
            {
                //Debug.Log("Throw!");
                ball.SetActive(true);
                //Ball index goes 0 for hi, 1 fo mid & 2 for lo, subject to change

                // choose random int for pitch type
                int pitchType = Random.Range(0, 3);
                Debug.Log(pitchType);
                
                if (ballAnimator)
                {
                    // choose different pitch based on pitchType
                    switch (pitchType) 
                    {
                        case 0:
                            Debug.Log("High ball!");
                            ballAnimator.SetInteger("PitchIndex", 0);
                            if (pitcherAnimator) pitcherAnimator.SetBool("isThrown", true);
                            ballThrow = false;
                            break;
                        case 1:
                            Debug.Log("Fast ball!");
                            ballAnimator.SetInteger("PitchIndex", 1);
                            if (pitcherAnimator) pitcherAnimator.SetBool("isThrown", true);
                            ballThrow = false;
                            break;
                        case 2:
                            Debug.Log("Low ball!");
                            ballAnimator.SetInteger("PitchIndex", 2);
                            if (pitcherAnimator) pitcherAnimator.SetBool("isThrown", true);
                            ballThrow = false;
                            break;
                        default:
                            break;
                    }
                    

                }
                

            }
        }
    }
}