using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TelemetryViewer
{
    class TextBox : Component
    {
        private Texture2D background;
        private SpriteFont font;
        private Color fontColor;
        private string value;
        private Rectangle position;
        private Vector2 stringPosition;

        private Game game;

        public string Value { get => value; set => this.value = value; }
        public Rectangle Position { get => position; set => position = value; }
        public SpriteFont Font { get => font;}

        public void setColor(Color color)
        {
            background = new Texture2D(game.GraphicsDevice, 1, 1);
            Color[] colors = new Color[1];
            colors[0] = color;
            background.SetData<Color>(colors);
        }

        public TextBox (Game game, Rectangle position)
        {
            this.game = game;
            this.position = position;
        }

        private void updatePosition()
        {
            stringPosition = new Vector2(position.X + position.Width / 2 - font.MeasureString(value).X / 2,
                position.Y + position.Height / 2 - font.MeasureString(value).Y / 2);
        }

        public void Load(string assetNameBackground, string assetNameFont, Color fontColor)
        {
            background = game.Content.Load<Texture2D>(assetNameBackground);
            font = game.Content.Load<SpriteFont>(assetNameFont);
            this.fontColor = fontColor;
        }
        public void Load(Color backgroundColor, string assetNameFont, Color fontColor)
        {
            background = new Texture2D(game.GraphicsDevice,16,16);

            font = game.Content.Load<SpriteFont>(assetNameFont);

            Color[] colors = new Color[16 * 16];
            for(int i = 0; i < 16*16; i++)
            {
                colors[i] = backgroundColor;
            }
            background.SetData<Color>(colors);

            this.fontColor = fontColor;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                background,
                position,
                Color.White);
            spriteBatch.DrawString(
                font,
                value,
                stringPosition,
                fontColor);
        }

        public override void Update(GameTime gameTime)
        {
            updatePosition();
        }
    }
}
