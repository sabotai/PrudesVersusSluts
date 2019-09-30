using System.Collections;
using System.Collections.Generic;
using DoodleStudio95;
using UnityEngine;
using UnityEngine.UI;

public class DoodleAnimSequence : MonoBehaviour
{
	DoodleAnimator animator;
	public DoodleAnimationFile[] animSequence;
	bool played = false;
	public bool m_Loop = true;
	public GameObject[] enableInBetween;
	public GameObject[] disableInBetween;
	public GameObject toggleImageInBetween;
    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<DoodleAnimator>();
      // Set the init animation
      animator.ChangeAnimation(animSequence[0]);
		}
  void Update(){
  			if (!played){
  				Play();
  				played = true;
  			}
  }

  IEnumerator PlaySequence() {
    
    int i = 0;
    while(i < animSequence.Length) {
      // Set the new animation
      animator.ChangeAnimation(animSequence[i]);
      // Play the animation and wait until it's finished
      yield return animator.PlayAndPauseAt();
      // Advanced to the next animation
      i++;
      foreach (GameObject obj in enableInBetween) obj.SetActive(true);
      foreach (GameObject obj in disableInBetween) obj.SetActive(false);
      toggleImageInBetween.GetComponent<Image>().enabled = false;
      toggleImageInBetween.transform.GetChild(0).GetComponent<Text>().text = "";
      // Loop if we've reached the end
      if (i >= animSequence.Length && m_Loop)
        i = 0;
    }
    animator.Stop();
  }

  public void Play() {
    StopAllCoroutines();
    StartCoroutine(PlaySequence());
  }
}
