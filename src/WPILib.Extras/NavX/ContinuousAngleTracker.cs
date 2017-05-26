using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    class ContinuousAngleTracker
    {
        private readonly object lockObject = new object();
        private bool fFirstUse;
        private double gyro_prevVal;
        private int ctrRollOver;
        float curr_yaw_angle;
        float last_yaw_angle;
        double angleAdjust;

        public ContinuousAngleTracker()
        {
            Init();
            angleAdjust = 0.0f;
        }

        private void Init()
        {
            gyro_prevVal = 0.0;
            ctrRollOver = 0;
            fFirstUse = true;
            last_yaw_angle = 0.0f;
            curr_yaw_angle = 0.0f;
        }

        public void NextAngle(float newAngle)
        {
            lock(lockObject){
                last_yaw_angle = curr_yaw_angle;
                curr_yaw_angle = newAngle;
            }
        }

        /* Invoked (internally) whenever yaw reset occurs. */
        public void Reset()
        {
            lock(lockObject){
                Init();
            }
        }

        public double GetAngle()
        {
            // First case
            // Old reading: +150 degrees
            // New reading: +170 degrees
            // Difference:  (170 - 150) = +20 degrees

            // Second case
            // Old reading: -20 degrees
            // New reading: -50 degrees
            // Difference : (-50 - -20) = -30 degrees 

            // Third case
            // Old reading: +179 degrees
            // New reading: -179 degrees
            // Difference:  (-179 - 179) = -358 degrees

            // Fourth case
            // Old reading: -179  degrees
            // New reading: +179 degrees
            // Difference:  (+179 - -179) = +358 degrees

            double difference;
            double gyroVal;
            double yawVal;

            lock(lockObject) {
                yawVal = curr_yaw_angle;

                // Has gyro_prevVal been previously set?
                // If not, return do not calculate, return current value
                if (!fFirstUse)
                {
                    // Determine count for rollover counter
                    difference = yawVal - gyro_prevVal;

                    /* Clockwise past +180 degrees
                     * If difference > 180*, increment rollover counter */
                    if (difference < -180.0)
                    {
                        ctrRollOver++;

                        /* Counter-clockwise past -180 degrees:
                         * If difference > 180*, decrement rollover counter */
                    }
                    else if (difference > 180.0)
                    {
                        ctrRollOver--;
                    }
                }

                // Mark gyro_prevVal as being used
                fFirstUse = false;

                // Calculate value to return back to calling function
                // e.g. +720 degrees or -360 degrees
                gyroVal = yawVal + (360.0 * ctrRollOver);
                gyro_prevVal = yawVal;

                return gyroVal + angleAdjust;
            }
        }

        public void SetAngleAdjustment(double adjustment)
        {
            angleAdjust = adjustment;
        }

        public double GetAngleAdjustment()
        {
            return angleAdjust;
        }

        public double GetRate()
        {
            float difference;
            lock(lockObject) {
                difference = curr_yaw_angle - last_yaw_angle;
            }
            if (difference > 180.0f)
            {
                /* Clockwise past +180 degrees */
                difference = 360.0f - difference;
            }
            else if (difference < -180.0f)
            {
                /* Counter-clockwise past -180 degrees */
                difference = 360.0f + difference;
            }
            return difference;
        }
    }
}
