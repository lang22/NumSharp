﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumSharp.Core
{
    public partial class NDArray
    {
        public NDArray AMin(int? axis = null)
        {
            NDArray res = new NDArray(dtype);
            if (axis == null)
            {
                switch (dtype.Name)
                {
                    case "Double":
                        //res.Storage.Set(new double[1] { Storage.Double8.Min() });
                        break;
                }
                
                res.Storage.Shape = new Shape(new int[] { 1 });
            }
            else
            {
                if (axis < 0 || axis >= shape.NDim)
                    throw new Exception("Invalid input: axis");
                int[] resShapes = new int[shape.Shapes.Count - 1];
                int index = 0; //index for result shape set
                //axis departs the shape into three parts: prev, cur and post. They are all product of shapes
                int prev = 1; 
                int cur = 1;
                int post = 1;
                int size = 1; //total number of the elements for result
                //Calculate new Shape
                for (int i = 0; i < shape.Shapes.Count; i++)
                {
                    if (i == axis)
                        cur = shape.Shapes[i];
                    else
                    {
                        resShapes[index++] = shape.Shapes[i];
                        size *= shape.Shapes[i];
                        if (i < axis)
                            prev *= shape.Shapes[i];
                        else
                            post *= shape.Shapes[i];
                    }
                }
                res.Storage.Shape = new Shape(resShapes);
                //Fill in data
                index = 0; //index for result data set
                int sameSetOffset = shape.DimOffset[axis.Value];
                int increments = cur * post;
                int start = 0;
                double min;
                for (int i = 0; i < this.size; i += increments)
                {
                    for (int j = i; j < i + post; j++)
                    {
                        start = j;
                        /*
                        min = Storage.Double8[start];
                        for (int k = 0; k < cur; k++)
                        {
                            min = Math.Min(min, Storage.Double8[start]);
                            start += sameSetOffset;
                        }
                        res.Storage.Double8[index++] = min;*/
                    }
                }
            }
            return res;
        }
    }
}
