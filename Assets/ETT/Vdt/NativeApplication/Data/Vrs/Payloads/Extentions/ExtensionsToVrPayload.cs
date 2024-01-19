using System;

namespace Ett.Vdt.NativeApplication.Data.Vrs.Payloads.Extentions
{
    internal static class ExtensionsToVrPayload
    {
        public static Vr ToVr(this VrPayload payload)
        {
            var vr = new Vr
            {
                Hotspots = new VrHotspot[payload.hotspots?.Length??0],
                MediaPath = payload.mediaPath,
                Rotation = payload.rotation
            };

            FrameType frameType;
            if (Enum.TryParse(payload.frameType, true, out frameType))
                vr.FrameType = frameType;

            Layout3D layout3D;
            if (Enum.TryParse(payload.layout3d, true, out layout3D))
                vr.Layout3D = layout3D;

            InternalMappingLayout mappingLayout;
            if (Enum.TryParse(payload.mappingLayout, true, out mappingLayout))
                vr.MappingLayout = (MappingLayout)(int)mappingLayout;

            FileType fileType;
            if (Enum.TryParse(payload.mediaType, true, out fileType))
                vr.MediaType = fileType;

            Mode mode;
            if (Enum.TryParse(payload.mode, true, out mode))
                vr.Mode = mode;

            if (payload.hotspots == null)
                return vr;

            for (var i = 0; i < vr.Hotspots.Length; i++)
                vr.Hotspots[i] = payload.hotspots[i].ToHotspot();

            return vr;
        }
    }
}