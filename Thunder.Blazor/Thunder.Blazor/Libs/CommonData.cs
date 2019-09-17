/* Ceated by Ya Lin. 2019/9/17 16:08:36 */

using System;

namespace Thunder.Blazor.Libs
{
    public class CommonData
    {
        private static CommonData current = new CommonData();
        public static CommonData Current => current;

        public readonly Random RndSeed = new Random(DateTime.Now.Millisecond);
    }
}
