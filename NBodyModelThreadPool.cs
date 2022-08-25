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
    using System.Threading;

    public class NBodyModelThreadPool : NBodyModel
    {
        private double[] xnew, ynew, znew;

        public static NBodyModelThreadPool RandomNBodyModel(int n, int seed, double scaleFactor, double gravityFactor, double mmax, double xmax, double ymax, double zmax)
        {
            Random r = new Random(seed);
            double[] m = new double[n];
            double[] x = new double[n];
            double[] y = new double[n];
            double[] z = new double[n];
            for (int i = 0; i < n; ++i)
            {
                m[i] = (r.NextDouble() + NBodyModel.eps) * mmax;
                x[i] = r.NextDouble() * xmax / 2 + xmax / 4;
                y[i] = r.NextDouble() * ymax / 2 + ymax / 4;
                z[i] = r.NextDouble() * zmax / 2 + zmax / 4;
            }
            return new NBodyModelThreadPool(m, x, y, z, scaleFactor, gravityFactor, xmax, ymax, zmax);
        }

        public NBodyModelThreadPool(double[] m, double[] x, double[] y, double[] z, double scaleFactor, double gravityFactor, double xmax, double ymax, double zmax) :
            this(m, x, y, z, new double[m.Length], new double[m.Length], new double[m.Length], scaleFactor, gravityFactor, xmax, ymax, zmax)
        {
        }

        public NBodyModelThreadPool(double[] m, double[] x, double[] y, double[] z, double[] vx, double[] vy, double[] vz, double scaleFactor, double gravityFactor, double xmax, double ymax, double zmax) :
            base(m, x, y, z, vx, vy, vz, scaleFactor, gravityFactor, xmax, ymax, zmax)
        {
            xnew = new double[n];
            ynew = new double[n];
            znew = new double[n];
        }

        public override void Update(double dt)
        {
            int n = this.n;

            ManualResetEvent done = new ManualResetEvent(false);
            int pendingCounter = n;
            for (int i = 0; i < n; ++i)
            {
                int ti = i;
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        double ax, ay, az, dx, dy, dz, dvx, dvy, dvz, invr, f, sqrt;
                        ax = 0.0;
                        ay = 0.0;
                        az = 0.0;
                        for (int j = 0; j < n; ++j)
                        {
                            dx = (x[j] - x[ti]) / scaleFactor;
                            dy = (y[j] - y[ti]) / scaleFactor;
                            dz = (z[j] - z[ti]) / scaleFactor;
                            sqrt = Math.Sqrt(dx * dx + dy * dy + dz * dz + NBodyModel.eps);
                            invr = 1.0 / sqrt;
                            f = m[j] * invr * invr * invr * gravityFactor;
                            ax += f * dx;
                            ay += f * dy;
                            az += f * dx;
                        }
                        dvx = dt * ax;
                        dvy = dt * ay;
                        dvz = dt * az;
                        this.ax[ti] = ax;
                        this.ay[ti] = ay;
                        this.az[ti] = az;
                        vx[ti] += dvx;
                        vy[ti] += dvy;
                        vz[ti] += dvz;
                        xnew[ti] = x[ti] + dt * vx[ti] + 0.5 * dt * dvx;
                        ynew[ti] = y[ti] + dt * vy[ti] + 0.5 * dt * dvy;
                        znew[ti] = z[ti] + dt * vz[ti] + 0.5 * dt * dvz;
                        Bounce(ti);
                    }
                    finally
                    {
                        if (Interlocked.Decrement(ref pendingCounter) == 0)
                        {
                            done.Set();
                        }
                    }
                });
            }
            done.WaitOne();
            for (int i = 0; i < n; ++i)
            {
                x[i] = xnew[i];
                y[i] = ynew[i];
                z[i] = znew[i];
            }
        }

        private void Bounce(int i)
        {
            if (xnew[i] < 0)
            {
                xnew[i] = -xnew[i];
                vx[i] = -vx[i];
                ax[i] = -ax[i];
            }
            else if (xnew[i] > xmax)
            {
                xnew[i] = (2 * xmax) - xnew[i];
                vx[i] = -vx[i];
                ax[i] = -ax[i];
            }
            if (ynew[i] < 0)
            {
                ynew[i] = -ynew[i];
                vy[i] = -vy[i];
                ay[i] = -ay[i];
            }
            else if (ynew[i] > ymax)
            {
                ynew[i] = (2 * ymax) - ynew[i];
                vy[i] = -vy[i];
                ay[i] = -ay[i];
            }
            if (znew[i] < 0)
            {
                znew[i] = -znew[i];
                vz[i] = -vz[i];
                az[i] = -az[i];
            }
            else if (znew[i] > zmax)
            {
                znew[i] = (2 * zmax) - znew[i];
                vz[i] = -vz[i];
                az[i] = -az[i];
            }
        }
    }
}