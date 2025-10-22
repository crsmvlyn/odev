using UstaPlatform.Domain;
using UstaPlatform.Pricing;

namespace UstaPlatform.Plugins
{
    /// <summary>
    /// Sadakat indirimi kuralı - Plugin örneği
    /// Bu kural ayrı bir DLL olarak derlenip Plugins klasörüne konulabilir
    /// </summary>
    public class SadakatIndirimiKurali : IPricingRule
    {
        public string KuralAdi => "Sadakat İndirimi";
        public string Aciklama => "Düzenli müşteriler için %10 indirim";

        public decimal FiyatHesapla(decimal temelFiyat, Talep talep, İşEmri işEmri)
        {
            return temelFiyat * 0.90m; // %10 indirim
        }

        public bool UygulanabilirMi(Talep talep, İşEmri işEmri)
        {
            // Basit sadakat kontrolü - gerçek uygulamada veritabanından kontrol edilir
            return talep.VatandaşId <= 2; // İlk 2 müşteri sadık müşteri olarak kabul edilir
        }
    }
}
