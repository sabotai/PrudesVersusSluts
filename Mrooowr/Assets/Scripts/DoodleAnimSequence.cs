using System.Collections;
using System.Collections.Generic;
using DoodleStudio95;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoodleAnimSequence : MonoBehaviour
{
	DoodleAnimator animator;
	public DoodleAnimationFile[] animSequence;
	bool played = false;
	public bool m_Loop = true;
	public GameObject[] enableInBetween;
	public GameObject[] disableInBetween;
	public GameObject toggleImageInBetween;
  public bool endGame = false;
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
      if (Manager.gameOver && endGame) StartCoroutine(LoadYourAsyncScene());
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

      IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
