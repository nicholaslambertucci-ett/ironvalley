using System;

namespace Ett.Vdt.NativeApplication.Data.Maps
{
    public struct Map
    {
        public string Description;
        public int Id;
        public MapItem[] Items;
        public string MediaPath;
        public string Subtitle;
        public string Tag;
        public string Title;


        [Obsolete("Obsolete!!! Please use MediaPath field instead.")]
        public string MediaUuid => this.MediaPath;
    }
}