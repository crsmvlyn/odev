using System;

namespace UstaPlatform.Domain
{
    /// <summary>
    /// Hizmet talep eden kişi sınıfı
    /// </summary>
    public record Vatandaş
    {
        public int Id { get; init; }
        public string Ad { get; init; } = string.Empty;
        public string Soyad { get; init; } = string.Empty;
        public string Telefon { get; init; } = string.Empty;
        public string Adres { get; init; } = string.Empty;
        public DateTime KayitZamani { get; init; }

        public override string ToString()
        {
            return $"{Ad} {Soyad} - {Adres}";
        }
    }
}
