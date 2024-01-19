using System;
using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Sheets
{
    public struct SheetItem
    {
        public int Id;
        public TypeOut Type;

        public object Extra;
        internal string ExtraData;
    }
}