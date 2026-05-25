using System.Diagnostics.Metrics;
using static ParticleSystem.Emitter;

namespace ParticleSystem
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;


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



            int baseX = picDisplay.Width / 2 - 180;
            int y = picDisplay.Height - 60;


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


        }

        private void picDisplay_MouseWheel(object sender, MouseEventArgs e)
        {

        }

        private void tbParticles_Scroll(object sender, EventArgs e)
        {
            emitter.ParticlesPerTick = tbParticles.Value;
        }
    }
}