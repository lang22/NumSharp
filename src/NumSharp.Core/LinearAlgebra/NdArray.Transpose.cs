﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using NumSharp.Core.Extensions;

namespace NumSharp.Core
{
    public partial class NDArray
    {
        public NDArray transpose()
        {
            var nd = new NDArray(dtype, new Shape(this.Storage.Shape.Shapes.Reverse().ToArray()));

            if (ndim == 1)
            {
                nd.Storage = NDStorage.CreateByShapeAndType(dtype, new Shape(1, shape.Shapes[0]));
            }
            else if (ndim == 2)
            {
                for (int idx = 0; idx < nd.shape.Shapes[0]; idx++)
                    for (int jdx = 0; jdx < nd.shape.Shapes[1]; jdx++)
                        nd[idx, jdx] = this[jdx, idx];
            }
            else
            {
                throw new NotImplementedException();
            }

            return nd;
        }
    }

    public partial class NDArrayGeneric<T> 
    {
        public NDArrayGeneric<T> transpose()
        {
            var np = new NDArrayGeneric<T>();
            np.Data = new T[this.Data.Length];

            if (NDim == 1)
            {
                np.Shape = new Shape(1,Shape.Shapes[0]);
            }
            else 
            {
                np.Shape = new Shape(this.Shape.Shapes.Reverse().ToArray());
                for (int idx = 0;idx < np.shape.Shapes[0];idx++)
                    for (int jdx = 0;jdx < np.shape.Shapes[1];jdx++)
                        np[idx,jdx] = this[jdx,idx];
            }
            
            return np;
        }
    }
}
