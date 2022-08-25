// Copyright (C) 2013 Andrea Esuli (andrea@esuli.it)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Esuli.NBodyDemo
{
    using System;

    public abstract class NBodyModel
    {
        public static double eps = 1E-2;

        protected int n;
        protected double[] m, x, y, z, vx, vy, vz, ax, ay, az;
        protected double scaleFactor, gravityFactor, xmax, ymax, zmax;

        public NBodyModel(int n, double scaleFactor, double gravityFactor, double xmax, double ymax, double zmax)
        {
            this.n = n;
            this.scaleFactor = scaleFactor;
            this.gravityFactor = gravityFactor;
            this.xmax = xmax;
            this.ymax = ymax;
            this.zmax = zmax;
        }

        public NBodyModel(double[] m, double[] x, double[] y, double[] z, double scaleFactor, double gravityFactor, double xmax, double ymax, double zmax) :
            this(m, x, y, z, new double[m.Length], new double[m.Length], new double[m.Length], scaleFactor, gravityFactor, xmax, ymax, zmax)
        {
        }

        public NBodyModel(double[] m, double[] x, double[] y, double[] z, double[] vx, double[] vy, double[] vz, double scaleFactor, double gravityFactor, double xmax, double ymax, double zmax)
        {
            this.n = m.Length;
            this.m = m;
            this.x = x;
            this.y = y;
            this.z = z;
            this.vx = vx;
            this.vy = vy;
            this.vz = vz;
            ax = new double[n];
            ay = new double[n];
            az = new double[n];
            this.scaleFactor = scaleFactor;
            this.gravityFactor = gravityFactor;
            this.xmax = xmax;
            this.ymax = ymax;
            this.zmax = zmax;
        }

        public int N
        {
            get
            {
                return n;
            }
        }

        public virtual Tuple<double, double, double, double> this[int index]
        {
            get
            {
                return new Tuple<double, double, double, double>(m[index], x[index], y[index], z[index]);
            }
        }

        public abstract void Update(double dt);
    }
}
