using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleSystem
{
    public class RainbowPoint : Emitter.GravityPoint
    {
        public Color RadarColor = Color.Red;

        public override void Render(Graphics g)
        {
            g.DrawEllipse(
                new Pen(RadarColor),
                X - Power / 2,
                Y - Power / 2,
                Power,
                Power
            );
        }

        public override void ImpactParticle(Particle particle)
        {
            float dx = X - particle.X;
            float dy = Y - particle.Y;

            double r = Math.Sqrt(dx * dx + dy * dy);

            if (r + particle.Radius < Power / 2)
            {
                particle.FromColor = RadarColor;
                particle.ToColor = RadarColor;
            }
        }
    }
}
