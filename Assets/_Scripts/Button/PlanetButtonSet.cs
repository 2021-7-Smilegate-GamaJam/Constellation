using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetButtonSet : MonoBehaviour
{
    private float mapLength;        //�� �� ����
    private float radius;           //�༺ ������
  
    private List<double> obstaclePosList = new List<double>();         //������Ʈ x����Ʈ
    private RectTransform buttonRect;
   
    [SerializeField] private GameObject renderImg;
    [SerializeField] private StageModel temp;
    [SerializeField] private Image gageAmount;

    private void Awake()
    {
        //�������� �𵨿��� ���� �޾ƿ���
        GetStageModelObstaclePos(temp);

        buttonRect = GetComponent<RectTransform>();
        radius = buttonRect.rect.width / 2;

        gageAmount.fillAmount = 0;
    }

    private void Start()
    {
        RenderObstacle();
        StartCoroutine(Rotate());
    }

    private void RenderObstacle()
    {
        foreach(var obj in obstaclePosList)
        {
            var neObj = Instantiate(renderImg, this.transform);
            //��ġ ����
            neObj.GetComponent<RectTransform>().localPosition = SetObstacleOnPlanet(obj);
            //�̹��� ����

            //ȸ�� �߰�
        }
    }

    //���� ��ǥ�� ����ǥ(? ���� �̻��ϱ� �ѵ� )�� ��ȯ
    //������ ��ֹ��� �߾�
    private Vector2 SetObstacleOnPlanet(double _previousX)
    {
        double ratio = (_previousX - 0) / 60;            //���� ����
        float theta = (float)ratio * 360f;
        float xPos = radius * Mathf.Sin(theta * Mathf.Deg2Rad);
        float yPos = radius * Mathf.Cos(theta * Mathf.Deg2Rad);

        return new Vector2(xPos, yPos);
    }

    public void GetStageModelObstaclePos(StageModel _stageModel)
    {
        foreach (var xPos in _stageModel.objectPositions)
        {
            obstaclePosList.Add(xPos);
        }
    }

    private IEnumerator Rotate()
    {
        while(gageAmount.fillAmount < 1)
        {
            buttonRect.Rotate(Vector3.forward * 6 * Time.deltaTime);
            gageAmount.fillAmount += (1f / 60f) * Time.deltaTime;

            yield return null;
        }

    }
}


