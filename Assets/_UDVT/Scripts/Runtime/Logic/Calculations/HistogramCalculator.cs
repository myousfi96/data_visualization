using System;
using System.Collections.Generic;

using System.Linq;

public class HistogramCalculator
{
    public double[] data;
    public int numBins;

    public HistogramCalculator(double[] data)
    {
        this.data = data;
    }

    public double[] CalculateHistogram()
    {
        // Calculate the number of bins using Sturges' formula
        int sturgesNumBins = (int)Math.Ceiling(Math.Log(data.Length, 2) + 1);

        // Calculate the number of bins using Square Root choice
        int sqrtNumBins = (int)Math.Ceiling(Math.Sqrt(data.Length));

        // Choose the appropriate number of bins
        int chosenNumBins = Math.Min(sturgesNumBins, sqrtNumBins);

        // Calculate the bin size (interval)
        double binSize = (data.Max() - data.Min()) / chosenNumBins;

        // Initialize the bin counts
        double [] binCounts = new double[chosenNumBins];

        // Group the data into the appropriate bins and count their frequency
        foreach (double value in data)
        {
            if (value >= data.Min() && value <= data.Max())
            {
            int binIndex = (int)Math.Floor((value - data.Min()) / binSize);
            if (binIndex >= 0 && binIndex < binCounts.Length)
            {
             binCounts[binIndex] +=  1;
            }
            }
        }

        return binCounts;
    }

    public double[] CalculateInterval()
    {


        int sturgesNumBins = (int)Math.Ceiling(Math.Log(data.Length, 2) + 1);

        // Calculate the number of bins using Square Root choice
        int sqrtNumBins = (int)Math.Ceiling(Math.Sqrt(data.Length));

        // Choose the appropriate number of bins
        int chosenNumBins = Math.Min(sturgesNumBins, sqrtNumBins);

        // Calculate the bin size (interval)
        double binSize = (data.Max() - data.Min()) / chosenNumBins;

        double[] intervalLabels = new double[chosenNumBins];
        for (int i = 0; i < chosenNumBins; i++)
        {
            intervalLabels[i] = data.Min() + i * binSize;
        }

        return intervalLabels;

    }

   
}