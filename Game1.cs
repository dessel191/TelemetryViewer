using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PCARS2_SharedMemory;
using System;
using System.Collections;

namespace TelemetryViewer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private PCARS2_SharedMemoryData telemetryData = null;
        private double fps;

        private ArrayList components;

        private StringBar StatusBar;
        

        private TextBox RTC;
        private TextBox airTemp;
        private TextBox airTempLabel;
        private TextBox trackTemp;
        private TextBox trackTempLabel;
        private TextBox waterTemp;
        private TextBox waterTempLabel;
        private TextBox waterPressure;
        private TextBox waterPressureLabel;
        private TextBox oilTemp;
        private TextBox oilTempLabel;
        private TextBox oilPressure;
        private TextBox oilPressureLabel;
        private TextBox fuelLevel;
        private TextBox fuelLevelLabel;
        private TextBox fuelUsage;
        private TextBox fuelUsageLabel;
        private TextBox flag;

        private RoundGauge tacho;
        private RoundGauge speedometer;
        private TextBox gear;
        private BarGauge clutch;
        private BarGauge brake;
        private BarGauge throtle;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            try
            {
                telemetryData = new PCARS2_SharedMemoryData();
            }
            catch
            {

            }
            components = new ArrayList();

            StatusBar = new StringBar(this);

            RTC = new TextBox(this,
                new Rectangle(
                    GraphicsDevice.Viewport.X,
                    GraphicsDevice.Viewport.Y,
                    GraphicsDevice.Viewport.Width / 4 - 5,
                    50));

            airTempLabel = new TextBox(this,
                new Rectangle(
                    GraphicsDevice.Viewport.X + 5,
                    RTC.Position.Y + RTC.Position.Height + 5,
                    (RTC.Position.Width / 2) - 5,
                    25));
            airTempLabel.Value = "Air Temp";

            airTemp = new TextBox(this,
                new Rectangle(
                    airTempLabel.Position.X,
                    airTempLabel.Position.Y + airTempLabel.Position.Height,
                    airTempLabel.Position.Width,
                    50));

            trackTempLabel = new TextBox(this,
                new Rectangle(
                    airTempLabel.Position.X + airTempLabel.Position.Width + 5,
                    RTC.Position.Y + RTC.Position.Height + 5,
                    (RTC.Position.Width / 2) - 10,
                    25));
            trackTempLabel.Value = "Track Temp";

            trackTemp = new TextBox(this,
                new Rectangle(
                    trackTempLabel.Position.X,
                    trackTempLabel.Position.Y + trackTempLabel.Position.Height,
                    trackTempLabel.Position.Width,
                    50));

            waterTempLabel = new TextBox(this,
                new Rectangle(
                    airTemp.Position.X,
                    airTemp.Position.Y + airTemp.Position.Height + 5,
                    airTempLabel.Position.Width,
                    airTempLabel.Position.Height));
            waterTempLabel.Value = "Water Temp";

            waterTemp = new TextBox(this,
                new Rectangle(
                    waterTempLabel.Position.X,
                    waterTempLabel.Position.Y + waterTempLabel.Position.Height,
                    waterTempLabel.Position.Width,
                    airTemp.Position.Height));

            waterPressureLabel = new TextBox(this,
                new Rectangle(
                    trackTemp.Position.X,
                    trackTemp.Position.Y + trackTemp.Position.Height + 5,
                    trackTempLabel.Position.Width,
                    trackTempLabel.Position.Height));
            waterPressureLabel.Value = "Water Pressure";

            waterPressure = new TextBox(this,
                new Rectangle(
                    waterPressureLabel.Position.X,
                    waterPressureLabel.Position.Y + waterPressureLabel.Position.Height,
                    waterPressureLabel.Position.Width,
                    trackTemp.Position.Height));

            oilTempLabel = new TextBox(this,
                new Rectangle(
                    waterTempLabel.Position.X,
                    waterTemp.Position.Y + waterTemp.Position.Height + 5,
                    waterTempLabel.Position.Width,
                    waterTempLabel.Position.Height));
            oilTempLabel.Value = "Oil Temp";

            oilTemp = new TextBox(this,
                new Rectangle(
                    oilTempLabel.Position.X,
                    oilTempLabel.Position.Y + oilTempLabel.Position.Height,
                    oilTempLabel.Position.Width,
                    waterTemp.Position.Height));

            oilPressureLabel = new TextBox(this,
                new Rectangle(
                    waterPressureLabel.Position.X,
                    waterPressure.Position.Y + waterPressure.Position.Height + 5,
                    waterPressureLabel.Position.Width,
                    waterPressureLabel.Position.Height));
            oilPressureLabel.Value = "Oil Pressure";

            oilPressure = new TextBox(this,
                new Rectangle(
                    oilPressureLabel.Position.X,
                    oilPressureLabel.Position.Y + oilPressureLabel.Position.Height,
                    oilPressureLabel.Position.Width,
                    waterPressure.Position.Height));

            fuelLevelLabel = new TextBox(this,
                new Rectangle(
                    oilTempLabel.Position.X,
                    oilTemp.Position.Y + oilTemp.Position.Height + 5,
                    oilTempLabel.Position.Width,
                    oilTempLabel.Position.Height));
            fuelLevelLabel.Value = "Fuel Level";

            fuelLevel = new TextBox(this,
                new Rectangle(
                    fuelLevelLabel.Position.X,
                    fuelLevelLabel.Position.Y + fuelLevelLabel.Position.Height,
                    fuelLevelLabel.Position.Width,
                    oilTemp.Position.Height));

            fuelUsageLabel = new TextBox(this,
                new Rectangle(
                    oilPressureLabel.Position.X,
                    oilPressure.Position.Y + oilPressure.Position.Height + 5,
                    oilPressureLabel.Position.Width,
                    oilPressureLabel.Position.Height));
            fuelUsageLabel.Value = "Fuel Usage";

            fuelUsage = new TextBox(this,
                new Rectangle(
                    fuelUsageLabel.Position.X,
                    fuelUsageLabel.Position.Y + fuelUsageLabel.Position.Height,
                    fuelUsageLabel.Position.Width,
                    oilPressure.Position.Height));
            fuelUsage.Value = "TO DO";

            flag = new TextBox(this,
                new Rectangle(
                    RTC.Position.X,
                    _graphics.GraphicsDevice.Viewport.Height * 2 / 3,
                    RTC.Position.Width,
                    _graphics.GraphicsDevice.Viewport.Height / 3));
            flag.Value = "";

            ///////////////////////////////////////////////////////

            tacho = new RoundGauge(this,
                new Rectangle(
                    GraphicsDevice.Viewport.Width / 4 + 5,
                    GraphicsDevice.Viewport.Y + 20,
                    GraphicsDevice.Viewport.Width / 5,
                    GraphicsDevice.Viewport.Width / 5),
                4);
            speedometer = new RoundGauge(this,
                new Rectangle(
                    GraphicsDevice.Viewport.Width - (GraphicsDevice.Viewport.Width / 4 + 5) - tacho.Position.Width,
                    tacho.Position.Y,
                    tacho.Position.Width,
                    tacho.Position.Height),
                3);

            gear = new TextBox(this,
                new Rectangle(
                    tacho.Position.X + tacho.Position.Width + 20,
                    tacho.Position.Y + tacho.Position.Height / 6,
                    (speedometer.Position.X - 20) - (tacho.Position.X + tacho.Position.Width + 20),
                    (speedometer.Position.X - 20) - (tacho.Position.X + tacho.Position.Width + 20)));

            clutch = new BarGauge(this, 
                Color.Blue,
                new Rectangle(
                    gear.Position.X,
                    gear.Position.Y + gear.Position.Height + 20,
                    gear.Position.Width / 4,
                    tacho.Position.Y + tacho.Position.Height - (gear.Position.Y + gear.Position.Height + 20)));

            brake = new BarGauge(this,
                Color.Red,
                new Rectangle(
                    gear.Position.X + gear.Position.Width / 3,
                    gear.Position.Y + gear.Position.Height + 20,
                    gear.Position.Width / 4,
                    tacho.Position.Y + tacho.Position.Height - (gear.Position.Y + gear.Position.Height + 20)));

            throtle = new BarGauge(this,
                Color.Green,
                new Rectangle(
                    gear.Position.X + gear.Position.Width - gear.Position.Width / 3,
                    gear.Position.Y + gear.Position.Height + 20,
                    gear.Position.Width / 4,
                    tacho.Position.Y + tacho.Position.Height - (gear.Position.Y + gear.Position.Height + 20)));
            //////////////////////////////////////////////////////

            if (telemetryData == null)
            {
                StatusBar.Message = "File Not Found";
                StatusBar.Status = false;
            }
            else
            {
                StatusBar.Message = "Connected";
                StatusBar.Status = true;
            }

            components.Add(RTC);
            components.Add(flag);
            components.Add(airTempLabel);
            components.Add(airTemp);
            components.Add(trackTempLabel);
            components.Add(trackTemp);
            components.Add(waterTempLabel);
            components.Add(waterTemp);
            components.Add(waterPressureLabel);
            components.Add(waterPressure);
            components.Add(oilTempLabel);
            components.Add(oilTemp);
            components.Add(oilPressureLabel);
            components.Add(oilPressure);
            components.Add(fuelLevelLabel);
            components.Add(fuelLevel);
            components.Add(fuelUsageLabel);
            components.Add(fuelUsage);

            components.Add(tacho);
            components.Add(speedometer);
            components.Add(gear);
            components.Add(clutch);
            components.Add(brake);
            components.Add(throtle);

            components.Add(StatusBar);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            StatusBar.Load("Fonts/BaseFont");

            RTC.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            airTempLabel.Load(Color.Gray, "Fonts/Small", Color.White);
            airTemp.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            trackTempLabel.Load(Color.Gray, "Fonts/Small", Color.White);
            trackTemp.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            waterTempLabel.Load(Color.Gray, "Fonts/Small", Color.White);
            waterTemp.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            waterPressureLabel.Load(Color.Gray, "Fonts/Small", Color.White);
            waterPressure.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            oilTempLabel.Load(Color.Gray, "Fonts/Small", Color.White);
            oilTemp.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            oilPressureLabel.Load(Color.Gray, "Fonts/Small", Color.White);
            oilPressure.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            fuelLevelLabel.Load(Color.Gray, "Fonts/Small", Color.White);
            fuelLevel.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            fuelUsageLabel.Load(Color.Gray, "Fonts/Small", Color.White);
            fuelUsage.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            flag.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            tacho.Load("Sprites/tacho", "Fonts/BaseFont", "Sprites/needle");
            speedometer.Load("Sprites/tacho", "Fonts/BaseFont", "Sprites/needle");

            gear.Load(Color.Gray, "Fonts/BaseFont", Color.White);

            clutch.Load();
            brake.Load();
            throtle.Load();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.RightShift) &&
                Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if(_graphics.IsFullScreen)
                {
                    _graphics.PreferredBackBufferWidth = 800;
                    _graphics.PreferredBackBufferHeight = 600;
                }
                else
                {
                    _graphics.PreferredBackBufferWidth = 1920;
                    _graphics.PreferredBackBufferHeight = 1080;
                }
                _graphics.ToggleFullScreen();
                Initialize();
            }
            
            // TODO: Add your update logic here
           
            if (telemetryData == null)
            { 
                try
                {
                    telemetryData = new PCARS2_SharedMemoryData();
                    StatusBar.Message = "Connected! Version: " + telemetryData.Version;
                    StatusBar.Status = true;
                }
                catch(Exception e)
                {
                    telemetryData = null;
                    StatusBar.Message = e.Message;
                }
            }
            else
            {
                StatusBar.Message = "Connected! Version: " + telemetryData.Version;

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    try
                    {
                        StatusBar.Message = telemetryData.Participants[0].Name;
                    }
                    catch (Exception e)
                    {
                        telemetryData.Dispose();
                        telemetryData = null;
                    }
                }

                tacho.Value = telemetryData.Rpm;
                tacho.MaxValue = telemetryData.MaxRPM;
                gear.Value = telemetryData.Gear.ToString();
                clutch.Value = telemetryData.Clutch;
                brake.Value = telemetryData.Brake;
                throtle.Value = telemetryData.Throttle;

                speedometer.Value = 18*telemetryData.Speed/5;
                speedometer.MaxValue = 320;

                airTemp.Value = telemetryData.AmbientTemperature.ToString("N0");
                trackTemp.Value = telemetryData.TrackTemperature.ToString("N0");
                waterTemp.Value = telemetryData.WaterTempCelsius.ToString("N0");
                waterPressure.Value = telemetryData.WaterPressureKPa.ToString("N0");
                oilTemp.Value = telemetryData.OilTempCelsius.ToString("N0");
                oilPressure.Value = telemetryData.OilPressureKPa.ToString("N0");
                fuelLevel.Value = (telemetryData.FuelLevel * telemetryData.FuelCapacity).ToString("N2") + "l";

                switch(telemetryData.HighestFlagColour)
                {
                    case (uint)PCARS2_SharedMemoryData.FlagColours.Black:
                        flag.setColor(Color.Black);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.BlackOrangeCircle:
                        flag.setColor(Color.Orange);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.BlackWhite:
                        flag.setColor(Color.LightGray);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.Blue:
                        flag.setColor(Color.Blue);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.Chequered:
                        flag.setColor(Color.DarkGray);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.DoubleYellow:
                        flag.setColor(Color.LightYellow);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.Green:
                        flag.setColor(Color.Green);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.None:
                        flag.setColor(Color.Transparent);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.Red:
                        flag.setColor(Color.Red);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.White_FinalLap:
                        flag.setColor(Color.White);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.White_SlowCar:
                        flag.setColor(Color.White);
                        break;
                    case (uint)PCARS2_SharedMemoryData.FlagColours.Yellow:
                        flag.setColor(Color.Yellow);
                        break;
                    default:                    
                        flag.setColor(Color.Transparent);
                        break;
                }

            }

            fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            StatusBar.Message += " FPS: " + fps.ToString("N2");

            RTC.Value = DateTime.Now.ToLocalTime().ToString("HH:mm:ss");

            foreach (Component component in components)
                component.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (Component component in components)
                component.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
