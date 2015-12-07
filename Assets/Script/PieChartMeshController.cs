using UnityEngine;

public class PieChartMeshController : MonoBehaviour
{
	public Material[] materials;
    PieChartMesh mPieChart;
    public float[] mData;
	public float msData;
	public float ncData;
	public float scData;
	
	public void Activate()
	{
	 	mPieChart = gameObject.AddComponent<PieChartMesh>() as PieChartMesh;
        if (mPieChart != null)
        {
			
            mPieChart.Init(mData, 100, 0, 100, materials);
            mData = new float[3];
			mData[0] = ncData;
			mData[1] = msData;
			mData[2] = scData;
			//mData = GenerateRandomValues(3);
            mPieChart.Draw(mData);
			GetComponent<Animation>().Play("chartAnim");
        }
	}
	
	/*
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            mData = GenerateRandomValues(3);
            mPieChart.Draw(mData);
        }
    }
	*/
    float[] GenerateRandomValues(int length)
    {
        float[] targets = new float[length];

        for (int i = 0; i < length; i++)
        {
            targets[i] = Random.Range(0f, 100f);
        }
        return targets;
    }
}
