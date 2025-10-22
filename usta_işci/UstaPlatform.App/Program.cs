using System;
using System.Collections.Generic;
using System.Linq;
using UstaPlatform.Domain;
using UstaPlatform.Infrastructure;
using UstaPlatform.Pricing;

namespace UstaPlatform.App
{
    /// <summary>
    /// Ana uygulama sınıfı - Demo senaryolarını çalıştırır
    /// </summary>
    class Program
    {
        private static readonly IUstaRepository _ustaRepository = new InMemoryUstaRepository();
        private static readonly ITalepRepository _talepRepository = new InMemoryTalepRepository();
        private static readonly IİşEmriRepository _işEmriRepository = new InMemoryİşEmriRepository();
        private static readonly PricingEngine _pricingEngine = new PricingEngine();
        private static readonly Çizelge _çizelge = new Çizelge();

        static void Main(string[] args)
        {
            Console.WriteLine("=== UstaPlatform - Şehrin Uzmanlık Platformu ===");
            Console.WriteLine();

            // Test verilerini oluştur
            TestVerileriniOluştur();

            // Demo senaryolarını çalıştır
            DemoSenaryolarınıÇalıştır();

            Console.WriteLine("\nDemo tamamlandı. Çıkmak için bir tuşa basın...");
            Console.ReadKey();
        }

        /// <summary>
        /// Test verilerini oluşturur
        /// </summary>
        private static void TestVerileriniOluştur()
        {
            Console.WriteLine(" Test verileri oluşturuluyor...");

            // Ustaları oluştur
            var ustalar = new List<Usta>
            {
                new Usta
                {
                    Ad = "Ahmet",
                    Soyad = "Yılmaz",
                    UzmanlikAlani = "Tesisatçı",
                    Puan = 85,
                    Yogunluk = 3,
                    KayitZamani = DateTime.Now.AddDays(-30),
                    Telefon = "0532-123-4567",
                    Adres = "Merkez Mahallesi"
                },
                new Usta
                {
                    Ad = "Mehmet",
                    Soyad = "Demir",
                    UzmanlikAlani = "Elektrikçi",
                    Puan = 92,
                    Yogunluk = 2,
                    KayitZamani = DateTime.Now.AddDays(-25),
                    Telefon = "0533-234-5678",
                    Adres = "Güney Mahallesi"
                },
                new Usta
                {
                    Ad = "Ali",
                    Soyad = "Kaya",
                    UzmanlikAlani = "Marangoz",
                    Puan = 78,
                    Yogunluk = 4,
                    KayitZamani = DateTime.Now.AddDays(-20),
                    Telefon = "0534-345-6789",
                    Adres = "Kuzey Mahallesi"
                }
            };

            foreach (var usta in ustalar)
            {
                _ustaRepository.Ekle(usta);
            }

            // Vatandaşları oluştur
            var vatandaşlar = new List<Vatandaş>
            {
                new Vatandaş
                {
                    Ad = "Ayşe",
                    Soyad = "Özkan",
                    Telefon = "0535-456-7890",
                    Adres = "Merkez Mahallesi",
                    KayitZamani = DateTime.Now.AddDays(-10)
                },
                new Vatandaş
                {
                    Ad = "Fatma",
                    Soyad = "Çelik",
                    Telefon = "0536-567-8901",
                    Adres = "Güney Mahallesi",
                    KayitZamani = DateTime.Now.AddDays(-5)
                }
            };

            Console.WriteLine($"✅ {ustalar.Count} usta ve {vatandaşlar.Count} vatandaş oluşturuldu");
            Console.WriteLine();
        }

        /// <summary>
        /// Demo senaryolarını çalıştırır
        /// </summary>
        private static void DemoSenaryolarınıÇalıştır()
        {
            // Senaryo 1: Normal talep ve fiyat hesaplama
            Console.WriteLine("🔧 Senaryo 1: Normal Talep ve Fiyat Hesaplama");
            NormalTalepSenaryosu();

            Console.WriteLine();

            // Senaryo 2: Acil talep ve fiyat hesaplama
            Console.WriteLine("🚨 Senaryo 2: Acil Talep ve Fiyat Hesaplama");
            AcilTalepSenaryosu();

            Console.WriteLine();

            // Senaryo 3: Hafta sonu talep ve fiyat hesaplama
            Console.WriteLine("📅 Senaryo 3: Hafta Sonu Talep ve Fiyat Hesaplama");
            HaftaSonuTalepSenaryosu();

            Console.WriteLine();

            // Senaryo 4: Çizelge ve Rota kullanımı
            Console.WriteLine("🗓️ Senaryo 4: Çizelge ve Rota Kullanımı");
            ÇizelgeVeRotaSenaryosu();

            Console.WriteLine();

            // Senaryo 5: Plugin sistemi demo
            Console.WriteLine("🔌 Senaryo 5: Plugin Sistemi Demo");
            PluginSistemiDemo();
        }

        private static void NormalTalepSenaryosu()
        {
            // Talep oluştur
            var talep = new Talep
            {
                VatandaşId = 1,
                Aciklama = "Musluk tamiri",
                UzmanlikAlani = "Tesisatçı",
                TalepZamani = DateTime.Now,
                TercihEdilenTarih = DateTime.Now.AddDays(2),
                Adres = "Merkez Mahallesi",
                AcilMi = false,
                Durum = TalepDurumu.Beklemede
            };

            var kaydedilenTalep = _talepRepository.Ekle(talep);
            Console.WriteLine($"Talep oluşturuldu: {kaydedilenTalep}");

            // Usta bul
            var usta = _ustaRepository.UzmanlikAlaninaGöreGetir("Tesisatçı").First();
            Console.WriteLine($"Usta bulundu: {usta}");

            // İş emri oluştur
            var işEmri = new İşEmri
            {
                TalepId = kaydedilenTalep.Id,
                UstaId = usta.Id,
                Fiyat = 100m, // Temel fiyat
                PlanlananTarih = DateTime.Now.AddDays(2),
                TahminiSure = TimeSpan.FromHours(2),
                Adres = kaydedilenTalep.Adres,
                Durum = İşEmriDurumu.Planlandi,
                OlusturmaZamani = DateTime.Now
            };

            // Fiyat hesapla
            var finalFiyat = _pricingEngine.FiyatHesapla(işEmri.Fiyat, kaydedilenTalep, işEmri);
            işEmri = işEmri with { Fiyat = finalFiyat };

            var kaydedilenİşEmri = _işEmriRepository.Ekle(işEmri);
            Console.WriteLine($"İş emri oluşturuldu: {kaydedilenİşEmri}");

            // Çizelgeye ekle
            _çizelge.İşEmriEkle(kaydedilenİşEmri);
            Console.WriteLine($"İş emri çizelgeye eklendi");
        }

        private static void AcilTalepSenaryosu()
        {
            var talep = new Talep
            {
                VatandaşId = 2,
                Aciklama = "Elektrik kesintisi",
                UzmanlikAlani = "Elektrikçi",
                TalepZamani = DateTime.Now,
                TercihEdilenTarih = DateTime.Now.AddHours(2),
                Adres = "Güney Mahallesi",
                AcilMi = true,
                Durum = TalepDurumu.Beklemede
            };

            var kaydedilenTalep = _talepRepository.Ekle(talep);
            Console.WriteLine($"Acil talep oluşturuldu: {kaydedilenTalep}");

            var usta = _ustaRepository.UzmanlikAlaninaGöreGetir("Elektrikçi").First();
            Console.WriteLine($"Usta bulundu: {usta}");

            var işEmri = new İşEmri
            {
                TalepId = kaydedilenTalep.Id,
                UstaId = usta.Id,
                Fiyat = 150m,
                PlanlananTarih = DateTime.Now.AddHours(2),
                TahminiSure = TimeSpan.FromHours(1),
                Adres = kaydedilenTalep.Adres,
                Durum = İşEmriDurumu.Planlandi,
                OlusturmaZamani = DateTime.Now
            };

            var finalFiyat = _pricingEngine.FiyatHesapla(işEmri.Fiyat, kaydedilenTalep, işEmri);
            işEmri = işEmri with { Fiyat = finalFiyat };

            var kaydedilenİşEmri = _işEmriRepository.Ekle(işEmri);
            Console.WriteLine($"Acil iş emri oluşturuldu: {kaydedilenİşEmri}");
        }

        private static void HaftaSonuTalepSenaryosu()
        {
            var talep = new Talep
            {
                VatandaşId = 1,
                Aciklama = "Dolap tamiri",
                UzmanlikAlani = "Marangoz",
                TalepZamani = DateTime.Now,
                TercihEdilenTarih = DateTime.Now.AddDays(1), // Yarın (hafta sonu)
                Adres = "Merkez Mahallesi",
                AcilMi = false,
                Durum = TalepDurumu.Beklemede
            };

            var kaydedilenTalep = _talepRepository.Ekle(talep);
            Console.WriteLine($"Hafta sonu talep oluşturuldu: {kaydedilenTalep}");

            var usta = _ustaRepository.UzmanlikAlaninaGöreGetir("Marangoz").First();
            Console.WriteLine($"Usta bulundu: {usta}");

            var işEmri = new İşEmri
            {
                TalepId = kaydedilenTalep.Id,
                UstaId = usta.Id,
                Fiyat = 200m,
                PlanlananTarih = DateTime.Now.AddDays(1), // Hafta sonu
                TahminiSure = TimeSpan.FromHours(3),
                Adres = kaydedilenTalep.Adres,
                Durum = İşEmriDurumu.Planlandi,
                OlusturmaZamani = DateTime.Now
            };

            var finalFiyat = _pricingEngine.FiyatHesapla(işEmri.Fiyat, kaydedilenTalep, işEmri);
            işEmri = işEmri with { Fiyat = finalFiyat };

            var kaydedilenİşEmri = _işEmriRepository.Ekle(işEmri);
            Console.WriteLine($"Hafta sonu iş emri oluşturuldu: {kaydedilenİşEmri}");
        }

        private static void ÇizelgeVeRotaSenaryosu()
        {
            // Çizelge kullanımı
            var bugün = DateOnly.FromDateTime(DateTime.Now);
            var bugününİşEmirleri = _çizelge[bugün];
            Console.WriteLine($"Bugün ({bugün:dd.MM.yyyy}) için {bugününİşEmirleri.Count} ustanın işi var");

            // Rota oluşturma
            var rota = new Rota
            {
                UstaId = 1,
                Tarih = DateTime.Now
            };

            // Koleksiyon başlatıcıları ile rota noktaları ekleme
            rota.Add(0, 0);   // Merkez
            rota.Add(10, 5);  // İlk müşteri
            rota.Add(15, 10); // İkinci müşteri
            rota.Add(5, 15);  // Üçüncü müşteri
            rota.Add(0, 0);   // Merkeze dönüş

            Console.WriteLine($"Rota oluşturuldu: {rota}");
            Console.WriteLine($"Toplam mesafe: {rota.ToplamMesafe():F1} km");

            // Rota üzerinde dolaşma
            Console.WriteLine("Rota noktaları:");
            int sıra = 1;
            foreach (var (x, y) in rota)
            {
                Console.WriteLine($"  {sıra}. Durak: ({x}, {y})");
                sıra++;
            }
        }

        private static void PluginSistemiDemo()
        {
            Console.WriteLine($"Yüklü fiyatlandırma kuralları ({_pricingEngine.KuralSayisi} adet):");
            foreach (var kural in _pricingEngine.YüklüKurallar)
            {
                Console.WriteLine($"  - {kural.KuralAdi}: {kural.Aciklama}");
            }

            Console.WriteLine();
            Console.WriteLine("💡 Plugin sistemi çalışıyor!");
            Console.WriteLine("   Yeni kurallar 'Plugins' klasörüne DLL olarak eklenebilir.");
            Console.WriteLine("   Uygulama yeniden başlatıldığında otomatik olarak yüklenecek.");
        }
    }
}
