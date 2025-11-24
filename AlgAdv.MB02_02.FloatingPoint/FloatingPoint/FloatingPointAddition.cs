using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloatingPoint
{
    public class FloatingPointAddition
    {
        public float Addition(int a, int b) 
        {

            //extract fields
            int signA = (int)(a >> 31);
            int expA = (int)(a >> 23) & 0xFF;
            int mantA = (int)(a & 0x7FFFFF) | (expA != 0 ? 0x800000 : 0);

            int signB = (int)(b >> 31);
            int expB = (int)((b >> 23) & 0xFF);
            int mantB = (int)(b & 0x7FFFFF) | (expB != 0 ? 0x800000 : 0);

            int delta = expA - expB;
            if (delta > 0)
            {
                mantB >>= delta;
                expB += delta;
            }
            else if (delta < 0)
            {
                mantA >>= -delta;
                expA += -delta;
            }
            // Add mantissas using bitwise integer addition
            int mantissaResult = BitwiseAdd(mantA, mantB); 

            // Normalize and reassemble (simplistic, missing rounding & edge cases)
            int expRes = expA;
            while ((mantissaResult & 0xFF000000) != 0)
            {
                mantissaResult >>= 1;
                expRes++;
            }
            // remove implicit leading bit
            mantissaResult &= 0x7FFFFF;

            uint result = ((uint)signA << 31) | ((uint)expRes << 23) | ((uint)mantissaResult);
            return result;

        }
        int BitwiseAdd(int a, int b)
        {
            while (b != 0)
            {
                int carry = a & b;      // Bits that need to be carried to the next higher bit
                a = a ^ b;              // Sum without carry
                b = carry << 1;         // Prepare carry for next position
            }
            return a;
        }
        public bool checkSign(float number)
        {
            if (number < 0)
            {
                return true;
            }
            else { return false; }
        }
    }
}
    