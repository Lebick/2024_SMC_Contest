using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCutScene : MonoBehaviour
{
    [TextArea]
    public List<string> startText;

    public Text descriptionText;

    public SpriteRenderer darkImage;

    public Material glitchMaterial;

    private IEnumerator currentCoroutine;

    public Transform realCamera;

    public ParticleSystem star;

    private void Start()
    {
        glitchMaterial.SetFloat("_Offset", 0f);
        StartCoroutine(StartAnimation());
    }
    
    private IEnumerator StartAnimation()
    {
        StartCoroutine(PrintText(2f, 0));
        yield return new WaitForSeconds(3.5f);

        float progress = 0f;
        while(progress < 1f)
        {
            progress += Time.deltaTime;
            darkImage.material.color = new Color(0, 0, 0, 1 - progress);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(PrintText(2f, 1));

        yield return new WaitForSeconds(4f);

        StartCoroutine(PrintText(3f, 2));

        yield return new WaitForSeconds(5f);

        StartCoroutine(PrintText(2f, 3));

        yield return new WaitForSeconds(5f);

        currentCoroutine = PrintText(0.5f, 4);
        StartCoroutine(currentCoroutine);
        yield return new WaitForSeconds(0.25f);

        progress = 0f;

        while(progress < 1f)
        {
            progress += Time.deltaTime * 4f;
            glitchMaterial.SetFloat("_Offset", Random.Range(-30, 30));
            yield return null;
        }

        darkImage.material.color = new Color(0, 0, 0, 1);
        StopCoroutine(currentCoroutine);
        descriptionText.text = string.Empty;

        yield return new WaitForSeconds(0.5f);

        realCamera.transform.position = new(40, 0, -10);
        glitchMaterial.SetFloat("_Offset", 0);

        yield return new WaitForSeconds(1.0f);

        progress = 0f;

        while(progress < 1f)
        {
            progress += Time.deltaTime;
            darkImage.material.SetFloat("_Progress", 1 - progress);
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        star.Play();

        yield return new WaitForSeconds(4.0f);
        darkImage.transform.localPosition = new Vector3(0, 17, 10);
        darkImage.material.SetFloat("_Progress", 1);
        progress = 0;

        while(progress < 1f)
        {
            progress += Time.deltaTime;
            float value = 1 - Mathf.Pow(1 - progress, 3);

            darkImage.transform.localPosition = Vector3.Lerp(new Vector3(0, 17, 10), new Vector3(0, 0, 10), value);

            yield return null;
        }

        yield return null;
    }

    private IEnumerator PrintText(float printTime, int index)
    {
        string text = startText[index];

        float progress = 0f;

        while(progress < 1f)
        {
            progress += Time.deltaTime / printTime;
            progress = Mathf.Clamp01(progress);

            int textIndex = (int)Mathf.Lerp(0, text.Length, progress);

            descriptionText.text = text[..textIndex];
            yield return null;
        } 

        yield return null;
    }
}
