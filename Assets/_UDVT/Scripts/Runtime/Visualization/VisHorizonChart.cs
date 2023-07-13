using System.Linq;
using UnityEngine;
public class VisHorizonChart : Vis
{
    public VisHorizonChart()
    {
        title = "Horizon Chart";

        //Define Data Mark and Tick Prefab
        dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
        tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
    }


    public override GameObject CreateVis(GameObject container)
    {
        visContainer = new VisContainerHorizon();
        visContainerObject = visContainer.CreateVisContainer(title);
        visContainerObject.transform.SetParent(container.transform);

        visContainer.SetAxisOffsets(xyzOffset);
        visContainer.SetAxisTickNumber(xyzTicks);
        visContainer.SetColorScheme(colorScheme);
        
        double [,] data = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 2, 100);

        double [] x = new double[100];
        double [] y = new double[100];
  


        
        for (int i = 0; i < 100; i++)
        {
           x[i] = data[i, 0] ;
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
        visContainer.SetChannel(VisChannel.Color, y);

        //## 03: Draw all Data Points with the provided Channels 
        visContainer.CreateDataMarks(dataMarkPrefab, false, false, false, true);
        //visContainer.renderLine(points);

        //## 04: Rescale Chart
        visContainerObject.transform.localScale = new Vector3(width, height, depth);

        return visContainerObject;
    }

     

}
