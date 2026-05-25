using System.Diagnostics.Metrics;
using static ParticleSystem.Emitter;

namespace ParticleSystem
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;

        RadarPoint point1;
        RadarPoint r1, r2, r3, r4, r5, r6, r7;

        public Form1()
        {
            InitializeComponent();
            picDisplay.MouseWheel += picDisplay_MouseWheel;
            picDisplay.Focus();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            this.emitter = new Emitter
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height - 180,

                Direction = 90,

                Spreading = 140,

                SpeedMin = 8,
                SpeedMax = 12,

                GravitationY = 0.35f,

                RadiusMin = 3,
                RadiusMax = 10,

                LifeMin = 80,
                LifeMax = 120,

                ParticlesPerTick = 22,

                ParticlesCount = 700,

                ColorFrom = Color.Gray,
                ColorTo = Color.FromArgb(0, Color.Gray)
            };

            emitters.Add(this.emitter);

            point1 = new RadarPoint
            {
                X = picDisplay.Width / 2 + 100,
                Y = picDisplay.Height / 2,
                Power = 100,
                RadarColor = Color.Red,
                ShowCount = true
            };

            emitter.impactPoints.Add(point1);

            int baseX = picDisplay.Width / 2 - 180;
            int y = picDisplay.Height - 60;

            r1 = new RadarPoint { X = baseX + 0 * 60, Y = y, Power = 50 };
            r2 = new RadarPoint { X = baseX + 1 * 60, Y = y, Power = 50 };
            r3 = new RadarPoint { X = baseX + 2 * 60, Y = y, Power = 50 };
            r4 = new RadarPoint { X = baseX + 3 * 60, Y = y, Power = 50 };
            r5 = new RadarPoint { X = baseX + 4 * 60, Y = y, Power = 50 };
            r6 = new RadarPoint { X = baseX + 5 * 60, Y = y, Power = 50 };
            r7 = new RadarPoint { X = baseX + 6 * 60, Y = y, Power = 50 };

            r1.RadarColor = Color.Red;
            r2.RadarColor = Color.Orange;
            r3.RadarColor = Color.Yellow;
            r4.RadarColor = Color.Green;
            r5.RadarColor = Color.Blue;
            r6.RadarColor = Color.Indigo;
            r7.RadarColor = Color.Violet;

            emitter.impactPoints.Add(r1);
            emitter.impactPoints.Add(r2);
            emitter.impactPoints.Add(r3);
            emitter.impactPoints.Add(r4);
            emitter.impactPoints.Add(r5);
            emitter.impactPoints.Add(r6);
            emitter.impactPoints.Add(r7);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
            }
            picDisplay.Invalidate();

        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var emitter in emitters)
            {
                emitter.MousePositionX = e.X;
                emitter.MousePositionY = e.Y;
            }

            point1.X = e.X;
            point1.Y = e.Y;
        }

        private void picDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                point1.Power += 10;
            else
                point1.Power -= 10;

            if (point1.Power < 20)
                point1.Power = 20;

            if (point1.Power > 300)
                point1.Power = 300;
        }

        private void tbParticles_Scroll(object sender, EventArgs e)
        {
            emitter.ParticlesPerTick = tbParticles.Value;

            lblParticles.Text = $"Частиц: {tbParticles.Value}";
        }
    }
}