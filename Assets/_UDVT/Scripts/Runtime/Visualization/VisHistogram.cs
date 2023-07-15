//NewCode_Group4
using System.Linq;
using UnityEngine;
public class VisHistogram : Vis
{
    public VisHistogram()
    {
        title = "Histogram";

        //Define Data Mark and Tick Prefab
        dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Cube");
        tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
    }

    public override GameObject CreateVis(GameObject container)
    {
        base.CreateVis(container);

        HistogramCalculator calculator = new HistogramCalculator(dataSets[0].ElementAt(0).Value);


        double[] data = calculator.CalculateHistogram();
        double[] interval = calculator.CalculateInterval();
        //## 01:  Create Axes and Grids

        // X Axis

        visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, interval, Direction.X);
        visContainer.CreateGrid(Direction.X, Direction.Y);

        // Y Axis
        visContainer.CreateAxis("Frequency", data, Direction.Y);

        // // Z Axis
        // visContainer.CreateAxis(dataSets[0].ElementAt(2).Key, dataSets[0].ElementAt(2).Value, Direction.Z);
        // visContainer.CreateGrid(Direction.Y, Direction.Z);
        // visContainer.CreateGrid(Direction.Z, Direction.X);

        //## 02: Set Remaining Vis Channels (Color,...)
        visContainer.SetChannel(VisChannel.XPos, interval);
        visContainer.SetChannel(VisChannel.YSize, data);

        // visContainer.SetChannel(VisChannel.ZPos, dataSets[0].ElementAt(2).Value);
        visContainer.SetChannel(VisChannel.Color, data);

        //## 03: Draw all Data Points with the provided Channels 
        visContainer.CreateDataMarks(dataMarkPrefab);

        //## 04: Rescale Chart
        visContainerObject.transform.localScale = new Vector3(width, height, depth);

        return visContainerObject;
    }

}
