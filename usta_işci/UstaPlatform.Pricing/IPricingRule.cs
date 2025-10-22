using UstaPlatform.Domain;

namespace UstaPlatform.Pricing
{
    /// <summary>
    /// Fiyatlandırma kuralları için temel arayüz
    /// SOLID OCP prensibi: Yeni fiyat kuralları bu arayüzü uygulayarak eklenebilir
    /// </summary>
    public interface IPricingRule
    {
        /// <summary>
        /// Kural adı
        /// </summary>
        string KuralAdi { get; }

        /// <summary>
        /// Kural açıklaması
        /// </summary>
        string Aciklama { get; }

        /// <summary>
        /// Fiyatı hesaplar
        /// </summary>
        /// <param name="temelFiyat">Temel fiyat</param>
        /// <param name="talep">Talep bilgileri</param>
        /// <param name="işEmri">İş emri bilgileri</param>
        /// <returns>Hesaplanan fiyat</returns>
        decimal FiyatHesapla(decimal temelFiyat, Talep talep, İşEmri işEmri);

        /// <summary>
        /// Bu kuralın uygulanıp uygulanmayacağını belirler
        /// </summary>
        /// <param name="talep">Talep bilgileri</param>
        /// <param name="işEmri">İş emri bilgileri</param>
        /// <returns>Kural uygulanacaksa true</returns>
        bool UygulanabilirMi(Talep talep, İşEmri işEmri);
    }
}
