//NewCode_Group4
using System.Linq;
using UnityEngine;
public class VisViolinPlot : Vis
{
    public VisViolinPlot()
    {
        title = "Violin Plot";

        //Define Data Mark and Tick Prefab
        dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
        tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
    }

    

    public override GameObject CreateVis(GameObject container)
    {
        base.CreateVis(container);
        
        double [,] data = KernelDensityEstimationViolin.KDE(dataSets[0].ElementAt(0).Value, 2, 100);
        DataStatistics  statistics =  new DataStatistics(dataSets[0].ElementAt(0).Value);



        double [] x = new double[100];
        double [] y = new double[200];




        
        for (int i = 0; i < 100; i++)
        {
          x[i] = data[i, 0] ;
        }

        for (int i = 0; i < 200; i++)
        {
          y[i] = data[i, 1];

   
        }

        //## 01:  Create Axes and Grids

  
        visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, x, Direction.X);
        visContainer.CreateGrid(Direction.X, Direction.Y);

        // Y Axis
        visContainer.CreateAxis("Density", y, Direction.Y);

        // // Z Axis
        // visContainer.CreateAxis(dataSets[0].ElementAt(2).Key, dataSets[0].ElementAt(2).Value, Direction.Z);
        // visContainer.CreateGrid(Direction.Y, Direction.Z);
        // visContainer.CreateGrid(Direction.Z, Direction.X);

        //## 02: Set Remaining Vis Channels (Color,...)
        visContainer.SetChannel(VisChannel.XPos, x);
        visContainer.SetChannel(VisChannel.YPos, y);


        
        // visContainer.SetChannel(VisChannel.ZPos, dataSets[0].ElementAt(2).Value);
        // visContainer.SetChannel(VisChannel.Color, y);

        //## 03: Draw all Data Points with the provided Channels 
        visContainer.CreateDataMarks(dataMarkPrefab, false, true, true);

        //## 04: Rescale Chart
        visContainerObject.transform.localScale = new Vector3(width, height, depth);

        return visContainerObject;
    }

     

}
