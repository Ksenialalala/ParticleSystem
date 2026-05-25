using System;
using System.Collections.Generic;
using System.Drawing;
using static ParticleSystem.Emitter;

namespace ParticleSystem
{
    public class RadarPoint : IImpactPoint
    {
        public Color RadarColor = Color.Red;
        public int Power = 100;
        public bool ShowCount = false;

        private List<Particle> detectedParticles = new List<Particle>();

        public override void ImpactParticle(Particle particle)
        {
            float dx = X - particle.X;
            float dy = Y - particle.Y;
            double r = Math.Sqrt(dx * dx + dy * dy);

            if (r < Power / 2)
            {
                detectedParticles.Add(particle);
            }
        }

        public override void Render(Graphics g)
        {
            using (var pen = new Pen(RadarColor, 2))
            {
                g.DrawEllipse(pen, X - Power / 2, Y - Power / 2, Power, Power);
            }

            using (var pen = new Pen(RadarColor, 1))
            {
                foreach (var particle in detectedParticles)
                {
                    int markerRadius = particle.Radius + 3;
                    g.DrawEllipse(
                        pen,
                        particle.X - markerRadius,
                        particle.Y - markerRadius,
                        markerRadius * 2,
                        markerRadius * 2
                    );
                }
            }

            if (ShowCount)
            {
                string text = $"{detectedParticles.Count}";
                using (var font = new Font("Verdana", 10, FontStyle.Bold))
                {
                    var size = g.MeasureString(text, font);

                    using (var brushBg = new SolidBrush(RadarColor))
                    {
                        g.FillRectangle(brushBg, X - size.Width / 2, Y - size.Height / 2, size.Width, size.Height);
                    }

                    var stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    using (var brushText = new SolidBrush(Color.White))
                    {
                        g.DrawString(text, font, brushText, X, Y, stringFormat);
                    }
                }
            }

            detectedParticles.Clear();
        }
    }
}