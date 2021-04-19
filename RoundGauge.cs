using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TelemetryViewer
{
    class RoundGauge : Component
    {
        private const float maxValueScale = 1.2f;

        private Texture2D background;
        private Texture2D needle;
        private Rectangle positionBackground;
        private Rectangle positionNeedle;
        private Vector2 rotationOrigin;
        private float rotation;
        private float value = 0;
        private float maxValue = 100;
        private TextBox box;
        private int decimals;

        private Game game;

        public float Value { get => value; set => this.value = value; }
        public float MaxValue { get => maxValue; set => maxValue = value * maxValueScale; }
        public Rectangle Position { get => positionBackground;}

        public RoundGauge(Game game, Rectangle position, int decimals)
        {
            this.game = game;
            this.positionBackground = position;
            box = new TextBox(game, new Rectangle());
            this.decimals = decimals;
        }

        private void updatePosition()
        {
            float scale = (float)positionBackground.Height / (float)needle.Height;

            positionNeedle = new Rectangle(
                positionBackground.X + (positionBackground.Width / 2),
                positionBackground.Y + positionBackground.Height / 2,
                (int)(needle.Width*scale)/3,
                (int)(needle.Height*scale)/3);

            rotation = (float)((-3 * (float)Math.PI / 4) + 
                (value / maxValue * (3 * Math.PI / 2)));

            rotationOrigin = new Vector2(
                needle.Width/2,
                needle.Height - (float)(needle.Height*0.142));

            string temp = "";
            for (int i = 0; i < decimals; i++)
                temp += "Z";


            box.Position = new Rectangle(
                positionBackground.X + positionBackground.Width/2 - (int)(box.Font.MeasureString(temp).X / 2),
                positionBackground.Y + (positionBackground.Height / 3 * 2) - (int)(box.Font.MeasureString(temp).Y / 2),
                (int)box.Font.MeasureString(temp).X,
                (int)box.Font.MeasureString(temp).Y);
        }

        public void Load(string assetNameTacho,string assetNameFont, string assetNameNeedle)
        {
            background = game.Content.Load<Texture2D>(assetNameTacho);
            needle = game.Content.Load<Texture2D>(assetNameNeedle);
            box.Load(Color.Transparent, assetNameFont, Color.White);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                background,
                positionBackground,
                Color.White);
            spriteBatch.Draw(
                needle,
                positionNeedle,
                new Rectangle(0, 0, needle.Width, needle.Height),
                Color.White,
                rotation,
                rotationOrigin,
                SpriteEffects.None,
                0.0f);
            box.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            box.Value = MathF.Floor(value).ToString();
            box.Update(gameTime);
            updatePosition();
        }
    }
}
