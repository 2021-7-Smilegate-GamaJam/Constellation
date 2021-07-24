using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetButtonSet : MonoBehaviour
{
    private float mapLength;        //�� �� ����
    private float radius;           //�༺ ������

    private float currZAngle;       //���� zȸ������
    private float currFill;         //���� fillamount;
  
    private List<double> obstaclePosList = new List<double>();         //������Ʈ x����Ʈ
    private RectTransform buttonRect;
    
    [SerializeField] private GameObject renderImg;
    [SerializeField] private StageModel temp;
    [SerializeField] private Image gageAmount;

    obstruction obs;
    private List<obstruction_struct> obstaclceList = new List<obstruction_struct>();

    private void Awake()
    {
        //�������� �𵨿��� ���� �޾ƿ���
        GetStageModelObstaclePos(temp);

        buttonRect = GetComponent<RectTransform>();
        radius = buttonRect.rect.width / 2;

        currZAngle = 0;
        currFill = 0;

        gageAmount.fillAmount = 0;

        obs = gameObject.AddComponent<obstruction>();     
    }

    private void Start()
    {
        obstaclceList = obs.SpawnData;
        RenderObstacle();
        StartCoroutine(Rotate());
    }

    private void RenderObstacle()
    {

        for(int i = 0; i < obstaclePosList.Count; i++)
        {
            var newObj = Instantiate(renderImg, this.transform);
            //��ġ ����
            newObj.GetComponent<RectTransform>().localPosition = SetObstacleOnPlanet(obstaclePosList[i]);
            //�̹��� ����
            //newObj.GetComponent<Image>().sprite = obstaclceList[i].monster.GetComponent<SpriteRenderer>().sprite;
            //ȸ�� �߰�
            newObj.GetComponent<RectTransform>().Rotate(Vector3.forward * GetTheta(obstaclePosList[i]) * -1f);

        }
    }

    //���� ��ǥ�� ����ǥ(? ���� �̻��ϱ� �ѵ� )�� ��ȯ
    //������ ��ֹ��� �߾�
    private Vector2 SetObstacleOnPlanet(double _previousX)
    {
        float theta = GetTheta(_previousX);
        float xPos = radius * Mathf.Sin(theta * Mathf.Deg2Rad);
        float yPos = radius * Mathf.Cos(theta * Mathf.Deg2Rad);

        return new Vector2(xPos, yPos);
    }

    private float GetTheta(double _previousX)
    {
        double ratio = (_previousX - 0) / 60;            //���� ����
        float theta = (float)ratio * 360f;

        return theta;
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
        //����۽�
        buttonRect.rotation = Quaternion.Euler(0, 0, currZAngle);
        gageAmount.fillAmount = currFill;

        while(gageAmount.fillAmount < 1)
        { 
            buttonRect.Rotate(Vector3.forward * 6 * Time.deltaTime);
            gageAmount.fillAmount += (1f / 60f) * Time.deltaTime;

            currZAngle = buttonRect.rotation.eulerAngles.z;
            currFill = gageAmount.fillAmount;

            yield return null;
        }

        buttonRect.rotation = Quaternion.identity;
    }

    public void StopRotate()
    {
        StopCoroutine(Rotate());
    }

}


