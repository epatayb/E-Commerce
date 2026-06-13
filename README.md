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

<img width="1915" height="905" alt="Ekran görüntüsü 2026-06-13 124604" src="https://github.com/user-attachments/assets/387dcd94-14f6-4c66-8417-042c3faf8154" />

---

<img width="1917" height="912" alt="image" src="https://github.com/user-attachments/assets/9b2a38dd-ee40-44ed-90b0-f5bd68373a68" />

---

<img width="1908" height="732" alt="image" src="https://github.com/user-attachments/assets/4c841274-a4ec-4a5c-be41-7ad2bb4246ed" />

---

<img width="1911" height="568" alt="image" src="https://github.com/user-attachments/assets/bc329693-01e5-4045-9ed7-d62a5ae2ec82" />



