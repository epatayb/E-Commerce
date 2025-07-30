# 🛒 E-Commerce Platform

Modern teknolojiyle geliştirilmiş **tam fonksiyonlu bir e-ticaret web uygulaması**.  
Kullanıcılar ürünleri keşfedebilir, sepetine ekleyebilir, ödeme işlemini gerçekleştirebilir.  
Yöneticiler ürünleri ve siparişleri yönetebilir, anlık bildirim alabilir.  
Gerçek dünyadaki e-ticaret sistemlerinin temel bileşenlerini barındırır.

---

## 🚀 Canlı Demo (isteğe bağlı)

🔗 [Siteyi Ziyaret Et](https://kilke.com.tr/)

---

## 🧩 Özellikler

### 👥 Kullanıcı Tarafı:
- 🛍️ Ürünleri listeleme ve detay sayfası
- 🛒 Sepet yönetimi (ekle, sil, miktar güncelle)
- 💳 Gerçek ödeme entegrasyonu (**İyzico**)
- 🔒 Kayıt, giriş, kimlik doğrulama
- 🧾 Sipariş geçmişi görüntüleme

### 🛠️ Yönetici Paneli:
- 📦 Ürün ve kategori; ekleme, düzenleme, silme
- 📊 Satış ve sipariş analizleri
- 🔔 Gerçek zamanlı sipariş bildirimleri (**SignalR**)
- 👤 Kullanıcı ve yetki yönetimi
- 📥 Ürün görseli yükleme

### 🔧 Altyapı ve Güvenlik:
- 🧠 Servis tabanlı mimari
- 🗄️ MSSQL veritabanı kullanımı
- 🔐 Yetki kontrolü ve rol bazlı erişim
- 🧪 Temel test altyapısı

---

## 🧰 Kullanılan Teknolojiler

| Alan | Teknolojiler |
|------|--------------|
| 🖥️ Backend | ASP.NET MVC, Entity Framework |
| 🎨 Frontend | Bootstrap |
| 🗃️ Veritabanı | Microsoft SQL Server |
| 💳 Ödeme | İyzico API |
| 📡 Gerçek Zamanlı | SignalR |
| 🛠️ Diğer | AutoMapper, TempData mesaj sistemi |

---

## ⚙️ Kurulum (Local)

> Projeyi kendi bilgisayarında çalıştırmak için adımlar:

1. Bu repoyu klonla:
   ```bash
   git clone https://github.com/kullaniciadi/E-Commerce.git
   cd E-Commerce
2. `appsettings.json` ve `appsettings.Development.json` dosyasındaki Connection Stringini, Email ve PaymentAPI bilgilerini güncelle.
3. Terminalde dotnet run veya dotnet watch ile projeni çalıştır.

### 🧪 Başlangıç Verileri (Initial Seed)
Projeyi ilk çalıştırdığınızda, **veritabanı otomatik olarak aşağıdaki iki kullanıcıyı oluşturur:**

#### 👑 Admin Kullanıcısı:
- **E-posta:** admin@info.com
- **Şifre:** admin123

#### 🙍‍♂️ Customer Kullanıcısı:
- **E-posta:** customer@info.com
- **Şifre:** customer123

> Bu kullanıcılar `SeedDatabase` metodu ile `Program.cs` içinde oluşturulur. 
Projeyi test etmek için doğrudan giriş yapabilirsiniz.

---
