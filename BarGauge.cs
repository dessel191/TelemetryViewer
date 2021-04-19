using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TelemetryViewer
{
    class BarGauge : Component
    {
        private Texture2D background;
        private Rectangle positionBackground;
        private Rectangle origin;
        private float value;
        private int scale;
        private Game game;
        private Color color;

        public float Value { get => value; set => this.value = value; }
        public Rectangle Position{ get => positionBackground; }

        public BarGauge(Game game, Color color, Rectangle position)
        {
            this.game = game;
            this.color = color;
            this.origin = position;;
        }

        private void updatePosition()
        {
            scale = (int)((origin.Height) * (1-value));

            positionBackground = new Rectangle(
                origin.X,
                origin.Y+scale,
                origin.Width, 
                origin.Height - scale);
        }

        public void Load()
        {
            background = new Texture2D(game.GraphicsDevice, 16, 128);
            Color[] DataColor = new Color[16 * 128];
            for(int i = 0; i<DataColor.Length; i++)
            {
                DataColor[i] = color;
            }
            background.SetData<Color>(DataColor);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, positionBackground, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            updatePosition();
        }

    }
}
