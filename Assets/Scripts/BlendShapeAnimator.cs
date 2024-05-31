using System.Collections;
using UnityEngine;

public class BlendShapeAnimator : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer; // BlendShape�� ������ SkinnedMeshRenderer
    public int blendShapeIndex = 0; // BlendShape �ε��� (0���� ����)
    public float animationSpeed = 1.0f; // �ִϸ��̼� �ӵ�
    private float blendShapeWeight = 0.0f;
    private bool increasing = true; // BlendShape ���� �����ϴ��� ����
    bool isStart = false;

    public Animator animator;
    public AudioSource audioSource;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.75f);
        isStart = true;
        if (skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        { 
            if (isStart)
            {
                AnimateBlendShape();
            }
        }
        else if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    bool blinking = false;
    float blinkTime = 0;
    float blinkWeight = 0.0f;
    bool blinkClose = true;

    void AnimateBlendShape()
    {
        // BlendShape ���� ���� �Ǵ� ����
        if (increasing)
        {
            blendShapeWeight += animationSpeed * Time.deltaTime;
            if (blendShapeWeight >= 100.0f)
            {
                blendShapeWeight = 100.0f;
                increasing = false;
            }
        }
        else
        {
            blendShapeWeight -= animationSpeed * Time.deltaTime;
            if (blendShapeWeight <= 0.0f)
            {
                blendShapeWeight = 0.0f;
                increasing = true;
            }
        }

        // BlendShape �� ����
        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeWeight);
        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex + 1, Random.Range(0, 100));
        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex + 2, Random.Range(50, 100));

        blinkTime += Time.deltaTime;

        if (blinkTime > 5)
        {
            blinkTime = 0;
            blinking = true;
        }

        if (blinking)
        {
            if (blinkClose)
            {
                blinkWeight += animationSpeed * 2 * Time.deltaTime;
                if (blinkWeight >= 100.0f)
                {
                    blinkWeight = 100.0f;
                    blinkClose = false;
                }
            }
            else
            {
                blinkWeight -= animationSpeed * 2 * Time.deltaTime;
                if (blinkWeight <= 0.0f)
                {
                    blinkWeight = 0.0f;
                    blinkClose = true;
                    blinking = false;
                }
            }

            skinnedMeshRenderer.SetBlendShapeWeight(0, blinkWeight / 100 * 70);
            skinnedMeshRenderer.SetBlendShapeWeight(6, blinkWeight / 100 * 30);
        }
    }
}
