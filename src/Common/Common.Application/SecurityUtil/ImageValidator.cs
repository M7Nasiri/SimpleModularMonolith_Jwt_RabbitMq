using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Common.Application.SecurityUtil
{
   public static class  ImageValidator
    {
        private static readonly Dictionary<string, List<byte[]>> ImageSignatures = new()
        {
            { "jpeg", new List<byte[]> { new byte[] { 0xFF, 0xD8, 0xFF } } },
            { "png",  new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { "gif",  new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
            { "webp", new List<byte[]> { new byte[] { 0x52, 0x49, 0x46, 0x46 } } } // RIFF....WEBP
        };

        public static bool IsImage(this IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return false;

            // حداقل طول هدر برای شناسایی فرمت‌ها
            if (file.Length < 16)
                return false;

            Span<byte> header = stackalloc byte[16];

            using var stream = file.OpenReadStream();
            var bytesRead = stream.Read(header);

            if (bytesRead < 16)
                return false;

            // بررسی امضای فایل
            foreach (var signatures in ImageSignatures.Values)
            {
                foreach (var sig in signatures)
                {
                    if (header.Slice(0, sig.Length).SequenceEqual(sig))
                    {
                        // بررسی تکمیلی برای فرمت WebP: باید در بایت ۸ تا ۱۲ عبارت WEBP باشد
                        if (sig[0] == 0x52 && sig[1] == 0x49) // RIFF
                        {
                            var webpTag = System.Text.Encoding.ASCII.GetString(header.Slice(8, 4));
                            return webpTag == "WEBP";
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
