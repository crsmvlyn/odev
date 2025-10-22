using System;
using System.Collections;
using System.Collections.Generic;

namespace UstaPlatform.Domain
{
    /// <summary>
    /// Bir uzmanın günlük ziyaret edeceği adreslerin sırası
    /// IEnumerable<(int X, int Y)> arayüzünü uygular ve koleksiyon başlatıcıları destekler
    /// </summary>
    public class Rota : IEnumerable<(int X, int Y)>
    {
        private readonly List<(int X, int Y)> _durakNoktalari = new();

        public int UstaId { get; init; }
        public DateTime Tarih { get; init; }

        /// <summary>
        /// Koleksiyon başlatıcıları için Add metodu
        /// </summary>
        public void Add(int X, int Y)
        {
            _durakNoktalari.Add((X, Y));
        }

        /// <summary>
        /// Durak sayısı
        /// </summary>
        public int DurakSayisi => _durakNoktalari.Count;

        /// <summary>
        /// Belirli bir indeksteki durağı getirir
        /// </summary>
        public (int X, int Y) this[int index] => _durakNoktalari[index];

        public IEnumerator<(int X, int Y)> GetEnumerator()
        {
            return _durakNoktalari.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Toplam mesafeyi hesaplar (basit Manhattan mesafesi)
        /// </summary>
        public double ToplamMesafe()
        {
            if (_durakNoktalari.Count < 2)
                return 0;

            double toplam = 0;
            for (int i = 0; i < _durakNoktalari.Count - 1; i++)
            {
                var current = _durakNoktalari[i];
                var next = _durakNoktalari[i + 1];
                toplam += Math.Abs(next.X - current.X) + Math.Abs(next.Y - current.Y);
            }
            return toplam;
        }

        public override string ToString()
        {
            return $"Rota - {Tarih:dd.MM.yyyy} ({DurakSayisi} durak, {ToplamMesafe():F1} km)";
        }
    }
}
