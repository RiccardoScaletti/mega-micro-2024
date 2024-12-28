using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Return0 
{
    public class Batter : MonoBehaviour
    {
        public float swingDelay = 0f;
        public bool canSwing;
        Animator animator;
        public GameObject hitBox;
        public List<Transform> hitBoxStances;
        [SerializeField] float cameraShakeStrenght;
        [SerializeField] float cameraShakeDelay;
        [SerializeField] int cameraShakeCycles;

        void Start()
        {
            Debug.Log("Hello!");
            canSwing = true;
            if (this.gameObject.GetComponent<Animator>()) animator = this.gameObject.GetComponent<Animator>();
            else Debug.Log("<color=red>Animator not found!</color>");
            animator.SetBool("CanSwing", true);
        }

        private void Update()
        {
            float axisSpace = Input.GetAxisRaw("Space");
            float axisVert = Input.GetAxisRaw("Vertical");

            if (canSwing)
            {
                //TODO: change stance between hi, mid & lo using vert input
                if (axisVert > 0) ChangeHitBoxStance(0);
                else if (axisVert == 0) ChangeHitBoxStance(1);
                else if (axisVert < 0) ChangeHitBoxStance(2);

                if (axisSpace > 0 && animator)  //swing bat when space is pressed
                {
                    //Debug.Log("SPace hit");
                    animator.SetBool("SpacePressed", true);

                    canSwing = false;
                    StartCoroutine(StartSwingDelay());
                    //TODO: time the hitbox with animation
                }
            }


        }

        IEnumerator StartSwingDelay()
        {
            yield return new WaitForSeconds(0.25f);
            animator.SetBool("CanSwing", false);
            yield return new WaitForSeconds(swingDelay);
            animator.SetBool("SpacePressed", false);
            
        }

        private void ChangeHitBoxStance (int _index)
        {
            hitBox.transform.position = hitBoxStances[_index].position;
            //Debug.Log("hitBoxStances[_index].name");
        }

        //TODO: hit the ball succesfully, add camera shake!!!!
        public void OnBallHit()
        {
            Debug.Log("<color=yellow>Hello!</color>");

            StartCoroutine(ShakeCamera());

            //store win condition
            //AudioSource win = Managers.AudioManager.CreateAudioSource();
            //win.PlayOneShot(winSound);

            Managers.MinigamesManager.DeclareCurrentMinigameWon();
            Managers.MinigamesManager.EndCurrentMinigame(1f);
        }

        private IEnumerator ShakeCamera()
        {
            //cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount; //cam shake
            Vector3 originalPos = Camera.main.transform.localPosition;
            for (int i = 0; i < cameraShakeCycles; i++)
            {
                Camera.main.transform.localPosition = originalPos + Random.insideUnitSphere * cameraShakeStrenght;
                yield return new WaitForSeconds(cameraShakeDelay);
            }
            Camera.main.transform.localPosition = originalPos;
        }
    }

}

