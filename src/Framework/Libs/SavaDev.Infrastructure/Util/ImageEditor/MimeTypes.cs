using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ImageEditor
{
    public static class MimeTypes
    {
        public static MimeTypeModel[] Collection { get; }
        static MimeTypes()
        {
            Collection = new MimeTypeModel[] {
                new MimeTypeModel("exe", new byte?[] { 0x5A, 0x4D }),
                new MimeTypeModel("exe,dll", new byte?[] { 0x4D, 0x5A }),
                new MimeTypeModel("com", new byte?[] { 0xC9 }),
                new MimeTypeModel("dat", new byte?[] { 0x50, 0x4D, 0x4F, 0x43, 0x43, 0x4D, 0x4F, 0x43 }),
                new MimeTypeModel("jpg,jpeg", new byte?[] { 0xFF, 0xD8, 0xFF, 0xDB }),
                new MimeTypeModel("jpg,jpeg", new byte?[] { 0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46, 0x49, 0x46, 0x00, 0x01 }),
                new MimeTypeModel("jpg,jpeg", new byte?[] { 0xFF, 0xD8, 0xFF, 0xEE }),
                new MimeTypeModel("jpg,jpeg", new byte?[] { 0xFF, 0xD8, 0xFF, 0xE1, null, null, 0x45, 0x78, 0x69, 0x66, 0x00, 0x00 }),
                new MimeTypeModel("png", new byte?[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }),
                new MimeTypeModel("gif", new byte?[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 }),
                new MimeTypeModel("gif", new byte?[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }),
                new MimeTypeModel("bmp,dib", new byte?[] { 0x42, 0x4D }),
                new MimeTypeModel("mp3", new byte?[] { 0xFF, 0xFB }),
                new MimeTypeModel("mp3", new byte?[] { 0x49, 0x44, 0x33 }),
                new MimeTypeModel("avi", new byte?[] { 0x52, 0x49, 0x46, 0x46, null, null, null, null, 0x41, 0x56, 0x49, 0x20 }),
                new MimeTypeModel("wav", new byte?[] { 0x52, 0x49, 0x46, 0x46, null, null, null, null, 0x57, 0x41, 0x56, 0x45 }),
                new MimeTypeModel("psd", new byte?[] { 0x38, 0x42, 0x50, 0x53 }),
                new MimeTypeModel("pdf", new byte?[] { 0x25, 0x50, 0x44, 0x46, 0x2d }),
                new MimeTypeModel("rar", new byte?[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00 }),
                new MimeTypeModel("rar", new byte?[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00 }),
                new MimeTypeModel("mid,midi", new byte?[] { 0x4D, 0x54, 0x68, 0x64 }),
                new MimeTypeModel("zip,aar,apk,docx,epub,ipa,jar,kmz,maff,odp,ods,odt,pk3,pk4,pptx,usdz,vsdx,xlsx,xpi", new byte?[] { 0x50, 0x4B, 0x03, 0x04 }),
                new MimeTypeModel("zip,aar,apk,docx,epub,ipa,jar,kmz,maff,odp,ods,odt,pk3,pk4,pptx,usdz,vsdx,xlsx,xpi", new byte?[] { 0x50, 0x4B, 0x05, 0x06 }),
                new MimeTypeModel("zip,aar,apk,docx,epub,ipa,jar,kmz,maff,odp,ods,odt,pk3,pk4,pptx,usdz,vsdx,xlsx,xpi", new byte?[] { 0x50, 0x4B, 0x07, 0x08 }),
                new MimeTypeModel("cr2", new byte?[] { 0x49, 0x49, 0x2A, 0x00, 0x10, 0x00, 0x00, 0x00, 0x43, 0x52 }),
                new MimeTypeModel("tif,tiff", new byte?[] { 0x49, 0x49, 0x2A, 0x00 }),
                new MimeTypeModel("tif,tiff", new byte?[] { 0x4D, 0x4D, 0x00, 0x2A }),
                new MimeTypeModel("ico", new byte?[] { 0x00, 0x00, 0x01, 0x00 }),
                new MimeTypeModel("asf,wma,wmv", new byte?[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C }),
                new MimeTypeModel("ogg,oga,ogv", new byte?[] { 0x4F, 0x67, 0x67, 0x53 }),
                new MimeTypeModel("iso", new byte?[] { 0x43, 0x44, 0x30, 0x30, 0x31 }),
                new MimeTypeModel("flac", new byte?[] { 0x66, 0x4C, 0x61, 0x43 }),
                new MimeTypeModel("doc,xls,ppt,msg", new byte?[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }),
                new MimeTypeModel("djvu,djv", new byte?[] { 0x41, 0x54, 0x26, 0x54, 0x46, 0x4F, 0x52, 0x4D, null, null, null, null, 0x44, 0x4A, 0x56 }),
                new MimeTypeModel("xml", new byte?[] { 0x3c, 0x3f, 0x78, 0x6d, 0x6c, 0x20 }),
                new MimeTypeModel("swf", new byte?[] { 0x43, 0x57, 0x53 }),
                new MimeTypeModel("swf", new byte?[] { 0x46, 0x57, 0x53 }),
                new MimeTypeModel("rtf", new byte?[] { 0x7B, 0x5C, 0x72, 0x74, 0x66, 0x31 }),
                new MimeTypeModel("mpg,mpeg,m2p,vob", new byte?[] { 0x00, 0x00, 0x01, 0xBA }),
                new MimeTypeModel("mpg,mpeg,m2p,vob", new byte?[] { 0x47 }),
                new MimeTypeModel("mpg,mpeg,m2p,vob", new byte?[] { 0x00, 0x00, 0x01, 0xB3 })
            };
        }
    }
}
