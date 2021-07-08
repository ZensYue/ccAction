//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ccAction
{
    public static class CCEaseFunc
    {
        const float M_PI = 3.14159265358f;
        const float M_PI_X_2 = M_PI * 2;
        const float M_PI_2 = 1.57079632679f;

        private static float cosf(float f)
        {
            return Mathf.Cos(f);
        }

        private static float sinf(float f)
        {
            return Mathf.Sin(f);
        }

        private static float powf(float f,float p)
        {
            return Mathf.Pow(f, p);
        }

        private static float sqrt(float f)
        {
            return Mathf.Sqrt(f);
        }

        // Linear
        public static float Linear(float time)
        {
            return time;
        }

        // Sine Ease
        public static float SineEaseIn(float time)
        {
            return -1 * cosf(time * (float)M_PI_2) + 1;
        }

        public static float SineEaseOut(float time)
        {
            return sinf(time * (float)M_PI_2);
        }

        public static float SineEaseInOut(float time)
        {
            return -0.5f * (cosf((float)M_PI * time) - 1);
        }


        // Quad Ease
        public static float QuadEaseIn(float time)
        {
            return time * time;
        }

        public static float QuadEaseOut(float time)
        {
            return -1 * time * (time - 2);
        }

        public static float QuadEaseInOut(float time)
        {
            time = time * 2;
            if (time < 1)
                return 0.5f * time * time;
            --time;
            return -0.5f * (time * (time - 2) - 1);
        }



        // Cubic Ease
        public static float CubicEaseIn(float time)
        {
            return time * time * time;
        }
        public static float CubicEaseOut(float time)
        {
            time -= 1;
            return (time * time * time + 1);
        }
        public static float CubicEaseInOut(float time)
        {
            time = time * 2;
            if (time < 1)
                return 0.5f * time * time * time;
            time -= 2;
            return 0.5f * (time * time * time + 2);
        }


        // Quart Ease
        public static float QuartEaseIn(float time)
        {
            return time * time * time * time;
        }

        public static float QuartEaseOut(float time)
        {
            time -= 1;
            return -(time * time * time * time - 1);
        }

        public static float QuartEaseInOut(float time)
        {
            time = time * 2;
            if (time < 1)
                return 0.5f * time * time * time * time;
            time -= 2;
            return -0.5f * (time * time * time * time - 2);
        }


        // Quint Ease
        public static float QuintEaseIn(float time)
        {
            return time * time * time * time * time;
        }

        public static float QuintEaseOut(float time)
        {
            time -= 1;
            return (time * time * time * time * time + 1);
        }

        public static float QuintEaseInOut(float time)
        {
            time = time * 2;
            if (time < 1)
                return 0.5f * time * time * time * time * time;
            time -= 2;
            return 0.5f * (time * time * time * time * time + 2);
        }


        // Expo Ease
        public static float ExpoEaseIn(float time)
        {
            return time == 0 ? 0 : powf(2, 10 * (time / 1 - 1)) - 1 * 0.001f;
        }
        public static float ExpoEaseOut(float time)
        {
            return time == 1 ? 1 : (-powf(2, -10 * time / 1) + 1);
        }
        public static float ExpoEaseInOut(float time)
        {
            time /= 0.5f;
            if (time < 1)
            {
                time = 0.5f * powf(2, 10 * (time - 1));
            }
            else
            {
                time = 0.5f * (-powf(2, -10 * (time - 1)) + 2);
            }

            return time;
        }


        // Circ Ease
        public static float CircEaseIn(float time)
        {
            return -1 * (sqrt(1 - time * time) - 1);
        }
        public static float CircEaseOut(float time)
        {
            time = time - 1;
            return sqrt(1 - time * time);
        }
        public static float CircEaseInOut(float time)
        {
            time = time * 2;
            if (time < 1)
                return -0.5f * (sqrt(1 - time * time) - 1);
            time -= 2;
            return 0.5f * (sqrt(1 - time * time) + 1);
        }


        // Elastic Ease
        public static float ElasticEaseIn(float time, float period)
        {

            float newT = 0;
            if (time == 0 || time == 1)
            {
                newT = time;
            }
            else
            {
                float s = period / 4;
                time = time - 1;
                newT = -powf(2, 10 * time) * sinf((time - s) * M_PI_X_2 / period);
            }

            return newT;
        }
        public static float ElasticEaseOut(float time, float period)
        {

            float newT = 0;
            if (time == 0 || time == 1)
            {
                newT = time;
            }
            else
            {
                float s = period / 4;
                newT = powf(2, -10 * time) * sinf((time - s) * M_PI_X_2 / period) + 1;
            }

            return newT;
        }
        public static float ElasticEaseInOut(float time, float period)
        {

            float newT = 0;
            if (time == 0 || time == 1)
            {
                newT = time;
            }
            else
            {
                time *= 2;
                if (period == 0)
                {
                    period = 0.3f * 1.5f;
                }

                float s = period / 4;

                time = time - 1;
                if (time < 0)
                {
                    newT = -0.5f * powf(2, 10 * time) * sinf((time - s) * M_PI_X_2 / period);
                }
                else
                {
                    newT = powf(2, -10 * time) * sinf((time - s) * M_PI_X_2 / period) * 0.5f + 1;
                }
            }
            return newT;
        }


        // Back Ease
        public static float BackEaseIn(float time)
        {
            float overshoot = 1.70158f;
            return time * time * ((overshoot + 1) * time - overshoot);
        }
        public static float BackEaseOut(float time)
        {
            float overshoot = 1.70158f;

            time = time - 1;
            return time * time * ((overshoot + 1) * time + overshoot) + 1;
        }
        public static float BackEaseInOut(float time)
        {
            float overshoot = 1.70158f * 1.525f;

            time = time * 2;
            if (time < 1)
            {
                return (time * time * ((overshoot + 1) * time - overshoot)) / 2;
            }
            else
            {
                time = time - 2;
                return (time * time * ((overshoot + 1) * time + overshoot)) / 2 + 1;
            }
        }



        // Bounce Ease
        static float BounceTime(float time)
        {
            if (time < 1 / 2.75)
            {
                return 7.5625f * time * time;
            }
            else if (time < 2 / 2.75)
            {
                time -= 1.5f / 2.75f;
                return 7.5625f * time * time + 0.75f;
            }
            else if (time < 2.5 / 2.75)
            {
                time -= 2.25f / 2.75f;
                return 7.5625f * time * time + 0.9375f;
            }

            time -= 2.625f / 2.75f;
            return 7.5625f * time * time + 0.984375f;
        }
        public static float BounceEaseIn(float time)
        {
            return 1 - BounceTime(1 - time);
        }

        public static float BounceEaseOut(float time)
        {
            return BounceTime(time);
        }

        public static float BounceEaseInOut(float time)
        {
            float newT = 0;
            if (time < 0.5f)
            {
                time = time * 2;
                newT = (1 - BounceTime(1 - time)) * 0.5f;
            }
            else
            {
                newT = BounceTime(time * 2 - 1) * 0.5f + 0.5f;
            }

            return newT;
        }


        

        public static float EaseIn(float time, float rate)
        {
            return powf(time, rate);
        }

        public static float EaseOut(float time, float rate)
        {
            return powf(time, 1 / rate);
        }

        public static float EaseInOut(float time, float rate)
        {
            time *= 2;
            if (time < 1)
            {
                return 0.5f * powf(time, rate);
            }
            else
            {
                return (1.0f - 0.5f * powf(2 - time, rate));
            }
        }

        public static float QuadraticIn(float time)
        {
            return powf(time, 2);
        }

        public static float QuadraticOut(float time)
        {
            return -time * (time - 2);
        }

        public static float QuadraticInOut(float time)
        {

            float resultTime = time;
            time = time * 2;
            if (time < 1)
            {
                resultTime = time * time * 0.5f;
            }
            else
            {
                --time;
                resultTime = -0.5f * (time * (time - 2) - 1);
            }
            return resultTime;
        }

        // Custom Ease
        public static float CustomEase(float time, float a, float b, float c, float d)
        {
            float tt = 1 - time;
            return a * tt * tt * tt + 3 * b * time * tt * tt + 3 * c * time * time * tt + d * time * time * time;
        }

        public static float BezieratEase(float t,float a, float b, float c, float d)
        {
            return (powf(1 - t, 3) * a + 3 * t * (powf(1 - t, 2)) * b + 3 * powf(t, 2) * (1 - t) * c + powf(t, 3) * d);
        }

    }
}
