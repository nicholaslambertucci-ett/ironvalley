namespace Ett.Vdt.NativeApplication.Data.Vrs
{
    public struct Vr
    {
        public FrameType FrameType;
        public VrHotspot[] Hotspots;
        public Layout3D Layout3D;
        public MappingLayout MappingLayout;
        public FileType MediaType;
        public string MediaPath;
        public Mode Mode;
        public float Rotation;
    }
}