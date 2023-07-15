using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class stores and draws elements of arbitrarily 2D/3D visualizations (Axis, Ticks and DataMarks).
/// VisContainer GameObject is created as Unit Container. Axes assume a length of 1.0 and start at (0,0) and end at (1,0).
/// </summary>
public class VisContainerHorizon : VisContainer
{
    

public  void RenderSurface(Vector3 [] points1,Vector3 [] points2,Vector3 [] points3, Vector3 [] points4, double [] scaledy ){

        GameObject meshContainer2;
        meshContainer2 = new GameObject("Mesh");
        meshContainer2.transform.parent = visContainer.transform;
         GameObject meshContainer3;
        meshContainer3 = new GameObject("Mesh");
        meshContainer3.transform.parent = visContainer.transform;
        GameObject meshContainer4;
        meshContainer4 = new GameObject("Mesh");
        meshContainer4.transform.parent = visContainer.transform;

 int[] triangles = new int[(points1.Length - 2)  * 6];

        // Populate the triangle indices based on the length of the points array
        int index = 0;
        for (int i = 1; i < points1.Length - 1; i++)
        {
            triangles[index] = i-1;
            triangles[index + 1] = i;
            triangles[index + 2] = i+1;
            triangles[index +3] = i+1;
            triangles[index + 4] = i;
            triangles[index + 5] = i-1;
            
            index += 6;
        }

                if (points3.Length < 1){
        points3 = new Vector3[3];
        points3[0] = new Vector3(0  , 0 , 0) ;
        points3[1] = new Vector3(0  , 0 , 0) ;
        points3[2] = new Vector3(0  , 0 , 0) ;

        }

         int[] triangles3 = new int[(points3.Length - 2)  * 6];

        // Populate the triangle indices based on the length of the points array
         index = 0;
        for (int i = 1; i < points3.Length - 1; i++)
        {
            triangles3[index] = i-1;
            triangles3[index + 1] = i;
            triangles3[index + 2] = i+1;
            triangles3[index +3] = i+1;
            triangles3[index + 4] = i;
            triangles3[index + 5] = i-1;
            
            index += 6;
        }

        if (points4.Length < 1){
        points4 = new Vector3[3];
        points4[0] = new Vector3(0  , 0 , 0) ;
        points4[1] = new Vector3(0  , 0 , 0) ;
        points4[2] = new Vector3(0  , 0 , 0) ;

        }
        int[] triangles4 = new int[(points4.Length - 2)  * 6];

        // Populate the triangle indices based on the length of the points array
         index = 0;
        for (int i = 1; i < points4.Length - 1; i++)
        {
            triangles4[index] = i-1;
            triangles4[index + 1] = i;
            triangles4[index + 2] = i+1;
            triangles4[index +3] = i+1;
            triangles4[index + 4] = i;
            triangles4[index + 5] = i-1;
            
            index += 6;
        }




        Mesh mesh1= new Mesh();
        Mesh mesh2= new Mesh();
                Mesh mesh3= new Mesh();
                                Mesh mesh4= new Mesh();





        MeshFilter meshFilter1 = meshContainer.AddComponent<MeshFilter>();
        MeshFilter meshFilter2 = meshContainer2.AddComponent<MeshFilter>();
        MeshFilter meshFilter3 = meshContainer3.AddComponent<MeshFilter>();
                MeshFilter meshFilter4 = meshContainer4.AddComponent<MeshFilter>();



        MeshRenderer meshRenderer1 = meshContainer.AddComponent<MeshRenderer>();
        MeshRenderer meshRenderer2 = meshContainer2.AddComponent<MeshRenderer>();
        MeshRenderer meshRenderer3= meshContainer3.AddComponent<MeshRenderer>();
                MeshRenderer meshRenderer4= meshContainer4.AddComponent<MeshRenderer>();


        meshRenderer1.material = new Material(Shader.Find("Transparent/Diffuse"));
        meshRenderer2.material = new Material(Shader.Find("Transparent/Diffuse"));
                meshRenderer3.material = new Material(Shader.Find("Transparent/Diffuse"));
                        meshRenderer4.material = new Material(Shader.Find("Transparent/Diffuse"));



        mesh1.vertices = points1;
        mesh2.vertices = points2;
        mesh3.vertices = points3;
                mesh4.vertices = points4;


        Color newColor = new Color(Color.blue.r , Color.blue.g, Color.blue.b, (float)0.5);
        meshRenderer1.material.color = newColor;

       newColor = new Color(Color.red.r , Color.red.g, Color.red.b, (float)0.5);

        meshRenderer2.material.color = newColor;

        newColor = new Color(Color.blue.r , Color.blue.g, Color.blue.b, (float)0.8);

        meshRenderer3.material.color = newColor;
        newColor = new Color(Color.red.r , Color.red.g, Color.red.b, (float)0.8);

        meshRenderer4.material.color = newColor;


    
        


        // Assign the triangles to the mesh
        mesh1.triangles = triangles;
       mesh2.triangles = triangles;
        mesh3.triangles = triangles3;
                mesh4.triangles = triangles4;



        meshFilter1.mesh = mesh1;
        meshFilter2.mesh = mesh2;
                meshFilter3.mesh = mesh3;
                        meshFilter4.mesh = mesh4;


        



}

    /// <summary>
    /// Method to create a new DataMark for each value in the channelValues Dictionary.
    /// For each value in the dataset (we assume each attribute has the same amount of values) a DataMark is created.
    /// The DataMark is created with each channel (Pos, Size, Color,...) which has data saved to it.
    /// </summary>
    /// <param name="markPrefab"></param>
public override void CreateDataMarks(GameObject markPrefab, bool line = false, bool mesh = false, bool statistics = false, bool surface = false)
    {
        // Set the positions to the Line Renderer component

        // Check how many values the datset has
        int numberOfMarks = channelValues[0].Length;
        Vector3[] linePoints = new Vector3[numberOfMarks];
        Vector3[] meshPoints1 = new Vector3[numberOfMarks*2];
        Vector3[] meshPoints2 = new Vector3[numberOfMarks*2];
        Vector3[] meshPoints3 = new Vector3[numberOfMarks*2];
        Vector3[] meshPoints4 = new Vector3[numberOfMarks*2];


        double[] scaledy = new double[numberOfMarks];
        double[] scaledx = new double[numberOfMarks];
        double[] scaledyPositve = new double [numberOfMarks];
        int i = 0;
         for (int mark = 0; mark < numberOfMarks; mark++)
        {
            DataMark dataMark = new DataMark(dataMarkList.Count, markPrefab);

            //Create Values
            DataMark.Channel channel = DataMark.DefaultDataChannel();

            channel = GetDataMarkChannelValues(channel,mark);
            scaledy[mark] = channel.position.y;
            scaledx[mark] = channel.position.x;


        }

        double maxValuey = 0;

        DataStatistics  statisticsy =  new DataStatistics(scaledy);
        double irq75y = statisticsy.UpperQuartile();

        double mediany = statisticsy.Median();
        
        int j = 0 ,k = 0 ;

        for (int mark = 0; mark < numberOfMarks; mark++)
        {

            DataMark dataMark = new DataMark(dataMarkList.Count, markPrefab);

            //Create Values
            DataMark.Channel channel = DataMark.DefaultDataChannel();

            channel = GetDataMarkChannelValues(channel,mark);
            
            linePoints[mark] = new Vector3(channel.position.x/4, channel.position.y/4, channel.position.z/4);
            if (channel.position.y < mediany){
                
                meshPoints1[j] = new Vector3(channel.position.x,  (channel.position.y + (float)mediany ) - (float)0.1 , channel.position.z);
                meshPoints1[j + 1] = new Vector3(channel.position.x, (float)mediany, 0);
                if (meshPoints1[j].y > maxValuey)
                maxValuey = meshPoints1[j].y;
                j = j + 2;

            }

            else{
                meshPoints2[k] = new Vector3(channel.position.x, channel.position.y, channel.position.z);
                meshPoints2[k + 1] = new Vector3(channel.position.x, (float)mediany, 0);
                if (meshPoints2[k].y > maxValuey)
                maxValuey = meshPoints2[k].y;
                 k = k + 2;
            }


            
            // dataMark.CreateDataMark(dataMarkContainer.transform, channel);
            // dataMarkList.Add(dataMark);
            
        }

        float  medianBand = (float)(((maxValuey-mediany)/2)+(float)mediany);
        int z =0 , l = 0;
          for (int mark = 0; mark < numberOfMarks ; mark++)
        {

            DataMark dataMark = new DataMark(dataMarkList.Count, markPrefab);

            //Create Values
            DataMark.Channel channel = DataMark.DefaultDataChannel();

            channel = GetDataMarkChannelValues(channel,mark);

            if( meshPoints1[i].y >= (double) medianBand ) {
                meshPoints3[z] = new Vector3(meshPoints1[i].x,  (meshPoints1[i].y + (float)mediany  - medianBand ) , meshPoints1[i].z);
                meshPoints3[z + 1] = new Vector3(meshPoints1[i].x, (float)mediany, 0);
                meshPoints1[i] = new Vector3(0,  0 , 0);
                meshPoints1[i+1] = new Vector3(0,  0 , 0);

                z = z +2 ;
            }
            if( meshPoints2[i].y >= (double) medianBand ) {
                meshPoints4[l] = new Vector3(meshPoints2[i].x,  (meshPoints2[i].y + (float)mediany  - medianBand ) , meshPoints2[i].z);
                meshPoints4[l + 1] = new Vector3(meshPoints2[i].x, (float)mediany, 0);
                meshPoints2[i] = new Vector3(0,  0 , 0);
                meshPoints2[i+1] = new Vector3(0, 0 , 0);
                l = l+2;
            }
            i = i + 2;


        }

        Vector3[] points1 = new Vector3[j];
        Vector3[] points2 = new Vector3[k];
        Vector3[] points3 = new Vector3[z];
        Vector3[] points4 = new Vector3[l];
        int index1 = 0;
        int index2 = 0;
        int index3 = 0;
        int index4 = 0;

        for ( i =0; i < meshPoints1.Length ; i ++){

        if (meshPoints1[i].x != 0){
            points1[index1] = meshPoints1[i];
            index1 ++;
        }
        if (meshPoints2[i].x != 0){
            points2[index2] = meshPoints2[i];
            index2 ++;
        }
        if (meshPoints3[i].x != 0){
            points3[index3] = meshPoints3[i];
            index3 ++;
        }
        if (meshPoints4[i].x != 0){
            points4[index4] = meshPoints4[i];
            index4 ++;
        }

    }

        if (mesh == true)
                    MeshRender(points1);


        if (surface == true)
            RenderSurface(points1,points2, points3, points4, scaledy);
        
        DataStatistics  statisticsx =  new DataStatistics(scaledx);
        double maxx = statisticsx.Max();
        double minx = statisticsx.Min();
        Vector3[] lineCut = new Vector3[2];
        Vector3[] lineCutBand = new Vector3[2];



        lineCut[0]= new Vector3((float)minx/4, (float)mediany/4, 0);
        lineCut[1] = new Vector3((float)maxx/4,(float) mediany/4, 0);
        lineCutBand[0]= new Vector3((float)minx/4, medianBand/4, 0);
        lineCutBand[1] = new Vector3((float)maxx/4, medianBand/4, 0);


        lineRender(lineCut);
        //lineRender(lineCutBand);

        if (line == true)
            lineRender(linePoints);


        if (statistics == true)
            DrawStatistics(scaledx, scaledy);


       
      
    }
}

   