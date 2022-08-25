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
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ResetModel();
        }

        private NBodyModel model;
        private bool run;
        private Graphics graphics;
        private Task task = null;

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            lock (this)
            {
                if (run)
                {
                    Stop();
                }
                else
                {
                    Start();
                }
            }
        }

        public void Stop()
        {
            lock (this)
            {
                if (run)
                {
                    run = false;
                    if (task != null)
                    {
                        task.Wait();
                        task = null;
                    }
                }
            }
        }

        public void Start()
        {
            lock (this)
            {
                if (!run)
                {
                    Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height, Graphics.FromHwnd(pictureBox.Handle));
                    pictureBox.Image = bitmap;
                    graphics = Graphics.FromImage(bitmap);
                    run = true;
                    task = Task.Run(
                        () => 
                        { 
                            this.Run(); 
                        }
                    );
                }
            }
        }

        public void Run()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            double last = stopwatch.Elapsed.TotalSeconds;
            double now;
            while (run)
            {
                now = stopwatch.Elapsed.TotalSeconds;
                double dt = now - last;

                model.Update(dt);

                if (checkBoxDraw.Checked)
                {
                    Draw(model, graphics);
                }

                last = now;
                int fps = (int)(1 / dt);
                try
                {
                    if (this.InvokeRequired)
                    {
                        var asyncWait = textBoxFPS.BeginInvoke((MethodInvoker)delegate() { textBoxFPS.Text = "FPS: " + fps; });
                        asyncWait.AsyncWaitHandle.WaitOne(50);
                    }
                    else
                    {
                        textBoxFPS.Text = "FPS: " + fps;
                    }
                }
                catch { }
            }
        }



        private void Draw(NBodyModel model, Graphics graphics)
        {
            graphics.Clear(Color.White);
            double massScale = 10;
            for (int i = 0; i < model.N; ++i)
            {
                var tuple = model[i];
                float x = (float)((tuple.Item2));
                float y = (float)((tuple.Item3));
                float ratio = (float)(Math.Sqrt(tuple.Item1) * massScale);
                float halfRatio = ratio / 2;
                graphics.DrawEllipse(Pens.Black, x - halfRatio, y - halfRatio, ratio, ratio);
            }
            try
            {
                if (this.InvokeRequired)
                {
                    var result = pictureBox.BeginInvoke((MethodInvoker)delegate() { pictureBox.Refresh(); });
                    result.AsyncWaitHandle.WaitOne(50);
                }
                else
                {
                    pictureBox.Refresh();
                }
            }
            catch
            {
                Stop();
            }
        }

        private void nBodyCount_ValueChanged(object sender, EventArgs e)
        {
            ResetModel();
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            ResetModel();
        }

        private void radioButtonSingleThreaded_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSingleThreaded.Checked)
            {
                ResetModel();
            }
        }

        private void radioButtonThreadPool_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMultiThreaded.Checked)
            {
                ResetModel();
            }
        }

        private void radioButtonOpenCL_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOpenCL.Checked)
            {
                ResetModel();
            }
        }

        private void ResetModel()
        {
            Stop();
            if (radioButtonSingleThreaded.Checked)
            {
                model = NBodyModelSingleThreaded.RandomNBodyModel((int)nBodyCount.Value, (int)nBodySeed.Value, (double)numericUpDownScale.Value, (double)numericUpDownGravity.Value, 1.0, pictureBox.Width, pictureBox.Height, 0.0);
            }
            else if (radioButtonMultiThreaded.Checked)
            {
                model = NBodyModelThreadPool.RandomNBodyModel((int)nBodyCount.Value, (int)nBodySeed.Value, (double)numericUpDownScale.Value, (double)numericUpDownGravity.Value, 1.0, pictureBox.Width, pictureBox.Height, 0.0);
            }
            else
            {
                try
                {
                    model = NBodyModelOpenCL.RandomNBodyModel((int)nBodyCount.Value, (int)nBodySeed.Value, (double)numericUpDownScale.Value, (double)numericUpDownGravity.Value, 1.0, pictureBox.Width, pictureBox.Height, 0.0);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void numericUpDownGravity_ValueChanged(object sender, EventArgs e)
        {
            ResetModel();
        }

        private void numericUpDownScale_ValueChanged(object sender, EventArgs e)
        {
            ResetModel();
        }
    }
}
