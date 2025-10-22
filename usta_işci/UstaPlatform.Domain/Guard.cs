using System;

namespace UstaPlatform.Domain
{
    /// <summary>
    /// Doğrulama işlemleri için static yardımcı sınıf
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Değerin null olmadığını kontrol eder
        /// </summary>
        public static void NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// String değerin boş olmadığını kontrol eder
        /// </summary>
        public static void NotNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"'{parameterName}' boş olamaz.", parameterName);
        }

        /// <summary>
        /// String değerin boş veya sadece boşluk karakterlerinden oluşmadığını kontrol eder
        /// </summary>
        public static void NotNullOrWhiteSpace(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"'{parameterName}' boş veya sadece boşluk karakterlerinden oluşamaz.", parameterName);
        }

        /// <summary>
        /// Sayısal değerin pozitif olduğunu kontrol eder
        /// </summary>
        public static void Positive(int value, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentException($"'{parameterName}' pozitif olmalıdır.", parameterName);
        }

        /// <summary>
        /// Sayısal değerin pozitif olduğunu kontrol eder
        /// </summary>
        public static void Positive(decimal value, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentException($"'{parameterName}' pozitif olmalıdır.", parameterName);
        }

        /// <summary>
        /// Sayısal değerin negatif olmadığını kontrol eder
        /// </summary>
        public static void NotNegative(int value, string parameterName)
        {
            if (value < 0)
                throw new ArgumentException($"'{parameterName}' negatif olamaz.", parameterName);
        }

        /// <summary>
        /// Sayısal değerin negatif olmadığını kontrol eder
        /// </summary>
        public static void NotNegative(decimal value, string parameterName)
        {
            if (value < 0)
                throw new ArgumentException($"'{parameterName}' negatif olamaz.", parameterName);
        }

        /// <summary>
        /// Değerin belirtilen aralıkta olduğunu kontrol eder
        /// </summary>
        public static void InRange(int value, int min, int max, string parameterName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(parameterName, $"'{parameterName}' {min} ile {max} arasında olmalıdır.");
        }

        /// <summary>
        /// Değerin belirtilen aralıkta olduğunu kontrol eder
        /// </summary>
        public static void InRange(decimal value, decimal min, decimal max, string parameterName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(parameterName, $"'{parameterName}' {min} ile {max} arasında olmalıdır.");
        }
    }
}
