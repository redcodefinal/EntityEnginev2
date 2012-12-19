namespace EntityEnginev2.Input
{
    public class Input
    {
        public int HoldTime;

        public virtual bool Released()
        {
            return false;
        }

        public virtual bool Pressed()
        {
            return false;
        }

        public virtual bool Down()
        {
            return false;
        }

        public virtual bool Up()
        {
            return false;
        }

        /// <summary>
        /// Will return true if the button is down and a certian amount of time has passed
        /// </summary>
        /// <param name="milliseconds">The milliseconds between firing.</param>
        /// <returns></returns>
        public virtual bool RapidFire(int milliseconds)
        {
            if (Pressed())
            {
                if (HoldTime == 0)
                {
                    HoldTime = 1;
                    return true;
                }
            }

            if (Down())
            {
                HoldTime += InputHandler.Gametime.ElapsedGameTime.Milliseconds;

                if (HoldTime > milliseconds)
                {
                    HoldTime = 0;
                    return true;
                }
            }

            else if (Up())
            {
                if(HoldTime != 0)
                {
                    HoldTime += InputHandler.Gametime.ElapsedGameTime.Milliseconds;
                    if (HoldTime > milliseconds)
                    {
                        HoldTime = 0;
                    }
                }
            }
            return false;
        }
    }
}