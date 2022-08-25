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

    public class NBodyModelSingleThreaded : NBodyModel
    {
        private double[] xnew, ynew, znew;

        public static NBodyModelSingleThreaded RandomNBodyModel(int n, int seed, double scaleFactor, double gravityFactor, double mmax, double xmax, double ymax, double zmax)
        {
            Random r = new Random(seed);
            double[] m = new double[n];
            double[] x = new double[n];
            double[] y = new double[n];
            double[] z = new double[n];
            for (int i = 0; i < n; ++i)
            {
                m[i] = (r.NextDouble() + eps) * mmax;
                x[i] = r.NextDouble() * xmax / 2 + xmax / 4;
                y[i] = r.NextDouble() * ymax / 2 + ymax / 4;
                z[i] = r.NextDouble() * zmax / 2 + zmax / 4;
            }
            return new NBodyModelSingleThreaded(m, x, y, z, scaleFactor, gravityFactor, xmax, ymax, zmax);
        }

        public NBodyModelSingleThreaded(double[] m, double[] x, double[] y, double[] z, double scaleFactor, double gravityFactor, double xmax, double ymax, double zmax) :
            this(m, x, y, z, new double[m.Length], new double[m.Length], new double[m.Length], scaleFactor, gravityFactor, xmax, ymax, zmax)
        {
        }

        public NBodyModelSingleThreaded(double[] m, double[] x, double[] y, double[] z, double[] vx, double[] vy, double[] vz, double scaleFactor, double gravityFactor, double xmax, double ymax, double zmax) :
            base(m, x, y, z, vx, vy, vz, scaleFactor, gravityFactor, xmax, ymax, zmax)
        {
            xnew = new double[n];
            ynew = new double[n];
            znew = new double[n];
        }

        public override void Update(double dt)
        {
            int n = this.n;
            double ax, ay, az, dx, dy, dz, dvx, dvy, dvz, invr, f, sqrt;
            for (int i = 0; i < n; ++i)
            {
                ax = 0.0;
                ay = 0.0;
                az = 0.0;
                for (int j = 0; j < n; ++j)
                {
                    dx = (x[j] - x[i]) / scaleFactor;
                    dy = (y[j] - y[i]) / scaleFactor;
                    dz = (z[j] - z[i]) / scaleFactor;
                    sqrt = Math.Sqrt(dx * dx + dy * dy + dz * dz + eps);
                    invr = 1.0 / sqrt;
                    f = m[j] * invr * invr * invr * gravityFactor;
                    ax += f * dx;
                    ay += f * dy;
                    az += f * dx;
                }
                dvx = dt * ax;
                dvy = dt * ay;
                dvz = dt * az;
                this.ax[i] = ax;
                this.ay[i] = ay;
                this.az[i] = az;
                vx[i] += dvx;
                vy[i] += dvy;
                vz[i] += dvz;
                xnew[i] = x[i] + dt * vx[i] + 0.5 * dt * dvx;
                ynew[i] = y[i] + dt * vy[i] + 0.5 * dt * dvy;
                znew[i] = z[i] + dt * vz[i] + 0.5 * dt * dvz;
            }

            for (int i = 0; i < n; ++i)
            {
                x[i] = xnew[i];
                y[i] = ynew[i];
                z[i] = znew[i];
            }
            Bounce();
        }

        private void Bounce()
        {
            for (int i = 0; i < n; ++i)
            {
                if (x[i] < 0)
                {
                    x[i] = -x[i];
                    vx[i] = -vx[i];
                    ax[i] = -ax[i];
                }
                if (x[i] > xmax)
                {
                    x[i] = (2 * xmax) - x[i];
                    vx[i] = -vx[i];
                    ax[i] = -ax[i];
                }
                if (y[i] < 0)
                {
                    y[i] = -y[i];
                    vy[i] = -vy[i];
                    ay[i] = -ay[i];
                }
                if (y[i] > ymax)
                {
                    y[i] = (2 * ymax) - y[i];
                    vy[i] = -vy[i];
                    ay[i] = -ay[i];
                }
                if (z[i] < 0)
                {
                    z[i] = -z[i];
                    vz[i] = -vz[i];
                    az[i] = -az[i];
                }
                if (z[i] > zmax)
                {
                    z[i] = (2 * zmax) - z[i];
                    vz[i] = -vz[i];
                    az[i] = -az[i];
                }
            }
        }
    }
}