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
    using Cloo;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class NBodyModelOpenCL : NBodyModel
    {

        public static NBodyModelOpenCL RandomNBodyModel(int bodyCount, int seed, double scaleFactor, double gravityFactor, double mmax, double xmax, double ymax, double zmax)
        {
            Random r = new Random(seed);
            double[] bodyMassPos = new double[bodyCount * 4];
            for (int i = 0; i < bodyCount; ++i)
            {
                bodyMassPos[(i * 4) + 3] = (r.NextDouble() + eps) * mmax;
                bodyMassPos[(i * 4)] = r.NextDouble() * xmax / 2 + xmax / 4;
                bodyMassPos[(i * 4) + 1] = r.NextDouble() * ymax / 2 + ymax / 4;
                bodyMassPos[(i * 4) + 2] = r.NextDouble() * zmax / 2 + zmax / 4;
            }
            return new NBodyModelOpenCL(bodyMassPos, scaleFactor, gravityFactor, xmax, ymax, zmax);
        }

        private IList<ComputeDevice> devices;
        private ComputeKernel kernel;
        private ComputeCommandQueue queue;
        private ComputeEventList events;

        private string sourceName = @".\NBody.cl";
        private string kernelName = "nbody";

        private double[] bodyMassPos;
        private double[] bodyMassPosNew;
        private double[] bodySpeed;
        private double[] bodyAccel;
        private ComputeContext context;

        public NBodyModelOpenCL(double[] bodyMassPos, double scaleFactor, double gravityFactor, double xmax, double ymax, double zmax) :
            this(bodyMassPos, new double[bodyMassPos.Length], scaleFactor, gravityFactor, xmax, ymax, zmax)
        {
        }

        public NBodyModelOpenCL(double[] bodyMassPos, double[] bodySpeed, double scaleFactor, double gravityFactor, double xmax, double ymax, double zmax) :
            base(bodyMassPos.Length / 4, scaleFactor, gravityFactor, xmax, ymax, zmax)
        {
            this.bodyMassPos = bodyMassPos;
            this.bodySpeed = bodySpeed;
            this.bodyAccel = new double[bodyMassPos.Length];
            this.bodyMassPosNew = new double[bodyMassPos.Length];

            var platform = ComputePlatform.Platforms[0];
            devices = new List<ComputeDevice>();
            devices.Add(platform.Devices[0]);
            var properties = new ComputeContextPropertyList(platform);
            context = new ComputeContext(devices, properties, null, IntPtr.Zero);
            var source = File.ReadAllText(sourceName);
            var program = new ComputeProgram(context, source);
            var statuses = new ComputeProgramBuildStatus[devices.Count];
            var buildLog = "";
            var success = true;
            try
            {
                program.Build(null, null, null, IntPtr.Zero);
            }
            catch
            {
                success = false;
                var sb = new StringBuilder();
                for (int i = 0; i < devices.Count; ++i)
                {
                    var device = devices[i];
                    statuses[i] = program.GetBuildStatus(device);
                    sb.Append("Device: ");
                    sb.AppendLine(device.Name);
                    sb.Append("Build status: ");
                    sb.AppendLine(program.GetBuildStatus(device).ToString());
                    sb.Append("Build log: ");
                    sb.AppendLine(program.GetBuildLog(devices[0]));
                }
                buildLog = sb.ToString();
            }
            if (!success)
            {
                throw new Exception(buildLog);
            }

            kernel = program.CreateKernel(kernelName);
            kernel.SetValueArgument<double>(0, eps);
            kernel.SetValueArgument<double>(1, scaleFactor);
            kernel.SetValueArgument<double>(2, gravityFactor);
            kernel.SetValueArgument<double>(3, xmax);
            kernel.SetValueArgument<double>(4, ymax);
            kernel.SetValueArgument<double>(5, zmax);
            queue = new ComputeCommandQueue(context, context.Devices[0], ComputeCommandQueueFlags.None);
            events = new ComputeEventList();
            bmp = new ComputeBuffer<double>(context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, bodyMassPos);
            bs = new ComputeBuffer<double>(context, ComputeMemoryFlags.CopyHostPointer, bodySpeed);
            ba = new ComputeBuffer<double>(context, ComputeMemoryFlags.CopyHostPointer, bodyAccel);
            bmpNew = new ComputeBuffer<double>(context, ComputeMemoryFlags.WriteOnly | ComputeMemoryFlags.CopyHostPointer, bodyMassPosNew);

            kernel.SetMemoryArgument(7, bmp);
            kernel.SetMemoryArgument(8, bs);
            kernel.SetMemoryArgument(9, ba);
            kernel.SetMemoryArgument(10, bmpNew);
            kernel.SetArgument(11, new IntPtr(n * 4 * sizeof(double)), IntPtr.Zero);

        }

        private ComputeBuffer<double> bmp;
        private ComputeBuffer<double> bs;
        private ComputeBuffer<double> ba;
        private ComputeBuffer<double> bmpNew;

        public override void Update(double dt)
        {
            kernel.SetValueArgument<double>(6, dt);

            events.Clear();

            queue.WriteToBuffer<double>(bodyMassPos, bmp, false, events);
            queue.WriteToBuffer<double>(bodySpeed, bs, false, events);
            queue.WriteToBuffer<double>(bodyAccel, ba, false, events);

            queue.Execute(kernel, null, new long[] { n }, null, events);

            queue.ReadFromBuffer(bs, ref bodySpeed, false, events);
            queue.ReadFromBuffer(ba, ref bodyAccel, false, events);
            queue.ReadFromBuffer(bmpNew, ref bodyMassPos, false, events);

            queue.Finish();

        }

        public override Tuple<double, double, double, double> this[int index]
        {
            get
            {
                index = index * 4;
                return new Tuple<double, double, double, double>(bodyMassPos[index + 3], bodyMassPos[index], bodyMassPos[index + 1], bodyMassPos[index + 2]);
            }
        }
    }
}