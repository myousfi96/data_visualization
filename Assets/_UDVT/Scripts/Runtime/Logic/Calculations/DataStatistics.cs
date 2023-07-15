//NewCode_Group4

using System;
using System.Linq;

public class DataStatistics
{
    private double[] data;

    public DataStatistics(double[] inputData)
    {
        data = inputData;
        Array.Sort(data); // Sort the data in ascending order
    }

    public double Median()
    {
        int n = data.Length;
        int middleIndex = n / 2;
        double median;

        if (n % 2 == 0)
        {
            // If the data set has an even number of elements, average the two middle values
            median = (data[middleIndex - 1] + data[middleIndex]) / 2.0;
        }
        else
        {
            // If the data set has an odd number of elements, take the middle value
            median = data[middleIndex];
        }

        return median;
    }

    public double LowerQuartile()
    {
        int n = data.Length;
        int lowerQuartileIndex = n / 4;

        return data[lowerQuartileIndex];
    }

    public double UpperQuartile()
    {
        int n = data.Length;
        int upperQuartileIndex = (3 * n) / 4;

        return data[upperQuartileIndex];
    }

    public double Min()
    {
        // The minimum value is the first element after sorting
        return data[0];
    }

    public double Max()
    {
        // The maximum value is the last element after sorting
        return data[data.Length - 1];
    }
}