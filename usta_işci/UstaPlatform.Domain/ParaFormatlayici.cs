using System;
using System.Globalization;

namespace UstaPlatform.Domain
{
    /// <summary>
    /// Para formatlaması için static yardımcı sınıf
    /// </summary>
    public static class ParaFormatlayici
    {
        private static readonly CultureInfo _türkçeKültür = new("tr-TR");

        /// <summary>
        /// Para miktarını Türk Lirası formatında döndürür
        /// </summary>
        public static string Formatla(decimal miktar)
        {
            return miktar.ToString("C", _türkçeKültür);
        }

        /// <summary>
        /// Para miktarını belirtilen kültüre göre formatlar
        /// </summary>
        public static string Formatla(decimal miktar, CultureInfo kültür)
        {
            return miktar.ToString("C", kültür);
        }

        /// <summary>
        /// Para miktarını özel format ile döndürür
        /// </summary>
        public static string Formatla(decimal miktar, string format)
        {
            return miktar.ToString(format, _türkçeKültür);
        }
    }
}
